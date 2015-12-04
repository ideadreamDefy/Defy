using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using BoTing.Framework;

namespace BoTing.GamePublic
{
    public class TournamentControl : ViewBase
    {
        private Text tournamentTitle;
        public Text TournamentTitle
        {
            get
            {
                if (!isInited)
                {
                    InitView();
                }
                return tournamentTitle;
            }
        }

        private Text countTime;
        public Text CountTime
        {
            get
            {
                if (!isInited)
                {
                    InitView();
                }
                return countTime;
            }
        }

        private Text playerNum;
        public Text PlayerNum
        {
            get
            {
                if (!isInited)
                {
                    InitView();
                }
                return playerNum;
            }
        }
        
        private Text currentRankingNo;
        public Text CurrentRankingNo
        {
            get
            {
                if (!isInited)
                {
                    InitView();
                }
                return currentRankingNo;
            }
        }

        private Text totalRankingNum;
        public Text TotalRankingNum
        {
            get
            {
                if (!isInited)
                {
                    InitView();
                }
                return totalRankingNum;
            }
        }

        private Text risingInfo;
        public Text RisingInfo
        {
            get
            {
                if (!isInited)
                {
                    InitView();
                }
                return risingInfo;
            }
        }

        private Text roundInfo1;
        public Text RoundInfo1
        {
            get
            {
                if (!isInited)
                {
                    InitView();
                }
                return roundInfo1;
            }
        }

        private Text roundInfo2;
        public Text RoundInfo2
        {
            get
            {
                if (!isInited)
                {
                    InitView();
                }
                return roundInfo2;
            }
        }

        private Text roundInfo3;
        public Text RoundInfo3
        {
            get 
            { 
                if(!isInited)
                {
                    InitView();
                }
                return roundInfo3; 
            }
        }

        private bool isInited = false;

        void Awake()
        {
            InitView();
        }

        public override void InitView()
        {
            if(isInited)
            {
                return;
            }
            isInited = false;

            Transform PanelTitle0 = transform.FindChild("PanelTitle");
            Transform TextTournamentTitle00 = PanelTitle0.FindChild("TextTournamentTitle");
            tournamentTitle = TextTournamentTitle00.GetComponent<Text>();
            Transform PanelClock2 = transform.FindChild("PanelClock");
            Transform PanelTime20 = PanelClock2.FindChild("PanelTime");
            Transform TextCountTime201 = PanelTime20.FindChild("TextCountTime");
            countTime = TextCountTime201.GetComponent<Text>();
            Transform PanelPlayerNum21 = PanelClock2.FindChild("PanelPlayerNum");
            Transform TextPlayerNum211 = PanelPlayerNum21.FindChild("TextPlayerNum");
            playerNum = TextPlayerNum211.GetComponent<Text>();
            Transform PanelRank3 = transform.FindChild("PanelRank");
            Transform TextCurrentRankingNo32 = PanelRank3.FindChild("TextCurrentRankingNo");
            currentRankingNo = TextCurrentRankingNo32.GetComponent<Text>();
            Transform TextTotalRankingNum34 = PanelRank3.FindChild("TextTotalRankingNum");
            totalRankingNum = TextTotalRankingNum34.GetComponent<Text>();
            Transform TextRisingInfo35 = PanelRank3.FindChild("TextRisingInfo");
            risingInfo = TextRisingInfo35.GetComponent<Text>();
            Transform PanelRoundInfo4 = transform.FindChild("PanelRoundInfo");
            Transform TextRoundInfo141 = PanelRoundInfo4.FindChild("TextRoundInfo1");
            roundInfo1 = TextRoundInfo141.GetComponent<Text>();
            Transform TextRoundInfo242 = PanelRoundInfo4.FindChild("TextRoundInfo2");
            roundInfo2 = TextRoundInfo242.GetComponent<Text>();
            Transform TextRoundInfo343 = PanelRoundInfo4.FindChild("TextRoundInfo3");
            roundInfo3 = TextRoundInfo343.GetComponent<Text>();
        }
    }
}
