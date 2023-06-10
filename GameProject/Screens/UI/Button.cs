using GameProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Screens.UI
{
    internal class Button : GameObject
    {
        private Texture2D texture;
        private Action action;

        public Button(Texture2D texture, Vector2 position, Action action)
        {
            this.texture = texture;
            this.hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.action = action;
        }

        public bool IsClicked(MouseState mouse)
        {
            return hitbox.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed;
        }

        public void Update(MouseState mouseState)
        {
            if (IsClicked(mouseState))
                action?.Invoke();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitbox, Color.White);
        }
    }
}
