using Synapse.Api.Plugin;

namespace WaitAndChill
{
    [PluginInformation(
        Name = "WaitAndChill",
        Author = "Dimenzio (Original: F4Fridey)",
        LoadPriority = int.MinValue,
        Description = "A Plugin which spawns player while waiting for Players",
        SynapseMajor = SynapseController.SynapseMajor,
        SynapseMinor = SynapseController.SynapseMinor,
        SynapsePatch = SynapseController.SynapsePatch,
        Version = "v.1.0.1"
        )]
    public class PluginClass : AbstractPlugin
    {
        [Synapse.Api.Plugin.Config(section = "WaitAndChill")]
        public static Config Config;

        public override void Load() => new EventHandlers();
    }
}
