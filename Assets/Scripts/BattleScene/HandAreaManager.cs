using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandAreaManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DeckManager deckManager;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform handArea;

    private readonly List<GameObject> spawnedCards = new List<GameObject>();

    public void UpdateHandUI()
    {
        // 1. 既存UI削除
        foreach (var card in spawnedCards)
        {
            Destroy(card);
        }
        spawnedCards.Clear();

        // 2. 新規UI生成
        foreach (var cardData in deckManager.GetHand())
        {
            GameObject cardObj = Instantiate(cardPrefab, handArea);

            // カード名
            // ? は null 条件演算子。簡単に言うと、参照がnullじゃなければアクセスして、nullなら処理をスキップする
            TMP_Text nameText = cardObj.transform.Find("NameText")?.GetComponent<TMP_Text>();
            if (nameText) nameText.text = cardData.GetCardName();

            // マナコスト
            TMP_Text manaText = cardObj.transform.Find("ManaCostText")?.GetComponent<TMP_Text>();
            if (manaText) manaText.text = cardData.GetManaCost().ToString();

            // 攻撃力
            TMP_Text powerText = cardObj.transform.Find("PowerText")?.GetComponent<TMP_Text>();
            if (powerText) powerText.text = cardData.GetPower().ToString();

            // 画像
            Image cardImage = cardObj.transform.Find("CardImage")?.GetComponent<Image>();
            if (cardImage) cardImage.sprite = cardData.GetSprite();

            spawnedCards.Add(cardObj);
        }
    }
}
