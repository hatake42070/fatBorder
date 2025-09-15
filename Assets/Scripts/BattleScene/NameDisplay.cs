using UnityEngine;
using TMPro; // TextMeshPro を使う場合

public class NameDisplay : MonoBehaviour
{
    private TMP_Text nameText; // TextMeshProコンポーネントを取得して使う

    void Awake()
    {
        // 同じオブジェクトにあるTextMeshProコンポーネントを取得
        nameText = GetComponent<TMP_Text>();
    }

    // 名前をセットするメソッド
    public void SetName(string playerName)
    {
        nameText.text = playerName;
    }
}
