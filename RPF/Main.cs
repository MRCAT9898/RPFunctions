using System;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomItems.API;
using Exiled.CustomItems.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using RPF.Events._914Event;
using RPF.Events.BroadCast;
using RPF.Events.CustomItems;
using RPF.Events.CustomRoles.Humans;
using RPF.Events.Misc;
using RPF.Events.RPSCP;
using UnityEngine;

namespace RPF
{
    public class Main : Plugin<Config>
    {
        private BroadCastBreach _broadcast;
        private Scp096ElevatorRestriction _scp096ElevatorRestriction;
        private NoDoorsFor106 _noDoorsFor10;
        private NoElevatorFor939 _noElevatorFor939;
        private TeslaConditions _teslaGate;
        private SSS.SSS _sss;
        private Kill914 _kill914;
        public static Main Instance { get; private set; }
        public FemurBreakerEvent FemurBreaker { get; private set; }

        public float HintDuration { get; } = 10f;
        
        public override string Name { get; } = "RPFunctions";
        public override string Author { get; } = "Mr.Cat, FIXI50000";
        public override string Prefix { get; } = "RPF";
        public override Version Version { get; } = new Version(1, 2, 0);
        public override Version RequiredExiledVersion { get; } = new Version(9, 8, 1);
        public override PluginPriority Priority { get; } = PluginPriority.Medium;
        

        public override void OnEnabled()
        {
            Instance = this;
            
            CustomItem.RegisterItems();
            CustomRole.RegisterRoles(true, this);
            
            _broadcast = new BroadCastBreach();
            _broadcast.Register();
            
            _noDoorsFor10 = new NoDoorsFor106();
            _noDoorsFor10.RegisterEvents();
            
            _noElevatorFor939 = new NoElevatorFor939();
            _noElevatorFor939.RegisterEvents();
            
            _scp096ElevatorRestriction = new Scp096ElevatorRestriction();
            _scp096ElevatorRestriction.RegisterEvents();
            
            FemurBreaker = new Events.Misc.FemurBreakerEvent(Config);
            FemurBreaker.Register();
            
            _kill914 = new Kill914();
            _kill914.Register();
            
            SSS.SSS.Register();
            new SuperAdrenaline().Register();
            new SiteManager().Register();
            new O5X().Register();
            new Chief_Guard().Register();
            new CI_CLASS_D().Register();
            new Expert_Guard().Register();
            new Scientist_Pro().Register();
            new Tech_Pro().Register();
            new EMP_Device().Register();
            CustomWeapon.RegisterItems();

            
            Debug.Log("Registered RPF Items and Roles");
            Debug.Log("RPF enabled");
            base.OnEnabled();
        }


        public override void OnDisabled()
        {
            SSS.SSS.Unregister();
            CustomItem.UnregisterItems();
            EMP_Device.UnregisterItems();
            SuperAdrenaline.UnregisterItems();
            SiteManager.UnregisterRoles();
            CustomItem.UnregisterItems();
            CustomRole.UnregisterRoles();
            O5X.UnregisterRoles();
            Chief_Guard.UnregisterRoles();
            CI_CLASS_D.UnregisterRoles();
            Expert_Guard.UnregisterRoles();
            Scientist_Pro.UnregisterRoles();
            Tech_Pro.UnregisterRoles();
            _broadcast.Unregister();
            _noDoorsFor10.UnregisterEvents();
            _kill914.Unregister();
            _noElevatorFor939.UnregisterEvents();
            _scp096ElevatorRestriction.UnregisterEvents();
            FemurBreaker?.Unregister();
            FemurBreaker = null;
            
            Debug.Log("RPF disabled");
            base.OnDisabled();
        }

        public override void OnReloaded()
        {
            Debug.Log("RPF reloaded");
            base.OnReloaded();
        }
    }
}