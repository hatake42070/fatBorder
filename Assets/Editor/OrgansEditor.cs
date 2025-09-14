using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class OrgansEditor : EditorWindow
{
    // --- 変数定義 ---
    private List<OrganData> allOrgans = new List<OrganData>();
    private Vector2 scrollPosition;
    // 検索用変数
    private string searchQuery = "";
    private enum SortType
    {
        // 名前でソート
        AssetName_Ascending, // 昇順
        AssetName_Descending, // 降順
        AssetName_Ascending,
        AssetName_Descending,
        // 臓器IDでソート
        OrganID_Ascending,
        OrganID_Descending,
        // レアリティでソート
        Rarity_Ascending,
        Rarity_Descending
    }
    private SortType currentSortType = SortType.OrganID_Ascending;

    // --- 新規作成用の変数 ---
    private string newOrganAssetName = "";
    private int newOrganId = 0;
    private int newOrganRarity = 1;
    private OrganCategory newOrganCategory = OrganCategory.Other;
    private Sprite newOrganIcon;

    [MenuItem("Tools/臓器管理エディタ")]
    public static void ShowWindow()
    {
        GetWindow<OrgansEditor>("臓器管理エディタ");
    }

    private void OnEnable()
    {
        LoadAllOrgans();
    }

    private void OnGUI()
    {
        // --- 制御UI ---
        GUILayout.Label("臓器一覧", EditorStyles.boldLabel);
        if (GUILayout.Button("データ更新"))
        {
            LoadAllOrgans();
        }
        searchQuery = EditorGUILayout.TextField("アセット名で検索", searchQuery);
        currentSortType = (SortType)EditorGUILayout.EnumPopup("並び替え", currentSortType);
        
        EditorGUILayout.Space(10);

        // --- データ表示・編集エリア ---
        // スクロールを管理
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        // 検索してリストを絞る
        var filteredOrgans = string.IsNullOrEmpty(searchQuery) ? allOrgans : allOrgans.Where(r => r.name.ToLower().Contains(searchQuery.ToLower())).ToList();
        // ソートしたリスト
        var sortedOrgans = SortOrgans(filteredOrgans);
        // 削除するアセットを保持
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        var filteredOrgans = string.IsNullOrEmpty(searchQuery) ? allOrgans : allOrgans.Where(r => r.name.ToLower().Contains(searchQuery.ToLower())).ToList();
        var sortedOrgans = SortOrgans(filteredOrgans);
        OrganData organToDelete = null;

        foreach (var organ in sortedOrgans)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField(organ.name, EditorStyles.boldLabel);
            
            // 監視を開始
            EditorGUI.BeginChangeCheck();
            organ.organID = EditorGUILayout.IntField("臓器ID", organ.organID);
            organ.rarity = EditorGUILayout.IntSlider("レアリティ", organ.rarity, 1, 5);
            organ.category = (OrganCategory)EditorGUILayout.EnumPopup("カテゴリー", organ.category);
            organ.icon = (Sprite)EditorGUILayout.ObjectField("アイコン", organ.icon, typeof(Sprite), false, GUILayout.Height(64));
            // 変更があった時のみデータを保存
            
            if (EditorGUI.EndChangeCheck())
            {
                // セットが変更されたことをUnityに通知
                EditorUtility.SetDirty(organ);
                AssetDatabase.SaveAssets();
            }

            if (GUILayout.Button("この臓器を削除"))
            {
                organToDelete = organ;
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(5);
        }
        EditorGUILayout.EndScrollView();

        if (organToDelete != null)
        {
            EditorUtils.DeleteAsset(organToDelete);
            // 削除されたアセットを参照するEditorUtils.DeleteAssetはnullを返すが、明示的にnullとする
            organToDelete = null;
            LoadAllOrgans();
        }

        // --- 区切り線 ---
        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.Space(20);

        // --- 新規追加エリア ---
        GUILayout.Label("新規臓器アセット作成", EditorStyles.boldLabel);
        newOrganAssetName = EditorGUILayout.TextField("新しいアセット名", newOrganAssetName);
        newOrganId = EditorGUILayout.IntField("臓器ID", newOrganId);
        newOrganRarity = EditorGUILayout.IntSlider("レアリティ", newOrganRarity, 1, 5);
        newOrganCategory = (OrganCategory)EditorGUILayout.EnumPopup("カテゴリー", newOrganCategory);
        newOrganIcon = (Sprite)EditorGUILayout.ObjectField("アイコン", newOrganIcon, typeof(Sprite), false);

        if (GUILayout.Button("新規アセットとして保存"))
        {
            CreateNewOrgan();
        }
    }

    private List<OrganData> SortOrgans(List<OrganData> organs)
    {
        switch (currentSortType)
        {
            case SortType.AssetName_Ascending: return organs.OrderBy(r => r.name).ToList();
            case SortType.AssetName_Descending: return organs.OrderByDescending(r => r.name).ToList();
            case SortType.OrganID_Ascending: return organs.OrderBy(r => r.organID).ToList();
            case SortType.OrganID_Descending: return organs.OrderByDescending(r => r.organID).ToList();
            case SortType.Rarity_Ascending: return organs.OrderBy(r => r.rarity).ToList();
            case SortType.Rarity_Descending: return organs.OrderByDescending(r => r.rarity).ToList();
            default: return organs;
        }
    }

    private void LoadAllOrgans()
    {
        allOrgans = EditorUtils.LoadAllAssets<OrganData>();
    }

    // --- 新規作成の処理 ---
    private void CreateNewOrgan()
    {
        if (string.IsNullOrEmpty(newOrganAssetName))
        {
            EditorUtility.DisplayDialog("エラー", "アセット名を入力してください。", "OK");
            return;
        }

        OrganData newOrgan = ScriptableObject.CreateInstance<OrganData>();
        newOrgan.organID = newOrganId;
        newOrgan.rarity = newOrganRarity;
        newOrgan.category = newOrganCategory;
        newOrgan.icon = newOrganIcon;

        string folderPath = "Assets/OrgansData"; // 保存先フォルダ
        // フォルダがなければ作成
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets/Data", "Organs");
        }

        string desiredPath = $"{folderPath}/{newOrganAssetName}.asset";
        string uniquePath = AssetDatabase.GenerateUniqueAssetPath(desiredPath);

        AssetDatabase.CreateAsset(newOrgan, uniquePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("成功", $"新しい臓器を {uniquePath} に保存しました。", "OK");
        LoadAllOrgans();

        // 入力欄をクリア
        newOrganAssetName = "";
        newOrganId = 0;
        newOrganRarity = 1;
        newOrganIcon = null;
    }
}