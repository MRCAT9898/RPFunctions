using System;
using CommandSystem;

namespace RPF.Commands.RA
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class InfoCommand : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            
            response = "==================================\n";
            response += "        RPFunctions\n    ";
            response += "Plugin Made by Soldier and Mr.Cat.\n";
            response += "==================================";
            return true;
        }

        public string Command => "infoRPF";
        public string[] Aliases => [ "infoRPF" ];
        public string Description => "Shows RP functions info";
    }
    
}