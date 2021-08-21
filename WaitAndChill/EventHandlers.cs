using System.Collections.Generic;
using MEC;
using Synapse;
using Synapse.Api;
using UnityEngine;
using System.Linq;
using System;

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
            while (!Map.Get.Round.RoundIsActive)
            {
                try
                {
                    Map.Get.RespawnPoint = PluginClass.Config.SpawnPoint.ElementAt(UnityEngine.Random.Range(0, PluginClass.Config.SpawnPoint.Count)).Parse().Position;
                }
                catch(Exception e)
                {
                    Synapse.Api.Logger.Get.Error($"Error while parsing a spawnpoint:\n{e}");
                    break;
                }

                var newmsg = PluginClass.Translation.ActiveTranslation.LobbyText.Replace("%players%", Server.Get.Players.Count.ToString()).Replace("%slots%", Server.Get.Slots.ToString()).Replace("\\n","\n");

                switch (GameCore.RoundStart.singleton.NetworkTimer)
                {
                    case -2:
                        newmsg = newmsg.Replace("%status%", PluginClass.Translation.ActiveTranslation.StatusNoPlayer);
                        break;

                    case -1:
                        newmsg = newmsg.Replace("%status%", PluginClass.Translation.ActiveTranslation.StatusStart);
                        break;

                    default:
                        newmsg = newmsg.Replace("%status%", PluginClass.Translation.ActiveTranslation.StatusWaiting.Replace("%seconds%", GameCore.RoundStart.singleton.NetworkTimer.ToString()));
                        break;
                }

                foreach (var player in Server.Get.Players)
                {
                    try
                    {
                        if ((player.RoleType == RoleType.None || player.RoleType == RoleType.Spectator) && player.Hub.Ready)
                            player.RoleID = (int)PluginClass.Config.Roles.ElementAt(UnityEngine.Random.Range(0, PluginClass.Config.Roles.Count));

                        player.GiveTextHint(newmsg, 1f);
                    }
                    catch(Exception e)
                    {
                        Synapse.Api.Logger.Get.Error($"Error while setting the role of player:\n{e}");
                    }
                }

                Map.Get.RespawnPoint = Vector3.zero;
                yield return Timing.WaitForSeconds(1f);
            }
        }

    }
}
