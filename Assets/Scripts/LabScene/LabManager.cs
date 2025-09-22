using UnityEngine;
using UnityEngine.UI;

public class LabManager : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button goToButtle;
    // 合成
    public Button goToSynthesis;
    // 錬成
    public Button goToStudy;
    // ショップ
    public Button goToShop;
    public Button goToMonsterInventry;
    public Button goToOrganInventry;

    void Start()
    {
        goToButtle.onClick.AddListener(GameManager.Instance.GoToBattle);
        goToSynthesis.onClick.AddListener(GameManager.Instance.GoToSynthesis);
        goToShop.onClick.AddListener(GameManager.Instance.GoToShop);
        goToStudy.onClick.AddListener(GameManager.Instance.GoToStudy);
        goToMonsterInventry.onClick.AddListener(GameManager.Instance.GoToMonsterInventory);
        goToOrganInventry.onClick.AddListener(GameManager.Instance.GoToOrganInventory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
