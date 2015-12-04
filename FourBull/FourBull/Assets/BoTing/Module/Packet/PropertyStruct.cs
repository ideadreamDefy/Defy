using System.Runtime.InteropServices;

namespace BoTing.Module
{//////////////////////////////////////////////////////////////////////////////////

    //道具信息
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct tagPropertyInfo
    {
        //道具信息
        public ushort wIndex;								//道具标识
        public ushort wDiscount;							//会员折扣
        public ushort wIssueArea;							//发布范围

        //销售价格
        public long lPropertyGold;						    //道具金币
        public double dPropertyCash;						//道具价格

        //赠送魅力
        public long lSendLoveLiness;					    //赠送魅力
        public long lRecvLoveLiness;					    //接受魅力
    };


    //道具属性
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct tagPropertyAttrib
    {
        public ushort wIndex;								//道具标识
        public ushort wPropertyType;                        //道具类型
        public ushort wServiceArea;						    //使用范围
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string szMeasuringunit;					    //计量单位 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPropertyName;					    //道具名字
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string szRegulationsInfo;				    //使用信息
    };

    //道具子项
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct tagPropertyItem
    {
        tagPropertyInfo PropertyInfo;						//道具信息
        tagPropertyAttrib PropertyAttrib;					//道具属性
    };

}
