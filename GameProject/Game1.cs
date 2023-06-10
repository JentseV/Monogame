﻿
using GameProject.Characters;
using GameProject.Enemies;
using GameProject.GameObjects;
using GameProject.GameObjects.Characters.Player;
using GameProject.GameObjects.Dynamic.Characters.Enemies;
using GameProject.GameObjects.Dynamic.DynamicCollidables;
using GameProject.Pickups;
using GameProject.Projectiles;
using GameProject.Screens;
using GameProject.Screens.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

using System.Linq;


namespace GameProject
{
    public class Game1 : Game
    {
        public static Texture2D healthTexture, coinTexture;
        private static List<Pickup> s = new List<Pickup>();



        private bool started = false;
        private UI ui;
        private ScoreUI scoreUI;
        private bool spawnEnemies = true;
        private float difficulty = 0;
        private Rectangle backgroundRect;
        private Texture2D background, buttonText;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Hero hero;
        private EnemyManager enemyManager;
        
        private Button button;
        private SpriteFont font;
        private UpgradeUI upgradeUI;

        private List<DynamicCollidable> dynamicCollidables = new List<DynamicCollidable>();
        private List<ICollidable> collidables = new List<ICollidable>();
        private List<Enemy> enemies = new List<Enemy>();
        private Texture2D[] coffinTextures = new Texture2D[11];
        private Texture2D[] heroTextures = new Texture2D[19];
        private Texture2D[] cactusTextures = new Texture2D[12];
        private Texture2D[] coyoteTextures = new Texture2D[14];

        string[] heroTextureNames = {
            "heroText/Fixed/runUp",
            "heroText/Fixed/runDown",
            "heroText/Fixed/shootFront",
            "heroText/Fixed/idleFacingFront",
            "heroText/Fixed/idleFacingRight",
            "heroText/Fixed/idleFacingUp",
            "heroText/Fixed/runRight",
            "heroText/Fixed/shootRight",
            "heroText/Fixed/shootUp",
            "heroText/Fixed/runDown",
            "heroText/red_square",
            "heroText/Fixed/runDownRight",
            "heroText/Fixed/runUpRight",
            "heroText/Fixed/shootDownRight",
            "heroText/Fixed/shootUpRight",
            "heroText/Fixed/idleFacingUpRight",
            "heroText/Fixed/idleFacingDownRight",
            "Projectiles/SpongeBullet",
            "heroText/Fixed/heroHitAnimation"
        };
        string[] cactusTextureNames = {
            "cactusEn/cactusIdleFront",
            "cactusEn/cactusRunRight",
            "cactusEn/cactusRunUp",
            "cactusEn/cactusIdleUp",
            "cactusEn/cactusIdleRight",
            "cactusEn/cactusRunDown",
            "cactusEn/cactusAttackFront",
            "cactusEn/cactusAttackRight",
            "cactusEn/cactusAttackUp",
            "heroText/red_square",
            "cactusEn/cactusHitAnimation",
            "Projectiles/SpongeBullet"
        };
        string[] coyoteTextureNames = {
            "coyoteEn/coyoteIdleFront",
            "coyoteEn/coyoteRunRight",
            "coyoteEn/coyoteRunUp",
            "coyoteEn/coyoteIdleUp",
            "coyoteEn/coyoteIdleRight",
            "coyoteEn/coyoteRunDown",
            "coyoteEn/coyoteAttackFront",
            "coyoteEn/coyoteAttackRight",
            "coyoteEn/coyoteAttackUp",
            "heroText/red_square",
            "coyoteEn/coyoteHitAnimation",
            "Projectiles/SpongeBullet",
            "coyoteEn/coyoteRunLeft",
            "coyoteEn/coyoteAttackLeft"
        };
        string[] coffinTextureNames = {
            "coffinEn/coffinIdleFront",
            "coffinEn/coffinWalkRight",
            "coffinEn/coffinRunUp",
            "coffinEn/coffinIdleUp",
            "coffinEn/coffinIdleRight",
            "coffinEn/coffinRunDown",
            "coffinEn/coffinAttackFront",
            "coffinEn/coffinAttackRight",
            "coffinEn/coffinAttackUp",
            "heroText/red_square",
            "coffinEn/coffinHitAnimation"
        };


        private IScreen screen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            scoreUI = new ScoreUI(font);
            difficulty = 2;
            button = new Button(buttonText, new Vector2(500f, 300f), "Buy Movement Speed ", font, () => hero.Hitpoints += 3);
            hero = new Hero(heroTextures, new KeyboardReader());
            enemies = EnemyFactory.SpawnEnemies(coffinTextures, cactusTextures, coyoteTextures, difficulty);
            enemyManager = new EnemyManager(enemies);

            dynamicCollidables.Add(hero);
            foreach (Enemy enemy in enemies)
            {
                if (dynamicCollidables.Contains(enemy) == false) dynamicCollidables.Add(enemy);
            }
            upgradeUI = new UpgradeUI(font, buttonText, hero);
            hero.Position = new Vector2(1000f, 600f);
            collidables.Add(hero);
            backgroundRect = new Rectangle(0, 0, 32, 47);
            screen = new SplashScreen();

        }

        protected override void LoadContent()
        {
            coyoteTextures = LoadTextures(coyoteTextureNames);
            cactusTextures = LoadTextures(cactusTextureNames);
            coffinTextures = LoadTextures(coffinTextureNames);
            heroTextures = LoadTextures(heroTextureNames);
            healthTexture = Content.Load<Texture2D>("Pickups/Heart");
            coinTexture = Content.Load<Texture2D>("Pickups/Coin");
            background = Content.Load<Texture2D>("World/background");
            font = Content.Load<SpriteFont>("Fonts/west");
            buttonText = Content.Load<Texture2D>("Buttons/boots");
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            screen.Update(gameTime);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here


            upgradeUI.Update(Mouse.GetState());


            hero.Update(gameTime, collidables);


            enemyManager.Update(gameTime, hero, collidables);


           
            dynamicCollidables.AddRange(collidables.OfType<DynamicCollidable>().Where(c => !c.Remove && !dynamicCollidables.Contains(c)));

            dynamicCollidables.RemoveAll(d => d.Remove);

            CollisionManager.CheckCollisions(dynamicCollidables);



            //pickup logic maybe this should belong in pickupfactory in the future?
            foreach (ICollidable c in collidables)
            {
                if (c is Coin)
                {
                    Coin p = c as Coin;
                    p.Update(gameTime);
                }
                else if (c is Health)
                {
                    Health h = c as Health;
                    h.Update(gameTime);
                }
            }


            scoreUI.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();


            _spriteBatch.Draw(background, Vector2.Zero, null, Color.White, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0f);
            hero.Draw(_spriteBatch);


            button.Draw(_spriteBatch);

            hero.DrawBullets(_spriteBatch);
            enemyManager.Draw(_spriteBatch);

            foreach (ICollidable c in collidables)
            {
                if (c is Coin)
                {
                    Coin p = c as Coin;
                    p.Draw(_spriteBatch);
                }
                else if (c is Health)
                {
                    Health h = c as Health;
                    h.Draw(_spriteBatch);
                }
            }

            //upgradeUI.Draw(_spriteBatch);
            scoreUI.Draw(_spriteBatch);

            _spriteBatch.End(); 

            base.Draw(gameTime);
        }

        private Texture2D[] LoadTextures(string[] textureNames)
        {
            Texture2D[] textures = new Texture2D[textureNames.Length];

            for (int i = 0; i < textureNames.Length; i++)
            {
                textures[i] = Content.Load<Texture2D>(textureNames[i]);
            }

            return textures;
        }
    }
}