using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BoTing.GamePublic
{
    public class GameLayout : UIComponentBase
    {
        private bool isInited = false;
        public bool IsInited
        {
            get { return isInited; }
        }

        public RectTransform MainLayout
        {
            get 
            {
                if (!isInited)
                {
                    InitView();
                }
                return mainLayout; 
            }
        }

        public RectTransform MainContainer
        {
            get 
            {
                if (!isInited)
                {
                    InitView();
                }
                return mainContainer; 
            }
        }

        public RectTransform BackgroundNode
        {
            get 
            {
                if (!isInited)
                {
                    InitView();
                }
                return backgroundNode; 
            }
        }

        public RectTransform TopCenterNode
        {
            get 
            {
                if (!isInited)
                {
                    InitView();
                }
                return topCenterNode; 
            }
        }

        public RectTransform LeftCenterNode
        {
            get 
            {
                if (!isInited)
                {
                    InitView();
                }
                return leftCenterNode; 
            }
        }

        public RectTransform RightCenterNode
        {
            get 
            {
                if (!isInited)
                {
                    InitView();
                }
                return rightCenterNode; 
            }
        }

        public RectTransform BottomCenterNode
        {
            get 
            {
                if (!isInited)
                {
                    InitView();
                }
                return bottomCenterNode; 
            }
        }

        public RectTransform LeftTopNode
        {
            get 
            {
                if (!isInited)
                {
                    InitView();
                }
                return leftTopNode; 
            }
        }

        public RectTransform RightTopNode
        {
            get 
            {
                if (!isInited)
                {
                    InitView();
                }
                return rightTopNode; 
            }
        }

        public RectTransform LeftBottomNode
        {
            get 
            {
                if (!isInited)
                {
                    InitView();
                }
                return leftBottomNode; 
            }
        }

        public RectTransform RightBottomNode
        {
            get 
            {
                if (!isInited)
                {
                    InitView();
                }
                return rightBottomNode; 
            }
        }

        public RectTransform ForegroundNode
        {
            get 
            {
                if (!isInited)
                {
                    InitView();
                }
                return foregroundNode; 
            }
        }

        private RectTransform mainLayout;
        private RectTransform mainContainer;
        private RectTransform backgroundNode;
        private RectTransform topCenterNode;
        private RectTransform leftCenterNode;
        private RectTransform rightCenterNode;
        private RectTransform bottomCenterNode;
        private RectTransform leftTopNode;
        private RectTransform rightTopNode;
        private RectTransform leftBottomNode;
        private RectTransform rightBottomNode;
        private RectTransform foregroundNode;

        protected override void OnAwakeView()
        {
            base.OnAwakeView();

            InitView();
        }

        public override void InitView()
        {
            if(isInited)
            {
                return;
            }
            isInited = true;

            Transform MainLayout0 = transform.FindChild("MainLayout");
            mainLayout = MainLayout0 as RectTransform;
            Transform MainContainer00 = MainLayout0.FindChild("MainContainer");
            mainContainer = MainContainer00 as RectTransform;
            Transform Background000 = MainContainer00.FindChild("Background");
            Transform Locator0000 = Background000.FindChild("Locator");
            Transform BackgroundNode00000 = Locator0000.FindChild("BackgroundNode");
            backgroundNode = BackgroundNode00000 as RectTransform;
            Transform TopCenter001 = MainContainer00.FindChild("TopCenter");
            Transform Locator0010 = TopCenter001.FindChild("Locator");
            Transform TopCenterNode00100 = Locator0010.FindChild("TopCenterNode");
            topCenterNode = TopCenterNode00100 as RectTransform;
            Transform LeftCenter002 = MainContainer00.FindChild("LeftCenter");
            Transform Locator0020 = LeftCenter002.FindChild("Locator");
            Transform LeftCenterNode00200 = Locator0020.FindChild("LeftCenterNode");
            leftCenterNode = LeftCenterNode00200 as RectTransform;
            Transform RightCenter003 = MainContainer00.FindChild("RightCenter");
            Transform Locator0030 = RightCenter003.FindChild("Locator");
            Transform RightCenterNode00300 = Locator0030.FindChild("RightCenterNode");
            rightCenterNode = RightCenterNode00300 as RectTransform;
            Transform BottomCenter004 = MainContainer00.FindChild("BottomCenter");
            Transform Locator0040 = BottomCenter004.FindChild("Locator");
            Transform BottomCenterNode00400 = Locator0040.FindChild("BottomCenterNode");
            bottomCenterNode = BottomCenterNode00400 as RectTransform;
            Transform LeftTop005 = MainContainer00.FindChild("LeftTop");
            Transform Locator0050 = LeftTop005.FindChild("Locator");
            Transform LeftTopNode00500 = Locator0050.FindChild("LeftTopNode");
            leftTopNode = LeftTopNode00500 as RectTransform;
            Transform RightTop006 = MainContainer00.FindChild("RightTop");
            Transform Locator0060 = RightTop006.FindChild("Locator");
            Transform RightTopNode00600 = Locator0060.FindChild("RightTopNode");
            rightTopNode = RightTopNode00600 as RectTransform;
            Transform LeftBottom007 = MainContainer00.FindChild("LeftBottom");
            Transform Locator0070 = LeftBottom007.FindChild("Locator");
            Transform LeftBottomNode00700 = Locator0070.FindChild("LeftBottomNode");
            leftBottomNode = LeftBottomNode00700 as RectTransform;
            Transform RightBottom008 = MainContainer00.FindChild("RightBottom");
            Transform Locator0080 = RightBottom008.FindChild("Locator");
            Transform RightBottomNode00800 = Locator0080.FindChild("RightBottomNode");
            rightBottomNode = RightBottomNode00800 as RectTransform;
            Transform Foreground009 = MainContainer00.FindChild("Foreground");
            Transform Locator0090 = Foreground009.FindChild("Locator");
            Transform ForegroundNode00900 = Locator0090.FindChild("ForegroundNode");
            foregroundNode = ForegroundNode00900 as RectTransform;
        }
    }
}