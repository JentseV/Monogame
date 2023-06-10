using GameProject.Pickups;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.GameObjects.Static.StaticCollidable.Pickups
{
    internal class PickupFactory
    {
        public static Pickup SpawnPickup(int id, string tag, Vector2 position, float timeDespawn)
        {
            Debug.WriteLine("Triggered pickup spawn");
            Random r = new Random();
            int rn = r.Next(0, 11);
            {
                if (rn < 3)
                {
                    return new Health(id, tag, position, timeDespawn);
                }
                else
                {
                    return new Coin(id, tag, position, timeDespawn);
                }
            }
        }
    }
}
