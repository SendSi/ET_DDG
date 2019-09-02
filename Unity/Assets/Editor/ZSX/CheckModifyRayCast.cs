using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CheckModifyRayCast : GetAssetGOWindow
{
    public GameObject mTargetRoot;
    Dictionary<string, Text> mDicTexts;
    Dictionary<string, Image> mDicImages;
    List<bool> mBools;
    int mCountHeight = 5;
    Vector2 v2;
    private void OnGUI()
    {
        mTargetRoot = (GameObject)EditorGUILayout.ObjectField("从Project视图拖Prefab进来", mTargetRoot, typeof(Object), false);
        if (GUILayout.Button("显示Image与Text的RayCastTarget=true(下面显示出来都为true的),\n含Button,btn的也不显示出来,ScrollRect下的Viewport,,input", GUILayout.Height(38)))
        {
            if (mTargetRoot == null) return;
            ShowChildrens();
        }
        v2 = EditorGUILayout.BeginScrollView(v2, GUILayout.Height(mCountHeight > 500 ? 500 : mCountHeight));
        if (mTargetRoot != null && mDicTexts != null && mDicImages != null)
        {
            var keyTxts = mDicTexts.Keys.ToList();
            for (int i = 0; i < keyTxts.Count; i++)
            {
                mBools[i] = EditorGUILayout.Toggle(keyTxts[i], mBools[i]);
            }
            var keyImgs = mDicImages.Keys.ToList();
            for (int i = 0; i < keyImgs.Count; i++)
            {
                mBools[keyTxts.Count + i] = EditorGUILayout.Toggle(keyImgs[i], mBools[keyTxts.Count + i]);
            }
        }
        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("修改Image与Text(组件)的RayCastTarget为false或true", GUILayout.Height(30)))
        {
            mCountHeight = 5;
            if (mTargetRoot == null) return;
            var tValuTxts = mDicTexts.Values.ToList();
            for (int i = 0; i < tValuTxts.Count; i++)
            {
                tValuTxts[i].raycastTarget = mBools[i];
            }

            var tValueImgs = mDicImages.Values.ToList();
            for (int i = 0; i < tValueImgs.Count; i++)
            {
                tValueImgs[i].raycastTarget = mBools[tValuTxts.Count + i];
            }
            SaveNewPrefabs(new List<GameObject>() { mTargetRoot });
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();        
            mTargetRoot = null;
        }

        GUILayout.Space(5);
        if (GUILayout.Button("唉,不管了太多了,把Image与Text(组件)的RayCastTarget全改为false,,再自己手动改回来要点击的", GUILayout.Height(30)))
        {
            mCountHeight = 5;
            if (mTargetRoot == null) return;
            var tTxts = mTargetRoot.GetComponentsInChildren<Text>(true);
            var tImgs = mTargetRoot.GetComponentsInChildren<Image>(true);
            for (int i = 0; i < tTxts.Length; i++)
            {
                tTxts[i].raycastTarget = false;
            }
            for (int i = 0; i < tImgs.Length; i++)
            {
                tImgs[i].raycastTarget = false;
            }
            SaveNewPrefabs(new List<GameObject>() { mTargetRoot });
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();            
            mTargetRoot = null;
        }
    }

    void ShowChildrens()
    {
        var tTxts = mTargetRoot.GetComponentsInChildren<Text>(true);
        mDicTexts = new Dictionary<string, Text>();
        mBools = new List<bool>();
        for (int i = 0; i < tTxts.Length; i++)
        {
            if (tTxts[i].raycastTarget)
            {
                if (tTxts[i].gameObject.name.Contains("Button"))
                {
                    //Debug.Log("tTxts[i].gameObject.name");
                }
                else
                {
                    mDicTexts[tTxts[i].gameObject.name + "_" + i] = tTxts[i];
                    mBools.Add(false);
                }
            }
        }
        var tImgs = mTargetRoot.GetComponentsInChildren<Image>(true);
        mDicImages = new Dictionary<string, Image>();
        for (int i = 0; i < tImgs.Length; i++)
        {
            if (tImgs[i].raycastTarget)
            {
                if (tImgs[i].gameObject.name.Contains("Button")||
                    tImgs[i].GetComponent<ReferenceCollector>()!=null||
                    tImgs[i].gameObject.name.Contains("btn") ||
                    tImgs[i].gameObject.name.Contains("Btn") ||
                    tImgs[i].gameObject.name.Contains("Background") ||
                    (tImgs[i].transform.parent!=null&&tImgs[i].transform.parent.GetComponent<ScrollRect>()!=null)||
                    tImgs[i].GetComponent<InputField>() != null)
                {
                   // Debug.Log("tTxts[i].gameObject.name");
                }
                else
                {
                    mDicImages[tImgs[i].gameObject.name + "_" + i] = tImgs[i];
                    mBools.Add(false);
                }
            }
        }
        mCountHeight = 18 * mBools.Count;
    }

}
