using System;
using System.Linq;
using System.Threading.Tasks;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles;
using RemoteAdmin;
using GameGenerator = Exiled.API.Features.Generator;

namespace hcassie
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class HackCommand : ICommand
    {
        private static bool _isHackInProgress = false;
        private static bool _hackCompleted = false;

        public string Command => "hack";
        public string[] Aliases => new[] { ".hack" };
        public string Description => "Avvia un timer di 60s e annuncia il completamento.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is PlayerCommandSender playerSender)
            {
                Player player = Player.Get(playerSender.ReferenceHub);

                string roomName = player.CurrentRoom.Name.ToLower();
                if (!roomName.Contains("096"))
                {
                    response = "You can Only do that in 096 room!";
                    return false;
                }

                if (player.Role.Team != Team.ChaosInsurgency)
                {
                    response = "Only CI can execute this command!";
                    return false;
                }

                if (!HasChaosKeycard(player))
                {
                    response = "You must have at least a ChaosKeycard!";
                    return false;
                }

                if (_hackCompleted)
                {
                    response = "The Command has already been executed!";
                    return false;
                }

                if (_isHackInProgress)
                {
                    response = "Your Team is already executing this command!";
                    return false;
                }

                _isHackInProgress = true;
                _ = RunHackAsync(player);
                response = "Hack in progress check the timer!.";
                return true;
            }

            response = "This command is only available for humans!";
            return false;
        }

        private static async Task RunHackAsync(Player player)
        {
            try
            {
                string targetRoom = player.CurrentRoom?.Name ?? string.Empty;

                for (int i = 60; i >= 0; i--)
                {
                    if (ActiveGeneratorsCount() >= 3)
                    {
                        player.SendConsoleMessage("[HACK] All generators have been activated Hack cancelled.", "red");
                        _isHackInProgress = false;
                        return;
                    }
                    
                    if (player.CurrentRoom == null || player.CurrentRoom.Name != targetRoom)
                    {
                        player.SendConsoleMessage("[HACK] You've left the room! The hack has been canceled. Try again...", "red");
                        _isHackInProgress = false;
                        return;
                    }
                    
                    if (!HasChaosKeycard(player))
                    {
                        player.SendConsoleMessage("[HACK] You no longer have the Chaos Insurgency keycard in your hand! The hack has been canceled.", "red");
                        _isHackInProgress = false;
                        return;
                    }

                    player.SendConsoleMessage($"[HACK] Time remaining: {i}s", "green");
                    await Task.Delay(1000);
                }

                _hackCompleted = true;

                Cassie.Message(
                    "bell_start <i><b><align=center> <color=white> pitch_0.4 .G4 .G4 .G5 pitch_0.9 .G4 <color=red> Error .G5 Error <color=white> in <color=blue> CASSIESystem <split> <i><b><align=center> <color=white> .G5 .G4 Activating pitch_0.2 .G4 pitch_0.9 <color=blue> security systems <color=white> in <color=red> T minus pitch_2  5 . 4 . 3 . 2 . 1 . <split> <i><b><align=center> <color=white> pitch_0.2 .G4 .G3 pitch_0.8 <color=red> Protocol failed .G4 <split>\n<i><b><align=center> <color=blue> Site <color=white> is now under .G4 <color=green> Delta Command <color=white> control bell_end",
                    isNoisy: false,
                    isSubtitles: true
                );

                _ = FlickerLightsAsync();

                Cassie.Message(
                    "bell_start <i><b><align=center> <color=green>The Chaos Insurgency has hacked the SCP Foundation's systems.</color></i></b>",
                    isNoisy: false,
                    isSubtitles: true
                );
            }
            catch (Exception ex)
            {
                Log.Error($"[HackCommand] Error during timer: {ex}");
            }
            finally
            {
                _isHackInProgress = false;
            }
        }

        private static int ActiveGeneratorsCount()
        {
            return GameGenerator.List.Count(g => g.IsEngaged);
        }

        private static bool HasChaosKeycard(Player player)
        {
            return player.CurrentItem != null && player.CurrentItem.Type == ItemType.KeycardChaosInsurgency;
        }

        private static async Task FlickerLightsAsync()
        {
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    Map.TurnOffAllLights(1f);
                    await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                Log.Error($"[FlickerLights] Error: {ex}");
            }
        }
        
        public static void ResetState()
        {
            _isHackInProgress = false;
            _hackCompleted = false;
        }
    }
}
