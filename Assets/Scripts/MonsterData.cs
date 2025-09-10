using UnityEngine;

// この行を追加することで、Unityのメニューからデータアセットを作成できるようになる
[CreateAssetMenu(fileName = "NewMonsterData", menuName = "Data/Monster Data")]
public class MonsterData : ScriptableObject
{
    [Header("基本情報")]
    public int monsterID;
    public string monsterName;

    [Header("ゲームロジック用")]
    public int maxHp;
    public int attackPower;
    // public SkillData specialSkill; // スキルなどもデータとして紐付けられる

    [Header("UI表示用")]
    [TextArea]
    public string description; // 図鑑用の説明文

    public Sprite icon; // モンスターのイラスト
}
