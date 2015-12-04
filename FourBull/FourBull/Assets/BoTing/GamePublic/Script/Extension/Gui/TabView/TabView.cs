using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using BoTing.Framework;

namespace BoTing.GamePublic
{
    //即可扩展到公共控件
    public class TabView : MonoBehaviour
    {
        /// <summary>
        /// 挂载tab的根节点
        /// </summary>
        public Transform rootNode;


        /// <summary>
        /// 切换用按钮的引用,会动态在此处绑定事件
        /// </summary>
        public List<Button> tabButtons;


        /// <summary>
        /// 每个tabButton对应的界面名称.
        /// 或者说prefab路径?
        /// </summary>
        public List<string> viewNames;


        ///每个view的根节点
        public List<Transform> viewTransforms;


        /// <summary>
        /// 当前界面名称
        /// </summary>
        public int currenViewIndex = 0;



        /// <summary>
        /// 传入prefab路径，创建gameobject
        /// 重写此接口
        /// </summary>
        public virtual GameObject CreateTab(string viewName, Transform node)
        {
            GameObject vobj = Instantiate(Resources.Load(viewName, typeof(GameObject))) as GameObject;
            return vobj;
        }


        /// <summary>
        /// 界面的初始化函数，若非拖动绑定tabberbuttons，可以在此处动
        /// 获取
        /// </summary>
        public virtual void InitialView()
        {
            int icounter = tabButtons.Count;
            for (int i = 0; i < icounter; i++)
            {
                Button go = tabButtons[i];
                go.onClick.RemoveAllListeners();
                go.onClick.AddListener(delegate()
                {
                    SwitchView(go);
                });


                Transform ts = viewTransforms[i];
                if (currenViewIndex == i)
                {
                    if (ts == null)
                    {
                        GameObject vobj = CreateTab(viewNames[currenViewIndex], rootNode);
                        viewTransforms[i] = vobj.transform;
                    }
                    else
                    {
                        ts.gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (ts != null)
                    {
                        ts.gameObject.SetActive(false);
                    }
                }
            }

            //check panel
            if (tabButtons.Count > currenViewIndex)
            {
                SwitchView(tabButtons[currenViewIndex]);
            }
        }


        /// <summary>
        /// 切换界面时候操作
        /// </summary>
        public void SwitchView(Button go)
        {
            int index = -1;
            for (int i = 0; i < tabButtons.Count; i++)
            {
                if (go == tabButtons[i])
                {
                    index = i;
                    break;
                }
            }
            DebugKit.Log("switchView,before:" + currenViewIndex + "now:" + index);

            if (index < 0 || currenViewIndex == index)
                return;


            //当前的界面隐藏掉
            Transform lastView = viewTransforms[currenViewIndex];
            if (lastView != null)
            {
                lastView.gameObject.SetActive(false);
            }
     

            //当前的界面被取消调用的函数
            OnCanceled(currenViewIndex);


            //索引指向新的界面
            currenViewIndex = index;
            Transform newView = viewTransforms[currenViewIndex];
            if (newView == null)
            {
                GameObject newObj = CreateTab(viewNames[currenViewIndex], rootNode);
                viewTransforms[currenViewIndex] = newObj.transform;
            }
            else
            {
                newView.gameObject.SetActive(true);
            }

            //新的界面被选中
            OnSelected(currenViewIndex);
        }

        //通过路径加载
        //直接绑定
        public void AddTab(Button sender, Transform ts)
        {
            if (sender == null || ts == null)
            {
                DebugKit.Log("TabView AddTab error!button or transform is null!");
                return;
            }

            int index = tabButtons.Count;
            tabButtons[index] = sender;
            viewTransforms[index] = ts;
            viewNames[index] = ts.gameObject.name;
            ts.gameObject.SetActive(false);
        }


        /// <summary>
        /// 通过名称添加新的页签
        /// </summary>
        public void AddTab(Button sender, string name)
        {
            if (sender == null || string.IsNullOrEmpty(name))
            {
                DebugKit.Log("TabView AddTab error!button or transform is null!");
                return;
            }

            int index = tabButtons.Count;
            tabButtons[index] = sender;
            viewTransforms[index] = null;
            viewNames[index] = name;
        }

        /// <summary>
        /// 删除页签，一般是点击按钮删除，因此只需要传入按钮
        /// </summary>
        public virtual void OnCloseTab(Button sender)
        {
            int ifind = -1;
            for (int i = 0; i < tabButtons.Count; i++)
            {
                if (tabButtons[i] == sender)
                {
                    ifind = i;
                    break;
                }
            }

            if (ifind >= 0)
            {
                //删除buttons
                GameObject.Destroy(tabButtons[ifind]);

                //删除界面
                GameObject.Destroy(viewTransforms[ifind]);

                //从list中移除
                tabButtons.RemoveAt(ifind);
                viewTransforms.RemoveAt(ifind);
                viewNames.RemoveAt(ifind);
            }
        }

        /// <summary>
        /// 当点击到新的页签，此函数被调用
        /// </summary>
        /// <param name="index">当前点下页面序号</param>
        public virtual void OnSelected(int index)
        {

        }

        /// <summary>
        /// 当老的界面即将被替换时调用
        /// </summary>
        /// <param name="index">即将被替换的也签序号</param>
        public virtual void OnCanceled(int index)
        {

        }


        void Start()
        {
            InitialView();
        }
    }

}
