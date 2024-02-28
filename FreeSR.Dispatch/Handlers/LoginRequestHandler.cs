namespace FreeSR.Dispatch.Handlers
{
    using Ceen;
    using FreeSR.Dispatch.Util;
    using FreeSR.Proto;
    using Newtonsoft.Json.Linq;
    using System.Threading.Tasks;

    internal class LoginRequestHandler : IHttpModule
    {
        public async Task<bool> HandleAsync(IHttpContext context)
        {
            context.Response.StatusCode = HttpStatusCode.OK;
            context.Response.ContentType = "application/json";

            string data = await context.Request.Body.ReadAllAsStringAsync();
            JObject loginJson = JObject.Parse(data);

            string accountName = (string)loginJson["account"];
            string password = (string)loginJson["password"];

            var accountData = DispatchHelper.ToLoginResponseData();

            await context.Response.WriteAllJsonAsync(DispatchResponseBuilder.Create()
                                                     .Retcode(0)
                                                     .Message("OK")
                                                     .Object("data", new JObject 
                                                     {
                                                         {"account", accountData},
                                                         {"device_grant_required", false},
                                                         {"safe_moblie_required", false},
                                                         {"realperson_required", false},
                                                         {"reactivate_required", false},
                                                         {"realname_operation", "None"}
,                                                     })
                                                     .Build());

            return true;
        }
    }
}
