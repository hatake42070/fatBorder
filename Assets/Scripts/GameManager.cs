using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // シングルトンインスタンス
    public static GameManager Instance { get; private set; }

    // プレイヤーデータへの参照
    public PlayerData PlayerData { get; private set; }

    // 現在挑戦中のステージ番号
    public int currentStageIndex { get; private set; }

    private void Awake()
    {
        // シングルトンパターンの実装
        if (Instance != null && Instance != this)
        {
            // 自分はテスト用の仮のマネージャーなので、自分を破棄して処理を終える
            Destroy(gameObject);
            return; // returnで、これ以降のAwake処理を実行しない
        }
        // 以下は、自分が最初のGameManagerだった場合のみ実行される
        Instance = this;
        DontDestroyOnLoad(gameObject);
        PlayerData = GetComponentInChildren<PlayerData>();
    }

    // --- シーン切り替え用の関数 ---
    public void GoToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void GoToLab()
    {
        SceneManager.LoadScene("LabScene");
    }

    public void GoToSynthesis()
    {
        SceneManager.LoadScene("SynthesisScene");
    }

    public void GoToBattle(int stageIndex)
    {
        // どのステージに挑戦するか、などの情報を保持してシーンをロード
        this.currentStageIndex = stageIndex;
        SceneManager.LoadScene("Battle_Scene");
    }
}