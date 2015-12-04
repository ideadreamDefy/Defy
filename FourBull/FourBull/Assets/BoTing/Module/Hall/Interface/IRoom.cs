
namespace BoTing.Module
{
    public interface IRoom
    {
        #region ∏˜÷÷ Ù–‘
        ushort ChairCount
        {
            get;
        }
        ushort TableCount
        {
            get;
        }
        bool IsCaiJinRoom
        {
            get;
        }
        uint ServerRule
        {
            get;
        }
        ushort KindID
        {
            get;
        }
        ushort ServerID
        {
            get;
        }
        string ServerName
        {
            get;
        }
        string ProcessName
        {
            get;
        }
        string KindName
        {
            get;
        }
        #endregion
        void PrefromStandUp(byte cbForceLeave);
        void PerformSitDown(ushort wTableID, ushort wChairID, string strPassword);
    }
}
