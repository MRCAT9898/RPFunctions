using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Usables.Scp330;

namespace RPF.Events.Misc
{
    public class PickUpCandy
    {
    
        public void Register()
        {
            Exiled.Events.Handlers.Player.ItemAdded += PickupCandyAndColor;
        }
        
        public void  Unregister() 
        {
            Exiled.Events.Handlers.Player.ItemAdded -= PickupCandyAndColor;
        }
        
        private void PickupCandyAndColor(ItemAddedEventArgs ev)
        {
            try
            {
                if (ev.Pickup.Type == ItemType.SCP330)
                {
                    var scp330 = ev.Pickup as Exiled.API.Features.Pickups.Scp330Pickup;
                    if (scp330 == null) return;
    
                    foreach (var candy in scp330.Candies)
                    {
                        if (candy == CandyKindID.Blue)
                        {
                            ev.Player.ShowHint("You have picked the Blue Candy", 15f);
                        }
                        else if (candy == CandyKindID.Red)
                        {
                            ev.Player.ShowHint("You have picked the Red Candy", 15f);
                        }
                        else if (candy == CandyKindID.Green)
                        {
                            ev.Player.ShowHint("You have picked the Green Candy", 15f);
                        }
                        else if (candy == CandyKindID.Yellow)
                        {
                            ev.Player.ShowHint("You have picked the Yellow Candy", 15f);
                        }
                        else if (candy == CandyKindID.Pink)
                        {
                            ev.Player.ShowHint("You have picked the Pink Candy", 15f);
                        }
                        else if (candy == CandyKindID.Purple)
                        {
                            ev.Player.ShowHint("You have picked the Purple Candy", 15f);
                        }
                    }
                }
    
    
            }
            catch (Exception ex)
            {
                Log.Error($"Exception occured in PickUpCandy: {ex}");
            }
            
        }
    
        
    }
}

