using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UtilityTool
{
    public static class NetTool
    {
        /// <summary>
        /// Server Domain Name System
        /// </summary>
        public static class ServerSide
        {
            /// <summary>
            /// Get server side Ip list
            /// </summary>
            /// <param name="includeIpv6"></param>
            /// <returns></returns>
            public static List<string> GetServerIpList(bool includeIpv6 = false)
            {
                var ipList = new List<string>();
                // 取得本機名稱
                string strHostName = Dns.GetHostName();

                // 取得本機的IpHostEntry類別實體，用這個會提示已過時
                //	IPHostEntry iphostentry = Dns.GetHostByName(strHostName);

                // 取得本機的IpHostEntry類別實體，MSDN建議新的用法
                IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);

                // 取得所有 IP 位址
                foreach (IPAddress ipaddress in iphostentry.AddressList)
                {
                    // 只取得IP V4的Address
                    if (ipaddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        ipList.Add(ipaddress.ToString());
                    }

                    if (ipaddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    {
                        ipList.Add(ipaddress.ToString());
                    }
                }

                return ipList;
            }
        }

        /// <summary>
        /// Request from web client
        /// </summary>
        public static class WebClientSide
        {
            /// <summary>
            /// MS_HttpContext
            /// </summary>
            private static readonly string HttpContextBaseKey = "MS_HttpContext";

            /// <summary>
            /// Gets the client ip.
            /// </summary>
            /// <param name="request">The request.</param>
            /// <returns></returns>
            public static string GetClientIp(HttpRequestMessage request)
            {
                var result = string.Empty;
                if (request.Properties.ContainsKey(HttpContextBaseKey))
                {
                    HttpContextBase context = (HttpContextBase)request.Properties[HttpContextBaseKey];
                    result = context.Request.UserHostAddress;
                }
                else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
                {
                    var prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                    result = prop.Address;
                }
                else if (HttpContext.Current != null)
                {
                    result = HttpContext.Current.Request.UserHostAddress;
                }
                else
                {
                    result = string.Empty;
                }

                if (result.Equals("::1"))
                {
                    result = "127.0.0.1";
                }
                return result;
            }

            /// <summary>
            /// Gets the client ip.
            /// </summary>
            /// <param name="request">The request.</param>
            /// <returns></returns>
            public static string GetClientIp(HttpRequestBase request)
            {
                string szRemoteAddr = request.UserHostAddress;
                string szXForwardedFor = request.ServerVariables["X_FORWARDED_FOR"];
                string szIP = "";

                if (szXForwardedFor == null)
                {
                    szIP = szRemoteAddr;
                }
                else
                {
                    szIP = szXForwardedFor;
                    if (szIP.IndexOf(",") > 0)
                    {
                        string[] arIPs = szIP.Split(',');

                        foreach (string item in arIPs)
                        {
                            if (!IsPrivateIpAddress(item))
                            {
                                return item;
                            }
                        }
                    }
                }

                if (szIP.Equals("::1"))
                {
                    szIP = "127.0.0.1";
                }
                return szIP;
            }

            private static bool IsPrivateIpAddress(string ipAddress)
            {
                // http://en.wikipedia.org/wiki/Private_network
                // Private IP Addresses are: 
                //  24-bit block: 10.0.0.0 through 10.255.255.255
                //  20-bit block: 172.16.0.0 through 172.31.255.255
                //  16-bit block: 192.168.0.0 through 192.168.255.255
                //  Link-local addresses: 169.254.0.0 through 169.254.255.255 (http://en.wikipedia.org/wiki/Link-local_address)

                var ip = IPAddress.Parse(ipAddress);
                var octets = ip.GetAddressBytes();

                var is24BitBlock = octets[0] == 10;
                if (is24BitBlock) return true; // Return to prevent further processing

                var is20BitBlock = octets[0] == 172 && octets[1] >= 16 && octets[1] <= 31;
                if (is20BitBlock) return true; // Return to prevent further processing

                var is16BitBlock = octets[0] == 192 && octets[1] == 168;
                if (is16BitBlock) return true; // Return to prevent further processing

                var isLinkLocalAddress = octets[0] == 169 && octets[1] == 254;
                return isLinkLocalAddress;
            }

            /// <summary>
            /// Determines whether [is local ip address] [the specified HTTP request].
            /// </summary>
            /// <param name="httpRequest">The HTTP request.</param>
            /// <returns><c>true</c> if [is local ip address] [the specified HTTP request]; otherwise, <c>false</c>.</returns>
            public static bool IsLocalIPAddress(HttpContextBase httpContextBase)
            {
                bool result = false;
                string ipAddress = httpContextBase.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                try
                {
                    if (!string.IsNullOrEmpty(ipAddress) &&
                        ipAddress.ToUpper().IndexOf("UNKNOWN") < 0)
                    {
                        return false;
                    }
                    if (!result && string.IsNullOrWhiteSpace(ipAddress))
                    {
                        ipAddress = httpContextBase.Request.ServerVariables["REMOTE_ADDR"];
                        if (ipAddress.Equals("::1"))
                        {
                            // 127.0.0.1
                            return true;
                        }
                    }
                }
                catch
                {
                    result = false;
                }
                return result;
            }
        }

    }
}
