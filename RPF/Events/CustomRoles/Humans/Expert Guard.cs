﻿using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;
using UnityEngine;

namespace RPF.Events.CustomRoles.Humans
{
    public class Expert_Guard : CustomRole
    {
        public override uint Id { get; set; } = 106;
        public override int MaxHealth { get; set; } = 100;
        public override string Name { get; set; } = "Expert Guard";
        public override string Description { get; set; } = "A special guard...";
        public override string CustomInfo { get; set; } = "A special guard...";
        public override RoleTypeId Role { get; set; } = RoleTypeId.FacilityGuard;
        public override float SpawnChance { get; set; } = 100;
        
        public override List<string> Inventory { get; set; } = new List<string>()
        {
            "KeycardGuard",
            "P90",
            "5.56x45mm",
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
                    Location = SpawnLocationType.InsideHczArmory
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
            base.AddRole(player);
            player.Broadcast(10, "You are an Expert Guard.");
        }
        
    }
}
    


