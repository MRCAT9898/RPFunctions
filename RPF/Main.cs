using System;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomItems.API;
using Exiled.CustomItems.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using RPF.Events._914Event;
using RPF.Events.BroadCast;
using RPF.Events.CustomItems;
using RPF.Events.CustomRoles;
using RPF.Events.CustomRoles.Humans;
using RPF.Events.CustomWeapon;
using RPF.Events.Misc;
using RPF.Events.RPSCP;
using RPF.Events.TeslaGate;
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
        private Kill914 _kill914;
        private CustomRoleHandler _customRoleHandler;
        private CustomItemsHandler _customItemsHandler;
        private WeaponHandler _weaponHandler;
        public static Main Instance { get; private set; }
        public FemurBreakerEvent FemurBreaker { get; private set; }

        public float HintDuration { get; } = 10f;
        
        public override string Name { get; } = "RPFunctions";
        public override string Author { get; } = "Mr.Cat";
        public override string Prefix { get; } = "RPF";
        public override Version Version { get; } = new Version(1, 3, 0);
        public override Version RequiredExiledVersion { get; } = new Version(9, 8, 1);
        public override PluginPriority Priority { get; } = PluginPriority.Medium;
        

        public override void OnEnabled()
        {
            Instance = this;
            
            CustomItem.RegisterItems();
            CustomWeapon.RegisterItems();
            CustomRole.RegisterRoles(true, this);
            
            _broadcast = new BroadCastBreach();
            _broadcast.Register();
            
            _noDoorsFor10 = new NoDoorsFor106();
            _noDoorsFor10.RegisterEvents();
            
            _noElevatorFor939 = new NoElevatorFor939();
            _noElevatorFor939.RegisterEvents();
            
            _scp096ElevatorRestriction = new Scp096ElevatorRestriction();
            _scp096ElevatorRestriction.RegisterEvents();
            
            _teslaGate = new RPF.Events.TeslaGate.TeslaConditions();
            _teslaGate.Register();
            
            FemurBreaker = new Events.Misc.FemurBreakerEvent(Config);
            FemurBreaker.Register();
            
            _kill914 = new Kill914();
            _kill914.Register();
            
            _customRoleHandler = new CustomRoleHandler();
            _customRoleHandler.Register();
            
            _customItemsHandler = new CustomItemsHandler();
            _customItemsHandler.Register();

            _weaponHandler = new WeaponHandler();
            _weaponHandler.Register();
            
            Debug.Log("Registered RPF Items and Roles");
            Debug.Log("RPF enabled");
            base.OnEnabled();
        }


        public override void OnDisabled()
        {
            _customItemsHandler.Unregister();
            _broadcast.Unregister();
            _noDoorsFor10.UnregisterEvents();
            _kill914.Unregister();
            _noElevatorFor939.UnregisterEvents();
            _scp096ElevatorRestriction.UnregisterEvents();
            _teslaGate.Unregister();
            _customRoleHandler.Unregister();
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