using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using UnityEngine;

namespace RPF.Events.CustomItems
{
    public class SuperAdrenaline : CustomItem
    {
        public override uint Id { get; set; } = 100;
        public override string Name { get; set; } = "Silent Adrenaline";
        public override string Description { get; set; } = "Gives you Silent Walk.";
        public override float Weight { get; set; } = 1.5f;
        public override ItemType Type { get; set; } = ItemType.Adrenaline;

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
        {
            Limit = 5,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
            {
                new DynamicSpawnPoint()
                {
                    Chance = 100,
                    Location = SpawnLocationType.Inside914
                }
            }
        };

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.PickingUpItem += OnPickup;
            Debug.Log($"Item {Name} Subscribed");
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.PickingUpItem -= OnPickup;
            Debug.Log($"Item {Name} Unsubscribed");
            base.UnsubscribeEvents();
        }

        private void OnPickup(PickingUpItemEventArgs ev)
        {
            //patched: In 1.1.0
            if (!Check(ev.Pickup)) return;
            ev.Player.ShowHint("You Have Picked The Silent Adrenaline!");
        }

        public void OnUsing(UsingItemEventArgs ev)
        {
            ev.Player.EnableEffect(EffectType.SilentWalk); 
            Log.Debug($"{ev.Player.Nickname} used the silent adrenaline!");
        }
    }
}