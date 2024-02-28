namespace FreeSR.Dispatch.Handlers
{
    using Ceen;
    using FreeSR.Dispatch.Util;
    using FreeSR.Proto;
    using System.Threading.Tasks;

    internal class QueryGatewayHandler : IHttpModule
    {
        public async Task<bool> HandleAsync(IHttpContext context)
        {
            context.Response.StatusCode = HttpStatusCode.OK;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAllAsync(Convert.ToBase64String(ProtobufUtil.Serialize(new Gateserver
            {
                Retcode = 0,
                Msg0 = "OK",
                Ip = "127.0.0.1",
                RegionName = "FreeSR",
                Port = 22301,
                B1 = true,
                B2 = true,
                B3 = true,
                B4 = true,
                B5 = true,
                B6 = true,
                B7 = true,
                B8 = true,
                useTcp = true,
                //MdkResVersion = "5335706",
                AssetBundleUrl = "https://autopatchos.starrails.com/asb/BetaLive/output_6510636_cb4da670a18a",
                ExResourceUrl = "https://autopatchos.starrails.com/design_data/BetaLive/output_6519585_2be8ac313835",
                IfixVersion = "https://autopatchos.starrails.com/ifix/BetaLive/output_6523427_28cc5c21c689",
                LuaUrl = "https://autopatchos.starrails.com/lua/BetaLive/output_6516960_dede96733b5b",
            })));

            return true;
        }
    }
}
