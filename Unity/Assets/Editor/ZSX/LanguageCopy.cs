//using ETModel;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using UnityEditor;
//using UnityEngine;
//using UnityEngine.UI;
////using BestHTTP;
//using System;

//public class LanguageCopy : GetAssetGOWindow
//{
//    public StringBuilder GetCopyKeyValue(string pGoStr)
//    {
//        StringBuilder sb = new StringBuilder();
//        var tGo = FindOneGo("prefab", pGoStr);
//        if (tGo != null)
//        {
//            var tLanguageTexts = tGo.GetComponentsInChildren<LanguageText>();
//            for (int i = 0; i < tLanguageTexts.Length; i++)
//            {
//                var key = tLanguageTexts[i].mKey;
//                if (string.IsNullOrEmpty(key)) continue;
//                var zhValue = tLanguageTexts[i].GetComponent<Text>().text;
//                var lineStr = key + "=" + zhValue;
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
//    public void GetCopyKeyValue_EN(string pGoStr)
//    {
//        StringBuilder sb = new StringBuilder();
//        var tGo = FindOneGo("prefab", pGoStr);
//        if (tGo != null)
//        {
//            var tLanguageTexts = tGo.GetComponentsInChildren<LanguageText>();
//            int successCount = 0;
//            for (int i = 0; i < tLanguageTexts.Length; i++)
//            {
//                var key = tLanguageTexts[i].mKey;
//                if (string.IsNullOrEmpty(key)) continue;
//                var zhValue = tLanguageTexts[i].GetComponent<Text>().text;

//                string uri = $"http://fanyi.baidu.com/transapi?from=auto&to=en&query={zhValue}";
//                Debug.Log($"{uri}");
//                var httpRequest = new HTTPRequest(new Uri(uri), HTTPMethods.Get, (originalRequest, response) =>
//                {
//                    Debug.Log($"{originalRequest.State}{response.IsSuccess}");
//                    if (originalRequest.State == HTTPRequestStates.Finished && response.IsSuccess)
//                    {
//                        string resultStr = response.DataAsText;
//                        var result = JsonHelper.FromJson<GoogleTranResult>(resultStr);
//                        var enValue = result.data[0].dst;
//                        var lineStr = key + "=" + enValue;
//                        sb.AppendLine(lineStr);
//                    }

//                    successCount++;
//                    if (successCount == tLanguageTexts.Length)
//                    {
//                        var filePath = Application.dataPath + "/Res/Config/USER_EN.txt";
//                        WriteTxtFile(filePath, sb.ToString()); ReadTxtFile(filePath);
//                        Debug.Log("翻译英文成功");
//                        Debug.Log(sb.ToString());
//                    }
//                });
//                httpRequest.Send();
//            }

//        }
//        else
//        {
//            Debug.Log("未搜索到有");
//        }
//    }

//    public async void GetCopyKeyValue_TW(string pGoStr)
//    {
//        StringBuilder sb = new StringBuilder();
//        var tGo = FindOneGo("prefab", pGoStr);
//        if (tGo != null)
//        {
//            var tLanguageTexts = tGo.GetComponentsInChildren<LanguageText>();
//            int successCount = 0;
//            for (int i = 0; i < tLanguageTexts.Length; i++)
//            {
//                var key = tLanguageTexts[i].mKey;
//                if (string.IsNullOrEmpty(key)) continue;
//                var zhValue = tLanguageTexts[i].GetComponent<Text>().text;

//                string uri = $"http://fanyi.baidu.com/transapi?from=auto&to=cht&query={zhValue}";
//                Debug.Log($"{uri}");
//                var httpRequest = new HTTPRequest(new Uri(uri), HTTPMethods.Get, (originalRequest, response) =>
//                {
//                    Debug.Log($"{originalRequest.State}{response.IsSuccess}");
//                    if (originalRequest.State == HTTPRequestStates.Finished && response.IsSuccess)
//                    {
//                        string resultStr = response.DataAsText;
//                        var result = JsonHelper.FromJson<GoogleTranResult>(resultStr);
//                        var enValue = result.data[0].dst;
//                        var lineStr = key + "=" + enValue;
//                        sb.AppendLine(lineStr);
//                    }

