using System;
using System.Collections.Generic;
using System.Text;

namespace hydroponics.Classes
{
    public interface IWifiService
    {
        List<string> WifiList();
        void GetWifiNetwork(string wifi);
    }
}
