using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BoTing.GamePublic
{
    public enum SideBarMode
    {
        Normal = 0, // 常规模式
        Tournament,   // 比赛模式
    }

    public class SideBar : MonoBehaviour
    {
        private RectTransform panelTournament;
        private RectTransform panelNormal;
        private RectTransform panelPlayerList;
        private RectTransform panelChatBox;

        private ChatBoxControl chatBox;

        private SideBarMode sideBarMode;

        // The Messager for current game.
        private Messager messager;

        void Awake()
        {
            panelTournament = transform.FindChild("PanelTournament") as RectTransform;
            panelNormal = transform.FindChild("PanelNormal") as RectTransform;
            panelPlayerList = transform.FindChild("PanelPlayerList") as RectTransform;
            panelChatBox = transform.FindChild("PanelChatBox") as RectTransform;
            chatBox = panelChatBox.GetComponent<ChatBoxControl>();
        }

        void Start()
        {
            Initailize(SideBarMode.Normal, null);
        }

        public void Initailize(SideBarMode mode, Messager messager)
        {
            sideBarMode = mode;
            this.messager = messager;

            HideParts(mode);
            UpdateChatBoxHeight();
        }

        private void HideParts(SideBarMode mode)
        {
            bool isNormal = mode == SideBarMode.Normal;

            panelNormal.gameObject.SetActive(isNormal);
            panelTournament.gameObject.SetActive(!isNormal);
        }

        private void UpdateChatBoxHeight()
        {
            var lastChild = transform.GetChild(transform.childCount - 1);
            if (lastChild != panelChatBox)
            {
                return;
            }

            float occupiedHeight = 0;
            for (int index = 0; index < transform.childCount - index; ++index)
            {
                var child = transform.GetChild(index) as RectTransform;
                if (child == null || !child.gameObject.activeSelf)
                {
                    continue;
                }

                float height = child.rect.height;
                if (Mathf.Abs(height) <= 0.00001)
                {
                    var layoutElement = child.gameObject.GetComponent<LayoutElement>();
                    if (layoutElement != null)
                    {
                        height = layoutElement.preferredHeight;
                    }
                }
                occupiedHeight += height;
            }

            float chatBoxHeight = (transform as RectTransform).rect.height - occupiedHeight;
            chatBox.SetHeight(chatBoxHeight);
        }
    }
}