using System;
using System.Runtime.InteropServices;
namespace BoTing.Module
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct SYSTEMTIME
    {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMilliseconds;
    }
    internal class SystemTime
    {
        static public SYSTEMTIME Now
        {
            get
            {
                SYSTEMTIME sys = new SYSTEMTIME();
                sys.wYear = (ushort)DateTime.Now.Year;
                sys.wMonth = (ushort)DateTime.Now.Month;
                sys.wDay = (ushort)DateTime.Now.Day;
                sys.wHour = (ushort)DateTime.Now.Hour;
                sys.wMinute = (ushort)DateTime.Now.Minute;
                sys.wSecond = (ushort)DateTime.Now.Second;
                sys.wMilliseconds = (ushort)DateTime.Now.Millisecond;
                return sys;
            }
        }

        static public SYSTEMTIME Convert(DateTime dt)
        {
            SYSTEMTIME sys = new SYSTEMTIME();
            sys.wYear = (ushort)dt.Year;
            sys.wMonth = (ushort)dt.Month;
            sys.wDay = (ushort)dt.Day;
            sys.wHour = (ushort)dt.Hour;
            sys.wMinute = (ushort)dt.Minute;
            sys.wSecond = (ushort)dt.Second;
            sys.wMilliseconds = (ushort)dt.Millisecond;
            return sys;
        }
        static public DateTime Convert(SYSTEMTIME sys)
        {
            DateTime dt = new DateTime(sys.wYear, sys.wMonth, sys.wDay, sys.wHour, sys.wMinute, sys.wSecond, sys.wMilliseconds);
            //dt.Year = (int)sys.wYear;
            //dt.Month = (int)sys.wMonth;
            //dt.Day = (int)sys.wDay;
            //dt.Hour = (int)sys.wHour;
            //dt.Minute = (int)sys.wMinute;
            //dt.Second = (int)sys.wSecond;
            //dt.Milliseconds = (int)sys.wMillisecond;
            return dt;
        }

    }

}