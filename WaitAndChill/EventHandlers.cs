using MEC;
using Synapse;
using Synapse.Api;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace WaitAndChill
{
    public class EventHandlers
    {
        public EventHandlers() => Server.Get.Events.Round.WaitingForPlayersEvent += OnWaiting;

        private void OnWaiting()
        {
            GameObject.Find("StartRound").transform.localScale = Vector3.zero;
            Timing.RunCoroutine(WaitingForPlayers());


            var component = Server.Get.Host.ClassManager;
            NetworkWriter writer = NetworkWriterPool.GetWriter();
            component.SendRPCInternal(typeof(global::CharacterClassManager), "RpcRoundStarted", writer, 0);
            NetworkWriterPool.Recycle(writer);
        }

        private IEnumerator<float> WaitingForPlayers()
        {
            while (!Map.Get.Round.RoundIsActive)
            {
                Map.Get.RespawnPoint = PluginClass.Config.LobbySpawn.Parse().Position;

                var newmsg = PluginClass.Config.LobbyText.Replace("%players%", Server.Get.Players.Count.ToString()).Replace("%slots%", Server.Get.Slots.ToString());

                switch (GameCore.RoundStart.singleton.NetworkTimer)
                {
                    case -2:
                        newmsg = newmsg.Replace("%status%", "");
                        break;

                    case -1:
                        newmsg = newmsg.Replace("%status%", "Round is starting");
                        break;

                    default:
                        newmsg = newmsg.Replace("%status%", $"{GameCore.RoundStart.singleton.NetworkTimer} seconds left");
                        break;
                }

                foreach (var player in Server.Get.Players)
                {
                    if ((player.RoleType == RoleType.None || player.RoleType == RoleType.Spectator) && player.Hub.Ready)
                        player.RoleID = (int)RoleType.Tutorial;

                    player.GiveTextHint(newmsg,1f);
                }

                Map.Get.RespawnPoint = Vector3.zero;
                yield return Timing.WaitForSeconds(1f);
            }
        }

    }
}
