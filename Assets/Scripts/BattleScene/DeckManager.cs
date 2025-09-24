using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [Header("Deck Management")]
    public List<Card> deck = new List<Card>();        // 山札
    public List<Card> hand = new List<Card>();        // 手札
    public List<Card> discardPile = new List<Card>(); // 墓地

    [Header("Start Game Hand Setting")]
    [SerializeField] private int initialHandSize = 5; // ゲーム開始時の手札枚数

    /// <summary>
    /// ゲーム開始時に初期手札を引くためのメソッド
    /// </summary>
    public void DrawInitialHand()
    {
        for (int i = 0; i < initialHandSize; i++)
        {
            DrawCard();
        }
    }

    /// <summary>
    /// ターン開始時に1枚だけドロー
    /// </summary>
    public void DrawCardAtTurnStart()
    {
        DrawCard();
    }

    /// <summary>
    /// ゲーム開始時に初期手札を引くためのメソッド
    /// </summary>
    public void DrawInitialHand()
    {
        for (int i = 0; i < initialHandSize; i++)
        {
            DrawCard();
        }
    }

    /// <summary>
    /// ターン開始時に1枚だけドロー
    /// </summary>
    public void DrawCardAtTurnStart()
    {
        DrawCard();
    }

    /// <summary>
    /// デッキから1枚ドロー
    /// </summary>
    public Card DrawCard()
    {
        if (deck.Count == 0)
        {
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

    // 手札リスト取得のためのゲッター
    public List<Card> GetHand() => hand;
}
