using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
using System.Collections.Generic;
using UserSettings.ServerSpecific;

namespace SSS
{
    public class SSS
    {
        internal static IEnumerable<SettingBase> _settings;
        internal static IEnumerable<string> _dropdownInputs;
        //Patched: 1.2.0
        public static Dictionary<string, bool> IsCustomRolesAllowed = new Dictionary<string, bool>();


        public static void Register()
        {

            _settings =
            [
                new HeaderSetting("RPFunctions Settings", "RPFunctions Ufficial Settings", true),
                new TwoButtonsSetting(22, "Exclude from Custom Roles.", "True", "False"),
                
                
            ];
            SettingBase.Register(_settings);
        }

        
        public static void Unregister()
        {
            
        }

        public void TwoButtonExample(Player player)
        {
            //patched: in 1.2.0
            SSTwoButtonsSetting twoButtonsSetting = ServerSpecificSettingsSync.GetSettingOfUser<SSTwoButtonsSetting>(player.ReferenceHub, 22);
            if (twoButtonsSetting.SyncIsA)
            {
                IsCustomRolesAllowed[player.UserId] = true;
            }
            else if (twoButtonsSetting.SyncIsB)
            {
                IsCustomRolesAllowed[player.UserId] = false;
            }
        }

        
    }
}