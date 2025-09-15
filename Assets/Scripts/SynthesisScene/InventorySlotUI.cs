using UnityEngine;
using UnityEngine.UI;
using System;

public class InventorySlotUI : MonoBehaviour
{
    public Image icon;
    public Text countText;
    public Image background;

    private Button button;
    private bool isSelected = false;
    private OrganData assignedOrganData;

    // イベント：選択状態の変更通知
    public static event Action<OrganData, bool> OnSelectionChanged;
    // イベント：クリック通知
    public static event Action<OrganData> OnSlotClicked;

    private void Awake()
    {
        button = GetComponent<Button>();
        if (button == null) { button = gameObject.AddComponent<Button>(); }
        button.onClick.AddListener(HandleClick);
        SetSelected(false);
    }

    private void HandleClick()
    {
        if (assignedOrganData != null)
        {
            isSelected = !isSelected;
            SetSelected(isSelected);
            OnSelectionChanged?.Invoke(assignedOrganData, isSelected);
            OnSlotClicked?.Invoke(assignedOrganData);
        }
    }

    public void Setup(OrganData organ, int count)
    {
        assignedOrganData = organ;
        icon.enabled = true;
        icon.sprite = organ.icon;
        countText.text = count.ToString();
        isSelected = false;
        SetSelected(false);
    }

    public void ClearSlot()
    {
        assignedOrganData = null;
        icon.enabled = false;
        countText.text = "";
        isSelected = false;
        SetSelected(false);
    }

    public OrganData GetAssignedOrgan()
    {
        return assignedOrganData;
    }

    public void SetSelected(bool isSelected)
    {
        if (background != null)
        {
            background.color = isSelected ? new Color32(78, 78, 255, 100) : new Color(1, 1, 1, 0);
        }
    }
}
