using ETModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ZSXMenu : EditorWindow
{
    [MenuItem("Helps/刷新并运行 _F1")]
    public static void ShowZHENTW_copyfff()
    {
        AssetDatabase.Refresh();
        EditorApplication.ExecuteMenuItem("Edit/Play");
    }

    [MenuItem("Helps/刷登录")]
    public static void ShowRefresh()
    {
        GetWindow<LoginEditPlay>(false);
    }


    [MenuItem("Helps/多语言copy(删减版)")]
    public static void ShowZHENTW_Language()
    {
        //GetWindow<GenerateStringLanguage>(false);
    }


    [MenuItem("Helps/检测某个Prefab使用了多少种 Sprite的Tag,2大图集替换成碎图")]
    public static void ShowZHENTW_copy()
    {
        GetWindow<CheckSpriteTag>(false);
    }

    [MenuItem("Helps/错误使用资源名")]
    public static void ShowErrorUseImage()
    {
        GetWindow<CheckErrorUsingImage>(false);
    }

    [MenuItem("Helps/prefab root的Scale倍数  %&v")]
    public static void ShowWindow_Scale()
    {
        GetWindow<CheckRootScale>(false);
    }

    [MenuItem("Helps/修改RayCast 是否勾选了 %#&r")]
    public static void ShowWindow_RayCast()
    {
        GetWindow<CheckModifyRayCast>(false);
    }

    [MenuItem("Helps/(删除前检查下)检测有无使用碎图 ", false, 3999)]
    public static void ShowWindowDC()
    {
        GetWindow<CheckDeleteBeforeImage>(false);
    }

    [MenuItem("Assets/标记AB包 %#&B", false, 80)]
    public static void ShowRightMouse()
    {
        var tSelect = Selection.activeObject;
        if (tSelect is GameObject)
        {
            var tSed = tSelect as GameObject;
            var tPath = AssetDatabase.GetAssetPath(tSed);
            var tAsset = AssetImporter.GetAtPath(tPath);
            tAsset.assetBundleName = tSed.name + ".unity3d"; //设置Bundle文件的名称    
            tAsset.SaveAndReimport();
            AssetDatabase.Refresh();
            Selection.activeObject = null;
        }
        else
        {
            Debug.Log("非 GameObject");
        }
    }

    [MenuItem("Helps/多语言copy  %&L")]
    public static void ShowWindow_LanguageMore()
    {
        //GetWindow<LanguageCopy>(false);
    }

    [MenuItem("Helps/简单生成脚本")]
    public static void ShowWindow_AddGenerate()
    {
        GetWindow<GenerateScriptCode1>(false);
        //  GetWindow<LanguageCopy>(false);
    }
    [MenuItem("Helps/清空playerPrefs %&c")]
    public static void ClearPrefab()
    {
        Debug.Log("清空playerPrefs了");
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Helps/显示时间戳 %T")]
    public static void ShowTime()
    {
        EditorWindow.GetWindow(typeof(TimeLookEditor)); //>(false,"",true);
    }
}


public class GetAssetGOWindow : EditorWindow
{
    /// <summary> 也就是Inspector面板 的 Apply按钮   并且标记上AB包了</summary>
    public void SaveNewPrefabs(List<GameObject> pWillChanges)
    {
        if (pWillChanges == null || pWillChanges.Count == 0) return;
        for (int i = 0; i < pWillChanges.Count; i++)
        {
            var tPrefab = pWillChanges[i];
            var tOldHierarchyPrefab = PrefabUtility.InstantiatePrefab(tPrefab) as GameObject;
            var tPath = AssetDatabase.GetAssetPath(tPrefab);
            var tNewEmpty = PrefabUtility.CreateEmptyPrefab(tPath);
            var gameNew = PrefabUtility.ReplacePrefab(tOldHierarchyPrefab, tNewEmpty, ReplacePrefabOptions.ConnectToPrefab);
            GameObject.DestroyImmediate(tOldHierarchyPrefab);

            var tPathAB = AssetDatabase.GetAssetPath(gameNew);
            var tAssetAB = AssetImporter.GetAtPath(tPathAB);
            tAssetAB.assetBundleName = gameNew.name + ".unity3d";
            tAssetAB.SaveAndReimport();
        }
    }


    /// <summary>根据字符串得到GO           pFilter:Pefab或Script                pGoStr:prefabName </summary>
    public GameObject FindOneGo(string pFilter, string pGoStr)
    {
        var tGuids = AssetDatabase.FindAssets("t:" + pFilter + " " + pGoStr, new string[] { "Assets/Bundles/UI" });
        GameObject tGo = null;
        int i = 0;
        foreach (var guid in tGuids)
        {
            tGo = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(guid)) as GameObject;
            i++;
        }
        if (i > 1) Debug.Log("提供的字符串中 搜索出来,有2个+(得到是最后一个)   建议prefab名字加个'_1' 得到字符串后改回来");
        return tGo;
    }
    public List<GameObject> GetPrefabs(string pPath)
    {
        if (string.IsNullOrEmpty(pPath)) pPath = "Assets/Bundles/UI";
        var guids = AssetDatabase.FindAssets("t:Prefab", new string[] { pPath });
        List<GameObject> tAllPrefab = new List<GameObject>();
        foreach (var item in guids)
        {
            var go = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(item)) as GameObject;
            tAllPrefab.Add(go);
        }
        return tAllPrefab;
    }
}