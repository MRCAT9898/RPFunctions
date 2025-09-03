using System;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using PluginAPI.Core;
using Cassie = Exiled.API.Features.Cassie;
using Log = Exiled.API.Features.Log;
using Map = Exiled.API.Features.Map;

namespace RPF.Events.BroadCast
{
    public class BroadCastBreach
    {
        private void FlickerAllLights()
        {
            try
            {
                Map.TurnOffAllLights(10f);
                Door.LockAll(20, DoorLockType.Lockdown079);
                Cassie.Message("<i><b><align=center> bell_start pitch_0.4 .G4 .G4 .G5 pitch_0.9 .G4 <color=blue> Attention .G3 Attention .G4 <color=red> SCP ? ? ? <color=white> has not escaped out of the <color=red> containment  <color=white> pitch_0.4 .G4 pitch_0.9 repeat .G4 <color=red> SCP ? ? ? <color=white> has not <color=red> breached <color=white> the <color=red> containment bell_end", isNoisy: false, isSubtitles: true);
            }
            catch (Exception ex)
            {
                Log.Error($"[FlickerLights] Error: {ex}");
            }
        }

        public void OnRoundStarted()
        {
            if (Main.Instance.Config.Start_Annoucment != true) return;
            FlickerAllLights();
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