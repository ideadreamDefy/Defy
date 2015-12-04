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

        // ��ЩƵ�����á�Ӱ�����ܡ������ϲ�̫���õ��ĺ������ڻ���ʹ�á�
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
