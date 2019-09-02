using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LoginEditPlay : GetAssetGOWindow
{
    int mTimeCalcAdd = 0;
    bool mWorking = false;
    string mTimeLabel = "4";

    void OnGUI()
    {
        mTimeLabel = EditorGUILayout.TextField("开始时间:", mTimeLabel);
        GUILayout.Label("若点击了按钮,不要关闭此页面哦__" + GetNowTime());
        if (GUILayout.Button("运行或停止", GUILayout.Height(30)))
        {
            mTimeCalcAdd = 0;
            mWorking = !mWorking;     
        }

        if (mWorking)
        {
            var t = int.Parse(mTimeLabel);
            mTimeCalcAdd++;

            if (GetNowTime() % t == 0)
            {
                EditorApplication.ExecuteMenuItem("Edit/Play");
            }
            Repaint();
        }
    }
        long GetNowTime()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return ((long)ts.TotalSeconds);
        }
    }
