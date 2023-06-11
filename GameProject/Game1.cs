
using GameProject.Characters;
using GameProject.Enemies;
using GameProject.GameObjects;
using GameProject.GameObjects.Characters.Player;
using GameProject.GameObjects.Dynamic.Characters.Enemies;
using GameProject.GameObjects.Dynamic.DynamicCollidables;
using GameProject.GameObjects.Static.StaticCollidable.Pickups;
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
        public static bool GameStarted { get; set; } = false;
        public static bool FirstUpdate { get; set; } = false;
        public static int Difficulty { get; set; } = 0;

        private List<IPickupObserver> pickupObservers = new();
        private UI ui;
        private ScoreUI scoreUI;
        private bool spawnEnemies = true;
        private Rectangle backgroundRect;
        private Texture2D background, buttonText, backGroundStart, backGroundDefeat;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Hero hero;
        private EnemyManager enemyManager;
        private StartScreen startScreen;
        private Button button;
        private SpriteFont font;
        private DefeatScreen defeatScreen;

        private UpgradeUI upgradeUI;

        private List<DynamicCollidable> dynamicCollidables = new List<DynamicCollidable>();
        private List<Enemy> enemies;

        private Texture2D[] defeatButtonTextures = new Texture2D[2];
        private Texture2D[] buttonTextures = new Texture2D[3];
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
        string[] buttonTexturesNames =
        {
            "Buttons/easyButton",
            "Buttons/buttonMed",
            "Buttons/hardButton"
        };
        string[] defeatButtonTexturesNames =
        {
             "Buttons/restartButton",
             "Buttons/exitButton"
        };

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
            button = new Button(buttonText, new Vector2(500f, 300f), () => hero.Hitpoints += 3);
            hero = new Hero(heroTextures, new KeyboardReader());
            defeatScreen = new DefeatScreen(defeatButtonTextures,font, hero);
            enemyManager = new EnemyManager(enemies);
            startScreen = new StartScreen(buttonTextures, font,hero) ;
            dynamicCollidables.Add(hero);
           
            upgradeUI = new UpgradeUI(font, buttonText, hero);
            hero.Position = new Vector2(1000f, 600f);
            pickupObservers.Add(hero);
            backgroundRect = new Rectangle(0, 0, 32, 47);

        }

        protected override void LoadContent()
        {
            coyoteTextures = LoadTextures(coyoteTextureNames);
            cactusTextures = LoadTextures(cactusTextureNames);
            coffinTextures = LoadTextures(coffinTextureNames);
            heroTextures = LoadTextures(heroTextureNames);
            buttonTextures = LoadTextures(buttonTexturesNames);
            defeatButtonTextures = LoadTextures(defeatButtonTexturesNames);

            healthTexture = Content.Load<Texture2D>("Pickups/Heart");
            coinTexture = Content.Load<Texture2D>("Pickups/Coin");
            background = Content.Load<Texture2D>("World/background");
            backGroundStart = Content.Load<Texture2D>("World/startScreen");
            backGroundDefeat = Content.Load<Texture2D>("World/defeatScreen");
            font = Content.Load<SpriteFont>("Fonts/west");
            buttonText = Content.Load<Texture2D>("Buttons/boots");
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (GameStarted && hero.Dead == false)
            {

                FirstUpdate = true;
                hero.Update(gameTime, dynamicCollidables);

                if (spawnEnemies)
                {
                    enemies = EnemyFactory.SpawnEnemies(coffinTextures, cactusTextures, coyoteTextures, Difficulty);
                    enemyManager.enemies = enemies;
                    dynamicCollidables.AddRange(enemies);
                }

                UpdateSpawnEnemies();
               
                //upgradeUI.Update(Mouse.GetState());

                enemyManager.Update(gameTime, hero, dynamicCollidables);
                PickupManager.Update(gameTime);


                CollisionManager.CheckCollisions(dynamicCollidables, pickupObservers);
                
                scoreUI.Update(gameTime);
            }
            else if(hero.Dead == true)
            {
                enemies.Clear();
                dynamicCollidables.Clear();
                dynamicCollidables.Add(hero);
                defeatScreen.Update(Mouse.GetState());
                
            }
            else if(GameStarted == false && FirstUpdate == false)
            {
                startScreen.Update(Mouse.GetState());
            }
            
            base.Update(gameTime);
        }

        int desiredWidth, desiredHeight;
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            if (GameStarted && FirstUpdate && hero.Dead == false)
            {
                desiredWidth = 1200;
                desiredHeight = 800;
                _spriteBatch.Draw(background, Vector2.Zero, null, Color.White, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0f);
                hero.Draw(_spriteBatch);
                hero.DrawBullets(_spriteBatch);
                enemyManager.Draw(_spriteBatch);
                PickupManager.Draw(_spriteBatch);
                //upgradeUI.Draw(_spriteBatch);
                scoreUI.Draw(_spriteBatch);
            }
            else if (hero.Dead)
            {
                desiredWidth = 960;
                desiredHeight = 1024;
                _spriteBatch.Draw(backGroundDefeat, Vector2.Zero, null, Color.White, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0f);
                defeatScreen.Draw(_spriteBatch);
            }
            else if(GameStarted == false && FirstUpdate == false)
            {
                desiredWidth = 960;
                desiredHeight = 1024;
                _spriteBatch.Draw(backGroundStart, Vector2.Zero, null, Color.White, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0f);
                startScreen.Draw(_spriteBatch);
            }
            _graphics.PreferredBackBufferWidth = desiredWidth;
            _graphics.PreferredBackBufferHeight = desiredHeight;
            _graphics.ApplyChanges();
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

        private void UpdateSpawnEnemies()
        {
            if (enemies.Count != 0 && spawnEnemies)
            {
                spawnEnemies = false;
            }

            if (enemies.Count == 0)
            {
                spawnEnemies = true;
                Difficulty += 1;
            }
        }
    }
}