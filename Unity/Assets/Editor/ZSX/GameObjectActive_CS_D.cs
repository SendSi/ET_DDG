using UnityEngine;
using UnityEditor;
using System.Collections;

public class GameObjectActive_CS_D : ScriptableObject
{

    //根据当前有没有选中物体来判断可否用快捷键
    [MenuItem("Helps/选中某物 Active(cs_d) %#d", true)]
    public static bool ValidateSelectEnableDisable()
    {
        GameObject[] go = GetSelectedGameObjects() as GameObject[];

        if (go == null || go.Length == 0)
            return false;
        return true;
    }

    [MenuItem("Helps/选中某物 Active(cs_d) %#d")]
    static void SeletEnable()
    {
        bool enable = false;
        GameObject[] gos = GetSelectedGameObjects() as GameObject[];

        foreach (GameObject go in gos)
        {
            enable = !go.activeInHierarchy;
            EnableGameObject(go, enable);
        }
    }

    //获得选中的物体
    static GameObject[] GetSelectedGameObjects()
    {
        return Selection.gameObjects;
    }

    //激活或关闭当前选中物体
    public static void EnableGameObject(GameObject parent, bool enable)
    {
        parent.gameObject.SetActive(enable);
    }

}