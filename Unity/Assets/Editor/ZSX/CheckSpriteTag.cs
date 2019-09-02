using ETModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;



public class CheckSpriteTag : GetAssetGOWindow
{
    private GameObject mTargetRoot;
    private Sprite mTargetSprite;
    private Sprite mTargetSprite_BigChangeSmall;
    Dictionary<string, List<string>> mDicTags = new Dictionary<string, List<string>>();

    List<string> mUintyLib = new List<string>() {
        "Background",
        "Checkmark",
        "DropdownArrow",
        "InputFieldBackground",
        "Knob",
        "UIMask",
        "UISprite"
    };

    public string ReadTxtFile(string _filePath)
    {
        string result;
        using (FileStream fs = new FileStream(_filePath, FileMode.Open))
        {
            byte[] bytes = new byte[fs.Length];
            int r = fs.Read(bytes, 0, bytes.Length);
            result = Encoding.UTF8.GetString(bytes, 0, r);
        }
        return result.Replace("\r\n", "\n");
    }

    string FindOneSprite(string spriteName)
    {
        var tGuids = AssetDatabase.FindAssets("t:Sprite " + spriteName, new string[] { "Assets/Res/Atlas" });
        var tag = string.Empty;
        foreach (var guid in tGuids)
        {
            var meta = AssetDatabase.GUIDToAssetPath(guid) + ".meta";
            var metaTxt = ReadTxtFile(meta);
            var tLines = metaTxt.Split(new string[] { "\n" }, StringSplitOptions.None);
            for (int i = 0; i < tLines.Length; i++)
            {
                if (tLines[i].Contains("spritePackingTag"))
                {
                    tag = tLines[i].Split(':')[1];
                    return tag;
                }
            }
        }
        return tag;
    }

