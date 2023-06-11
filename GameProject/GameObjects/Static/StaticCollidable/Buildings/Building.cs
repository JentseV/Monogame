using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.GameObjects.Static.StaticCollidable.Buildings
{
    internal class Building : StaticCollidable
    {
        public Building(Texture2D texture, Vector2 position, Rectangle hitbox)
        {
            Texture = texture;
            Position = position;
            Hitbox = hitbox;
        }
    }
}
