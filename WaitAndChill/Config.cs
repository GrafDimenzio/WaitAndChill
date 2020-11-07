using Synapse.Config;

namespace WaitAndChill
{
    public class Config : AbstractConfigSection
    {
        public SerializedMapPoint LobbySpawn = new SerializedMapPoint("EZ_Shelter", 0f, 2f, 0f);
    }
}
