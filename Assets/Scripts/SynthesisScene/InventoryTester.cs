using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    // Inspectorで、テスト追加したい臓器アセットを設定する
    public OrganData testOrgan1;
    public OrganData testOrgan2;
    public OrganData testOrgan3;

    void Update()
    {
        // 「P」キーが押されたら
        if (Input.GetKeyDown(KeyCode.P))
        {
            // 1. PlayerDataにテスト用の臓器を1個追加する
            GameManager.Instance.PlayerData.AddOrgan(testOrgan1, 1);
            GameManager.Instance.PlayerData.AddOrgan(testOrgan2, 1);
            GameManager.Instance.PlayerData.AddOrgan(testOrgan3, 1);
        }
    }
}