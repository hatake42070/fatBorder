using UnityEngine;
using UnityEngine.UI; // Buttonを使うために必要

/// <summary>
/// タイトル画面のUIイベントを管理するクラス。
/// </summary>
public class TitleUIManager : MonoBehaviour
{
    [Header("UI Buttons")]
    // インスペクターで「合成シーンへ」移動するボタンを設定
    public Button goToSynthesisButton;

    void Start()
    {
        // goToSynthesisButtonにリスナーが設定されていなければ追加
        if (goToSynthesisButton != null)
        {
            // ボタンがクリックされたら、GameManagerのGoToSynthesisメソッドを呼び出す
            //goToSynthesisButton.onClick.AddListener(() => GameManager.Instance.GoToSynthesis());
            goToSynthesisButton.onClick.AddListener(GameManager.Instance.GoToLab);

        }
    }
}