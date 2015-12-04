using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace BoTing.GamePublic
{

public class WinAPI
{
//#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
    public struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;


        public override String ToString()
        {
            return base.ToString() + "Left=" + this.Left + "  Top=" + this.Top + "  Right=" + this.Right + " Bottom=" + this.Bottom;
        }
    }

    [DllImport("user32.dll")]
    public static extern int GetWindowLong(IntPtr hwnd, int _nIndex);

    [DllImport("user32.dll")]
    public static extern IntPtr SetWindowLong(IntPtr hwnd, int _nIndex, int dwNewLong);

    [DllImport("user32.dll")]
    public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    public static extern bool UpdateWindow(IntPtr hWnd);
    
    [DllImport("user32.dll")]
    public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    [DllImport("user32.dll")]
    public static extern bool GetWindowRect(IntPtr hwnd, ref Rect lpRect);

    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

    [DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hwnd);

    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();
    
    [DllImport("user32.dll")]
    public static extern IntPtr SetCapture(IntPtr hwnd); 

    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

    [DllImport("user32.dll")]
    public static extern int PostMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

    public const int GWL_STYLE = -16;                 
                                               
    //#define WS_POPUP            0x80000000L  
    public const Int32 WS_POPUP = -1;
    public const Int32 WS_CAPTION = 0x00C00000;
    public const Int32 WS_THICKFRAME = 0x00040000;
    public const Int32 WS_VSCROLL = 0x00200000;
    public const Int32 WS_HSCROLL = 0x00100000;

    public const Int32 SWP_NOSIZE = 0x0001;
    public const Int32 SWP_NOMOVE = 0x0002;
    public const Int32 SWP_NOZORDER = 0x0004;
    public const Int32 SWP_FRAMECHANGED = 0x0020;
    public const Int32 SWP_SHOWWINDOW = 0x0040;
    public const Int32 WM_NCLBUTTONDOWN = 0x00A1;
    public const Int32 WM_NCLBUTTONUP = 0x00A2;

    public const Int32 WM_SYSCOMMAND = 0x0112;
    public const Int32 SC_MOVE = 0xF010;
    public const Int32 SC_MINIMIZE = 0xF020;
    public const Int32 SC_MAXIMIZE = 0xF030;
    public const Int32 SC_CLOSE = 0xF060;

    public const Int32 HTCAPTION = 2;

//#endif
}

}