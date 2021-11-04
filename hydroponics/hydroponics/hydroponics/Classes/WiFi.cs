using System;
using System.Collections.Generic;
using System.Text;

namespace hydroponics.Classes
{
    static class WiFi
    {
        static private string SSID;
        static private string Password;
        static public bool Test()
        {
            return SSID != null && Password != null;
        }
        static public bool Save(string SSIDSave, string PasswordSave)
        {
            if(SSIDSave != SSID || PasswordSave != Password)
            {
                if (SSIDSave != null && PasswordSave.Length >= 8)
                {
                    object name = "";
                    if (App.Current.Properties.TryGetValue("SSID", out name) && App.Current.Properties.TryGetValue("Password", out name))
                    {
                        App.Current.Properties.Remove("SSID");
                        App.Current.Properties.Remove("Password");
                    }
                    App.Current.Properties.Add("SSID", SSIDSave);
                    App.Current.Properties.Add("Password", PasswordSave);
                    Password = PasswordSave;
                    SSID = SSIDSave;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
            
            

        }
        static public string GetPassword()
        {
            object name = "";
            return App.Current.Properties.TryGetValue("Password", out name) ? App.Current.Properties["Password"].ToString() : "";
        }
        static public string GetSSID()
        {
            object name = "";
            return App.Current.Properties.TryGetValue("SSID", out name) ? App.Current.Properties["SSID"].ToString() : "";
        }


    }
}
