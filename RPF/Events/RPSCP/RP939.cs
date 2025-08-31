using Exiled.Events.EventArgs.Player;
using PlayerRoles;

namespace RPF.Events.RPSCP
{
    public class NoElevatorFor939
    {
        public void RegisterEvents()
        {
            Exiled.Events.Handlers.Player.InteractingDoor += OnInteractingDoor;
        }

        public void UnregisterEvents()
        {
            Exiled.Events.Handlers.Player.InteractingDoor -= OnInteractingDoor;
        }

        private void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (ev.Player.Role.Type == RoleTypeId.Scp939)
            {
                if (ev.Door.Type.ToString().Contains("Elevator"))
                {
                    ev.IsAllowed = false;
                    ev.Player.ShowHint(Config.ScpRpFunctions939);
                }
            }
        }
    }
}