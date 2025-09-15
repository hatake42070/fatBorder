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
        if (Instance == null)
        {
            Instance = this;
            // このオブジェクトをシーン切り替え時に破棄しない
            DontDestroyOnLoad(gameObject);

            // 自分の子オブジェクトからPlayerDataを探して参照を保持
            PlayerData = GetComponentInChildren<PlayerData>();
        }
        else
        {
            Destroy(gameObject); // 既にインスタンスが存在すれば自分を破棄
        }
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