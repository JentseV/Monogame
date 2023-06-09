using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.GameObjects.Static.StaticCollidable
{
    internal class StaticCollidable : StaticGO, ICollidable
    {
        public virtual void CheckCollision(ICollidable collidables)
        {
            
        }
    }
}
