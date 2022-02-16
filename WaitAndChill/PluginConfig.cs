using Synapse.Config;
using System.Collections.Generic;

namespace WaitAndChill
{
    public class PluginConfig : AbstractConfigSection
    {
        public List<SerializedMapPoint> SpawnPoint { get; set; } = new List<SerializedMapPoint> { new SerializedMapPoint("EZ_Shelter", 0f, 2f, 0f) };

        public List<RoleType> Roles { get; set; } = new List<RoleType> { RoleType.Tutorial };
    }
}
