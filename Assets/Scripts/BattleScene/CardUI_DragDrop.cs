using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardUI_DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private Card cardData;
    private BattleManager battleManager;
    private Transform originalParent;
    private Vector3 originalPosition;
    private Canvas canvas;  // ドラッグ時にUIが隠れないようにする

    public void Setup(Card card, BattleManager manager)
    {
        cardData = card;
        battleManager = manager;
        canvas = GetComponentInParent<Canvas>();
    }

    // ドラッグ開始
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalPosition = transform.position;
        transform.SetParent(canvas.transform); // UIが最前面になるように
    }

    // ドラッグ中
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    // ドラッグ終了
    public void OnEndDrag(PointerEventData eventData)
    {
        // 仮に画面中央より上でドロップしたらカードをプレイする
        if (eventData.position.y > Screen.height / 2f)
        {
            if (battleManager != null)
            {
                battleManager.PlayCard(cardData);
            }
            Destroy(gameObject); // カードをUIから削除
        }
        else
        {
            // 元の位置に戻す
            transform.SetParent(originalParent);
            transform.position = originalPosition;
        }
    }
}