using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using PlayerRoles;
using UnityEngine;

namespace RPF.Commands.Client
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class FemurActivator : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
        {
            var player = Player.Get(sender);
            
            if (Main.Instance?.FemurBreaker == null)
            {
                response = "FemurBreaker non initialized.";
                return false;
            }
            
            string roomName = player.CurrentRoom.Name.ToLower();
            if (!roomName.Contains("106"))
            {
                response = "You can Only do that in 106 room!";
                return false;
            }
            
            response = "Running Femour Breaker...";
            _ = Main.Instance.FemurBreaker.RunFemurBreaker();
            Task.Run(Extetic);
            
            return true;
        }
        
        RoleTypeId[] _humanRoles =
        {
            RoleTypeId.ClassD,
            RoleTypeId.Scientist,
            RoleTypeId.FacilityGuard,
            RoleTypeId.NtfPrivate,
            RoleTypeId.NtfSergeant,
            RoleTypeId.NtfSpecialist,
            RoleTypeId.NtfCaptain,
            RoleTypeId.ChaosConscript,
            RoleTypeId.ChaosRifleman,
            RoleTypeId.ChaosRepressor,
            RoleTypeId.ChaosMarauder
        };
        
        private static async Task Extetic()
        {
            try
            {
                Map.ChangeLightsColor(Color.red);
                Cassie.Message(
                    "ACTIVING FEMUR BREAKER",
                    isNoisy: false,
                    isSubtitles: true
                );
                await Task.Delay(3000);
                Cassie.Message(
                    "SCP 106 SUCCEFULLY TERMINATED",
                    isNoisy: false,
                    isSubtitles: true
                );
                Map.ChangeLightsColor(Color.green);
                await Task.Delay(1000);
                Map.ChangeLightsColor(Color.white);
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured: {ex}");
            }
        }
        
        public string Command { get; } = Main.Instance.Config.femur_command;
        public string[] Aliases { get; } = [ "femur" ];
        public string Description { get; } = "Activate Femur Event.";
    }    
}
