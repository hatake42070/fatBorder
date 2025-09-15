using UnityEngine;
using UnityEngine.UI;
using System;
//using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    public Image icon;
    //public TextMeshProUGUI countText;
    public Text countText;
    
    public static event Action<OrganData> OnSlotClicked;

    // データを受け取ってスロットの見た目を設定する
    public void Setup(OrganData organ, int count)
    {
        icon.enabled = true;
        icon.sprite = organ.icon;
        countText.text = count.ToString();
    }

    // スロットを空の状態にする
    public void ClearSlot()
    {
        icon.enabled = false; // アイコンを非表示にする
        countText.text = "";
    }
}