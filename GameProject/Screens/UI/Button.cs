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
    internal class Button
    {
        private Texture2D texture;
        private Vector2 position;
        private string text;
        private SpriteFont font;
        private Rectangle hitbox;
        private bool destroy;
        private Action action;
        public bool Destroy { get { return destroy; } set { destroy = value; } }
        public Button(Texture2D textureIn, Vector2 positionIn, string textIn, SpriteFont fontIn,Action actionIn)
        {
            this.texture = textureIn;
            this.position = positionIn;
            this.text = textIn;
            this.font = fontIn;
            this.action = actionIn;
            this.Destroy = false;
            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }


        public bool OnClick(MouseState mouse)
        {
            if (hitbox.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed)
            {
                
                return true;
            }

            return false;
        }

        public void Update(MouseState mouseState)
        {
            if (OnClick(mouseState) && !Destroy)
            {
                    action?.Invoke();
                    Destroy = true;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(texture, position, null, Color.White, 0f, new Vector2(0f, 0f), 0.1f, SpriteEffects.None, 0f);
            Vector2 textPosition = position + new Vector2(0f, 70f);
            spriteBatch.DrawString(font, text, textPosition, Color.Red);
        }
    }
}
