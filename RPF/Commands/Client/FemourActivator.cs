using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using CommandSystem;
using Exiled.API.Features;
using PlayerRoles;
using UnityEngine;

namespace RPF.Commands.Client
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class FemourActivator : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
        {
            var player = Player.Get(sender);
            if (!_humanRoles.Contains(player.Role.Type))
            {
                response = "You Must be a human to run this command.";
                return false;
            }
            
            if (Main.Instance?.FemurBreaker == null)
            {
                response = "FemurBreaker non inizializzato!";
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
                Map.ChangeLightsColor(Color.green);
                Cassie.Message(
                    "SCP 106 SUCCEFULLY TERMINATED",
                    isNoisy: false,
                    isSubtitles: true
                );
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured: {ex}");
            }
        }
        
        public string Command { get; } = ".femour";
        public string[] Aliases { get; } = [ "femour" ];
        public string Description { get; } = "Activate Femour Event.";
    }    
}
