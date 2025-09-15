using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class SynthesisUIManager : MonoBehaviour
{
    [Header("合成スロットのUI")]
    // Inspectorで左側の3つのスロット(Image)を設定
    public List<Image> synthesisSlots;

    [Header("合成ボタンと結果表示")]
    // 合成ボタンを設定
    public Button synthesisButton;
    // 生成するモンスターを表示するイメージを設定
    public Image resultMonsterImage;

    [Header("インベントリUIへの参照")]
    // Inspectorでインベントリパネルを設定
    public InventoryUI inventoryUI;

    // 現在選択されている素材リスト
    private List<OrganData> selectedIngredients = new List<OrganData>();
    // 合成ロジック本体
    private MonsterSynthesizer synthesizer = new MonsterSynthesizer();
    // 表示中のレシピ結果
    private MonsterData currentRecipeResult;

    /// <summary>
    /// このUIが表示状態になったときに呼び出される。
    /// インベントリスロットからのクリックイベントの購読を開始する。
    /// </summary>
    private void OnEnable()
    {
        InventorySlotUI.OnSlotClicked += HandleSlotClick;
        // 合成ロジック用のレシピをロード
        synthesizer.LoadAllRecipes();
    }

    /// <summary>
    /// このUIが非表示状態になったときに呼び出される。
    /// メモリリークを防ぐため、クリックイベントの購読を必ず解除する。
    /// </summary>
    private void OnDisable()
    {
        InventorySlotUI.OnSlotClicked -= HandleSlotClick;
    }

    private void Start()
    {
        // ボタンがクリックされるとPerformSynthesisメソッドを呼び出す予約
        synthesisButton.onClick.AddListener(PerformSynthesis);
        UpdateSynthesisUI(); // 初期表示を更新
    }

    // インベントリスロットがクリックされるたびに呼ばれる
    private void HandleSlotClick(OrganData clickedOrgan)
    {
        // もし、クリックされた臓器が既に選択リストにあれば、リストから除去
        if (selectedIngredients.Contains(clickedOrgan))
        {
            selectedIngredients.Remove(clickedOrgan);
        }
        // もし、選択リストに空きがあれば（3つ未満）、リストに追加
        else if (selectedIngredients.Count < synthesisSlots.Count) // 3ではなくリストの数で判定
        {
            selectedIngredients.Add(clickedOrgan);
        }

        UpdateSynthesisUI();
    }

    // 合成UI全体の表示を更新する
    private void UpdateSynthesisUI()
    {
        // 1. 合成スロットのアイコンを更新
        for (int i = 0; i < synthesisSlots.Count; i++)
        {
            if (i < selectedIngredients.Count)
            {
                synthesisSlots[i].enabled = true;
                synthesisSlots[i].sprite = selectedIngredients[i].icon;
            }
            else
            {
                synthesisSlots[i].enabled = false; // 空のスロットは非表示
            }
        }

        // 2. レシピと照合して、合成可能かチェック
        currentRecipeResult = synthesizer.Synthesize(selectedIngredients);
        
        // 3. 合成ボタンの有効/無効を切り替え
        synthesisButton.interactable = (currentRecipeResult != null);
        
        // 4. 結果表示を更新（プレビュー）
        if (currentRecipeResult != null)
        {
            resultMonsterImage.enabled = true;
            resultMonsterImage.sprite = currentRecipeResult.icon;
        }
        else
        {
            resultMonsterImage.enabled = false;
        }
    }

    // 合成ボタンが押されたときに呼ばれる
    private void PerformSynthesis()
    {
        if (currentRecipeResult == null) return;

        // --- 実際にアイテムを消費してモンスターを入手する処理 ---

        // 1. InventoryManagerからselectedIngredientsを消費
        foreach (var ingredient in selectedIngredients)
        {
            // 今回は1つずつ消費する想定
            // InventoryManager.Instance.ownedOrgans[ingredient]--;
            // if(InventoryManager.Instance.ownedOrgans[ingredient] <= 0)
            // {
            //     InventoryManager.Instance.ownedOrgans.Remove(ingredient);
            // }
            InventoryManager.Instance.RemoveOrgan(ingredient);
        }

        // 2. PlayerDataにcurrentRecipeResultを追加
        GameManager.Instance.PlayerData.AddMonster(currentRecipeResult, 1);

        // 3. InventoryUIの表示を更新
        inventoryUI.UpdateDisplay();

        Debug.Log(currentRecipeResult.monsterName + " を生成しました！");

        // 合成後、選択をクリアしてUIを再更新
        selectedIngredients.Clear();
        UpdateSynthesisUI();
    }
}