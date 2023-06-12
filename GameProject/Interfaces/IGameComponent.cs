using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Interfaces
{
    internal interface IGameComponent
    {
        int ID { get; set; }
        string Tag { get; set; }
        Vector2 Position { get; set; }

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
