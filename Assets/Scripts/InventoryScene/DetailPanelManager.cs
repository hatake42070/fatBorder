using TMPro;
using UnityEngine;

public class DetailPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject detailPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        GenericSlotUI.OnSlotClicked += ShowDetail;
    }
    private void OnDisable()
    {
        GenericSlotUI.OnSlotClicked -= ShowDetail;
    }

    // 詳細パネルの表示を更新する
    private void ShowDetail(ScriptableObject data)
    {

    }

}