//                    successCount++;
//                    if (successCount == tLanguageTexts.Length)
//                    {
//                        var filePath = Application.dataPath + "/Res/Config/USER_TW.txt";
//                        WriteTxtFile(filePath, sb.ToString()); ReadTxtFile(filePath);
//                        Debug.Log("翻译繁体成功");
//                        Debug.Log(sb.ToString());
//                    }
//                });
//                httpRequest.Send();
//            }

//        }
//        else
//        {
//            Debug.Log("未搜索到有");
//        }
//    }

//    public sealed class GoogleTranResult
//    {
//        public List<GoogleTranDetail> data { get; set; }
//    }

//    public sealed class GoogleTranDetail
//    {
//        public string dst { get; set; }
//    }

//    private string mPrefabStr;
//    Dictionary<string, string> mDic = new Dictionary<string, string>();
//    GameObject mGOPrefab;
//    private string mForntName = "tc_:8";

//    private string manualKeyStr;
//    private string manualZHStr;

//    void OnGUI()
//    {
//        GUILayout.Space(5);
//        EditorGUILayout.LabelField("多语言流程:把一个Prefab加后缀_1,如UIMine改为UIMine_1，给要加的所有Text加上LanguageText.cs脚本,\n点击生成列表,会生成列表输入框,给其加个名(其实也就是mKey),\n然后点击生成下.会自动翻译为英文和繁体，并复制到相应文件中\n最后使用Tool/打包工具/仅仅一键设置标记", GUILayout.Height(60));

//        mPrefabStr = EditorGUILayout.TextField("prefabName", mPrefabStr);

//        if (GUILayout.Button("生成含(LanguageText.cs)的列表", GUILayout.Height(30)))
//        {
//            mDic.Clear();
//            mGOPrefab = FindOneGo("Prefab", mPrefabStr);
//            var tLanguageTexts = mGOPrefab.GetComponentsInChildren<LanguageText>(true);
//            for (int i = 0; i < tLanguageTexts.Length; i++)
//            {
//                var value = tLanguageTexts[i].mKey;
//                if (value.Length == 0)
//                {
//                    string objectName = tLanguageTexts[i].gameObject.name;
//                    if (objectName != "Text")
//                    {

//                        value = $"{mPrefabStr.Replace("_1", "")}_{objectName.Replace("Text_", "")}";
//                    }
//                    else if (tLanguageTexts[i].gameObject.transform.parent.gameObject.name != "GameObject")
//                    {
//                        string parentObjectName = tLanguageTexts[i].gameObject.transform.parent.gameObject.name;
//                        value = $"{mPrefabStr.Replace("_1", "")}_{parentObjectName.Replace("Text_", "")}";
//                    }
//                }
//                mDic[tLanguageTexts[i].GetComponent<Text>().text] = value;
//            }
//            if (mDic.Count == 0) Debug.Log("并无 LanguageText.cs在" + mPrefabStr + "上");
//        }

//        var keys = mDic.Keys.ToList();
//        var values = mDic.Values.ToList();
//        for (int i = 0; i < keys.Count; i++)
//        {
//            mDic[keys[i]] = EditorGUILayout.TextField(keys[i], values[i]);
//        }
//        EditorGUILayout.BeginHorizontal();
//        {
//            mForntName = EditorGUILayout.TextField("随机名  前缀:数量", mForntName);
//            if (GUILayout.Button("-随机生成下名字-", GUILayout.Height(16)))
//            {
//                var tLength = mForntName.Split(':');
//                if (tLength == null || tLength.Length != 2) return;
//                for (int i = 0; i < keys.Count; i++)
//                {
//                    mDic[keys[i]] = getRandomString(tLength[0], int.Parse(tLength[1]));
//                }
//            }
//        }
//        EditorGUILayout.EndHorizontal();

//        if (GUILayout.Button("-生成下-", GUILayout.Height(30)))//0给mKey建立名字\n 1生成Prefab \n 2复制字符串
//        {
//            if (mGOPrefab == null) return;
//            var tLanguageTexts = mGOPrefab.GetComponentsInChildren<LanguageText>(true);
//            for (int i = 0; i < tLanguageTexts.Length; i++)
//            {
//                tLanguageTexts[i].mKey = mDic[tLanguageTexts[i].GetComponent<Text>().text];
//            }
//            SaveNewPrefabs(new List<GameObject>() { mGOPrefab });
//            var tBuild = GetCopyKeyValue(mPrefabStr);
//            GetCopyKeyValue_EN(mPrefabStr);
//            GetCopyKeyValue_TW(mPrefabStr);
//            if (string.IsNullOrEmpty(tBuild.ToString())) return;
//            GUIUtility.systemCopyBuffer = tBuild.ToString();

