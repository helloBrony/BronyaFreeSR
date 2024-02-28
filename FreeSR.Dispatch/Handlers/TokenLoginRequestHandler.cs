namespace FreeSR.Dispatch.Handlers
{
    using Ceen;
    using FreeSR.Dispatch.Util;
    using FreeSR.Proto;
    using Newtonsoft.Json.Linq;
    using System.Threading.Tasks;

    internal class TokenLoginRequestHandler : IHttpModule
    {
        public async Task<bool> HandleAsync(IHttpContext context)
        {

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
                                                     })
                                                     .Build());
            return true;
        }
    }
}
