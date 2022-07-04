using System.Net;

namespace HC.Infrastructure.Utils
{
    public class RemoteIpAddress //normalde Application içerisinde Extensions içerisinde verecektim ancak refereans olarak veremediğim için(Servisleri orada tanımladığımdan dolayı) burada kullandım.
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
