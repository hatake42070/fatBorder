using UnityEngine;

// この行を追加することで、Unityのメニューからデータアセットを作成できるようになる
[CreateAssetMenu(fileName = "NewMonsterData", menuName = "Data/Monster Data")]
public class MonsterData : ScriptableObject
{
    public int monsterID;
    public string monsterName;
    [TextArea]
    public string description; // 図鑑用の説明文

    public Sprite illustration; // モンスターのイラスト

    public int maxHp;
    public int attackPower;
    // public SkillData specialSkill; // スキルなどもデータとして紐付けられる
}
