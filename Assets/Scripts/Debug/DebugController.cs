using UnityEngine;

public class DebugController : MonoBehaviour
{
    void Update()
    {
        // 「M」キーを押したら、研究ポイントを10000増やす
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (GameManager.Instance != null && GameManager.Instance.PlayerData != null)
            {
                GameManager.Instance.PlayerData.AddPoints(10000);
                Debug.Log("デバッグ: 研究ポイントを10000追加しました。");
            }
        }
    }
}