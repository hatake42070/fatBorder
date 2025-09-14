using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    // どの臓器(OrganData)を、いくつ(int)持っているかを管理する辞書
    public Dictionary<OrganData, int> ownedOrgans = new Dictionary<OrganData, int>();

    void Awake()
    {
        Instance = this;
    }

    // 臓器を追加する関数
    public void AddOrgan(OrganData organ, int amount)
    {
        if (ownedOrgans.ContainsKey(organ))
        {
            ownedOrgans[organ] += amount; // 既に持っていれば数を増やす
        }
        else
        {
            ownedOrgans.Add(organ, amount); // 新しく追加
        }
        
        Debug.Log(organ.organName + " を " + amount + "個追加しました。");
        // ここでUI更新のイベントを発行するのが理想的
    }
}