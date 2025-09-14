using UnityEngine;
using System.Collections.Generic;
using System.Linq; // ToList()を使うために必要

public class InventoryUI : MonoBehaviour
{
    public GameObject inventorySlotPrefab; // スロットのプレハブ
    public Transform contentPanel;         // スロットを生成する親オブジェクト
    public int maxSlots = 50;             // インベントリの最大スロット数

    // 生成した全スロットの参照を保存しておくリスト
    private List<InventorySlotUI> slotUIs = new List<InventorySlotUI>();

    void Start()
    {
        // 最初に空のスロットを最大数だけ生成する
        for (int i = 0; i < maxSlots; i++)
        {
            // prefabのクローンをcontentPanelに配置
            GameObject slotGO = Instantiate(inventorySlotPrefab, contentPanel);
            InventorySlotUI slotUI = slotGO.GetComponent<InventorySlotUI>();
            slotUI.ClearSlot(); // スロットを空の状態にする
            slotUIs.Add(slotUI);
        }

        // インベントリの初期表示を更新
        UpdateDisplay();
    }

    // インベントリの表示を更新する関数
    public void UpdateDisplay()
    {
        // InventoryManagerから最新の所持品リストを取得
        var ownedOrgans = InventoryManager.Instance.ownedOrgans;
        List<OrganData> ownedOrgansList = ownedOrgans.Keys.ToList();

        // 全スロットをループして、表示を更新する
        for (int i = 0; i < slotUIs.Count; i++)
        {
            // 表示すべきアイテムがまだある場合
            if (i < ownedOrgansList.Count)
            {
                OrganData organ = ownedOrgansList[i];
                int count = ownedOrgans[organ];
                slotUIs[i].Setup(organ, count); // スロットにデータを設定
            }
            // 表示すべきアイテムがもうない（空のスロット）の場合
            else
            {
                slotUIs[i].ClearSlot(); // スロットを空にする
            }
        }
    }
}