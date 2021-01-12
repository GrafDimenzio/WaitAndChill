using Synapse.Config;

namespace WaitAndChill
{
    public class Config : AbstractConfigSection
    {
        public SerializedMapPoint LobbySpawn = new SerializedMapPoint("EZ_Shelter", 0f, 2f, 0f);

        public string LobbyText = "<i><color=blue>Waiting for more Players!</color></i>\n<i><color=red>%players%/%slots%</color></i>\n<i>%status%</color></i>";
    }
}
