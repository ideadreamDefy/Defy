using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.Text;

namespace BoTing.GamePublic
{
    public class WebViewAPI
    {
        [DllImport("WebView", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CreateWebView(IntPtr parent);

        [DllImport("WebView", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DestroyWebView(IntPtr webView);


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void NavigatingCallback([MarshalAs(UnmanagedType.LPWStr)] string url);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void NavigatedCallback([MarshalAs(UnmanagedType.LPWStr)] string url);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void NavigateErrorCallback(int errorCode);

        [DllImport("WebView", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetNavigatingCallback(IntPtr webView, NavigatingCallback handler);
        [DllImport("WebView", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetNavigatedCallback(IntPtr webView, NavigatedCallback handler);
        [DllImport("WebView", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetNavigateErrorCallback(IntPtr webView, NavigateErrorCallback handler);

        [DllImport("WebView", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Navigate(IntPtr hWebView, [MarshalAs(UnmanagedType.LPWStr)] string url);
        [DllImport("WebView", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Refresh(IntPtr hWebView);
        [DllImport("WebView", CallingConvention = CallingConvention.Cdecl)]
        public static extern void GoBack(IntPtr hWebView);
        [DllImport("WebView", CallingConvention = CallingConvention.Cdecl)]
        public static extern void GoForward(IntPtr hWebView);
        [DllImport("WebView", CallingConvention = CallingConvention.Cdecl)]
        public static extern void GoHome(IntPtr hWebView);
        [DllImport("WebView", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CanGoBack(IntPtr hWebView);
        [DllImport("WebView", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CanGoForward(IntPtr hWebView);
        [DllImport("WebView", CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetLocationURL(IntPtr hWebView, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder outLocationUrl, int outBufferLen);


        [DllImport("WebView", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Test();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void LogFunc([MarshalAs(UnmanagedType.LPWStr)] string text);

        [DllImport("WebView", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void SetLoger(LogFunc logFunc);
    }
}
