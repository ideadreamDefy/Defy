using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using BoTing.Framework;

namespace BoTing.GamePublic
{
    public class NormalControl : ViewBase
    {
        private Text textTitleRoundCount;
        public Text TitleRoundCount
        {
            get 
            {
                if (!isInited)
                {
                    InitView();
                }
                return textTitleRoundCount; 
            }
        }

        private Text textTitleRoundLabel;
        public Text TitleRoundLabel
        {
            get 
            {
                if (!isInited)
                {
                    InitView();
                }
                return textTitleRoundLabel; 
            }
        }


        private Image imageWinningSteakProgress;
        public Image WinningSteakProgress
        {
            get
            {
                if (!isInited)
                {
                    InitView();
                } 
                return imageWinningSteakProgress;
            }
        }
        
        private Text textWinningSteakReward;
        public Text WinningSteakReward
        {
            get
            {
                if (!isInited)
                {
                    InitView();
                }
                return textWinningSteakReward;
            }
        }


        private Text textWinningSteakCount;
        public Text WinningSteakCount
        {
            get
            {
                if (!isInited)
                {
                    InitView();
                }
                return textWinningSteakCount;
            }
        }

        private bool isInited = false;
        public bool IsInited
        {
            get { return isInited; }
        }

        void Awake()
        {
            InitView();
        }
        
        public override void InitView()
        {
            if (isInited)
            {
                return;
            }
            isInited = true;

            Transform PanelTitle0 = transform.FindChild("PanelTitle");
            Transform TextTitleRoundCount00 = PanelTitle0.FindChild("TextTitleRoundCount");
            textTitleRoundCount = TextTitleRoundCount00.GetComponent<Text>();
            Transform TextTitleRoundLabel01 = PanelTitle0.FindChild("TextTitleRoundLabel");
            textTitleRoundLabel = TextTitleRoundLabel01.GetComponent<Text>();
            Transform PanelInfo1 = transform.FindChild("PanelInfo");
            Transform ImageProgressBackground11 = PanelInfo1.FindChild("ImageProgressBackground");
            Transform ImageWinningSteakProgress110 = ImageProgressBackground11.FindChild("ImageWinningSteakProgress");
            imageWinningSteakProgress = ImageWinningSteakProgress110.GetComponent<Image>();
            Transform TextWinningSteakReward12 = PanelInfo1.FindChild("TextWinningSteakReward");
            textWinningSteakReward = TextWinningSteakReward12.GetComponent<Text>();
            Transform TextWinningSteakCount13 = PanelInfo1.FindChild("TextWinningSteakCount");
            textWinningSteakCount = TextWinningSteakCount13.GetComponent<Text>();
        }
    }
}