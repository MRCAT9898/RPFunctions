using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;

namespace RPF.Events.RPSCP
{
    public class Scp096ElevatorRestriction
    {
        private readonly Dictionary<Player, bool> _isRaging = new Dictionary<Player, bool>();
        
        public void RegisterEvents()
        {
            Exiled.Events.Handlers.Player.InteractingDoor += OnInteractingDoor;
            Exiled.Events.Handlers.Player.ChangingRole += OnChangingRole;
            Exiled.Events.Handlers.Player.Spawning += OnPlayerSpawn;
        }
        
        public void UnregisterEvents()
        {
            Exiled.Events.Handlers.Player.InteractingDoor -= OnInteractingDoor;
            Exiled.Events.Handlers.Player.ChangingRole -= OnChangingRole;
            Exiled.Events.Handlers.Player.Spawning -= OnPlayerSpawn;
        }
        
        private void OnPlayerSpawn(SpawningEventArgs ev)
        {
            if (_isRaging.ContainsKey(ev.Player))
                _isRaging[ev.Player] = false;
            else
                _isRaging.Add(ev.Player, false);
        }
        
        private void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.NewRole == RoleTypeId.Scp096)
            {
                if (_isRaging.ContainsKey(ev.Player))
                    _isRaging[ev.Player] = false;
                else
                    _isRaging.Add(ev.Player, false);
            }
        }
        
        public void SetRage(Player player, bool value)
        {
            if (_isRaging.ContainsKey(player))
                _isRaging[player] = value;
            else
                _isRaging.Add(player, value);
        }
        
        private void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            var player = ev.Player;
            var door = ev.Door;

            if (player.Role.Type != RoleTypeId.Scp096)
                return;

            bool isRaging = _isRaging.ContainsKey(player) && _isRaging[player];

            if (door.Type.ToString().Contains("Elevator") && !isRaging)
            {
                ev.IsAllowed = false;
                player.ShowHint(Config.ScpRpFunctions096);
            }
        }
    }
}
