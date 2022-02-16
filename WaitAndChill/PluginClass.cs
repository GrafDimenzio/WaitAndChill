using Synapse.Api.Plugin;
using Synapse.Translation;

namespace WaitAndChill
{
    [PluginInformation(
        Name = "WaitAndChill",
        Author = "Dimenzio (Original: F4Fridey)",
        LoadPriority = 0,
        Description = "A Plugin which spawns player while waiting for Players",
        SynapseMajor = 2,
        SynapseMinor = 9,
        SynapsePatch = 0,
        Version = "v.1.2.1"
        )]
    public class PluginClass : AbstractPlugin
    {
        [Config(section = "WaitAndChill")]
        public static PluginConfig Config;

        [SynapseTranslation]
        public static new SynapseTranslation<PluginTranslation> Translation { get; set; }

        public override void Load()
        {
            Translation.AddTranslation(new PluginTranslation());
            Translation.AddTranslation(new PluginTranslation
            {
                LobbyText = "<i><color=blue>Warten auf mehr Spieler!</color></i>\\n<i><color=red>%players%/%slots%</color></i>\\n<i>%status%</color></i>",
                StatusNoPlayer = "",
                StatusStart = "Runde startet",
                StatusWaiting = "Es sind noch %seconds% Sekunden übrig"
            }, "GERMAN");

            new EventHandlers();
        }
    }
}
