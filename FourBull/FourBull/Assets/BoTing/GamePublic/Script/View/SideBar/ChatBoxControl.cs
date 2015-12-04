using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BoTing.GamePublic
{
    public class ChatBoxControl : MonoBehaviour
    {
        private RectTransform mainButtonContainer;
        private Button buttonSystem;
        private Button buttonChat;

        private RectTransform messageContainer;
        
        private RectTransform systemContainer;
        private RectTransform systemRichEdit;

        private RectTransform chatContainer;
        private RectTransform chatRichEdit;

        private RectTransform chatButtonContainer;
        private Text chatInputText;
        private Button buttonChatFace;
        private Button buttonSend;        

        private bool IsChatting
        {
            get { return chatContainer.gameObject.activeSelf; }
        }
        
        void Awake()
        {
            Transform PanelMessageContainer0 = transform.FindChild("PanelMessageContainer");
            messageContainer = PanelMessageContainer0.GetComponent<RectTransform>();
            Transform PanelSystemContainer00 = PanelMessageContainer0.FindChild("PanelSystemContainer");
            systemContainer = PanelSystemContainer00.GetComponent<RectTransform>();
            Transform PanelSystemRichEdit000 = PanelSystemContainer00.FindChild("PanelSystemRichEdit");
            systemRichEdit = PanelSystemRichEdit000.GetComponent<RectTransform>();
            Transform PanelChatContainer01 = PanelMessageContainer0.FindChild("PanelChatContainer");
            chatContainer = PanelChatContainer01.GetComponent<RectTransform>();
            Transform PanelChatRichEdit010 = PanelChatContainer01.FindChild("PanelChatRichEdit");
            chatRichEdit = PanelChatRichEdit010.GetComponent<RectTransform>();
            Transform PanelChatButtonContainer011 = PanelChatContainer01.FindChild("PanelChatButtonContainer");
            chatButtonContainer = PanelChatButtonContainer011.GetComponent<RectTransform>();
            Transform InputFieldChat0110 = PanelChatButtonContainer011.FindChild("InputFieldChat");
            Transform TextChatInputText01100 = InputFieldChat0110.FindChild("ChatInputText");
            chatInputText = TextChatInputText01100.GetComponent<Text>();
            Transform ButtonChatFace0111 = PanelChatButtonContainer011.FindChild("ButtonChatFace");
            buttonChatFace = ButtonChatFace0111.GetComponent<Button>();
            buttonChatFace.onClick.AddListener(OnButtonChatFaceClicked);

            Transform ButtonSend0112 = PanelChatButtonContainer011.FindChild("ButtonSend");
            buttonSend = ButtonSend0112.GetComponent<Button>();
            buttonSend.onClick.AddListener(OnButtonSendClicked);

            Transform PanelMainButtonContainer1 = transform.FindChild("PanelMainButtonContainer");
            mainButtonContainer = PanelMainButtonContainer1.GetComponent<RectTransform>();
            Transform ButtonSystem10 = PanelMainButtonContainer1.FindChild("ButtonSystem");
            buttonSystem = ButtonSystem10.GetComponent<Button>();
            buttonSystem.onClick.AddListener(OnButtonSystemClicked);

            Transform ButtonChat11 = PanelMainButtonContainer1.FindChild("ButtonChat");
            buttonChat = ButtonChat11.GetComponent<Button>();
            buttonChat.onClick.AddListener(OnButtonChatClicked);
        }

        void Start()
        {
        }

        public void SetHeight(float height)
        {
            // Set system and chat message box 's main height.
            var layoutElement = transform.GetComponent<LayoutElement>();
            layoutElement.preferredHeight = height;

            // Set message container's height.
            var verticalLayoutGroup = transform.GetComponent<VerticalLayoutGroup>();
            var messageContainerLayoutElement = messageContainer.GetComponent<LayoutElement>();
            var mainButtonContainerLayoutElement = mainButtonContainer.GetComponent<LayoutElement>();
            float messageContainerHeight = height - mainButtonContainerLayoutElement.preferredHeight - verticalLayoutGroup.spacing;
            messageContainerLayoutElement.preferredHeight = messageContainerHeight;

            // Set chat box's height.
            verticalLayoutGroup = chatContainer.GetComponent<VerticalLayoutGroup>();
            var chatRichEditLayoutElement = chatRichEdit.GetComponent<LayoutElement>();
            var chatButtonContainerLayoutElement = chatButtonContainer.GetComponent<LayoutElement>();
            chatRichEditLayoutElement.preferredHeight = messageContainerHeight - chatButtonContainerLayoutElement.preferredHeight - verticalLayoutGroup.spacing;
        }

        private void OnButtonSystemClicked()
        {
            SwitchChatBox(false);
        }

        private void OnButtonChatClicked()
        {
            SwitchChatBox(true);
        }

        private void OnButtonChatFaceClicked()
        {

        }
        
        private void OnButtonSendClicked()
        {

        }

        private void SwitchChatBox(bool showChat)
        {
            if(IsChatting == showChat)
            {
                return;
            }

            systemContainer.gameObject.SetActive(!showChat);
            chatContainer.gameObject.SetActive(showChat);
        }
    }
}
