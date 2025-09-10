using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewSynthesisRecipe", menuName = "Data/Synthesis Recipe")]
public class SynthesisRecipe : ScriptableObject
{
    [Header("素材となる臓器")]
    public List<OrganData> ingredients = new List<OrganData>(); // 材料リスト

    [Header("結果として生まれるモンスター")]
    public MonsterData resultingMonster;
}