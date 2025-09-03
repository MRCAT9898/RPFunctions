using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using CommandSystem;
using PlayerRoles;
using PluginAPI.Core;
using UnityEngine;
using Log = PluginAPI.Core.Log;
using Map = PluginAPI.Core.Map;

namespace RPF.Commands.Client
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Overload : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
        {
            var player = Exiled.API.Features.Player.Get(sender);
            
            if (player.Role.Type != RoleTypeId.Scp079)
            {
                response = "You must be Scp 079";
                return false;
            }

            if (_usedThisRound)
            {
                response = "You cannot do this command anymore you had alredy done it!";
                return false;
            }
            
            _usedThisRound = true;
            
            response = "Overload in progress...";
            Task.Run(async () =>
                {
                    await FlickerLights();
                    await LightsColor();
                });
            
            return true;
            
        }

        private static async Task FlickerLights()
        {
            if (Main.Instance.Config.EnableOverloadCommand != true) return;
                try
                {
                    Map.FlickerAllLights(5f);
                    await Task.Delay(500);
                }
                catch (Exception ex)
                {
                    Log.Error($"[FlickerLights] error in executing command: {ex}");
                }
        }

        private static async Task LightsColor()
        {
            if (Main.Instance.Config.EnableOverloadCommand != true) return;
                try
                {
                    Map.ChangeColorOfAllLights(Color.red);
                    await Task.Delay(500);
                    Cassie.Message(
                        Main.Instance.Config.Overload079Cassie,
                        isNoisy: false,
                        isSubtitles: true
                        );
                }
                catch (Exception ex)
                {
                    Log.Error($"[DoorLocksColor] error in executing command: {ex}");
                }
            
        }
        
        private static bool _usedThisRound = false;
        
        public string Command => Main.Instance.Config.OverloadCommand;
        public string[] Aliases => ["Overload"];
        public string Description => "Command for 079";
    }
}
