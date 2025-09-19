using UnityEngine;
using UnityEngine.UI;

public class LabManager : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button goToButtle;
    public Button goToStudy;
    public Button goToInventry;

    void Start()
    {
        goToButtle.onClick.AddListener(GameManager.Instance.GoToBattle);
        goToStudy.onClick.AddListener(GameManager.Instance.GoToStudy);
        goToInventry.onClick.AddListener(GameManager.Instance.GoToInventory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
