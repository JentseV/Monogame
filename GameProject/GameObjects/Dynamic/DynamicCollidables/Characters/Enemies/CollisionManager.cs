
using GameProject.GameObjects.Characters.Player;
using GameProject.GameObjects.Dynamic.DynamicCollidables;
using GameProject.Pickups;
using GameProject.Projectiles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.GameObjects.Dynamic.Characters.Enemies
{
    internal static class CollisionManager
    {
        public static void CheckCollisions(List<DynamicCollidable> gameObjects, List<IPickupObserver> pickupObservers)
        {
            foreach (var collidable in gameObjects.OfType<ICollidable>())
            {
                foreach (var otherCollidable in gameObjects.OfType<ICollidable>())
                {
                    if (collidable != otherCollidable)
                    {
                        collidable.CheckCollision(otherCollidable);
                    }
                }

                if (collidable is Pickup pickup)
                {
                    foreach (var otherCollidable in gameObjects.OfType<ICollidable>())
                    {
                        if (pickup.Hitbox.Intersects(otherCollidable.Hitbox) && !(otherCollidable is Pickup) && !(otherCollidable is Bullet))
                        {
                            if (otherCollidable is Hero hero)
                            {
                                pickup.OnPickup(hero);
                                NotifyObservers(pickupObservers, pickup);
                                break;
                            }
                        }
                    }
                }
            }
            gameObjects.RemoveAll(c => c.Remove);
        }

        private static void NotifyObservers(List<IPickupObserver> pickupObservers, Pickup pickup)
        {
            foreach (IPickupObserver observer in pickupObservers)
            {
                observer.OnPickup(pickup);
            }
        }
    }

}
