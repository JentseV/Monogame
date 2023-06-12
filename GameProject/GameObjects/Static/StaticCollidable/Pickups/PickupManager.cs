using GameProject.Pickups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.GameObjects.Static.StaticCollidable.Pickups
{
    internal static class PickupManager
    {
        public static List<Pickup> pickups = new List<Pickup>();

        public static  void Update(GameTime gameTime , IList<ICollidable> collidables)
        {
           
            foreach(Pickup pickup in pickups.ToList())
            {
                if (!pickup.Remove)
                {
                    if (collidables.Contains(pickup) == false) collidables.Add(pickup);
                    pickup.Update(gameTime);
                }
                else
                {
                    collidables.Remove(pickup);
                }
            }

            pickups.RemoveAll(p => p.Remove);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (Pickup pickup in pickups)
            {
                if(!pickup.Remove)
                    pickup.Draw(spriteBatch);
            }
        }

        public static void AddPickup(Pickup pickup)
        {
            if(pickups.Contains(pickup) == false) pickups.Add(pickup);
        }

        public static int CountPickups()
        {
            return pickups.Count;
        }
    }
}
