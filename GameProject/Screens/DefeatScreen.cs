using GameProject.GameObjects.Characters.Player;
using GameProject.Screens.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameProject.Screens
{
    internal class DefeatScreen : GameScreen, IScreen
    {
        private Button restartButton, exitButton;

        public DefeatScreen(Texture2D[] buttonText, SpriteFont font, Hero hero):base(buttonText,font,hero)
        {
            restartButton = new Button(buttonText[0], new Microsoft.Xna.Framework.Vector2(266f, 368f), () =>
            {
                Game1.Difficulty = 0;
                Game1.GameStarted = false;
                Game1.FirstUpdate = false;
                hero.Dead = false;
            });
            exitButton = new Button(buttonText[1], new Microsoft.Xna.Framework.Vector2(425f, 368f), () =>
            {
                Environment.Exit(0);
            });

            Buttons.Add(restartButton);
            Buttons.Add(exitButton);
        }


    }
}
