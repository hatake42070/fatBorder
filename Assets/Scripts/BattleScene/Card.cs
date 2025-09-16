using UnityEngine;

/// カード1枚の情報を保持するクラス
[System.Serializable]  // インスペクタで表示できるように
public class Card
{
    [Header("基本情報")]
    public string cardName;          // カードの名前
    public int manaCost;             // 消費マナ
    public CardType cardType;        // カードの種類（攻撃・回復・魔法など）
    
    [Header("効果設定")]
    public int power;                // 効果量（攻撃力や回復量など）
    
    [Header("UI用情報")]
    [TextArea] 
    public string description;       // UI表示用のカードの説明文

    // コンストラクタ（コードからカードを生成する時に使用）
    public Card(string name, int cost, CardType type, int powerValue, string desc)
    {
        cardName = name;
        manaCost = cost;
        cardType = type;
        power = powerValue;
        description = desc;
    }
}

/// カードの種類
public enum CardType
{
    Attack,
    Heal,
    Magic,
    Buff,
    Debuff
}
