namespace FreeSR.Dispatch
{
    using FreeSR.Dispatch.Configuration;
    using FreeSR.Shared.Configuration;

    internal class DispatchServerConfiguration
    {
        public NetworkConfiguration Network { get; set; }
        public RegionConfiguration Region { get; set; }
    }
}
