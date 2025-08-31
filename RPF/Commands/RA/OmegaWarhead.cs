using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using CommandSystem;
using Exiled.API.Features;
using MEC;
using UnityEngine;

namespace RPF.Commands.RA
{
    public class OmegaWarhead : ICommand
    {
        private void Part()
        {
            foreach (Player ply in Player.List)
            {
                if (ply == null) continue;
                ply.Kill("Vaporized by Omega Warhead.");
            }
            
            Log.Info("Omega Warhead has been detonated.");
        }
        
        private static Task Extetic()
        {
            try
            {
                Map.ChangeLightsColor(Color.blue);
                Cassie.Message(
                    Config.OmegaWarheadInstructions,
                    isNoisy: false,
                    isSubtitles: true
                );
            }
            catch (Exception ex)
            {
                Log.Error($"Omega Warhead exeption occurd: {ex}");
            }

            return Task.CompletedTask;
        }

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
        {
            response = "Omega Warhead is running...";
            Task.Run(Extetic);
            Timing.WaitForSeconds(60);
            Part();
            return true;
            
        }

        public string Command { get; } = "OmegaWarhead";
        public string[] Aliases { get; } = [ "OmegaWarhead" ];
        public string Description { get; } = "Starts the legendary Omega Warhead";
    } 
}
