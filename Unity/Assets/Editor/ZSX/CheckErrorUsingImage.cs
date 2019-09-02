using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckErrorUsingImage : GetAssetGOWindow
{

    private void OnGUI()
    {
        if (GUILayout.Button("检测 Root的组件 是否挂着'bg'的sprite", GUILayout.Height(30)))
        {
            var tAll = GetPrefabs(null);
            for (int i = 0; i < tAll.Count; i++)
            {
                var tSings = tAll[i].GetComponent<Image>();
                if (tSings == null || tSings.sprite == null || tSings.sprite.name != "bg")
                {
                    Debug.Log("prefab name = " + tAll[i]);
                }
            }
        }

        if (GUILayout.Button("使 Root的组件 'bg'的 用纯白", GUILayout.Height(30)))
        {
            List<GameObject> tNews = new List<GameObject>();
            var tAll = GetPrefabs(null);
            for (int i = 0; i < tAll.Count; i++)
            {
                var tSings = tAll[i].GetComponent<Image>();
                if (tSings != null && tSings.sprite != null && tSings.sprite.name == "bg" && tSings.color.r != 1 && tSings.color.g != 1 && tSings.color.b != 1)
                {
                    tSings.color = new Color32(255, 255, 255, 255);
                    tNews.Add(tAll[i]);
                    Debug.Log("using prefab name = " + tAll[i]);
                }
            }
            SaveNewPrefabs(tNews);
        }

        if (GUILayout.Button("检测 scrollview", GUILayout.Height(30)))
        {
            List<GameObject> tNews = new List<GameObject>();
            var tAll = GetPrefabs(null);
            for (int i = 0; i < tAll.Count; i++)
            {
                var tChildrens = tAll[i].GetComponentsInChildren<ScrollRect>(true);
                for (int j = 0; j < tChildrens.Length; j++)
                {
                    var tSRImage = tChildrens[j].GetComponent<Image>();
                    if (tSRImage == null || tSRImage.sprite == null)
                    {
                    }
                    else
                    {
                        if (tSRImage.color.a != 0)
                        {
                            tSRImage.color = new Color32(255, 255, 255, 0);
                            Debug.Log("纯白 scrollview=" + tAll[i]);
                            tNews.Add(tAll[i]);
                        }
                    }
                }
            }
            SaveNewPrefabs(tNews);
        }



        if (GUILayout.Button("检测 UINavigationBar", GUILayout.Height(30)))
        {
            var tAll = GetPrefabs(null);
            for (int i = 0; i < tAll.Count; i++)
            {
                var tChildrens = tAll[i].GetComponentsInChildren<Image>(true);
                for (int j = 0; j < tChildrens.Length; j++)
                {
                    if (tChildrens[j].gameObject.name == "UINavigationBar")
                    {
                        if (tChildrens[j].sprite == null || tChildrens[j].sprite.name != "Background")
                        {
                            Debug.Log("nav=" + tAll[i]);
                        }
                    }
                }
            }
        }
    }
}
