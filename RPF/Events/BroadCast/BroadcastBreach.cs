using System;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.API.Features.Doors;
using PluginAPI.Core;

namespace RPF.Events.BroadCast
{
    public class BroadCastBreach
    {
        private static async Task FlickerAllLights()
        {
            try
            {
                {
                    Map.FlickerAllLights(10f);
                    Door.LockAll(20, DoorLockType.Lockdown079);
                }
            }
            catch (Exception ex)
            {
                Log.Error($"[FlickerLights] Errore: {ex}");
            }
            
        }

        public void OnRoundStarted()
        {
            Task.Run(FlickerAllLights);
        }

        public void Register()
        {
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
        }

        public void Unregister()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
        }
    }

}