using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BoTing.GamePublic
{
    public class PlayerListControl : MonoBehaviour
    {
        private RectTransform panelPlayerInfoSample = null;

        private List<PlayerInfoControl> playerInfoList = new List<PlayerInfoControl>();

        void Awake()
        {
            InitView();
        }

        // Use this for initialization
        void Start()
        {

        }

        private void InitView()
        {
            panelPlayerInfoSample = transform.Find("PanelPlayerInfo").GetComponent<RectTransform>();
            playerInfoList.Add(panelPlayerInfoSample.GetComponent<PlayerInfoControl>());
        }

        public PlayerInfoControl AddPlayerInfoControl(bool isSelf)
        {
            PlayerInfoControl playerInfoControl = null;
            if(isSelf)
            {
                playerInfoControl = playerInfoList[0];
            }
            else
            {
                var controlObject = GameObject.Instantiate(panelPlayerInfoSample.gameObject);
                controlObject.transform.SetParent(transform);
                controlObject.transform.SetAsLastSibling();
                playerInfoControl = controlObject.GetComponent<PlayerInfoControl>();
            }
            return playerInfoControl;
        }        
    }
}
