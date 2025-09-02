using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;

namespace RPF.Events.TeslaGate;

public class TeslaConditions
{
    public void OnTeslaActivated(TriggeringTeslaEventArgs ev)
    {
        if (!Main.Instance.Config.TeslaConditions)
            return;
        if (ev.Player.Role.Team == Team.FoundationForces)
        {
            ev.IsAllowed = false;
            ev.Player.ShowHint("Tesla disabled for member of the foundation.");
        }
    }
    
    public void Register()
    {
        Exiled.Events.Handlers.Player.TriggeringTesla += OnTeslaActivated;
    }

    public void Unregister()
    {
        Exiled.Events.Handlers.Player.TriggeringTesla -= OnTeslaActivated;
    }
}