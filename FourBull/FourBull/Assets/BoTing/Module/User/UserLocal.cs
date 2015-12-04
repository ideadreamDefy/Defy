using UnityEngine;
namespace BoTing.Module
{

    //负责本地数据持久化。注意只是本地。
    //玩家切换机器或者卸载游戏数据即消失。
    public class UserLocal
    {
        public static bool GetBoolPrefs(string keyname)
        {
            if (!PlayerPrefs.HasKey(keyname))
            {
                return true;
            }
            if (PlayerPrefs.GetInt(keyname) == 0)
            {
                return false;
            }
            return true;
        }

        public static void SetBoolPrefs(string keyname, bool value)
        {
            SetValue(keyname, value);
        }


        public static float GetFloatKey(string keyname)
        {
            if (!PlayerPrefs.HasKey(keyname))
            {
                return 1;
            }

            return PlayerPrefs.GetFloat(keyname);
        }

        public static void SetFloatKey(string keyname, float value)
        {
            SetValue(keyname, value);
        }

        public static int GetIntKey(string keyname)
        {
            if (!PlayerPrefs.HasKey(keyname))
            {
                return 0;
            }

            return PlayerPrefs.GetInt(keyname);
        }

        public static void SetIntKey(string keyname, int value)
        {
            SetValue(keyname, value);
        }

        public static string GetStringKey(string keyname)
        {
            if (!PlayerPrefs.HasKey(keyname))
            {
                return null;
            }

            return PlayerPrefs.GetString(keyname);
        }

        public static void SetStringKey(string keyname, string value)
        {
            SetValue(keyname, value);
        }

        private static void SetValue(string key, object value)
        {
            if (value is string)
            {
                PlayerPrefs.SetString(key, (string)value);
            }
            else if (value is float)
            {
                PlayerPrefs.SetFloat(key, (float)value);
            }
            else if (value is int)
            {
                PlayerPrefs.SetInt(key, (int)value);
            }
            else if (value is bool)
            {
                PlayerPrefs.SetInt(key, (bool)value == true ? 1 : 0);
            }

            PlayerPrefs.Save();
        }
    }
}