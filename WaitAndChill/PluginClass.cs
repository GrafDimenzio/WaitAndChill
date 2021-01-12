using Synapse.Api.Plugin;

namespace WaitAndChill
{
    [PluginInformation(
        Name = "WaitAndChill",
        Author = "Dimenzio (Original: F4Fridey)",
        LoadPriority = int.MinValue,
        Description = "A Plugin which spawns player while waiting for Players",
        SynapseMajor = 2,
        SynapseMinor = 4,
        SynapsePatch = 2,
        Version = "v.1.1.0"
        )]
    public class PluginClass : AbstractPlugin
    {
        [Synapse.Api.Plugin.Config(section = "WaitAndChill")]
        public static Config Config;

        public override void Load() => new EventHandlers();
    }
}
