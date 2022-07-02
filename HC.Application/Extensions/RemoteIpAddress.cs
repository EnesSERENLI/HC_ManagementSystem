using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Extensions
{
    public class RemoteIpAddress
    {
        public static string GetIpAddress()
        {
            string ip = "";

            IPAddress[] localIps = Dns.GetHostAddresses(Dns.GetHostName());

            foreach (IPAddress ipAddress in localIps)
            {
                if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ip = ipAddress.ToString();
                    return ip;
                }
            }
            return "Ip address not found!";
        }
    }
}
