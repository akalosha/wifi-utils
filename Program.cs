using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.WiFi;
using Xamarin.Essentials;

namespace WifiFixerUtil
{
    internal class Program
    {

        private const int minute = 1000 * 60;
        Dictionary<WiFiAvailableNetwork, WiFiAdapter> _wifiNetworks;

        static void Main(string[] args)
        {
            while (true)
            {
                if (checkStatus())
                {
                    Console.WriteLine("Server available");
                }
                else
                {
                    Console.WriteLine("Server unavailable");
                    connect();
                }
                Thread.Sleep(minute);
            }
            Console.ReadLine();
        }

        static Boolean checkStatus()
        {
            IPStatus status = IPStatus.Unknown;
            try
            {
                status = new Ping().Send("192.168.100.1").Status;
            }
            catch { }

            return status == IPStatus.Success;
        }

        static void connect()
        {
            
            var profiles = Connectivity.ConnectionProfiles;
            if (profiles.Contains(ConnectionProfile.WiFi))
            {
                Console.WriteLine("Active Wi-Fi connection.");
            }
            else {
                Console.WriteLine("Inactive Wi-Fi connection.");
                //var task = Task.Run(async () => await UpdateWifiInfo());

                WiFiAdapter wiFiAdapter = new WiFiAdapter();
                
            }
        }
        /*
        public async static 
            UpdateWifiInfo()
        {
            _wifiNetworks = new Dictionary<WiFiAvailableNetwork, WiFiAdapter>();
            var adapters = WiFiAdapter.FindAllAdaptersAsync();

            foreach (var adapter in await adapters)
            {
                await adapter.ScanAsync();

                if (adapter.NetworkReport == null)
                {
                    continue;
                }

                foreach (var network in adapter.NetworkReport.AvailableNetworks)
                {
                    if (!HasSsid(_wifiNetworks, network.Ssid))
                    {
                        _wifiNetworks[network] = adapter;
                    }
                }
            }
        }
        */
        private bool HasSsid(Dictionary<WiFiAvailableNetwork, WiFiAdapter> resultCollection, string ssid)
        {
            foreach (var network in resultCollection)
            {
                if (!string.IsNullOrEmpty(network.Key.Ssid) && network.Key.Ssid == ssid)
                {
                    return true;
                }
            }
            return false;
        }
    }
}