//            var filePath3 = Application.dataPath + "/Res/Config/USER_ZH.txt";
//            WriteTxtFile(filePath3, tBuild.ToString()); ReadTxtFile(filePath3);

//            // Debug.Log("已复制,手动加到USER_ZH.txt");
//            mDic = new Dictionary<string, string>();
//            mGOPrefab = null;
//            AssetDatabase.Refresh();
//        }


//        GUILayout.Space(20);
//        EditorGUILayout.LabelField("Text有个HorizontalOverflow的属性,默认为Wrap,但英文版文字比较长,\n改成Overflow吧(PS:生成之后重新标记下)", GUILayout.Height(35));
//        if (GUILayout.Button("-prefabName输入框.使用Overflow模式吧-", GUILayout.Height(30)))//0给mKey建立名字\n 1生成Prefab \n 2复制字符串
//        {
//            var tGOPrefab = FindOneGo("Prefab", mPrefabStr);
//            if (tGOPrefab == null) return;
//            var tLanguageTexts = mGOPrefab.GetComponentsInChildren<LanguageText>(true);
//            for (int i = 0; i < tLanguageTexts.Length; i++)
//            {
//                tLanguageTexts[i].GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
//            }
//            SaveNewPrefabs(new List<GameObject>() { mGOPrefab });
//            AssetDatabase.Refresh();
//        }
//        if (GUILayout.Button("-使Bundles/UI目录下,所有的Text使用Overflow模式吧(Text下有组件LanguageText)-", GUILayout.Height(30)))
//        {
//            var tCountPrefabs = GetPrefabs("");
//            var tHasGos = new List<GameObject>();
//            for (int i = 0; i < tCountPrefabs.Count; i++)
//            {
//                var tHasLT = tCountPrefabs[i].GetComponentsInChildren<LanguageText>(true);
//                for (int j = 0; j < tHasLT.Length; j++)
//                {
//                    tHasLT[j].GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
//                    if (tHasGos.Contains(tCountPrefabs[i]) == false)
//                        tHasGos.Add(tCountPrefabs[i]);
//                }
//            }
//            SaveNewPrefabs(tHasGos);
//            AssetDatabase.Refresh();
//        }

//        GUILayout.Space(60);
//        if (GUILayout.Button("一个个在Text手动建立的脚本\n生成键值对,看Log输出---一般用不上这个", GUILayout.Height(45)))
//        {
//            if (string.IsNullOrEmpty(mPrefabStr)) return;
//            var tBuild = GetCopyKeyValue(mPrefabStr);
//            if (string.IsNullOrEmpty(tBuild.ToString())) return;
//            GUIUtility.systemCopyBuffer = tBuild.ToString();
//            Debug.Log("已复制,手动加到USER_ZH.txt");
//        }
//        EditorGUILayout.BeginHorizontal();
//        {
//            if (GUILayout.Button("测试一", GUILayout.Height(30)))
//            {
//                var tLanguageTexts = sss.Split('\n');
//                mSB = new StringBuilder();
//                for (int i = 0; i < tLanguageTexts.Length; i++)
//                {
//                    var keyValue = tLanguageTexts[i].Split('=');
//                    // string uri = "http://fanyi.baidu.com/transapi?from=auto&to=cht&query={" + keyValue[1] + "}";
//                    string uri = "http://fanyi.baidu.com/transapi?from=auto&to=en&query={" + keyValue[1] + "}";
//                    var httpRequest = new HTTPRequest(new Uri(uri), HTTPMethods.Get, (originalRequest, response) =>
//                    {
//                        if (originalRequest.State == HTTPRequestStates.Finished && response.IsSuccess)
//                        {
//                            string resultStr = response.DataAsText;
//                            var result = JsonHelper.FromJson<GoogleTranResult>(resultStr);
//                            var enValue = result.data[0].dst;                        //  enValue = enValue.Replace("{", "").Replace("\r}", "");
//                            enValue = enValue.Replace("{", "").Replace("}", "");
//                            var lineStr = keyValue[0] + "=" + enValue;
//                            mSB.AppendLine(lineStr);
//                        }
//                    });
//                    httpRequest.Send();
//                }
//            }
//            if (GUILayout.Button("测试二", GUILayout.Height(30)))
//            {
//                Debug.Log(mSB.ToString());
//            }
//        }
//        EditorGUILayout.EndHorizontal();

