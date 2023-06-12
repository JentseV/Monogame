
using GameProject.Characters;
using GameProject.Enemies;
using GameProject.Factories;
using GameProject.GameObjects;
using GameProject.GameObjects.Characters.Player;
using GameProject.GameObjects.Dynamic.Characters.Enemies;
using GameProject.GameObjects.Dynamic.DynamicCollidables;
using GameProject.GameObjects.Static.StaticCollidable;
using GameProject.GameObjects.Static.StaticCollidable.Buildings;
using GameProject.Interfaces;
using GameProject.Managers;
using GameProject.Pickups;
using GameProject.Projectiles;
using GameProject.Screens;
using GameProject.Screens.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Diagnostics;

using System.Linq;


namespace GameProject
{
    public class Game1 : Game
    {
        public static Texture2D healthTexture, coinTexture;
        private static List<Pickup> s = new List<Pickup>();
         

        public static bool Victory { get; set; } = false;
        public static bool GameStarted { get; set; } = false;
        public static bool FirstUpdate { get; set; } = false;
        public static int Difficulty { get; set; } = 0;

        private List<Building> buildings = new List<Building>();

        private List<IPickupObserver> pickupObservers = new();
        private ScoreUI scoreUI;
        private bool spawnEnemies = true;
        private Rectangle backgroundRect;
        private Texture2D background, buttonText, backGroundStart, backGroundDefeat,backGroundVictory;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Hero hero;
        private EnemyManager enemyManager;
        private StartScreen startScreen;
        private SpriteFont font;
        private DefeatScreen defeatScreen;
        private VictoryScreen victoryScreen;
        private Song backGroundMusic;
        private List<SoundEffect> heroSounds = new List<SoundEffect>();
        private Dictionary<string, int[]> buildingParams = new Dictionary<string, int[]>();


        private List<Enemy> enemies;
        List<ICollidable> collidables = new List<ICollidable>();

        private Texture2D[] defeatButtonTextures = new Texture2D[2];
        private Texture2D[] buttonTextures = new Texture2D[3];
        private Texture2D[] coffinTextures = new Texture2D[12];
        private Texture2D[] heroTextures = new Texture2D[19];
        private Texture2D[] cactusTextures = new Texture2D[12];
        private Texture2D[] coyoteTextures = new Texture2D[12];

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
            "coffinEn/coffinHitAnimation",
             "Projectiles/SpongeBullet"
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

            
            base.Initialize();
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.4f;
            scoreUI = new ScoreUI(font);
            hero = new Hero(heroTextures, new KeyboardReader(),heroSounds);
            defeatScreen = new DefeatScreen(defeatButtonTextures,font, hero);
            enemyManager = new EnemyManager(enemies);
            startScreen = new StartScreen(buttonTextures, font,hero) ;
            victoryScreen = new VictoryScreen(defeatButtonTextures, font, hero);
            collidables.Add(hero);

            InitializeBuildings();
         
            pickupObservers.Add(hero);
            backgroundRect = new Rectangle(0, 0, 32, 47);

        }

