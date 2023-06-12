using GameProject.GameObjects.Characters.Player;
using GameProject.Pickups;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Factories
{
    internal class PickupFactory
    {
        public static Pickup SpawnPickup(int id, string tag, Vector2 position, float timeDespawn, Hero hero)
        {
            Random r = new Random();
            int rn = r.Next(0, 11);

            Pickup pickup;

            if (rn < 3)
            {
                pickup = new Health(id, tag, position, timeDespawn);
            }
            else
            {
                pickup = new Coin(id, tag, position, timeDespawn);
            }

            
            pickup.AttachObserver(hero);

            return pickup;
        }
    }
}
