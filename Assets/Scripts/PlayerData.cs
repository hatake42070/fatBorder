using UnityEngine;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour
{
    public int researchPoints; // 研究ポイント

    // 所持している臓器とその数
    public Dictionary<OrganData, int> ownedOrgans = new Dictionary<OrganData, int>();
    // 解放済みのモンスター
    public List<MonsterData> unlockedMonsters = new List<MonsterData>();
    // 所持しているアーティファクト
    public List<ArtifactData> ownedArtifacts = new List<ArtifactData>();

    // --- データ操作用の関数 (例) ---

    public void AddPoints(int amount)
    {
        researchPoints += amount;
    }

    public bool UsePoints(int amount)
    {
        if (researchPoints >= amount)
        {
            researchPoints -= amount;
            return true; // 消費成功
        }
        return false; // ポイント不足
    }

    public void AddOrgan(OrganData organ, int amount)
    {
        if (ownedOrgans.ContainsKey(organ))
        {
            ownedOrgans[organ] += amount;
        }
        else
        {
            ownedOrgans.Add(organ, amount);
        }
    }
}