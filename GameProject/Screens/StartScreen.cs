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
    internal class StartScreen
    {
        private Button easyButton, mediumButton, hardButton;
        private List<Button> buttons;

        public StartScreen(Texture2D[] buttonText, SpriteFont font)
        {
            buttons = new List<Button>();
            easyButton = new Button(buttonText[0], new Microsoft.Xna.Framework.Vector2(266f, 568f),  () =>
            {
                Game1.Difficulty = 1;
                Game1.GameStarted = true;

            });
            mediumButton = new Button(buttonText[1], new Microsoft.Xna.Framework.Vector2(425f, 568f), () =>
            {
                Game1.Difficulty = 3;
                Game1.GameStarted = true;

            });

            hardButton = new Button(buttonText[2], new Microsoft.Xna.Framework.Vector2(687f, 568f),  () =>
            {
                Game1.Difficulty = 5;
                Game1.GameStarted = true;

            });

            buttons.Add(easyButton);
            buttons.Add(mediumButton);
            buttons.Add(hardButton);

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
