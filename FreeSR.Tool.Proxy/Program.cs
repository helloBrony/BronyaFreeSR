using System.Net;

namespace FreeSR.Tool.Proxy
{
    internal static class Program
    {
        private const string Title = "FreeSR Proxy";

        private static ProxyService s_proxyService;
        private static EventHandler s_processExitHandler = new EventHandler(OnProcessExit);
        
        private static void Main(string[] args)
        {
            Console.Title = Title;
            CheckProxy();

            s_proxyService = new ProxyService("127.0.0.1", 8888);
            AppDomain.CurrentDomain.ProcessExit += s_processExitHandler;

            Thread.Sleep(-1);
        }

        private static void OnProcessExit(object sender, EventArgs args)
        {
            s_proxyService.Shutdown();
        }

        public static void CheckProxy()
        {
            try
            {
                string ProxyInfo = GetProxyInfo();
                if (ProxyInfo != null)
                {
                    Console.WriteLine("well... It seems you are using other proxy software(such as Clash,V2RayN,Fiddler,etc)");
                    Console.WriteLine($"You system proxy: {ProxyInfo}");
                    Console.WriteLine("You have to close all other proxy software to make sure FreeSR.Tool.Proxy can work well.");
                    Console.WriteLine("Press any key to continue if you closed other proxy software, or you think you are not using other proxy.");
                    Console.ReadKey();
                }
            }
            catch (NullReferenceException)
            {

            }
        }

        public static string GetProxyInfo()
        {
            try
            {
                IWebProxy proxy = WebRequest.GetSystemWebProxy();
                Uri proxyUri = proxy.GetProxy(new Uri("https://www.example.com"));

                string proxyIP = proxyUri.Host;
                int proxyPort = proxyUri.Port;
                string info = proxyIP + ":" + proxyPort;
                return info;
            }
            catch
            {
                return null;
            }
        }
    }
}