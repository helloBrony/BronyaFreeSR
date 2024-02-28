namespace FreeSR.Gateserver.Manager.Handlers
{
    using FreeSR.Gateserver.Manager.Handlers.Core;
    using FreeSR.Gateserver.Network;
    using FreeSR.Proto;
    using System.ComponentModel.Design;

    internal static class NPCReqGroup
    {
        //maybe useless
        [Handler(CmdType.CmdGetNpcTakenRewardCsReq)]
        public static void OnGetNpcTakenRewardCsReq(NetSession session, int cmdId, object data)
        {

            var npcRewardReq = data as GetNpcTakenRewardCsReq;

            session.Send(CmdType.CmdGetNpcTakenRewardScRsp, new GetNpcTakenRewardScRsp
            {
                NpcId = npcRewardReq.NpcId,
                Retcode = 0
            });
        }

        [Handler(CmdType.CmdGetFirstTalkByPerformanceNpcCsReq)]
        public static void OnGetFirstTalkByPerformanceNpcCsReq(NetSession session, int cmdId, object data)
        {
            var npcPerformanceReq = data as GetFirstTalkByPerformanceNpcCsReq;

            var response = new GetFirstTalkByPerformanceNpcScRsp
            {
                Retcode = 0
            };

            foreach(uint id in npcPerformanceReq.FirstTalkIdLists)
            {
                    response.NpcMeetStatusLists.Add(new NpcMeetStatusInfo
                    {
                        IsMeet = true,
                        MeetId = id
                    });
                
            }
            
            session.Send(CmdType.CmdGetFirstTalkByPerformanceNpcScRsp, response);
        }
    }
}
