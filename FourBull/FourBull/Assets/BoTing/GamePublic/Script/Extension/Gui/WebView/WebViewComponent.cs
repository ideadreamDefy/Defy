using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BoTing.GamePublic
{
    public class WebViewComponent : UIComponentBase
    {
        private WebViewWindow _webView = null;

        private RectTransform rectTransform
        {
            get { return transform as RectTransform; }
        }


        protected override void OnStartView()
        {
            base.OnStartView();
            var leftTopPosition = GetNodeUILeftTop(gameObject);

            Debug.Log("ScreenHeight = " + leftTopPosition.ToString());

            _webView = new WebViewWindow();
            bool result = _webView.Initialize();
            if (result)
            {
                _webView.Navigating += OnNavigating;
                _webView.Navigated += OnNavigated;
                _webView.NavigateError += OnNavigateError;

                _webView.SetPosition(leftTopPosition.x, leftTopPosition.y, rectTransform.rect.width, rectTransform.rect.height);

                _webView.Navigate("https://www.baidu.com");

                int i = 0;
                ++i;
            }
            else
            {
                _webView = null;
            }
        }

        protected override void OnDestroyView()
        {
            Destory();

            base.OnDestroyView();
        }

        void Destory()
        {
            if (_webView != null)
            {
                _webView.Destroy();
                _webView = null;
            }
        }

        private void OnNavigating(string url)
        {
            Debug.Log("OnNavigating url = " + url);
        }

        private void OnNavigated(string url)
        {
            Debug.Log("OnNavigated url = " + url);
        }

        private void OnNavigateError(int errorCode)
        {
            Debug.Log("OnNavigateError errorCode = " + errorCode);
        }

        private Vector2 GetNodeSize(GameObject nodeObject)
        {
            Vector2 size = new Vector2();
            size.x = (nodeObject.transform as RectTransform).rect.width;
            size.y = (nodeObject.transform as RectTransform).rect.height;
            if (size.x == 0 && size.y == 0)
            {
                var layoutElement = nodeObject.GetComponent<LayoutElement>();
                if (layoutElement)
                {
                    size.x = layoutElement.preferredWidth;
                    size.y = layoutElement.preferredHeight;
                }
            }
            return size;
        }

        private Vector3 GetNodeUILeftTop(GameObject nodeObject)
        {
            Vector2 size = GetNodeSize(nodeObject);
            RectTransform rectTransform = nodeObject.transform as RectTransform;
            float left = rectTransform.position.x - rectTransform.rect.width * rectTransform.pivot.x;
            float top = rectTransform.position.y + rectTransform.rect.height * (1 - rectTransform.pivot.y);

            // Reverse to UI Position.
            top = Screen.height - top;

            return new Vector3(left, top, 0f);
        }
    }
}
