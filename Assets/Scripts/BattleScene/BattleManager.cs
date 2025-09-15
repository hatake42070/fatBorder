using System.Collections;
using UnityEngine;
using TMPro; // TextMeshPro を使う場合

public class BattleManager : MonoBehaviour
{
    // === プレイヤーと敵のステータス ===
    [SerializeField] private int playerHP = 100;
    [SerializeField] private int enemyHP = 50;

    // === HUDやUIへの参照 ===
    [SerializeField] private HpGaugeController playerHpBar;  // HPゲージ制御
    [SerializeField] private HpGaugeController enemyHpBar;   // HPゲージ制御
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private GameObject commandPanel;        // コマンドボタンパネル

    // バトルの状態管理
    private enum BattleState { START, PLAYER_TURN, ENEMY_TURN, WIN, LOSE }
    private BattleState state;

    void Start()
    {
        // バトル開始
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    // バトル開始時の初期化処理
    IEnumerator SetupBattle()
    {
        messageText.text = "バトル開始！";

        // HPバー初期化
        playerHpBar.BeInjured(0); // 現在HP表示用
        enemyHpBar.BeInjured(0);  // 現在HP表示用

        yield return new WaitForSeconds(1f);

        // プレイヤーターンへ
        PlayerTurn();
    }

    // プレイヤーターン開始
    void PlayerTurn()
    {
        state = BattleState.PLAYER_TURN;
        messageText.text = "コマンドを選んでください";
        commandPanel.SetActive(true); // コマンドボタンを表示
    }

    // 攻撃ボタンから呼び出す用
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYER_TURN) return;
        commandPanel.SetActive(false);
        StartCoroutine(PlayerAttack());
    }

    // プレイヤーの攻撃処理
    IEnumerator PlayerAttack()
    {
        messageText.text = "プレイヤーの攻撃！";

        int damage = 10;
        enemyHP = Mathf.Max(enemyHP - damage, 0);
        enemyHpBar.BeInjured(damage); // HPバーをアニメーションで更新

        yield return new WaitForSeconds(1f);

        if (enemyHP <= 0)
        {
            state = BattleState.WIN;
            EndBattle();
        }
        else
        {
            EnemyTurn();
        }
    }

    // 敵ターン
    void EnemyTurn()
    {
        state = BattleState.ENEMY_TURN;
        StartCoroutine(EnemyAttack());
    }

    // 敵の攻撃処理
    IEnumerator EnemyAttack()
    {
        messageText.text = "敵の攻撃！";

        int damage = 5;
        playerHP = Mathf.Max(playerHP - damage, 0);
        playerHpBar.BeInjured(damage); // HPバーをアニメーションで更新

        yield return new WaitForSeconds(1f);

        if (playerHP <= 0)
        {
            state = BattleState.LOSE;
            EndBattle();
        }
        else
        {
            PlayerTurn();
        }
    }

    // バトル終了
    void EndBattle()
    {
        if (state == BattleState.WIN)
        {
            messageText.text = "勝利しました！";
        }
        else if (state == BattleState.LOSE)
        {
            messageText.text = "負けました...";
        }
    }
}
