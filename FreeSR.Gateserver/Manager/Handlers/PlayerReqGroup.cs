namespace FreeSR.Gateserver.Manager.Handlers
{
    using FreeSR.Gateserver.Manager.Handlers.Core;
    using FreeSR.Gateserver.Network;
    using FreeSR.Proto;
    using NLog;

    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using Newtonsoft.Json;

    internal static class PlayerReqGroup
    {
        private static readonly Logger s_log = LogManager.GetCurrentClassLogger();

        [Handler(CmdType.CmdPlayerHeartBeatCsReq)]
        public static void OnPlayerHeartBeatCsReq(NetSession session, int cmdId, object data)
        {
            var heartbeatReq = data as PlayerHeartBeatCsReq;

            session.Send(CmdType.CmdPlayerHeartBeatScRsp, new PlayerHeartBeatScRsp
            {
                Retcode = 0,

                DownloadData = new ClientDownloadData
                {
                    Version = 51,
                    Time = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                    Data = Convert.FromBase64String("G0x1YVMBGZMNChoKBAQICHhWAAAAAAAAAAAAAAAod0ABD0BGcmVlU1JMdWEudHh0AAAAAAAAAAAAAQccAAAAJABAAClAQAApgEAAKcBAAFYAAQAsgAABXUBBAOSAQQAkAUAAKcFBAikBQgIpQUIC7AAAAWyAAACWgAIA6cDCAMEAwwEWAQMAqoABgKlBgQCpQUMDqYFDAxLAQwMRQACAqUGBAJ9BRIiewP1/GQCAABIAAAAEA0NTBAxVbml0eUVuZ2luZQQLR2FtZU9iamVjdAQFRmluZAQpVUlSb290L0Fib3ZlRGlhbG9nL0JldGFIaW50RGlhbG9nKENsb25lKQQYR2V0Q29tcG9uZW50c0luQ2hpbGRyZW4EB3R5cGVvZgQEUlBHBAdDbGllbnQEDkxvY2FsaXplZFRleHQTAAAAAAAAAAAEB0xlbmd0aBMBAAAAAAAAAAQLZ2FtZU9iamVjdAQFbmFtZQQJSGludFRleHQEBXRleHQUYTxiPkZyZWVTUiBpcyBhIGZyZWUgc29mdHdhcmUuRnJlZVNS5piv5LiA5Liq5YWN6LS56L2v5Lu244CCIGh0dHBzOi8vZGlzY29yZC5nZy9yZXZlcnNlZHJvb21zPC9iPgEAAAABAAAAAAAcAAAAAQAAAAEAAAABAAAAAQAAAAEAAAABAAAAAwAAAAMAAAADAAAAAwAAAAMAAAADAAAAAwAAAAMAAAAEAAAABAAAAAQAAAAEAAAABAAAAAUAAAAFAAAABQAAAAUAAAAFAAAABgAAAAYAAAAEAAAACQAAAAYAAAAEb2JqBgAAABwAAAAHY29tcHRzDgAAABwAAAAMKGZvciBpbmRleCkSAAAAGwAAAAwoZm9yIGxpbWl0KRIAAAAbAAAACyhmb3Igc3RlcCkSAAAAGwAAAAJpEwAAABoAAAABAAAABV9FTlY=")
                },
                ClientTimeMs = heartbeatReq.ClientTimeMs,
                ServerTimeMs = (ulong)DateTimeOffset.Now.ToUnixTimeMilliseconds()
            });
        }

        [Handler(CmdType.CmdGetHeroBasicTypeInfoCsReq)]
        public static void OnGetHeroBasicTypeInfoCsReq(NetSession session, int cmdId, object _)
        {
            session.Send(CmdType.CmdGetHeroBasicTypeInfoScRsp, new GetHeroBasicTypeInfoScRsp
            {
                Retcode = 0,
                Gender = Gender.GenderMan,
                BasicTypeInfoLists ={
                    new PlayerHeroBasicTypeInfo
                    {
                        BasicType = HeroBasicType.BoyWarrior,
                        Rank = 1,
                        Knhaecbafbas = {}
                    }
                },
                CurBasicType = HeroBasicType.BoyWarrior,
            });
        }

        [Handler(CmdType.CmdGetBasicInfoCsReq)]
        public static void OnGetBasicInfoCsReq(NetSession session, int cmdId, object _)
        {
            session.Send(CmdType.CmdGetBasicInfoScRsp, new GetBasicInfoScRsp
            {
                CurDay = 1,
                ExchangeTimes = 0,
                Retcode = 0,
                NextRecoverTime = 2281337,
                WeekCocoonFinishedCount = 0
            });
        }

        [Handler(CmdType.CmdPlayerLoginCsReq)]
        public static void OnPlayerLoginCsReq(NetSession session, int cmdId, object data)
        {
            var request = data as PlayerLoginCsReq;

            session.Send(CmdType.CmdPlayerLoginScRsp, new PlayerLoginScRsp
            {
                Retcode = 0,
                //IsNewPlayer = false,
                LoginRandom = request.LoginRandom,
                Stamina = 240,
                ServerTimestampMs = (ulong)DateTimeOffset.Now.ToUnixTimeSeconds() * 1000,
                BasicInfo = new PlayerBasicInfo
                {
                    Nickname = "xeondev",
                    Level = 70,
                    Exp = 0,
                    Stamina = 100,
                    Mcoin = 0,
                    Hcoin = 0,
                    Scoin = 0,
                    WorldLevel = 6
                }
            });
        }

        [Handler(CmdType.CmdPlayerGetTokenCsReq)]
        public static void OnPlayerGetTokenCsReq(NetSession session, int cmdId, object data)
        {
            session.Send(CmdType.CmdPlayerGetTokenScRsp, new PlayerGetTokenScRsp
            {
                Retcode = 0,
                Uid = 114514,
                //BlackInfo = null,
                Msg = "OK",
                SecretKeySeed = 0
            });
        }
    }
}
