using Android.Net.Wifi;
using hydroponics.Classes;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
[assembly: Xamarin.Forms.Dependency(typeof(hydroponics.Classes.GetSSIDAndroid))]
namespace hydroponics.Classes
{
    
    class GetSSIDAndroid:IWifi
    {
        public string GetSSID()
        {
            WifiManager wifiManager = (WifiManager)Android.App.Application.Context.GetSystemService(Android.Content.Context.WifiService);

            if (wifiManager != null && !string.IsNullOrEmpty(wifiManager.ConnectionInfo.SSID))
            {
                return wifiManager.ConnectionInfo.SSID;
            }
            else
            {
                return "WiFiManager is NULL";
            }
        }
    }
}
