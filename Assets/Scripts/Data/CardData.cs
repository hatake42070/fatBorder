using UnityEngine;

// カードの効果の種類を定義するenum
public enum CardEffectType { Attack, Defense, Buff, Debuff, Heal}

[CreateAssetMenu(fileName = "NewCardData", menuName = "Data/Card Data")]
public class CardData : ScriptableObject
{
    [Header("カードの基本情報")]
    public int cardID;
    public string cardName;
    public Sprite illustration; // カードのイラスト
    [TextArea]
    public string description; // カードの効果説明文

    [Header("ゲームプレイ用のパラメータ")]
    public int manaCost; // このカードをプレイするためのマナコスト
    public CardEffectType effectType; // このカードがどんな種類の効果か

    public int power; // 攻撃や防御の基本となる数値
    // public int duration; // バフ・デバフの持続ターン数など
    // public GameObject effectPrefab; // カード使用時のエフェクト
}