using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Screens
{
    internal interface IScreen
    {

        void Update(MouseState mouseState);

        void Draw(SpriteBatch spriteBatch);
    }
}
