using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Interfaces
{
    internal interface IUpdateableCharacter
    {
        public void Update(GameTime gameTime, List<ICollidable> collidables);
    }
}
