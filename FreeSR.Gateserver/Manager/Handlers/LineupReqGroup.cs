namespace FreeSR.Gateserver.Manager.Handlers
{
    using FreeSR.Gateserver.Manager.Handlers.Core;
    using FreeSR.Gateserver.Network;
    using FreeSR.Proto;

    internal static class LineupReqGroup
    {
        public static uint Avatar1 = 1308;
        public static uint Avatar2 = 1006;
        public static uint Avatar3 = 1303;
        public static uint Avatar4 = 1217;
        [Handler(CmdType.CmdGetCurLineupDataCsReq)]
        public static void OnGetCurLineupDataCsReq(NetSession session, int cmdId, object _)
        {
            var response = new GetCurLineupDataScRsp
            {
                Retcode = 0
            };

            response.Lineup = new LineupInfo
            {
                ExtraLineupType = ExtraLineupType.LineupNone,
                Name = "队伍1",
                LeaderSlot = 0,
                Mp = 5,
                MaxMp = 5
            };

            var characters = new uint[] { Avatar1, Avatar2, Avatar3, Avatar4 };

            foreach (uint id in characters)
            {
                response.Lineup.AvatarLists.Add(new LineupAvatar
                {
                    AvatarType = AvatarType.AvatarFormalType,
                    Hp = 10000,
                    Sp = new AmountInfo { CurAmount = 10000,MaxAmount = 10000},
                    Satiety = 100,
                    Id = id,
                    Slot = (uint)response.Lineup.AvatarLists.Count
                });
            }

            session.Send(CmdType.CmdGetCurLineupDataScRsp, response);
        }

        [Handler(CmdType.CmdGetAllLineupDataCsReq)]
        public static void OnGetAllLineupDataCsReq(NetSession session, int cmdId, object data)
        {
            var response = new GetAllLineupDataScRsp
            {
                Retcode = 0,
                CurIndex = 0,
            };

            response.LineupLists.Add(new LineupInfo
            {
                ExtraLineupType = ExtraLineupType.LineupNone,
                Name = "Squad 1",
                Mp = 5,
                MaxMp = 5,
                LeaderSlot = 0
            });

            var characters = new uint[] { Avatar1, Avatar2, Avatar3, Avatar4 };

            foreach (uint id in characters)
            {
                response.LineupLists[0].AvatarLists.Add(new LineupAvatar
                {
                    AvatarType = AvatarType.AvatarFormalType,
                    Sp = new AmountInfo { CurAmount = 10000, MaxAmount = 10000 },
                    Hp = 10000,
                    Satiety = 100,
                    Id = id,
                    Slot = (uint)response.LineupLists[0].AvatarLists.Count
                });
            }

            session.Send(CmdType.CmdGetAllLineupDataScRsp, response);
        }

        [Handler(CmdType.CmdChangeLineupLeaderCsReq)]
        public static void OnChangeLineupLeaderCsReq(NetSession session, int cmdId, object data)
        {
            var request = data as ChangeLineupLeaderCsReq;
            session.Send(CmdType.CmdChangeLineupLeaderScRsp, new ChangeLineupLeaderScRsp
            {
                Slot = request.Slot,
                Retcode = 0
            });
        }

        [Handler(CmdType.CmdJoinLineupCsReq)]
        public static void OnJoinLineupCsReq(NetSession session, int cmdId, object data)
        {
            var request = data as JoinLineupCsReq;
            if (request.Slot == 0) Avatar1 = request.BaseAvatarId;
            if (request.Slot == 1) Avatar2 = request.BaseAvatarId;
            if (request.Slot == 2) Avatar3 = request.BaseAvatarId;
            if (request.Slot == 3) Avatar4 = request.BaseAvatarId;
            RefreshLineup(session);

            session.Send(CmdType.CmdJoinLineupScRsp, new JoinLineupScRsp
            {
                Retcode = 0
            });
        }

        [Handler(CmdType.CmdReplaceLineupCsReq)]
        public static void OnReplaceLineupCsReq(NetSession session, int cmdId, object data)
        {

            var request = data as ReplaceLineupCsReq;
            Avatar1 = 0; Avatar2 = 0; Avatar3 = 0; Avatar4 = 0;
            foreach (LineupSlotData slotData in request.LineupSlotLists)
            {
                if (slotData.Slot == 0) Avatar1 = slotData.Id;
                if (slotData.Slot == 1) Avatar2 = slotData.Id;
                if (slotData.Slot == 2) Avatar3 = slotData.Id;
                if (slotData.Slot == 3) Avatar4 = slotData.Id;
            }

            RefreshLineup(session);
            session.Send(CmdType.CmdReplaceLineupScRsp, new ReplaceLineupScRsp
            {
                Retcode = 0
            });
        }

        [Handler(CmdType.CmdQuitLineupCsReq)]
        public static void OnQuitLineupCsReq(NetSession session, int cmdId, object data)
        {
            var request = data as QuitLineupCsReq;
            if (request.BaseAvatarId == Avatar1) Avatar1 = 0;
            if (request.BaseAvatarId == Avatar2) Avatar2 = 0;
            if (request.BaseAvatarId == Avatar3) Avatar3 = 0;
            if (request.BaseAvatarId == Avatar4) Avatar4 = 0;

            RefreshLineup(session);
            session.Send(CmdType.CmdQuitLineupScRsp, new QuitLineupScRsp
            {
                Retcode = 0,
                BaseAvatarId = request.BaseAvatarId,
                IsVirtual = request.IsVirtual
            });
        }
        public static void RefreshLineup(NetSession session) {
            var characters = new uint[] { Avatar1, Avatar2, Avatar3, Avatar4 };
            var response = new SyncLineupNotify
            {
                Lineup = new LineupInfo
                {
                    ExtraLineupType = ExtraLineupType.LineupNone,
                    Name = "Squad 1",
                    Mp = 5,
                    MaxMp = 5,
                    LeaderSlot = 0
                }
            };
            foreach (uint id in characters)
            {
                if (id == 0) continue;
                response.Lineup.AvatarLists.Add(new LineupAvatar
                {
                    AvatarType = AvatarType.AvatarFormalType,
                    Sp = new AmountInfo { CurAmount = 10000, MaxAmount = 10000 },
                    Hp = 10000,
                    Satiety = 100,
                    Id = id,
                    Slot = (uint)response.Lineup.AvatarLists.Count
                });
            }
            session.Send(CmdType.CmdSyncLineupNotify, response);
        }
    }
}
