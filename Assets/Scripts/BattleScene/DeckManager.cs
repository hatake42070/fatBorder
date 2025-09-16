using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [Header("デッキ管理")]
    public List<Card> deck = new List<Card>();       // 山札
    public List<Card> hand = new List<Card>();       // 手札
    public List<Card> discardPile = new List<Card>(); // 墓地

    [Header("手札設定")]
    public int handSize = 5; // 手札枚数

    /// <summary>
    /// デッキにカードを追加（初期設定用）
    /// </summary>
    public void AddCardToDeck(Card card)
    {
        deck.Add(card);
    }

    /// <summary>
    /// デッキから1枚ドロー
    /// </summary>
    public Card DrawCard()
    {
        if (deck.Count == 0)
        {
            // 山札が空なら墓地をシャッフルして戻す
            ReshuffleDiscardIntoDeck();
        }

        if (deck.Count == 0)
        {
            Debug.LogWarning("山札も墓地も空です！");
            return null;
        }

        Card drawn = deck[0];
        deck.RemoveAt(0);
        hand.Add(drawn);
        return drawn;
    }

    /// <summary>
    /// ターン開始時に手札を補充する
    /// </summary>
    public void DrawHandToFull()
    {
        while (hand.Count < handSize)
        {
            DrawCard();
        }
    }

    /// <summary>
    /// 使用済みカードを墓地に送る
    /// </summary>
    public void DiscardCard(Card card)
    {
        if (hand.Contains(card))
        {
            hand.Remove(card);
            discardPile.Add(card);
        }
    }

    /// <summary>
    /// 墓地をシャッフルして山札に戻す
    /// </summary>
    private void ReshuffleDiscardIntoDeck()
    {
        deck.AddRange(discardPile);
        discardPile.Clear();
        ShuffleDeck();
    }

    /// <summary>
    /// 山札をシャッフル
    /// </summary>
    public void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int rand = Random.Range(i, deck.Count);
            Card temp = deck[i];
            deck[i] = deck[rand];
            deck[rand] = temp;
        }
    }
}
