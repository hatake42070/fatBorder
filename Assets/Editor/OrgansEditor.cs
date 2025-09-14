using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class OrgansEditor : EditorWindow
{
    // --- 変数定義 ---
    private List<OrganData> allOrgans = new List<OrganData>();
    private Vector2 scrollPosition;

    [MenuItem("Tools/臓器管理エディタ")]
    public static void ShowWindow()
    {
        GetWindow<OrgansEditor>("臓器管理エディタ");
    }

    private void OnEnable()
    {
        LoadAllOrgans();
    }

    private void LoadAllOrgans()
    {
        allOrgans = EditorUtils.LoadAllAssets<OrganData>();
    }

}