using System.ComponentModel;
using Exiled.API.Interfaces;

namespace RPF
{
    public class Config : IConfig
    {
        [Description("----------------------- Pl Main -----------------------")]
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("----------------------- SCP RP EVENTS ----------------------")]
        public static string ScpRpFunctions096 { get; set; } = "You Can't use elevators when you are not in Rage!";
        public static string ScpRpFunctions106 { get; set; } = "You Can't use doors ONLY elevators!";
        public static string ScpRpFunctions939 { get; set; } = "You Can't use elevators!";
        
        [Description("----------------------- FemurBreaker -----------------------")]
        public int GeneratorsRequired { get; set; } = 3;
        public bool OnlyHumansCanTrigger { get; set; } = true;
        public int FemurBreakerDelay { get; set; } = 8000;

        public static string FemurBreakerCassie { get; set; } = "<b><color=red>Femur Breaker Actived . . .</color></b>";
        
        [Description("------------------------- Hack Command ---------------------")]
        public static string HackCommandCassieHacked { get; set; } = "<b><color=red>Error... Error... Hacking Event Occurred ...</color></b>";
        public static string HackCommandCassieResolving { get; set; } = "<b><color=green>Resolving... Hacking Succeded</color></b>";
        public static string HackCommandCassieFinalMessage { get; set; } = "<b><color=red>SCP FOUNDATION IS UNDER DELTA COMMAND CONTROL...</color></b>";
        
        [Description("------------------------ Overload Command ---------------------")]
        public static string Overload079Cassie { get; set; } = "Overload... Completed...";
        
        [Description("------------------------ Scientist Command ------------------------")]
        public static string ScientistInstructions { get; set; } = "Go to the Exit here a keycard to help!";
        [Description("------------------------- Omega Warhead  ----------------------")]
        public static string OmegaWarheadInstructions { get; set; } = "<i><b><align=center> <color=red> .g4 pitch_0.7 .g4 pitch_0.6 .g4 pitch_0.5 .g4 pitch_0.4 .g4 pitch_0.3 .g4 pitch_0.2 .g4 pitch_0.1 .g4 .g4 pitch_0.9 Omega Warhead activation code accepted . Omega Warhead procedure engaged . All facility will be detonated in T - Minus 120 seconds . All personnel have to evacuate immediately to surface of Gate B .g4 .g5 .g2 .g2 .g1 .g6 </color> ";
        
        [Description("------------------------- BradCastBreach Main ---------------------")]
        public static string MainBroadCastMessage { get; set; } = "<i><b><align=center> bell_start pitch_0.4 .G4 .G4 .G5 pitch_0.9 .G4 <color=blue> Attention .G3 Attention .G4 <color=red> SCP ? ? ? <color=white> has escaped out of the <color=red> containment  <color=white> pitch_0.4 .G4 pitch_0.9 repeat .G4 <color=red> SCP ? ? ? <color=white> has <color=red> breached <color=white> the <color=red> containment bell_end";
        
        [Description("------------------------- BradCastScp Specific -----------------------")]
        public static string BroadCastMessage096 { get; set; } = "Example Breach 096";
        public static string BroadCastMessage173 { get; set; } = "Example Breach 173";
        public static string BroadCastMessage049 { get; set; } = "Example Breach 049";
        public static string BroadCastMessage079 { get; set; } = "Example Breach 079";
        public static string BroadCastMessage939 { get; set; } = "Example Breach 939";
        public static string BroadCastMessage106 { get; set; } = "Example Breach 106";
        public static string BroadCastMessage3114 { get; set; } = "Example Breach 3114";
    }
}