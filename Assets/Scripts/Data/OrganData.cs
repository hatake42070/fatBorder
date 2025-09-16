using UnityEngine;

// 臓器のカテゴリをenumで定義
public enum OrganCategory { Viscera, Muscle, Bone, Other } //内臓、筋肉、骨格、その他

[CreateAssetMenu(fileName = "NewOrganData", menuName = "Data/Organ Data")]
public class OrganData : ScriptableObject
{
    [Header("基本情報")]
    public int organID;
    public string organName;

    [Header("ゲームロジック用")] //合成で使う
    public OrganCategory category; //カテゴリー
    public int rarity; // 1~5

    [Header("UI表示用")]
    public Sprite icon;
    [TextArea]
    public string description; // 図鑑用の説明文
    public int price; // ショップで購入する時の価格
}