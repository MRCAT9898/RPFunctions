using System;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using PluginAPI.Core;

namespace RPF.Events.BroadCast
{
    public class ScpCastBreach
    {
        
        private void Breach079And939And106(SpawningEventArgs ev)
        {
            if (ev.OldRole.Type == RoleTypeId.Scp079)
            {
                Cassie.Message(
                    Config.BroadCastMessage079,
                    isNoisy: false,
                    isSubtitles: true
                    );
            }
            
            if (ev.OldRole.Type == RoleTypeId.Scp939)
            {
                Cassie.Message(
                    Config.BroadCastMessage939,
                    isNoisy: false,
                    isSubtitles: true
                );
            }
            
            if (ev.OldRole.Type == RoleTypeId.Scp106)
            {
                Cassie.Message(
                    Config.BroadCastMessage106,
                    isNoisy: false,
                    isSubtitles: true
                );
            }
        }
        
        private void ScpBreachDoors(InteractingDoorEventArgs ev)
        {
            if (ev.Door.Type == DoorType.Scp049Gate)
            {
                Cassie.Message
                (
                    Config.BroadCastMessage049,
                    isNoisy: false,
                    isSubtitles: true
                    );
            }
            
            if (ev.Door.Type == DoorType.Scp096)
            {
                Cassie.Message
                (
                    Config.BroadCastMessage096,
                    isNoisy: false,
                    isSubtitles: true
                );
            }
            
            if (ev.Door.Type == DoorType.Scp173NewGate)
            {
                Cassie.Message
                (
                    Config.BroadCastMessage173,
                    isNoisy: false,
                    isSubtitles: true
                );
            }
            
            
            if (ev.Door.Type == DoorType.Scp173Gate)
            {
                Cassie.Message
                (
                    Config.BroadCastMessage3114,
                    isNoisy: false,
                    isSubtitles: true
                );
            }
        }
        
        private static async Task FlickerLights()
        {
            try
            {
                for (var i = 60; i >= 0; i--)
                {
                    Map.FlickerAllLights(10f);
                    await Task.Delay(500);
                }

            }
            catch (Exception ex)
            {
                Exiled.API.Features.Log.Error($"[FlickerLights] Errore: {ex}");
            }
            
        }

        public void OnRoundStarted()
        {
            Task.Run(FlickerLights);
        }

        public void Register()
        {
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
            Exiled.Events.Handlers.Player.InteractingDoor += ScpBreachDoors;
            Exiled.Events.Handlers.Player.Spawning += Breach079And939And106;
        }

        public void Unregister()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
            Exiled.Events.Handlers.Player.InteractingDoor -= ScpBreachDoors;
            Exiled.Events.Handlers.Player.Spawning -= Breach079And939And106;
        }
        
    }
}
