using Exiled.API.Extensions;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;

namespace RPF.Events.RPSCP
{
    public class NoDoorsFor106
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
            if (ev.Player.Role.Type == RoleTypeId.Scp106)
            {
                if (!ev.Door.Type.IsElevator())
                {
                    ev.IsAllowed = false;
                    ev.Player.ShowHint(Main.Instance.Config.ScpRpFunctions106);
                }
            }
        }
    }
}