    void OnGUI()
    {
        GUILayout.Label("理论上一个Prefab有两个图集的图片,一个Common图集,一个业务图集.\n注:要处理的Image的sprite必挂我们的图集图片(Unity内置提供的图片不能使用)", GUILayout.Height(35));
        mTargetRoot = (GameObject)EditorGUILayout.ObjectField("Prefab", mTargetRoot, typeof(UnityEngine.Object), false);
        if (GUILayout.Button("检测", GUILayout.Height(30)))
        {
            if (mTargetRoot == null) { Debug.Log("prefab为null"); return; }
            var tImages = mTargetRoot.GetComponentsInChildren<Image>(true);
            mDicTags.Clear();
            for (int i = 0; i < tImages.Length; i++)
            {
                var tSprite = tImages[i].sprite;
                if (tSprite == null)
                {
                    Debug.LogError("1.无图片的=" + tImages[i].gameObject.name);
                }
                else
                {
                    if (mUintyLib.Contains(tSprite.name))
                    {
                        Debug.LogError("2.使用了Unity内置提供的图片,名字=" + tImages[i].gameObject.name);
                    }
                    else
                    {
                        var tTag = FindOneSprite(tSprite.name);
                        if (string.IsNullOrEmpty(tTag))
                        {
                            Debug.LogError("3.无Tag的图片,名字=" + tImages[i].gameObject.name);
                        }
                        else
                        {
                            if (mDicTags.ContainsKey(tTag) == false)
                            {
                                mDicTags[tTag] = new List<string>() { tImages[i].gameObject.name + ":" + tSprite.name + ";" };
                            }
                            else
                            {
                                var tLists = mDicTags[tTag];
                                tLists.Add(tImages[i].gameObject.name + ":" + tSprite.name + ";");
                                mDicTags[tTag] = tLists;
                            }
                        }
                    }
                }
            }
            var Keys = mDicTags.Keys.ToList();
            for (int i = 0; i < Keys.Count; i++)
            {
                Debug.LogWarning("图集名=" + Keys[i]);
            }
            Debug.Log("共使用 有名字的Tag 数量为" + mDicTags.Count);
        }

        if (GUILayout.Button("输出 各Tag下的 Image", GUILayout.Height(30)))
        {
            Debug.LogError("分隔线");
            foreach (var item in mDicTags)
            {
                Debug.LogWarning("如下:Tag=" + item.Key);
                var tLists = item.Value;
                for (int i = 0; i < tLists.Count; i++)
                {
                    Debug.Log("组件名:图片名" + tLists[i]);
                }
            }
        }


        GUILayout.Space(60);
        GUILayout.Label("UINavigationBar全是Unity内置的图片,要一口气换成工程的图片");
        mTargetSprite = (Sprite)EditorGUILayout.ObjectField("图片Sprite", mTargetSprite, typeof(UnityEngine.Object), false);
        if (GUILayout.Button("勿点", GUILayout.Height(30)))
        {
            //Debug.Log("注释了");
            //return;
            var tAllPrefab = GetPrefabs(null);
            var tNewSaves = new List<GameObject>();
            for (int j = 0; j < tAllPrefab.Count; j++)
            {
                //if (tAllPrefab[j].name.Contains("Club") == false)
                //{
                var tImages = tAllPrefab[j].GetComponentsInChildren<Image>(true);
                for (int i = 0; i < tImages.Length; i++)
                {
                    //  if (tImages[i].gameObject.name == "segment_btn" )//&& tImages[i].sprite!=null&&tImages[i].sprite.name=="UIMask")
                    if (tImages[i].sprite != null && tImages[i].sprite.name == "Background")
                    // if (tImages[i].sprite == null )
                    {
                        tImages[i].sprite = mTargetSprite;
                        //  tImages[i].color = new Color32(26, 26, 28, 255);
                        if (tNewSaves.Contains(tAllPrefab[j]) == false)
                            tNewSaves.Add(tAllPrefab[j]);
                    }
                }
                //}
            }
            SpriteSaveNewPrefabs(tNewSaves);
        }


        GUILayout.Space(20);
        GUILayout.Label("UINavigationBar Button_back下的图片,大小");
        if (GUILayout.Button("Button_back nav_back", GUILayout.Height(30)))
        {
            var tAllPrefab = GetPrefabs(null);
            var tNewSaves = new List<GameObject>();
            for (int j = 0; j < tAllPrefab.Count; j++)
            {
                var tImages = tAllPrefab[j].GetComponentsInChildren<Image>(true);
                for (int i = 0; i < tImages.Length; i++)
                {
                    if (tImages[i].sprite != null && tImages[i].sprite.name == "images_line" && tImages[i].gameObject.name == "DownLine")
                    {
                        Debug.LogWarning(tAllPrefab[j].gameObject.name + "   " + tImages[i].gameObject.name);
                        if (tNewSaves.Contains(tAllPrefab[j]) == false)
                            tNewSaves.Add(tAllPrefab[j]);
                    }
                }
            }
            Debug.LogError(tNewSaves.Count);
            SpriteSaveNewPrefabs(tNewSaves);
        }


        GUILayout.Space(20);
        GUILayout.Label("prefab不使用大图集,使用碎图");
        mTargetSprite_BigChangeSmall = (Sprite)EditorGUILayout.ObjectField("小碎图位置", mTargetSprite_BigChangeSmall, typeof(UnityEngine.Object), false);
        if (GUILayout.Button("替换", GUILayout.Height(30)))
        {
            var tAllPrefab = GetPrefabs(null);
            var tNewSaves = new List<GameObject>();
            for (int j = 0; j < tAllPrefab.Count; j++)
            {    
                var tImages = tAllPrefab[j].GetComponentsInChildren<Image>(true);
                for (int i = 0; i < tImages.Length; i++)
                {                 
                    if (tImages[i].sprite != null && tImages[i].sprite.name == mTargetSprite_BigChangeSmall.name)              
                    {
                        tImages[i].sprite = mTargetSprite_BigChangeSmall;                      
                        if (tNewSaves.Contains(tAllPrefab[j]) == false)
                            tNewSaves.Add(tAllPrefab[j]);
                    }
                }            
            }
            SpriteSaveNewPrefabs(tNewSaves);
        }
    }


    //UIClub_RoomCreat
    /// <summary> 也就是Inspector面板 的 Apply按钮 </summary>
    public void SpriteSaveNewPrefabs(List<GameObject> pWillChanges)
    {
        if (pWillChanges == null || pWillChanges.Count == 0) return;
        for (int i = 0; i < pWillChanges.Count; i++)
        {
            Debug.Log(pWillChanges[i].name);
            var tPrefab = pWillChanges[i];
            var tOldHierarchyPrefab = PrefabUtility.InstantiatePrefab(tPrefab) as GameObject;
            var tPath = AssetDatabase.GetAssetPath(tPrefab);
            var tNewEmpty = PrefabUtility.CreateEmptyPrefab(tPath);
            var gameNew = PrefabUtility.ReplacePrefab(tOldHierarchyPrefab, tNewEmpty, ReplacePrefabOptions.ConnectToPrefab);

            //var tImages = gameNew.GetComponentsInChildren<Image>(true);
            //for (int j = 0; j < tImages.Length; j++)
            //{
            //    if (tImages[j].sprite != null && tImages[j].sprite.name == "images_line" && tImages[j].gameObject.name == "DownLine"&&tImages[j].transform.parent.name== "UINavigationBar")
            //    {
            //        DestroyImmediate(tImages[j].gameObject);
            //    }
            //}
            //GameObject.DestroyImmediate(tOldHierarchyPrefab);

            var tPathAB = AssetDatabase.GetAssetPath(gameNew);
            var tAssetAB = AssetImporter.GetAtPath(tPathAB);
            tAssetAB.assetBundleName = gameNew.name + ".unity3d";
            tAssetAB.SaveAndReimport();
        }
        AssetDatabase.Refresh();
    }
}