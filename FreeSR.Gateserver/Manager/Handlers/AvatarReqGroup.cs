namespace FreeSR.Gateserver.Manager.Handlers {
    using FreeSR.Gateserver.Manager.Handlers.Core;
    using FreeSR.Gateserver.Network;
    using FreeSR.Proto;

    internal static class AvatarReqGroup {
        [Handler(CmdType.CmdGetAvatarDataCsReq)]
        public static void OnGetAvatarDataCsReq(NetSession session, int cmdId, object data) {
            var request = data as GetAvatarDataCsReq;

            var response = new GetAvatarDataScRsp {
                Retcode = 0,
                IsAll = request.IsGetAll
            };

            uint[] characters = new uint[] { 8001,8002,8003,8004,
                                             1001,1002,1003,1004,1005,1006,1008,1009,1013,
                                             1101,1102,1103,1104,1105,1106,1107,1108,1109,1110,1111,1112,
                                             1201,1202,1203,1204,1205,1206,1207,1208,1209,1210,1211,1212,1213,1214,1215,1217,
                                             1301,1302,1303,1304,1305,1306,1307,1308,1312};

            foreach (uint id in characters) {
                var avatarData = new Avatar {
                    BaseAvatarId = id,
                    Exp = 0,
                    Level = 80,
                    Promotion = 6,
                    Rank = 6,
                    EquipmentUniqueId = 0,
                    HasTakenPromotionRewardLists = new uint[] { 1, 3, 5 }
                };
                List<uint> SkillIdEnds = new List<uint> { 1, 2, 3, 4, 7, 101, 102, 103, 201, 202, 203, 204, 205, 206, 207, 208, 209, 210 };
                foreach (uint end in SkillIdEnds) {
                    uint level;
                    if (end == 1) {
                        level = 6;
                    } else if (end == 2 || end == 3 || end == 4) {
                        level = 10;
                    } else {
                        level = 1;
                    }
                    avatarData.SkilltreeLists.Add(new AvatarSkillTree {
                        PointId = id * 1000 + end,
                        Level = level
                    });
                }

                response.AvatarLists.Add(avatarData);
            }

            session.Send(CmdType.CmdGetAvatarDataScRsp, response);
        }
    }
}
