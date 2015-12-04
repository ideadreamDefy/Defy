using UnityEngine;
using System.Collections;

using BoTing.Framework;

namespace BoTing.GamePublic
{
    public class UIComponentBase : ViewBase
    {
        void Awake()
        {
            OnAwakeView();
        }
        protected virtual void OnAwakeView()
        {
            InitView();
        }

        public override void InitView()
        {

        }

        void OnEnable()
        {
            OnEnableView();
        }
        protected virtual void OnEnableView()
        {
        }

        void Start()
        {
            OnStartView();
        }
        protected virtual void OnStartView()
        {
        }

        // 这些频繁调用、影响性能、理论上不太会用到的函数不在基类使用。
        // We don't use these methods.
        /*void Update()
        {
            OnUpdateView();
        }
        protected virtual void OnUpdateView()
        {
        }

        void OnGUI()
        {
            OnViewGUI();
        }
        protected virtual void OnViewGUI()
        {
        }*/

        void OnDisable()
        {
            OnDisableView();
        }
        protected virtual void OnDisableView()
        {
        }

        void OnDestory()
        {
            OnDestroyView();
        }
        protected virtual void OnDestroyView()
        {
        }
    }
}
