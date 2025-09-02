using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;

namespace RPF.Events.Misc
{
    public class FemurBreakerEvent
    {
        private readonly Config _config;

        private Door _entranceDoor;
        private Door _chamberDoor;
        private bool _isRunning;
        private CancellationTokenSource _monitorCts;
        private bool _doorUnlockedByGenerators;

        public FemurBreakerEvent(Config config)
        {
            this._config = config ?? throw new ArgumentNullException(nameof(config));
        }
        
        public void Register()
        {
            Exiled.Events.Handlers.Player.InteractingDoor += OnDoorInteract;
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
        }

        public void Unregister()
        {
            Exiled.Events.Handlers.Player.InteractingDoor -= OnDoorInteract;
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;

            _monitorCts?.Cancel();
            _monitorCts?.Dispose();
            _monitorCts = null;
        }

        private void OnRoundStarted()
        {
            _isRunning = false;
            _doorUnlockedByGenerators = false;

            var hcz106Doors = Door.List.Where(d =>
                d.Room?.Type == RoomType.Hcz106 ||
                (d.Rooms?.Any(r => r.Type == RoomType.Hcz106) ?? false)).ToList();

            _entranceDoor = hcz106Doors.FirstOrDefault(d => d.Type == DoorType.Scp106Primary);
            _chamberDoor = hcz106Doors.FirstOrDefault(d => d.Type == DoorType.Scp106Secondary);

            if (_entranceDoor != null)
            {
                _entranceDoor.IsOpen = false;
                _entranceDoor.ChangeLock(DoorLockType.AdminCommand);
            }

            if (_chamberDoor != null)
            {
                _chamberDoor.IsOpen = false;
                _chamberDoor.ChangeLock(DoorLockType.AdminCommand);
            }

            _monitorCts?.Cancel();
            _monitorCts?.Dispose();
            _monitorCts = new CancellationTokenSource();
            _ = MonitorGeneratorsAsync(_monitorCts.Token);
        }

        private async Task MonitorGeneratorsAsync(CancellationToken ct)
        {
            try
            {
                while (!ct.IsCancellationRequested && Round.IsStarted)
                {
                    int active = CountActiveGenerators();
                    if (!_doorUnlockedByGenerators && active >= _config.GeneratorsRequired)
                    {
                        _doorUnlockedByGenerators = true;

                        _entranceDoor?.ChangeLock(DoorLockType.None);
                        _chamberDoor?.ChangeLock(DoorLockType.None);

                        Log.Info("[FemurBreaker] Generators threshold reached: SCP-106 doors unlocked.");
                        break;
                    }

                    await Task.Delay(1000, ct);
                }
            }
            catch (TaskCanceledException) { }
            catch (Exception ex)
            {
                Log.Error($"[FemurBreaker] MonitorGeneratorsAsync error: {ex}");
            }
        }

        private int CountActiveGenerators()
        {
            try
            {
                return Exiled.API.Features.Generator.List.Count(g => g.IsEngaged);
            }
            catch
            {
                try
                {
                    foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        var generatorType = asm.GetType("Exiled.API.Features.Generator") ?? asm.GetType("Generator");
                        if (generatorType == null)
                            continue;

                        var listProp = generatorType.GetProperty("List", BindingFlags.Static | BindingFlags.Public);
                        if (listProp == null)
                            continue;

                        var listObj = listProp.GetValue(null) as System.Collections.IEnumerable;
                        if (listObj == null)
                            continue;

                        int count = 0;
                        foreach (var item in listObj)
                        {
                            var isEngagedProp = item.GetType().GetProperty("IsEngaged", BindingFlags.Instance | BindingFlags.Public);
                            if (isEngagedProp != null)
                            {
                                var val = isEngagedProp.GetValue(item);
                                if (val is bool b && b) count++;
                            }
                            else
                            {
                                var isEngagedField = item.GetType().GetField("isEngaged", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                                if (isEngagedField != null)
                                {
                                    var val = isEngagedField.GetValue(item);
                                    if (val is bool b && b) count++;
                                }
                            }
                        }

                        return count;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"[FemurBreaker] Reflection count failed: {ex}");
                }

                return 0;
            }
        }

        private void OnDoorInteract(InteractingDoorEventArgs ev)
        {
            if (ev.Door == null)
                return;

            if (ev.Door != _entranceDoor && ev.Door != _chamberDoor)
                return;

            if (!_doorUnlockedByGenerators && CountActiveGenerators() < _config.GeneratorsRequired)
            {
                ev.Player.ShowHint("<color=red>The cell is Blocked! Activate all Generators!</color>", 3);
                ev.IsAllowed = false;
                return;
            }

            if (_config.OnlyHumansCanTrigger && (ev.Player.Role.Team == Team.SCPs || ev.Player.Role.Team == Team.Dead))
                return;

            if (!_isRunning)
                _ = RunFemurBreaker();
        }

        public async Task RunFemurBreaker()
        {
            _isRunning = true;

            Log.Info("[FemurBreaker] Activated: playing ambient sound.");
            Map.PlayAmbientSound(28);

            await Task.Delay(_config.FemurBreakerDelay);

            var scp106 = Player.List.FirstOrDefault(p => p.IsAlive && p.Role.Type == RoleTypeId.Scp106);
            if (scp106 != null)
            {
                scp106.Kill("Killed by FemurBreaker");
                Log.Info("[FemurBreaker] SCP-106 neutralized.");
            }

            _isRunning = true;
        }
    }
}
