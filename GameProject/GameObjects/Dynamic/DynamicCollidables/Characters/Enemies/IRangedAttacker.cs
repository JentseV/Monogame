using GameProject.GameObjects.Dynamic.DynamicCollidables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.GameObjects.Dynamic.Characters.Enemies
{
    internal interface IRangedAttacker
    {
        public void UpdateBullets(GameTime gameTime, List<ICollidable> collidables);
        public void DrawBullets(SpriteBatch spriteBatch);
    }
}
