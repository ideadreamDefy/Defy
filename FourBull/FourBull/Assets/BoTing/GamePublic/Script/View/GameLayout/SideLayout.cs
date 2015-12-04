using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BoTing.GamePublic
{
    public class SideLayout : UIComponentBase
    {
        /// <summary>
        /// 界面主体部分向左和向右移动的距离，当侧边栏显示和隐藏时发生。
        /// The distance the PanelMainLayout will move when the SideBar shows and hides.
        /// </summary>
        public float LeftMovableDistance = 180;
        /// <summary>
        /// 侧边栏显示和隐藏的动画时间, 秒。
        /// The duration in second for PanelMainLayout and SideBar to do the animation.
        /// </summary>
        public float AnimationDuration = 0.1f;

        private bool isInited = false;
        public bool IsInited
        {
            get { return isInited; }
        }

        public RectTransform SideBarLayout
        {
            get
            {
                if (!isInited)
                {
                    InitView();
                }
                return sideBarLayout;
            }
        }
        public RectTransform SideBarContainer
        {
            get
            {
                if (!isInited)
                {
                    InitView();
                }
                return sideBarContainer;
            }
        }

        public RectTransform SideBarNode
        {
            get
            {
                if (!isInited)
                {
                    InitView();
                }
                return sideBarNode;
            }
        }

        public Button SideBarButton
        {
            get
            {
                if (!isInited)
                {
                    InitView();
                }
                return sideBarButton;
            }
        }

        private RectTransform mainLayout;
        private RectTransform mainContainer;
        private RectTransform sideBarLayout;
        private RectTransform sideBarContainer;
        private RectTransform sideBarNode;
        private Button sideBarButton;

        private LayoutElement mainContainerLayoutElement;

        private Vector3 sideLayoutFixedLeftPos = new Vector3();
        private Vector3 sideLayoutFixedRightPos = new Vector3();


        private bool isAnimationRunning = false;

        private bool IsSideLayoutVisible
        {
            get
            {
                return sideBarLayout.transform.localPosition == sideLayoutFixedLeftPos;
            }
        }

        private float SideBarWidth
        {
            get
            {
                float width = (sideBarContainer.transform as RectTransform).rect.width;
                if (width < 0.1)
                {
                    var layoutElement = sideBarContainer.GetComponent<LayoutElement>();
                    if (layoutElement)
                    {
                        width = layoutElement.preferredWidth;
                    }
                }
                return width;
            }
        }

        protected override void OnAwakeView()
        {
            base.OnAwakeView();

            InitView();
        }

        public override void InitView()
        {
            if (isInited)
            {
                return;
            }
            isInited = true;

            Transform MainLayout0 = transform.parent.FindChild("MainLayout");
            mainLayout = MainLayout0 as RectTransform;
            Transform MainContainer00 = MainLayout0.FindChild("MainContainer");
            mainContainer = MainContainer00 as RectTransform;

            sideBarLayout = transform as RectTransform;
            Transform SideBarContainer10 = sideBarLayout.FindChild("SideBarContainer");
            sideBarContainer = SideBarContainer10 as RectTransform;
            Transform SideBarNode100 = SideBarContainer10.FindChild("SideBarNode");
            sideBarNode = SideBarNode100 as RectTransform;
            Transform ButtonLayout101 = SideBarContainer10.FindChild("ButtonLayout");
            Transform PanelButtonLocator1010 = ButtonLayout101.FindChild("Locator");
            Transform _Button_SideBar10100 = PanelButtonLocator1010.FindChild("SideBarButton");
            sideBarButton = _Button_SideBar10100.GetComponent<Button>();
            sideBarButton.onClick.AddListener(OnButtonSideBarClicked);

            var localPosition = sideBarLayout.transform.localPosition;
            localPosition.x = localPosition.x + SideBarWidth;

            sideLayoutFixedRightPos = localPosition;
            sideLayoutFixedLeftPos = sideBarLayout.transform.localPosition;

            var horizontalLayoutElement = mainLayout.gameObject.GetComponent<HorizontalLayoutGroup>();
            if (horizontalLayoutElement == null)
            {
                horizontalLayoutElement = mainLayout.gameObject.AddComponent<HorizontalLayoutGroup>();
            }
            horizontalLayoutElement.childForceExpandWidth = false;
            horizontalLayoutElement.childForceExpandHeight = true;

            mainContainerLayoutElement = mainContainer.gameObject.GetComponent<LayoutElement>();
            if (mainContainerLayoutElement == null)
            {
                mainContainerLayoutElement = mainContainer.gameObject.AddComponent<LayoutElement>();
            }
            mainContainerLayoutElement.preferredWidth = mainLayout.rect.width;
        }

        // Use this for initialization
        protected override void OnStartView()
        {
            base.OnStartView();

            // Move Main Content to left, by defualt.
            mainContainerLayoutElement.preferredWidth = mainLayout.rect.width - LeftMovableDistance;
        }

        private void OnButtonSideBarClicked()
        {
            if (isAnimationRunning)
            {
                return;
            }
            ShowSideLayout(!IsSideLayoutVisible);
        }

        private void ShowSideLayout(bool show)
        {
            if (isAnimationRunning)
            {
                return;
            }
            isAnimationRunning = true;

            var mainLayoutFrom = show ? mainLayout.rect.width : mainLayout.rect.width - LeftMovableDistance;
            var mainLayoutTo = show ? mainLayout.rect.width - LeftMovableDistance : mainLayout.rect.width;
            LeanTween.value(mainContainerLayoutElement.gameObject, mainLayoutFrom, mainLayoutTo, AnimationDuration).setOnUpdate((float val) =>
            {
                mainContainerLayoutElement.preferredWidth = val;
            });

            var sideLayoutTo = show ? sideLayoutFixedLeftPos : sideLayoutFixedRightPos;
            LeanTween.moveLocal(sideBarLayout.gameObject, sideLayoutTo, AnimationDuration).setOnComplete(() =>
            {
                isAnimationRunning = false;
            });
        }
    }
}
