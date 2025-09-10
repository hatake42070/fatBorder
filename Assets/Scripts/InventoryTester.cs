using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    // Inspectorで、テスト追加したい臓器アセットを設定する
    public OrganData testOrgan1;
    public OrganData testOrgan2;
    
    // 表示を更新するためのInventoryUIへの参照
    public InventoryUI inventoryUI;

    void Update()
    {
        // 「P」キーが押されたら
        if (Input.GetKeyDown(KeyCode.P))
        {
            // 1. InventoryManagerにテスト用の臓器を1個追加する
            InventoryManager.Instance.AddOrgan(testOrgan1, 1);

            // 2. InventoryUIに表示を更新するように命令する
            inventoryUI.UpdateDisplay();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            // 1. InventoryManagerにテスト用の臓器を1個追加する
            InventoryManager.Instance.AddOrgan(testOrgan2, 1);

            // 2. InventoryUIに表示を更新するように命令する
            inventoryUI.UpdateDisplay();
        }
    }
}