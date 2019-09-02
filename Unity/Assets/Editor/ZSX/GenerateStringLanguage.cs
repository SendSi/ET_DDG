//using ETModel;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using UnityEditor;
//using UnityEngine;
//using UnityEngine.UI;



//public class GenerateStringLanguage : GetAssetGOWindow
//{
//    private GameObject mTargetRoot;
//    private Dictionary<string, string> mDicName = new Dictionary<string, string>();
//    private string mForntName = "tc_:8";
//    private bool mIsOverFlowHorizontal;
    
//    void OnGUI()
//    {
//        GUILayout.Space(5);
//        EditorGUILayout.LabelField("多语言流程:1.给要加的所有Text加上LanguageText.cs脚本\n2.把Prefab拖入视图中去\n3.点击生成列表\n4.随机生成下名字,用':'分隔开\n5.copy到USER_ZH.txt去\n6.选中刚才的Prefab右键(标记AB包)", GUILayout.Height(88));

//        mTargetRoot = (GameObject)EditorGUILayout.ObjectField("Prefab", mTargetRoot, typeof(UnityEngine.Object), false);
//        mIsOverFlowHorizontal = EditorGUILayout.Toggle("水平Overflow", mIsOverFlowHorizontal);

//        if (GUILayout.Button("生成含(LanguageText.cs)的列表", GUILayout.Height(30)))
//        {
//            mDicName.Clear();
//            if (mTargetRoot == null) { Debug.Log("prefab为null"); return; }
//            var tLanguageTexts = mTargetRoot.GetComponentsInChildren<LanguageText>(true);
//            for (int i = 0; i < tLanguageTexts.Length; i++)
//            {
//                var value = tLanguageTexts[i].mKey;
//                mDicName[tLanguageTexts[i].GetComponent<Text>().text] = value;
//            }
//            if (mDicName.Count == 0)
//                Debug.Log("并无 LanguageText.cs在" + mTargetRoot.name + "上");
//            mForntName = mTargetRoot.name + ":8";
//        }

//        var keys = mDicName.Keys.ToList();
//        var values = mDicName.Values.ToList();
//        for (int i = 0; i < keys.Count; i++)
//        {
//            mDicName[keys[i]] = EditorGUILayout.TextField(keys[i], values[i]);
//        }
//        EditorGUILayout.BeginHorizontal();
//        {
//            mForntName = EditorGUILayout.TextField("随机名(前缀:数量)", mForntName);
//            if (GUILayout.Button("-随机生成下名字-", GUILayout.Height(17)))
//            {
//                var tLength = mForntName.Split(':');
//                if (tLength == null || tLength.Length != 2) return;
//                for (int i = 0; i < keys.Count; i++)
//                {
//                    mDicName[keys[i]] = GetRandomString(tLength[0], int.Parse(tLength[1]));
//                }
//            }
//        }
//        EditorGUILayout.EndHorizontal();

//        if (GUILayout.Button("生成下", GUILayout.Height(30)))
//        {
//            if (mTargetRoot == null) return;
//            var tLanguageTexts = mTargetRoot.GetComponentsInChildren<LanguageText>(true);
//            for (int i = 0; i < tLanguageTexts.Length; i++)
//            {
//                tLanguageTexts[i].mKey = mDicName[tLanguageTexts[i].GetComponent<Text>().text];

//                tLanguageTexts[i].GetComponent<Text>().horizontalOverflow = mIsOverFlowHorizontal ? HorizontalWrapMode.Overflow : HorizontalWrapMode.Wrap;
//            }
//            var tBuild = GetCopyKeyValue(mTargetRoot);

//            SaveNewPrefabs(new List<GameObject>() { mTargetRoot });
//            if (string.IsNullOrEmpty(tBuild.ToString())) return;
//            GUIUtility.systemCopyBuffer = tBuild.ToString();

//            mDicName = new Dictionary<string, string>();
//            mTargetRoot = null;
//            AssetDatabase.Refresh();
//        }
//    }

//    public string GetRandomString(string a, int length)
//    {
//        string str = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
//        StringBuilder sb = new StringBuilder();
//        for (int i = 0; i < length; i++)
//        {
//            int number = UnityEngine.Random.Range(0, 62);
//            sb.Append(str.Substring(number, 1));
//        }
//        return a + sb.ToString();
//    }

//    public StringBuilder GetCopyKeyValue(GameObject tGo)
//    {
//        StringBuilder sb = new StringBuilder();
//        if (tGo != null)
//        {
//            var tLanguageTexts = tGo.GetComponentsInChildren<LanguageText>(true);
//            for (int i = 0; i < tLanguageTexts.Length; i++)
//            {
//                var key = tLanguageTexts[i].mKey;
//                if (string.IsNullOrEmpty(key)) continue;
//                var zhValue = tLanguageTexts[i].GetComponent<Text>().text;
//                var lineStr = key + "\t" + zhValue;
//                sb.AppendLine(lineStr);
//            }
//            Debug.Log(sb.ToString());
//        }
//        else
//        {
//            Debug.Log("未搜索到有");
//        }
//        return sb;
//    }
//}