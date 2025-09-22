using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject Panel; // 演出全体の親パネル
    [SerializeField] private GameObject OrganPanel; // 臓器表示する用のパネル
    [SerializeField] private GameObject MonsterPanel; // モンスター表示用のUIパネル

    [SerializeField] private Transform organGridContent; // 臓器一覧表示の親オブジェクト
    [SerializeField] private Transform monsterGridContent; // 臓器一覧表示の親オブジェクト


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
