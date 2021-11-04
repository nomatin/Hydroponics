using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Net.Wifi;
using Android.Net;
using hydroponics.Classes;

[assembly: Xamarin.Forms.Dependency(typeof(WifiService))]
namespace hydroponics.Classes
{
    public class WifiService: IWifiService
    {
        public void GetWifiNetwork(string wifi)
        {
            throw new NotImplementedException();
        }

        public List<string> WifiList()
        {
            WifiManager wifiManager = (WifiManager)Application.Context.GetSystemService(Context.WifiService);
            var a = wifiManager.StartScan();

            var networks = wifiManager.ScanResults;

            var wifiDisponiveis = new List<string>();

            foreach (var network in networks)
            {
                wifiDisponiveis.Add(network.Ssid);
            }

            return wifiDisponiveis;
        }
    }
}
