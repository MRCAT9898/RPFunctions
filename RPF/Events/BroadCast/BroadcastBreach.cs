using System;
using System.Threading.Tasks;
using PluginAPI.Core;

namespace RPF.Events.BroadCast
{
    public class BroadCastBreach
    {
        private static async Task FlickerAllLights()
        {
            try
            {
                for (var i = 60; i >= 0; i--)
                {
                    Map.FlickerAllLights(1f);
                    await Task.Delay(500);
                }
                
            }
            catch (Exception ex)
            {
                Log.Error($"[FlickerLights] Errore: {ex}");
            }
            
        }

        private static Task CassieBreach()
        {
            Cassie.Message(
                Config.MainBroadCastMessage,
                isNoisy: false,
                isSubtitles: true
            );
            return Task.CompletedTask;
        }
        

        public void OnRoundStarted()
        {
            Task.Run(FlickerAllLights);
            Task.Run(CassieBreach);
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