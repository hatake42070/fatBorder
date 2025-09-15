using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class MonstersEditor : EditorWindow
{
    // --- 変数定義 ---
    private List<MonsterData> allMonsters = new List<MonsterData>();
    private Vector2 scrollPosition;
    // 検索用変数
    private string searchQuery = "";
    // 各モンスターのFoldoutが開いているか(true)閉じているか(false)を保存
    private Dictionary<MonsterData, bool> monsterFoldoutStates = new Dictionary<MonsterData, bool>();
    private enum SortType
    {
        // 名前でソート
        AssetName_Ascending, // 昇順
        AssetName_Descending, // 降順
        // モンスターIDでソート
        MonsterID_Ascending,
        MonsterID_Descending,
        // レアリティでソート
        Rarity_Ascending,
        Rarity_Descending
    }
    private SortType currentSortType = SortType.MonsterID_Ascending;

    // --- 新規作成用の変数 ---
    private string newMonsterAssetName = "";
    private int newMonsterId = 0;
    private int newMonsterRarity = 1;
    private MonsterType newMonsterType = MonsterType.Other;
    private int newMonsterMaxHp = 100;
    private int newMonsterAttackPower = 5;
    private Sprite newMonsterIcon;

    [MenuItem("Tools/モンスター管理エディタ")]
    public static void ShowWindow()
    {
        GetWindow<MonstersEditor>("モンスター管理エディタ");
    }

    private void OnEnable()
    {
        LoadAllMonsters();
    }

    private void OnGUI()
    {
        // --- 制御UI ---
        GUILayout.Label("モンスタ一覧", EditorStyles.boldLabel);
        if (GUILayout.Button("データ更新"))
        {
            LoadAllMonsters();
        }
        searchQuery = EditorGUILayout.TextField("アセット名で検索", searchQuery);
        currentSortType = (SortType)EditorGUILayout.EnumPopup("並び替え", currentSortType);
        EditorGUILayout.Space(10);

        // --- データ表示・編集エリア ---
        // スクロールを管理
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        // 検索してリストを絞る
        var filteredMonsters = string.IsNullOrEmpty(searchQuery) ? allMonsters : allMonsters.Where(r => r.name.ToLower().Contains(searchQuery.ToLower())).ToList();
        // ソートしたリスト
        var sortedMonsters = SortMonsters(filteredMonsters);
        // 削除するアセットを保持
        MonsterData monsterToDelete = null;

        foreach (var monster in sortedMonsters)
        {
            // Foldoutを描画し、開閉状態を取得
            bool isFoldoutOpen = GetFoldoutState(monster);
            isFoldoutOpen = EditorGUILayout.Foldout(isFoldoutOpen, monster.name, true, EditorStyles.foldoutHeader);
            SetFoldoutState(monster, isFoldoutOpen);

            // もしFoldoutが開かれていたら、詳細情報を描画
            if (isFoldoutOpen)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUI.indentLevel++;

                EditorGUI.BeginChangeCheck();

                // --- 既存のパラメータ編集 ---
                monster.monsterID = EditorGUILayout.IntField("モンスターID", monster.monsterID);
                monster.rarity = EditorGUILayout.IntSlider("レアリティ", monster.rarity, 1, 5);
                monster.type = (MonsterType)EditorGUILayout.EnumPopup("タイプ", monster.type);
                monster.maxHp = EditorGUILayout.IntField("最大HP", monster.maxHp);
                monster.attackPower = EditorGUILayout.IntField("攻撃力", monster.attackPower);
                monster.icon = (Sprite)EditorGUILayout.ObjectField("アイコン", monster.icon, typeof(Sprite), false, GUILayout.Height(64));

                // --- カードリスト編集機能 ---

                EditorGUILayout.Space(10);
                EditorGUILayout.LabelField("カードリスト", EditorStyles.boldLabel);

                // 現在のカードリストのサイズを表示・編集
                int newCardCount = EditorGUILayout.IntField("カード枚数", monster.cards.Count);
                // リストのサイズが変更されたら、それに合わせてリストを調整
                while (newCardCount != monster.cards.Count)
                {
                    if (newCardCount > monster.cards.Count)
                        monster.cards.Add(null);
                    else
                        monster.cards.RemoveAt(monster.cards.Count - 1);
                }

                // 各カードのアセットを設定するスロットを描画
                for (int i = 0; i < monster.cards.Count; i++)
                {
                    monster.cards[i] = (CardData)EditorGUILayout.ObjectField($"カード {i + 1}", monster.cards[i], typeof(CardData), false);
                }

                // --- カードリスト編集ここまで ---

                if (EditorGUI.EndChangeCheck())
                {
                    EditorUtility.SetDirty(monster);
                    AssetDatabase.SaveAssets();
                }

                if (GUILayout.Button("このモンスターを削除"))
                {
                    monsterToDelete = monster;
                }

                EditorGUI.indentLevel--;
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space(5);
            }
        }
        EditorGUILayout.EndScrollView();

        if (monsterToDelete != null)
        {
            EditorUtils.DeleteAsset(monsterToDelete);
            // 削除されたアセットを参照するEditorUtils.DeleteAssetはnullを返すが、明示的にnullとする
            monsterToDelete = null;
            LoadAllMonsters();
        }

        // --- 区切り線 ---
        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.Space(20);

        // --- 新規追加エリア ---
        GUILayout.Label("新規モンスターアセット作成", EditorStyles.boldLabel);
        newMonsterAssetName = EditorGUILayout.TextField("新しいアセット名", newMonsterAssetName);
        newMonsterId = EditorGUILayout.IntField("モンスターID", newMonsterId);
        newMonsterRarity = EditorGUILayout.IntSlider("レアリティ", newMonsterRarity, 1, 5);
        newMonsterType = (MonsterType)EditorGUILayout.EnumPopup("タイプ", newMonsterType);
        newMonsterIcon = (Sprite)EditorGUILayout.ObjectField("アイコン", newMonsterIcon, typeof(Sprite), false);

        if (GUILayout.Button("新規アセットとして保存"))
        {
            CreateNewMonster();
        }
    }

    private List<MonsterData> SortMonsters(List<MonsterData> monsters)
    {
        switch (currentSortType)
        {
            case SortType.AssetName_Ascending: return monsters.OrderBy(r => r.name).ToList();
            case SortType.AssetName_Descending: return monsters.OrderByDescending(r => r.name).ToList();
            case SortType.MonsterID_Ascending: return monsters.OrderBy(r => r.monsterID).ToList();
            case SortType.MonsterID_Descending: return monsters.OrderByDescending(r => r.monsterID).ToList();
            case SortType.Rarity_Ascending: return monsters.OrderBy(r => r.rarity).ToList();
            case SortType.Rarity_Descending: return monsters.OrderByDescending(r => r.rarity).ToList();
            default: return monsters;
        }
    }

    private void LoadAllMonsters()
    {
        allMonsters = EditorUtils.LoadAllAssets<MonsterData>();
    }

    // --- 新規作成の処理 ---
    private void CreateNewMonster()
    {
        if (string.IsNullOrEmpty(newMonsterAssetName))
        {
            EditorUtility.DisplayDialog("エラー", "アセット名を入力してください。", "OK");
            return;
        }

        MonsterData newMonster = ScriptableObject.CreateInstance<MonsterData>();
        newMonster.monsterID = newMonsterId;
        newMonster.rarity = newMonsterRarity;
        newMonster.type = newMonsterType;
        newMonster.maxHp = newMonsterMaxHp;
        newMonster.attackPower = newMonsterAttackPower;
        newMonster.icon = newMonsterIcon;

        string folderPath = "Assets/MonsterData"; // 保存先フォルダ
        // フォルダがなければ作成
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets/Data", "Organs");
        }

        string desiredPath = $"{folderPath}/{newMonsterAssetName}.asset";
        string uniquePath = AssetDatabase.GenerateUniqueAssetPath(desiredPath);

        AssetDatabase.CreateAsset(newMonster, uniquePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("成功", $"新しい臓器を {uniquePath} に保存しました。", "OK");
        LoadAllMonsters();

        // 入力欄をクリア
        newMonsterAssetName = "";
        newMonsterId = 0;
        newMonsterRarity = 1;
        newMonsterIcon = null;
    }
    private bool GetFoldoutState(MonsterData monster)
    {
        if (!monsterFoldoutStates.ContainsKey(monster))
        {
            monsterFoldoutStates[monster] = false;
        }
        return monsterFoldoutStates[monster];
    }

    private void SetFoldoutState(MonsterData monster, bool state)
    {
        monsterFoldoutStates[monster] = state;
    }
}