        protected override void LoadContent()
        {
            SoundEffect gunshot0 = Content.Load<SoundEffect>("Sounds/Hero/Gun/revolver_shot_2");
            SoundEffect gunshot1 = Content.Load<SoundEffect>("Sounds/Hero/Gun/revolver_shot_3");
            SoundEffect gunshot2 = Content.Load<SoundEffect>("Sounds/Hero/Gun/revolver_shot_4");
            backGroundMusic = Content.Load<Song>("Sounds/soundTrack");
            heroSounds.Add(gunshot0);
            heroSounds.Add(gunshot1);
            heroSounds.Add(gunshot2);

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
            backGroundVictory = Content.Load<Texture2D>("World/victoryScreen");
            font = Content.Load<SpriteFont>("Fonts/west");
            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void Update(GameTime gameTime)
        {
            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(backGroundMusic);
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (GameStarted && hero.Dead == false && !Victory)
            {
                FirstUpdate = true;
                hero.Update(gameTime, collidables);
                if (hero.Gold > 5) Victory = true;
                if (spawnEnemies)
                {
                    enemies = EnemyFactory.SpawnEnemies(coffinTextures, cactusTextures, coyoteTextures, Difficulty,hero);
                    enemyManager.enemies = enemies;
                    collidables.AddRange(enemies);
                }
                UpdateSpawnEnemies();
                
                CollisionManager.CheckCollisions(collidables, pickupObservers);
                enemyManager.Update(gameTime, hero, collidables);
                PickupManager.Update(gameTime, collidables);
                CollisionManager.CheckCollisions(collidables, pickupObservers);
                
                scoreUI.Update(gameTime);
            }
            else if(hero.Dead == true)
            {
                enemies.Clear();
                collidables.Clear();
                collidables.Add(hero);
                InitializeBuildings();
                defeatScreen.Update(Mouse.GetState());
                
            }
            else if(GameStarted == false && FirstUpdate == false)
            {
                startScreen.Update(Mouse.GetState());
            }else if(Victory)
            {
                victoryScreen.Update(Mouse.GetState());
            }
            
            base.Update(gameTime);
        }

        int desiredWidth, desiredHeight;
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            if (GameStarted && FirstUpdate && hero.Dead == false && !Victory)
            {
                desiredWidth = 1200;
                desiredHeight = 800;
                _spriteBatch.Draw(background, Vector2.Zero, null, Color.White, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0f);
                hero.Draw(_spriteBatch);
               
                hero.DrawBullets(_spriteBatch);
                enemyManager.Draw(_spriteBatch);
                PickupManager.Draw(_spriteBatch);
                scoreUI.Draw(_spriteBatch);
            }
            else if (hero.Dead)
            {
                desiredWidth = 960;
                desiredHeight = 1024;
                _spriteBatch.Draw(backGroundDefeat, Vector2.Zero, null, Color.White, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0f);
                defeatScreen.Draw(_spriteBatch);
            }
            else if(GameStarted == false && FirstUpdate == false && !Victory)
            {
                desiredWidth = 960;
                desiredHeight = 1024;
                _spriteBatch.Draw(backGroundStart, Vector2.Zero, null, Color.White, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0f);
                startScreen.Draw(_spriteBatch);
            }else if (Victory)
            {
                desiredWidth = 960;
                desiredHeight = 1024;
                _spriteBatch.Draw(backGroundVictory, Vector2.Zero, null, Color.White, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0f);
                victoryScreen.Draw(_spriteBatch);
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

        private void InitializeBuildings()
        {
            buildingParams.Clear();
            buildingParams.Add("Building1", new int[] { 140, 109, 90, 120 });
            buildingParams.Add("Building2", new int[] { 49, 168, 38, 50 });
            buildingParams.Add("Building3", new int[] { 223, 273, 118, 38 });
            buildingParams.Add("Building4", new int[] { 441, 123, 180, 110 });
            buildingParams.Add("Building5", new int[] { 691, 138, 40, 55 }); 
            buildingParams.Add("Building6", new int[] { 907, 164, 125, 100 }); 
            buildingParams.Add("Building7", new int[] { 1039, 320, 80, 95 }); 
            buildingParams.Add("Building8", new int[] { 636, 600, 561, 20 }); 
            buildingParams.Add("Building9", new int[] { 0, 586, 564, 20 });
            buildingParams.Add("Building10", new int[] { 0, 107, 1254, 20 }); 
            buildingParams.Add("Building11", new int[] { -30, 147, 17, 681 }); 
            buildingParams.Add("Building12", new int[] { 1198, 147, 19, 689 }); 
            buildingParams.Add("Building13", new int[] { -20, 786, 1201, 14 }); 


            foreach ( var k in buildingParams)
            {
                Building building = new Building(null, new Vector2(k.Value[0], k.Value[1]), new Rectangle((int)k.Value[0], (int)k.Value[1], k.Value[2], k.Value[3]));
                collidables.Add(building);
            }
        }
    }
}