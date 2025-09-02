using Exiled.API.Extensions;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;

namespace RPF.Events.RPSCP
{
    public class NoElevatorFor939
    {
        public void RegisterEvents()
        {
            Exiled.Events.Handlers.Player.InteractingElevator += OnInteractingDoor;
        }

        public void UnregisterEvents()
        {
            Exiled.Events.Handlers.Player.InteractingElevator -= OnInteractingDoor;
        }

        private void OnInteractingDoor(InteractingElevatorEventArgs ev)
        {
            if (ev.Player.Role.Type == RoleTypeId.Scp939)
            {
                ev.IsAllowed = false;
                ev.Player.ShowHint(Main.Instance.Config.ScpRpFunctions939);
            }
        }
    }
}