using Synapse.Translation;

namespace WaitAndChill
{
    public class PluginTranslation : IPluginTranslation
    {
        public string LobbyText { get; set; } = "<i><color=blue>Waiting for more Players!</color></i>\\n<i><color=red>%players%/%slots%</color></i>\\n<i>%status%</color></i>";
        public string StatusStart { get; set; } = "Round is starting";
        public string StatusWaiting { get; set; } = "%seconds% seconds left";
        public string StatusNoPlayer { get; set; } = "";
    }
}
