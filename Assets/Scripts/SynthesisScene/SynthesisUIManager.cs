using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class SynthesisUIManager : MonoBehaviour
{
    [Header("合成スロットのUI")]
    public List<Image> synthesisSlots; // Inspectorで左側の3つのスロット(Image)を設定
    
    [Header("合成ボタンと結果表示")]
    public Button synthesisButton;
    public Image resultMonsterImage;

    // 現在選択されている素材リスト
    private List<OrganData> selectedIngredients = new List<OrganData>();
    // 合成ロジック本体
    private MonsterSynthesizer synthesizer = new MonsterSynthesizer();
    // 表示中のレシピ結果
    private MonsterData currentRecipeResult;

    private void OnEnable()
    {
        // InventorySlotUIからのクリックイベントを購読
        InventorySlotUI.OnSlotClicked += HandleSlotClick;
        // 合成ロジック用のレシピをロード
        synthesizer.LoadAllRecipes();
    }

    private void OnDisable()
    {
        // イベントの購読を解除（重要）
        InventorySlotUI.OnSlotClicked -= HandleSlotClick;
    }

    private void Start()
    {
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
        else if (selectedIngredients.Count < 3)
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
        // レシピが見つかった場合のみ、ボタンを押せるようにする
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

        // ここに、実際にアイテムを消費してモンスターを入手する処理を書く
        // 1. InventoryManagerからselectedIngredientsを消費
        // 2. PlayerDataにcurrentRecipeResultを追加
        // 3. InventoryUIの表示を更新

        Debug.Log(currentRecipeResult.monsterName + " を生成しました！");

        // 合成後、選択をクリア
        selectedIngredients.Clear();
        UpdateSynthesisUI();
    }
}