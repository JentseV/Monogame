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
    internal class StartScreen : GameScreen, IScreen
    {
        private Button easyButton, mediumButton, hardButton;
        public StartScreen(Texture2D[] buttonText, SpriteFont font, Hero  hero)
        {
            Buttons = new List<Button>();
            easyButton = new Button(buttonText[0], new Microsoft.Xna.Framework.Vector2(266f, 568f),  () =>
            {
                Game1.Difficulty = 1;
                Game1.GameStarted = true;
                hero.Hitpoints = 3;
            });
            mediumButton = new Button(buttonText[1], new Microsoft.Xna.Framework.Vector2(425f, 568f), () =>
            {
                Game1.Difficulty = 3;
                Game1.GameStarted = true;
                hero.Hitpoints = 3;

            });

            hardButton = new Button(buttonText[2], new Microsoft.Xna.Framework.Vector2(687f, 568f),  () =>
            {
                Game1.Difficulty = 5;
                Game1.GameStarted = true;
                hero.Hitpoints = 3;

            });

            Buttons.Add(easyButton);
            Buttons.Add(mediumButton);
            Buttons.Add(hardButton);

        }

    }
}
