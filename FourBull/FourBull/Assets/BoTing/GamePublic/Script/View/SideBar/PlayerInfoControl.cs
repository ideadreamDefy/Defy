using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BoTing.GamePublic
{
    public class PlayerInfoControl : MonoBehaviour
    {
        private Text textName;
        public Text Name
        {
            get
            {
                Initailize();
                return textName;
            }
        }

        private Text textLevel;
        public Text Level
        {
            get
            {
                Initailize();
                return textLevel;
            }
        }

        private RectTransform panelGameIconList;
        public RectTransform PanelGameIconList
        {
            get
            {
                Initailize();
                return panelGameIconList;
            }
        }

        private bool isInited = false;
        private bool IsInitialized
        {
            get { return isInited; }
        }

        void Awake()
        {
            Initailize();
        }

        // Use this for initialization
        void Start()
        {

        }

        public void Initailize()
        {
            if (isInited)
            {
                return;
            }
            isInited = true;

            Transform PanelInfo0 = transform.FindChild("PanelInfo");
            Transform _Text_Name00 = PanelInfo0.FindChild("_Text_Name");
            textName = _Text_Name00.GetComponent<Text>();
            Transform _Text_Level01 = PanelInfo0.FindChild("_Text_Level");
            textLevel = _Text_Level01.GetComponent<Text>();
            Transform _Panel_GameIconList2 = transform.FindChild("_Panel_GameIconList");
            panelGameIconList = _Panel_GameIconList2.GetComponent<RectTransform>();
        }
    }
}