using ETModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GenerateScriptCode : GetAssetGOWindow
{
    //private string frontPath;
    //private string behindPath;
    void OnGUI()
    {
        //if (GUILayout.Button("点击下", GUILayout.Height(30)))
        //{
        //    //frontPath = Application.dataPath + "/Hotfix/UI/UI /Component/";
        //}
        //if (string.IsNullOrEmpty(frontPath) == false)
        //{
        //    GUILayout.Label("路径=" + frontPath);
        //    frontPath = EditorGUILayout.TextField("路径:", frontPath);
        //    behindPath = EditorGUILayout.TextField("名字:", behindPath);
        //    GUILayout.Space(5);
        //    if (GUILayout.Button("生成", GUILayout.Height(30)))
        //    {
        //        Debug.Log(frontPath + behindPath);
        //        FileStream fs = new FileStream(frontPath + behindPath + "Component.cs", FileMode.CreateNew);
        //        FileInfo fInfo = new FileInfo(frontPath + behindPath + "Component.cs");
        //        StreamWriter sw = new StreamWriter(fs);
        //        var newCode = code.Replace("UIMine_WalletFlow", behindPath);
        //        sw.Write(newCode);
        //        sw.Flush();
        //        sw.Close();
        //        sw.Dispose();
        //        AssetDatabase.Refresh();
        //    }
        //}
    }

//    private string code = @"using System;
//using System.Collections.Generic;
//using ETModel;
//using UnityEngine;
//using UnityEngine.UI;


//namespace ETHotfix
//{
//    [ObjectSystem]
//    public class UIMine_WalletFlowComponentSystem : AwakeSystem<UIMine_WalletFlowComponent>
//    {
//        public override void Awake(UIMine_WalletFlowComponent self)
//        {
//            self.Awake();
//        }
//    }

//    [ObjectSystem]
//    public class UIMine_WalletFlowComponentUpdateSystem : UpdateSystem<UIMine_WalletFlowComponent>
//    {
//        public override void Update(UIMine_WalletFlowComponent self)
//        {
//            self.Update();
//        }
//    }

//    /// <summary> 页面名: </summary>
//    public class UIMine_WalletFlowComponent : UIBaseComponent
//    {
//        private ReferenceCollector rc;
//        public void Awake()
//        {
//            InitUI();
//        }

//        public void Update()
//        {

//        }

//        public override void OnShow(object obj)
//        {         
//            SetUpNav(""名字"", UIType.UIMine_WalletFlow);
//        }

//        public override void OnHide()
//        {
//        }

//        public override void Dispose()
//        {
//            base.Dispose();
//        }       
//        protected virtual void InitUI()
//        {
//            rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();           
//        }  
//    }
//}

// #region 
//    [UIFactory(UIType.UIMine_WalletFlow)]
//    public class UIMine_WalletFlowFactory : IUIFactoryExtend
//    {
//        public UI Create(Scene scene, string type, GameObject gameObject)
//        {
//            try
//            {
//                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
//                resourcesComponent.LoadBundle($""{type}.unity3d"");
//                GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset($""{type}.unity3d"", $""{type}"");
//                GameObject club = UnityEngine.Object.Instantiate(bundleGameObject);
//                club.layer = LayerMask.NameToLayer(LayerNames.UI);
//                UI ui = ComponentFactory.Create<UI, GameObject>(club);
//                ui.AddUIBaseComponent<UIMine_WalletFlowComponent>();
//                AddSubComponent(ui);
//                return ui;
//            }
//            catch (Exception e)
//            {
//                Log.Error(e);
//                return null;
//            }
//        }

//        public async Task<UI> CreateAsync(Scene scene, string type, GameObject parent)
//        {
//            try
//            {
//                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
//                await resourcesComponent.LoadBundleAsync($""{type}.unity3d"");
//                GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset($""{type}.unity3d"", $""{type}"");
//                GameObject club = UnityEngine.Object.Instantiate(bundleGameObject);
//                club.layer = LayerMask.NameToLayer(LayerNames.UI);
//                UI ui = ComponentFactory.Create<UI, GameObject>(club);
//                ui.AddUIBaseComponent<UIMine_WalletFlowComponent>();
//                AddSubComponent(ui);
//                return ui;
//            }
//            catch (Exception e)
//            {
//                Log.Error(e);
//                return null;
//            }
//        }

//        public void Remove(string type)
//        {
//            RemoveSubComponents();
//            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle($""{type}.unity3d"");
//        }
//        public void RemoveSubComponents()
//        {
//            UIComponent uiComponent = Game.Scene.GetComponent<UIComponent>();
//            for (int i = 0, n = subs.Count; i < n; i++)
//            {
//                uiComponent.RemoveSub(subs[i]);
//            }
//        }

//        private List<string> subs = new List<string>();
//        public void AddSubComponent(UI ui)
//        {
//            if (subs == null)
//                subs = new List<string>();
//            else
//                subs.Clear();
//        }
//    }
//    #endregion
//";

}
