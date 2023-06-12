using GameProject.GameObjects.Characters.Player;
using GameProject.Screens.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GameProject.Screens
{
    internal class GameScreen : IScreen
    {
        private List<Button> buttons;
        public List<Button> Buttons { get { return buttons; } set { buttons = value; } }

        private Texture2D[] buttonTextI;

        public GameScreen(Texture2D[] buttonText, SpriteFont font, Hero hero)
        {
            buttonTextI = buttonText;
            buttons = new List<Button>();

        }

        public void Update(MouseState mouseState)
        {
            foreach (var button in buttons)
            {
                button.Update(mouseState);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var button in buttons)
            {
                button.Draw(spriteBatch);
            }
        }
    }
}
