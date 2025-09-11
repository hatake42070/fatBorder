using UnityEngine;
using UnityEditor; // エディタ拡張に必須

public class SynthesisRecipeEditor : EditorWindow
{
    // メニューの「Tools」に「合成レシピエ-ディタ」という項目を追加
    [MenuItem("Tools/合成レシピエディタ")]
    public static void ShowWindow()
    {
        // ウィンドウを表示する
        GetWindow<SynthesisRecipeEditor>("合成レシピエディタ");
    }

    // ウィンドウのUIを描画する特別な関数
    private void OnGUI()
    {
        // ここにUIのコードを書いていく
        GUILayout.Label("ここにレシピの一覧が表示されます", EditorStyles.boldLabel);
    }
}