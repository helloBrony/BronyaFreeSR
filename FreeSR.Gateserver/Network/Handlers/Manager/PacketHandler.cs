namespace FreeSR.Gateserver.Network.Handlers.Manager
{
    using DotNetty.Transport.Channels;
    using FreeSR.Gateserver.Manager;
    using FreeSR.Gateserver.Network.Packet;
    using FreeSR.Proto;
    using NLog;
    using ProtoBuf;

    internal class PacketHandler : ChannelHandlerAdapter
    {
        private static readonly Logger s_log = LogManager.GetCurrentClassLogger();
        private readonly NetSession _session;

        public PacketHandler(NetSession session)
        {
            _session = session;
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            NetPacket packet = message as NetPacket;
            if (packet.Data == null)
            {
                if (!SendDummyResponse(packet.CmdId))
                    s_log.Warn($"CmdID {packet.CmdId} is undefined.");

                return;
            }

            s_log.Info($"Received packet {packet.CmdId}!");
            NotifyManager.Notify(_session, packet.CmdId, packet.Data);
        }

        private bool SendDummyResponse(int id)
        {
            if (s_dummyTable.TryGetValue((CmdType)id, out CmdType rspId))
            {
                _session.Send(rspId, new DummyPacket());
                return true;
            }

            return false;
        }

        private static Dictionary<CmdType, CmdType> s_dummyTable = new Dictionary<CmdType, CmdType>
        {
            {CmdType.CmdGetLevelRewardTakenListCsReq, CmdType.CmdGetLevelRewardTakenListScRsp},
            {CmdType.CmdGetRogueScoreRewardInfoCsReq, CmdType.CmdGetRogueScoreRewardInfoScRsp},
            {CmdType.CmdGetGachaInfoCsReq, CmdType.CmdGetGachaInfoScRsp},
            {CmdType.CmdQueryProductInfoCsReq, CmdType.CmdQueryProductInfoScRsp},
            {CmdType.CmdGetQuestDataCsReq, CmdType.CmdGetQuestDataScRsp},
            {CmdType.CmdGetQuestRecordCsReq, CmdType.CmdGetQuestRecordScRsp},
            {CmdType.CmdGetFriendListInfoCsReq, CmdType.CmdGetFriendListInfoScRsp},
            {CmdType.CmdGetFriendApplyListInfoCsReq, CmdType.CmdGetFriendApplyListInfoScRsp},
            {CmdType.CmdGetCurAssistCsReq, CmdType.CmdGetCurAssistScRsp},
            {CmdType.CmdGetRogueHandbookDataCsReq, CmdType.CmdGetRogueHandbookDataScRsp},
            {CmdType.CmdGetDailyActiveInfoCsReq, CmdType.CmdGetDailyActiveInfoScRsp},
            {CmdType.CmdGetFightActivityDataCsReq, CmdType.CmdGetFightActivityDataScRsp},
            {CmdType.CmdGetMultipleDropInfoCsReq, CmdType.CmdGetMultipleDropInfoScRsp},
            {CmdType.CmdGetPlayerReturnMultiDropInfoCsReq, CmdType.CmdGetPlayerReturnMultiDropInfoScRsp},
            {CmdType.CmdGetShareDataCsReq, CmdType.CmdGetShareDataScRsp},
            {CmdType.CmdGetTreasureDungeonActivityDataCsReq, CmdType.CmdGetTreasureDungeonActivityDataScRsp},
            {CmdType.CmdPlayerReturnInfoQueryCsReq, CmdType.CmdPlayerReturnInfoQueryScRsp},
            {CmdType.CmdGetBasicInfoCsReq, CmdType.CmdGetBasicInfoScRsp},
            {CmdType.CmdGetHeroBasicTypeInfoCsReq, CmdType.CmdGetHeroBasicTypeInfoScRsp},
            {CmdType.CmdGetBagCsReq, CmdType.CmdGetBagScRsp},
            {CmdType.CmdGetPlayerBoardDataCsReq, CmdType.CmdGetPlayerBoardDataScRsp},
            {CmdType.CmdGetAvatarDataCsReq, CmdType.CmdGetAvatarDataScRsp},
            {CmdType.CmdGetAllLineupDataCsReq, CmdType.CmdGetAllLineupDataScRsp},
            {CmdType.CmdGetActivityScheduleConfigCsReq, CmdType.CmdGetActivityScheduleConfigScRsp},
            {CmdType.CmdGetMissionDataCsReq, CmdType.CmdGetMissionDataScRsp},
            {CmdType.CmdGetMissionEventDataCsReq, CmdType.CmdGetMissionEventDataScRsp},
            {CmdType.CmdGetChallengeCsReq, CmdType.CmdGetChallengeScRsp},
            {CmdType.CmdGetCurChallengeCsReq, CmdType.CmdGetCurChallengeScRsp},
            {CmdType.CmdGetRogueInfoCsReq, CmdType.CmdGetRogueInfoScRsp},
            {CmdType.CmdGetExpeditionDataCsReq, CmdType.CmdGetExpeditionDataScRsp},
            {CmdType.CmdGetRogueDialogueEventDataCsReq, CmdType.CmdGetRogueDialogueEventDataScRsp},
            {CmdType.CmdGetJukeboxDataCsReq, CmdType.CmdGetJukeboxDataScRsp},
            {CmdType.CmdSyncClientResVersionCsReq, CmdType.CmdSyncClientResVersionScRsp},
            {CmdType.CmdDailyFirstMeetPamCsReq, CmdType.CmdDailyFirstMeetPamScRsp},
            {CmdType.CmdGetMuseumInfoCsReq, CmdType.CmdGetMuseumInfoScRsp},
            {CmdType.CmdGetLoginActivityCsReq, CmdType.CmdGetLoginActivityScRsp},
            {CmdType.CmdGetRaidInfoCsReq, CmdType.CmdGetRaidInfoScRsp},
            {CmdType.CmdGetTrialActivityDataCsReq, CmdType.CmdGetTrialActivityDataScRsp},
            {CmdType.CmdGetBoxingClubInfoCsReq, CmdType.CmdGetBoxingClubInfoScRsp},
            {CmdType.CmdGetNpcStatusCsReq, CmdType.CmdGetNpcStatusScRsp},
            {CmdType.CmdTextJoinQueryCsReq, CmdType.CmdTextJoinQueryScRsp},
            {CmdType.CmdGetSpringRecoverDataCsReq, CmdType.CmdGetSpringRecoverDataScRsp},
            {CmdType.CmdGetChatFriendHistoryCsReq, CmdType.CmdGetChatFriendHistoryScRsp},
            {CmdType.CmdGetSecretKeyInfoCsReq, CmdType.CmdGetSecretKeyInfoScRsp},
            {CmdType.CmdGetVideoVersionKeyCsReq, CmdType.CmdGetVideoVersionKeyScRsp},
            {CmdType.CmdGetCurLineupDataCsReq, CmdType.CmdGetCurLineupDataScRsp},
            {CmdType.CmdGetCurBattleInfoCsReq, CmdType.CmdGetCurBattleInfoScRsp},
            {CmdType.CmdGetCurSceneInfoCsReq, CmdType.CmdGetCurSceneInfoScRsp},
            {CmdType.CmdGetPhoneDataCsReq, CmdType.CmdGetPhoneDataScRsp},
            {CmdType.CmdPlayerLoginFinishCsReq, CmdType.CmdPlayerLoginFinishScRsp}
        };

        [ProtoContract]
        private class DummyPacket { }
    }
}
