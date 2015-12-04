using System;
using System.Text;
using UnityEngine;
using BoTing.Framework;

namespace BoTing.GamePublic
{
    public delegate void NavigatingHandler(string url);
    public delegate void NavigatedHandler(string url);
    public delegate void NavigateErrorHandler(int errorCode);

    public class WebViewWindow
    {
        private static readonly Rect DefaultPositionRect = new Rect(40, 40, 400, 400);

        /// <summary>
        /// The window handle for WebView.
        /// </summary>
        private IntPtr _webView = IntPtr.Zero;

        private WebViewAPI.NavigatingCallback _navigatingCallback;
        private WebViewAPI.NavigatedCallback _navigatedCallback;
        private WebViewAPI.NavigateErrorCallback _navigateErrorCallback;
        private WebViewAPI.LogFunc _logFunc;

        private Rect _positionRect = DefaultPositionRect;
        public Rect PositionRect
        {
            get { return _positionRect; }
        }

        public event NavigatingHandler Navigating;
        public event NavigatedHandler Navigated;
        public event NavigateErrorHandler NavigateError;

        public WebViewWindow()
        {
            _navigatingCallback = new WebViewAPI.NavigatingCallback(OnNavigating);
            _navigatedCallback = new WebViewAPI.NavigatedCallback(OnNavigated);
            _navigateErrorCallback = new WebViewAPI.NavigateErrorCallback(OnNavigateError);
            _logFunc = new WebViewAPI.LogFunc(OnLogFunc);
        }

        public bool Initialize()
        {
            if (_webView != IntPtr.Zero)
            {
                return true;
            }

            bool ret = true;
            try
            {
                WebViewAPI.SetLoger(_logFunc);

                var parent = WinAPI.GetForegroundWindow();
                _webView = WebViewAPI.CreateWebView(parent);

                UpdatePosition();

                Debug.Log("CreateWebView -- End");

                WebViewAPI.SetNavigatingCallback(_webView, _navigatingCallback);
                WebViewAPI.SetNavigatedCallback(_webView, _navigatedCallback);
                WebViewAPI.SetNavigateErrorCallback(_webView, _navigateErrorCallback);

                Debug.Log("WebView EventListener Added -- End");

                //WebViewAPI.Navigate(_webView, "http://www.hao123.com/?tn=98052938_hao_pg");
            }
            catch (Exception ex)
            {
                ret = false;
                Destroy();
                Debug.Log("Hello " + ex.Message);
            }
            return ret;
        }

        public void Destroy()
        {
            WebViewAPI.DestroyWebView(_webView);
            _webView = IntPtr.Zero;
            _positionRect = DefaultPositionRect;
        }

        public void SetPosition(float x, float y)
        {
            _positionRect.x = x;
            _positionRect.y = y;

            UpdatePosition();
        }

        public void SetSize(float width, float height)
        {
            _positionRect.width = width;
            _positionRect.height = height;

            UpdatePosition();
        }

        public void SetPosition(Rect positionRect)
        {
            _positionRect = positionRect;
            UpdatePosition();
        }

        public void SetPosition(float x, float y, float width, float height)
        {
            _positionRect = new Rect(x, y, width, height);
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            WinAPI.MoveWindow(_webView, (int)_positionRect.x, (int)_positionRect.y, (int)_positionRect.width, (int)_positionRect.height, false);
        }

        public void Navigate(string url)
        {
            if (_webView != IntPtr.Zero)
            {
                WebViewAPI.Navigate(_webView, url);
            }
        }

        public void Refresh()
        {
            if (_webView != IntPtr.Zero)
            {
                WebViewAPI.Refresh(_webView);
            }

        }

        public void GoBack()
        {
            if (_webView != IntPtr.Zero && CanGoBack)
            {
                WebViewAPI.GoBack(_webView);
            }
        }

        public void GoForward()
        {
            if (_webView != IntPtr.Zero && CanGoForward)
            {
                WebViewAPI.GoForward(_webView);
            }
        }

        public void GoHome()
        {
            if (_webView != IntPtr.Zero)
            {
                WebViewAPI.GoHome(_webView);
            }
        }

        public bool CanGoBack
        {
            get { return _webView != IntPtr.Zero ? WebViewAPI.CanGoBack(_webView) : false; }
        }

        public bool CanGoForward
        {
            get { return _webView != IntPtr.Zero ? WebViewAPI.CanGoForward(_webView) : false; }
        }

        public string LocationURL
        {
            get
            {
                if (_webView == IntPtr.Zero)
                {
                    return "";
                }

                StringBuilder locationUrl = new StringBuilder(1024);
                WebViewAPI.GetLocationURL(_webView, locationUrl, 1024);
                return locationUrl.ToString();
            }
        }

        private void OnNavigating(string url)
        {
            Debug.Log("OnNavigating url = " + url);
            Navigating(url);
        }

        private void OnNavigated(string url)
        {
            Debug.Log("OnNavigated url = " + url);
            Navigated(url);
        }

        private void OnNavigateError(int errorCode)
        {
            Debug.Log("OnNavigateError errorCode = " + errorCode);
            NavigateError(errorCode);
        }

        private void OnLogFunc(string text)
        {
            Debug.Log(text);
        }
    }
}
