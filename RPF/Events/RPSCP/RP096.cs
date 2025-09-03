using System.Collections.Generic;
using Exiled.API.Extensions;
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
            Exiled.Events.Handlers.Player.InteractingElevator += OnInteractingDoor;
            Exiled.Events.Handlers.Player.ChangingRole += OnChangingRole;
            Exiled.Events.Handlers.Player.Spawning += OnPlayerSpawn;
        }
        
        public void UnregisterEvents()
        {
            Exiled.Events.Handlers.Player.InteractingElevator -= OnInteractingDoor;
            Exiled.Events.Handlers.Player.ChangingRole -= OnChangingRole;
            Exiled.Events.Handlers.Player.Spawning -= OnPlayerSpawn;
        }
        
        private void OnPlayerSpawn(SpawningEventArgs ev)
        {
            if (Main.Instance.Config.enable_096_functions != true) return;
            if (_isRaging.ContainsKey(ev.Player))
                _isRaging[ev.Player] = false;
            else
                _isRaging.Add(ev.Player, false);
        }
        
        private void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (Main.Instance.Config.enable_096_functions != true) return;
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
            if (Main.Instance.Config.enable_096_functions != true) return;
            if (_isRaging.ContainsKey(player))
                _isRaging[player] = value;
            else
                _isRaging.Add(player, value);
        }
        
        private void OnInteractingDoor(InteractingElevatorEventArgs ev)
        {
            if (Main.Instance.Config.enable_096_functions != true) return;
            var player = ev.Player;
            var door = ev.Elevator;

            if (player.Role.Type != RoleTypeId.Scp096)
                return;

            bool isRaging = _isRaging.ContainsKey(player) && _isRaging[player];
            
            ev.IsAllowed = false;
            player.ShowHint(Main.Instance.Config.ScpRpFunctions096);
        }
    }
}
