namespace FreeSR.Gateserver
{
    using FreeSR.Gateserver.Network;
    using FreeSR.Shared.Configuration;
    using FreeSR.Shared.Exceptions;
    using NLog;

    internal static class GateServer
    {
        private const string Title = "FreeSR Gate Server (EXPERIMENTAL OPEN SOURCE BUILD)";

        private static readonly Logger s_log = LogManager.GetCurrentClassLogger();

        private static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);
            AppDomain.CurrentDomain.UnhandledException += OnFatalException;

            Console.Title = Title;
            Console.WriteLine("FreeSR is a free and open-source software, if you paid for this, you have been scammed!");
            Console.WriteLine("FreeSR是一个免费且开源的软件，如果你是花钱买来的，则说明你被骗了！");
            Console.WriteLine("https://git.xeondev.com/Moux23333/FreeSR");
            s_log.Info("Initializing...");

            ConfigurationManager<GateServerConfiguration>.Instance.Initialize("GateServer.json");
            var serverConfiguration = ConfigurationManager<GateServerConfiguration>.Instance.Model;
            NetworkManager.Instance.Initialize(serverConfiguration.Network).GetAwaiter().GetResult();

            s_log.Info("Server is ready!");
            Thread.Sleep(-1); // TODO: Console handler
        }

        private static void OnFatalException(object sender, UnhandledExceptionEventArgs args)
        {
            if (args.ExceptionObject is ServerInitializationException initException)
            {
                Console.WriteLine("Server initialization failed, unhandled exception!");
                Console.WriteLine(initException);
            }
            else
            {
                Console.WriteLine("Unhandled exception in runtime!");
                Console.WriteLine(args.ExceptionObject);
            }

            Console.WriteLine("Press enter to close this window...");
            Console.ReadLine();
        }
    }
}