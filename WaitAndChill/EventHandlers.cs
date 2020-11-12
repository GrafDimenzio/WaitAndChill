using MEC;
using Synapse;
using Synapse.Api;
using System.Collections.Generic;
using UnityEngine;

namespace WaitAndChill
{
    public class EventHandlers
    {
        public EventHandlers() => Server.Get.Events.Round.WaitingForPlayersEvent += OnWaiting;

        private void OnWaiting()
        {
            GameObject.Find("StartRound").transform.localScale = Vector3.zero;
            Timing.RunCoroutine(WaitingForPlayers());
        }

        private IEnumerator<float> WaitingForPlayers()
        {
            var msg = $"<i><color=blue>Waiting for more Players!</color></i>\n<i><color=red>%players%/{CustomNetworkManager.slots}</color></i>\n<i>%status%</color></i>";

            foreach (var door in Map.Get.Doors)
                door.Locked = true;

            while (!Map.Get.Round.RoundIsActive)
            {
                Map.Get.RespawnPoint = PluginClass.Config.LobbySpawn.Parse().Position;

                var newmsg = msg.Replace("%players%", Server.Get.Players.Count.ToString());
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
                    if (player.RoleType == RoleType.None && player.Hub.Ready)
                        player.RoleID = (int)RoleType.Tutorial;

                    player.GiveTextHint(newmsg,1f);
                }

                Map.Get.RespawnPoint = Vector3.zero;
                yield return Timing.WaitForSeconds(1f);
            }

            foreach (var door in Map.Get.Doors)
                door.Locked = false;
        }

    }
}
