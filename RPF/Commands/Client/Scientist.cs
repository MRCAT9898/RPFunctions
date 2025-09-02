using System;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;
using Exiled.API.Features;
using PlayerRoles;

namespace RPF.Commands.Client
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Scientist : ICommand
    {
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
        {
            var player = Player.Get(sender);

            if (_usedThisRound)
            {
                response = "You cant't do the command anymore. Please try again. Or another Scientist Alredy executed it!";
                return false;
            }

            if (player.Role.Type != RoleTypeId.Scientist)
            {
                response = "You MUST be a Scientist to run this command!";
                return false;
            }
            
            _usedThisRound = true;
            
            response = "Command received.";
            player.Broadcast(10,
                Main.Instance.Config.ScientistInstructions,
                Broadcast.BroadcastFlags.Normal,
                false);
            player.AddItem(ItemType.KeycardFacilityManager);
            return true;
        }
        
        private static bool _usedThisRound = false;

        public string Command => "excapeTool";
        public string[] Aliases => ["excapeTool"];
        public string Description => "A Scientist Can Excape the facility";
    }
}