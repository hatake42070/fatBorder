using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private HpGaugeController hpGauge; // プレイヤーのHPゲージ
    [SerializeField] private TMP_Text playerNameText;   // 名前表示

    public void SetName(string name)
    {
        playerNameText.text = name;
    }

    public void TakeDamage(int damage)
    {
        hpGauge.BeInjured(damage);
    }
}
