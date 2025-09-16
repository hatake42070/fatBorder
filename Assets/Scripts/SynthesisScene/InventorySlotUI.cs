using UnityEngine;
using UnityEngine.UI;
using System;
//using TMPro;

/// <summary>
/// インベントリスロット一つ分のUI表示と、クリックイベントの発行を担当するクラス。
/// このスクリプトはインベントリスロットのプレハブにアタッチする。
/// </summary>
public class InventorySlotUI : MonoBehaviour
{
    public Image icon;
    //public TextMeshProUGUI countText;
    public Text countText;
    private Button button;

    // このスロットが担当する臓器データ
    private OrganData assignedOrganData;

    // このスロットがクリックされたときに外部に通知するためのイベント
    public static event Action<OrganData> OnSlotClicked;

    private void Awake()
    {
        button = GetComponent<Button>();
        // ボタンがなければ追加する（安全対策）
        if (button == null) { button = gameObject.AddComponent<Button>(); }
        button.onClick.AddListener(HandleClick);
    }

    // データを受け取ってスロットの見た目を設定する
    public void Setup(OrganData organ, int count)
    {
        assignedOrganData = organ; // 担当データを記憶
        icon.enabled = true;
        icon.sprite = organ.icon;
        countText.text = count.ToString();
    }

    // スロットを空の状態にする
    public void ClearSlot()
    {
        assignedOrganData = null;
        icon.enabled = false; // アイコンを非表示にする
        countText.text = "";
    }
    // ボタンがクリックされたときに呼ばれる
    private void HandleClick()
    {
        if (assignedOrganData != null)
        {
            // 自分が担当する臓器データをイベントで通知
            OnSlotClicked?.Invoke(assignedOrganData);
        }
    }
}