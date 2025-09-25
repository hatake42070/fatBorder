using UnityEngine;

public class Card
{
    private CardData data;       // 元データへの参照
    public bool isUsed = false; // 使用済みかどうか等の状態

    // コンストラクタ
    public Card(CardData cardData)
    {
        data = cardData;
    }

    // CardDataに格納されている情報を取り出すためのゲッターメソッド
    public string GetName() => data.cardName;
    public int GetManaCost() => data.manaCost;
    public int GetPower() => data.power;
    public Sprite GetSprite() => data.cardImage;
    public CardType GetCardType() => data.cardType;
    public string GetDescription() => data.description;


}
