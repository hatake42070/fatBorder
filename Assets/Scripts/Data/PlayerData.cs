using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class PlayerData : MonoBehaviour
{
    public int researchPoints; // 研究ポイント

    // --- データ変更を通知するためのイベント ---
    public static event Action OnInventoryChanged;

    // 所持している臓器とその数
    public Dictionary<OrganData, int> ownedOrgans = new Dictionary<OrganData, int>();
    // 所持しているモンスター
    public Dictionary<MonsterData, int> ownedMonsters = new Dictionary<MonsterData, int>();
    // public List<MonsterData> unlockedMonsters = new List<MonsterData>();
    // 所持しているアーティファクト
    public List<ArtifactData> ownedArtifacts = new List<ArtifactData>();

    // --- デバック用 ---
    [Header("デバッグ用インベントリ表示")]
    [SerializeField] private List<OrganData> organKeys = new List<OrganData>();
    [SerializeField] private List<int> organValues = new List<int>();
    [SerializeField] private List<MonsterData> monsterKeys = new List<MonsterData>();
    [SerializeField] private List<int> monsterValues = new List<int>();

    // --- インスペクターの表示を更新するためのメソッドを追加 ---
    // Updateはゲーム実行中に毎フレーム呼び出される
    private void Update()
    {
        // エディタで再生中のみ、デバッグリストを更新する（パフォーマンスのため）
        #if UNITY_EDITOR
        UpdateDebugLists();
        #endif
    }

    // OnValidateからも呼び出して、非再生中の編集にも対応
    private void OnValidate()
    {
        // すぐに更新するとパフォーマンスに影響する場合があるため、
        // 念のためエディタの更新ループで一度だけ呼ばれるようにする
        UnityEditor.EditorApplication.delayCall += UpdateDebugLists;
    }

    // 更新処理を一つのメソッドにまとめる
    private void UpdateDebugLists()
    {
        // nullチェックを追加して、エディタでのエラーを防ぐ
        if (ownedOrgans == null || ownedMonsters == null) return;
        
        organKeys = ownedOrgans.Keys.ToList();
        organValues = ownedOrgans.Values.ToList();
        monsterKeys = ownedMonsters.Keys.ToList();
        monsterValues = ownedMonsters.Values.ToList();
    }

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
        // イベント発行
        OnInventoryChanged?.Invoke();
    }
    // 臓器を削除するメソッド
    public void RemoveOrgan(OrganData organ)
    {
        ownedOrgans[organ]--;
        if (ownedOrgans[organ] <= 0)
        {
            ownedOrgans.Remove(organ);
        }
        // イベント発行
        OnInventoryChanged?.Invoke();
    }

    public void AddMonster(MonsterData monster, int amount)
    {
        if (ownedMonsters.ContainsKey(monster))
        {
            ownedMonsters[monster] += amount;
        }
        else
        {
            ownedMonsters.Add(monster, amount);
        }
    }
}