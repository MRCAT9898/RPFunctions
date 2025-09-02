using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;
using UnityEngine;
using Cassie = LabApi.Features.Wrappers.Cassie;

namespace RPF.Commands.RA
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class OmegaWarhead : ICommand
    {
        private void Part()
        {
            foreach (Player ply in Player.List)
            {
                if (ply == null) continue;
                ply.Kill("Vaporized by Omega Warhead.");
                ply.ExplodeEffect(ProjectileType.FragGrenade);
            }
            
            Log.Info("Omega Warhead has been detonated.");
        }
        
        private static Task Extetic()
        {
            try
            {
                Warhead.Start();
                Map.ChangeLightsColor(Color.blue);
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
            Extetic();
            Timing.CallDelayed(100f, () =>
            {
                Part();
                Cassie.Clear();
            });
            return true;
            
        }

        public string Command { get; } = "OmegaWarhead";
        public string[] Aliases { get; } = [ "OmegaWarhead" ];
        public string Description { get; } = "Starts the legendary Omega Warhead";
    } 
}
