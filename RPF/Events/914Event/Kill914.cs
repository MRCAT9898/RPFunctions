using Exiled.Events.EventArgs.Scp914;

namespace RPF.Events._914Event;

public class Kill914
{
    private void OnPlayerUpgrade(UpgradingPlayerEventArgs ev)
    {
        ev.Player.Kill("Corroded by SCP-914.");
    }

    public void Register()
    {
        Exiled.Events.Handlers.Scp914.UpgradingPlayer += OnPlayerUpgrade;
    }

    public void Unregister()
    {
        Exiled.Events.Handlers.Scp914.UpgradingPlayer -= OnPlayerUpgrade;
    }
}