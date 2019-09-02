using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary 看 localScale是否为1 </summary>
public class CheckRootScale : GetAssetGOWindow
{
    void OnGUI()
    {
        GUILayout.Space(5);
        if (GUILayout.Button("root-搜索Scale不等One,看Log输出", GUILayout.Height(35)))
        {
            var tAll = GetPrefabs(null);
            int tNonCount = 0;
            for (int i = 0; i < tAll.Count; i++)
            {
                if (tAll[i].GetComponent<RectTransform>().localScale != Vector3.one)
                {
                    Debug.Log(tAll[i].name);
                    tNonCount++;
                }
            }
            Debug.LogError("总Prefabs=" + tAll.Count.ToString() + ",不规范的总数有=" + tNonCount.ToString());
        }
        GUILayout.Space(5);
        if (GUILayout.Button("root-一键生成 Scale等于Vector3.One", GUILayout.Height(35)))
        {
            var tCountPrefabs = GetPrefabs(null);
            var tNonHasOnes = new List<GameObject>();
            for (int i = 0; i < tCountPrefabs.Count; i++)
            {
                if (tCountPrefabs[i].GetComponent<RectTransform>().localScale != Vector3.one)
                {
                    tCountPrefabs[i].GetComponent<RectTransform>().localScale = Vector3.one;
                    tNonHasOnes.Add(tCountPrefabs[i]);
                }
            }
            SaveNewPrefabs(tNonHasOnes);
            Debug.LogError("--->总Prefabs=" + tCountPrefabs.Count.ToString() + ",不规范的Count=" + tNonHasOnes.Count.ToString());
        }

        GUILayout.Space(5);
        if (GUILayout.Button("childs-搜索Scale不等One,看Log输出", GUILayout.Height(35)))
        {
            var tAll = GetPrefabs(null);
            for (int i = 0; i < tAll.Count; i++)
            {
                var tChildCount = tAll[i].GetComponentsInChildren<RectTransform>();
                for (int j = 0; j < tChildCount.Length; j++)
                {
                    if (tAll[i].GetComponentsInChildren<RectTransform>()[j].localScale != Vector3.one)
                    {
                        Debug.Log(tAll[i] + "---->" + tAll[i].GetComponentsInChildren<RectTransform>()[j].gameObject.name);
                    }
                }
            }
        }
        GUILayout.Space(5);
        if (GUILayout.Button("childs-一键生成 Scale等于Vector3.One", GUILayout.Height(35)))
        {
            var tAll = GetPrefabs(null);
            var tNonHasOnes = new List<GameObject>();
            for (int i = 0; i < tAll.Count; i++)
            {
                var tChildCount = tAll[i].GetComponentsInChildren<RectTransform>(true);
                for (int j = 0; j < tChildCount.Length; j++)
                {
                    if (tAll[i].GetComponentsInChildren<RectTransform>(true)[j].localScale != Vector3.one)
                    {
                        tAll[i].GetComponentsInChildren<RectTransform>(true)[j].localScale = Vector3.one;
                        if (tNonHasOnes.Contains(tAll[i]) == false)
                        {
                            tNonHasOnes.Add(tAll[i]);
                        }
                    }
                }
            }
            SaveNewPrefabs(tNonHasOnes);
        }
    }


}
