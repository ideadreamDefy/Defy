
namespace BoTing.Module
{
    public class UserCenter
    {
        static private UserCenter gUserCenter;
        static public UserCenter Instance
        {
            get
            {
                if (gUserCenter == null)
                    gUserCenter = new UserCenter();
                return gUserCenter;
            }
        }

        public string LoginAddress
        {
            get { return "127.0.0.1"; }
        }
        public ushort LoginPort
        {
            get { return 7701; }
        }
        public uint UserID
        {
            get;
            set;
        }
        public uint FaceID
        {
            get;
            set;
        }
        public string Account
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public string NickName
        {
            get;
            set;
        }
        public uint GameID
        {
            get;
            set;
        }
        public string BankPwd
        {
            get;
            set;
        }
    }
}