//        GUILayout.Space(60);
//        EditorGUILayout.LabelField("不在界面上的多语言在这里生成");
//        manualKeyStr = EditorGUILayout.TextField("mkey", manualKeyStr);
//        manualZHStr = EditorGUILayout.TextField("中文", manualZHStr);
//        if (GUILayout.Button("手动生成", GUILayout.Height(30)))
//        {
//            if (string.IsNullOrEmpty(manualKeyStr)) return;
//            if (string.IsNullOrEmpty(manualZHStr)) return;
//            var lineStrZH = manualKeyStr + "=" + manualZHStr;
//            var filePathZH = Application.dataPath + "/Res/Config/USER_ZH.txt";
//            WriteTxtFile(filePathZH, lineStrZH); ReadTxtFile(filePathZH);

//            string uri = $"http://fanyi.baidu.com/transapi?from=auto&to=en&query={manualZHStr}";
//            var httpRequest = new HTTPRequest(new Uri(uri), HTTPMethods.Get, (originalRequest, response) =>
//            {
//                Debug.Log($"{originalRequest.State}{response.IsSuccess}");
//                if (originalRequest.State == HTTPRequestStates.Finished && response.IsSuccess)
//                {
//                    string resultStr = response.DataAsText;
//                    var result = JsonHelper.FromJson<GoogleTranResult>(resultStr);
//                    var enValue = result.data[0].dst;
//                    var lineStr = manualKeyStr + "=" + enValue;
//                    var filePath = Application.dataPath + "/Res/Config/USER_EN.txt";
//                    WriteTxtFile(filePath, lineStr); ReadTxtFile(filePath);
//                }
//            });
//            httpRequest.Send();

//            string twuri = $"http://fanyi.baidu.com/transapi?from=auto&to=cht&query={manualZHStr}";
//            var twhttpRequest = new HTTPRequest(new Uri(twuri), HTTPMethods.Get, (originalRequest, response) =>
//            {
//                Debug.Log($"{originalRequest.State}{response.IsSuccess}");
//                if (originalRequest.State == HTTPRequestStates.Finished && response.IsSuccess)
//                {
//                    string twresultStr = response.DataAsText;
//                    var twresult = JsonHelper.FromJson<GoogleTranResult>(twresultStr);
//                    var twenValue = twresult.data[0].dst;
//                    var twlineStr = manualKeyStr + "=" + twenValue;
//                    var twfilePath = Application.dataPath + "/Res/Config/USER_TW.txt";
//                    WriteTxtFile(twfilePath, twlineStr); ReadTxtFile(twfilePath);
//                }
//            });
//            twhttpRequest.Send();
//            GUIUtility.systemCopyBuffer = $"LanguageManager.Get(\"{manualKeyStr}\")";
//            Debug.Log($"已复制代码:LanguageManager.Get(\\\"{manualKeyStr}\\\")");
//        }

//    }
//    StringBuilder mSB;
//    private string sss = @"";




//   public string ReadTxtFile(string _filePath)
//    {
//        string result;
//        //创建一个FileStream对象。
//        using (FileStream fs = new FileStream(_filePath, FileMode.Open))
//        {
//            //声明一个字节数组，其长度等于读取到的文件的长度。s
//            byte[] bytes = new byte[fs.Length];
//            //读取txt文件中的内容。r代表实际读取到的有效字节数。
//            int r = fs.Read(bytes, 0, bytes.Length);
//            //将读取到的文件转换为字符串后赋值给result。
//            result = Encoding.UTF8.GetString(bytes, 0, r);
//        }
//        return result;
//    }

//    void WriteTxtFile(string filePath, string sb)
//    {
//        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Write))
//        {
//            //将要追加的字符串转换成字节数组。
//            byte[] byteBuffer = Encoding.UTF8.GetBytes(sb);
//            //设置当前流的位置(如果不设置下面的Position属性，执行Write方法的时候是从前往后覆盖)。
//            fs.Position = fs.Length;
//            //写入文件。
//            fs.Write(byteBuffer, 0, byteBuffer.Length);
//        }
//    }

//    public string getRandomString(string a, int length)
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


//}

