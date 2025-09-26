using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class DetailSystem : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI objectName;
    [SerializeField] private int rarity;
    [SerializeField] private int ownedCount;
    [SerializeField] private Image typeIcon;
    [SerializeField] private List<Image> typeIconList;
    [SerializeField] private string descrption;

    
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

    /// <summary>
    /// 詳細パネルの表示を更新する
    /// </summary>
    private void ShowDetail(ScriptableObject data)
    {
        if (data is OrganData)
        {
            // OrganData型に変換（キャスト）
            OrganData organData = data as OrganData;
        }
        else if (data is MonsterData)
        {

        }
        else
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
