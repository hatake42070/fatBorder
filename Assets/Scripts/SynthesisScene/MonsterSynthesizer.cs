using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MonsterSynthesizer
{
    private List<SynthesisRecipe> allRecipes;

    public void LoadAllRecipes()
    {
        allRecipes = Resources.LoadAll<SynthesisRecipe>("Recipes").ToList();
    }

    // 引数をリストに変更
    public MonsterData Synthesize(List<OrganData> inputIngredients)
    {
        foreach (var recipe in allRecipes)
        {
            // レシピと入力された素材の数が違う場合は、次のレシピへ
            if (recipe.ingredients.Count != inputIngredients.Count)
            {
                continue;
            }

            // 素材が一致するかどうかをチェック
            // レシピと渡された素材をID順にそーそして、比較する
            var sortedRecipeIngredients = recipe.ingredients.OrderBy(organ => organ.organID).ToList();
            var sortedInputIngredients = inputIngredients.OrderBy(organ => organ.organID).ToList();

            // 順番を揃えたリストが完全に一致するかどうかを判定
            if (sortedRecipeIngredients.SequenceEqual(sortedInputIngredients))
            {
                // 一致したら、結果のモンスターを返す
                return recipe.resultingMonster;
            }
        }

        // 一致するレシピがなければ、nullを返す
        return null;
    }
}