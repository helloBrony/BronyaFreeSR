namespace FreeSR.Dispatch.Service.Manager
{
    using FreeSR.Dispatch.Configuration;
    using FreeSR.Proto;

    internal static class RegionManager
    {
        private static RegionConfiguration s_configuration;

        public static void Initialize(RegionConfiguration configuration)
        {
            s_configuration = configuration;
        }

        public static RegionInfo GetRegionList()
        {
            var region = new RegionInfo
            {
                EnvType = s_configuration.EnvType,
                DispatchUrl = s_configuration.DispatchUrl,
                Name = s_configuration.Name,
                DisplayName = s_configuration.Name,
                Title = s_configuration.Name
            };

            return region;
        }

        public static string GetTopServerRegionName() => s_configuration.Name;
    }
}
