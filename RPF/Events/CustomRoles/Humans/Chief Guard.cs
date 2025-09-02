﻿using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;
using UnityEngine;

namespace RPF.Events.CustomRoles.Humans
{
    public class Chief_Guard : CustomRole
    {
        public override uint Id { get; set; } = 105;
        public override int MaxHealth { get; set; } = 120;
        public override string Name { get; set; } = "Chief Guard";
        public override string Description { get; set; } = "Lead your Guard team to Victory!";
        public override string CustomInfo { get; set; } = "Chief Guard";
        public override RoleTypeId Role { get; set; } = RoleTypeId.FacilityGuard;
        public override float SpawnChance { get; set; } = 100;
        
        public override List<string> Inventory { get; set; } = new List<string>()
        {
            "KeycardMTFPrivate",
            "MediGun",
            "COM15",
            "9x19mm",
            "Medkit",
            "Flashlight",
            "Radio"
        };

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
        {
            Limit = 1,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>()
            {
                new DynamicSpawnPoint()
                {
                    Chance = 100,
                    Location = SpawnLocationType.InsideGateA
                }
            }
        };
        

        protected override void SubscribeEvents()
        {
            Debug.Log($"Role {Name} subscribed");
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Debug.Log($"Role {Name} unsubscribed");
            base.UnsubscribeEvents();
        }

        public override void AddRole(Player player)
        {
            //Patched: in 1.2.0
            if (!SSS.SSS.IsCustomRolesAllowed.TryGetValue(player.UserId, out bool isAllowed) || !isAllowed)
                return;
            base.AddRole(player);
            player.Broadcast(10, "You are a Chief Guard.");
        }
        
    }
}
    


