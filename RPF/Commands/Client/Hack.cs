using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles;
using UnityEngine;
using Cassie = PluginAPI.Core.Cassie;
using Log = PluginAPI.Core.Log;
using Map = PluginAPI.Core.Map;

namespace RPF.Commands.Client
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Hack : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
        {
            var player = Player.Get(sender);
            if (player.Role.Type != RoleTypeId.ChaosRepressor)
            {
                response = "You MUST at least be a ChaosRepressor role!";
                return false;
            }
            
            if (player.CurrentRoom.Type != RoomType.Hcz096)
            {
                response = "You MUST be IN the SCP 066 room!";
                return false;
            }

            if (_usedThisRound)
            {
                response = "You or someone has alredy used the command!";
                return false;
            }
            
            _usedThisRound = true;
            response = "Executing the hacking device...";

            Task.Run(async () =>
            {
                await FirstPart();
                await SecondPart();
                await ThirdPart();
            });
            
            return true;


        }

        private static async Task FirstPart()
        {
            try
            {
                Map.ChangeColorOfAllLights(Color.red);
                await Task.Delay(500);
                Cassie.Message(
                    Main.Instance.Config.HackCommandCassieHacked,
                    isNoisy: false,
                    isSubtitles: true
                );
            }
            catch (Exception ex)
            {
                Log.Error($"Error during the first-part command: {ex}");
            }
        }

        private static async Task SecondPart()
        {
            try
            {
                Map.ChangeColorOfAllLights(Color.yellow);
                await Task.Delay(500);
                Cassie.Message(
                    Main.Instance.Config.HackCommandCassieResolving,
                    isNoisy: false,
                    isSubtitles: true
                );
            }
            catch (Exception ex)
            {
                Log.Error($"Error during the second-part command: {ex}");
            }
        }

        private static async Task ThirdPart()
        {
            try
            {
                Map.ChangeColorOfAllLights(Color.magenta);
                await Task.Delay(500);
                Cassie.Message(
                    Main.Instance.Config.HackCommandCassieFinalMessage,
                    isNoisy: false,
                    isSubtitles: true
                );
            }
            catch (Exception ex)
            {
                Log.Error($"Error during the third-part command: {ex}");
            }
        }
        
        private static bool _usedThisRound = false;
        
        public string Command => "Hack";
        public string[] Aliases => ["hack"];
        public string Description => "Hacks the entire Facily";
    }
}
