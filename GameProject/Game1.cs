
using GameProject.Enemies;
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
        public static Texture2D healthTexture,coinTexture;
        private static List<Pickup> s = new List<Pickup>();


        private UI ui;
        
        private Rectangle backgroundRect;
        private Texture2D background,buttonText;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Hero hero;
        private Button button;
        private SpriteFont font;
        private UpgradeUI upgradeUI;
        private int[] test = { 10, 50, 100, 300, 200, 10, 50, 100, 300, 200 };
        private List<Coffin> coffins = new List<Coffin>();
        private List<Cactus> cacti = new List<Cactus>();
        private List<Coyote> coyotes = new List<Coyote>();
        private List<ICollidable> collidables = new List<ICollidable>();
        private Texture2D[] coffinTextures = new Texture2D[11];
        private Texture2D[] heroTextures = new Texture2D[19];
        private Texture2D[] cactusTextures = new Texture2D[12];
        private Texture2D[] coyoteTextures = new Texture2D[12];

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
            ui = new UI(font);
            
            button = new Button(buttonText, new Vector2(500f, 300f), "Buy Movement Speed ", font,() => hero.Hitpoints+=3);
            hero = new Hero(heroTextures, new KeyboardReader());
            
            for(int i = 0; i < 5; i++)
            {
                coffins.Add(new Coffin(new Vector2(1f, 1f), new Vector2(test[i+2], test[i+2]), coffinTextures));
            }

            for (int i = 0; i < 5; i++)
            {
                cacti.Add(new Cactus(new Vector2(1f, 1f), new Vector2(test[i+3], test[i+3]), cactusTextures));
            }

            for (int i = 0; i < 5; i++)
            {
                coyotes.Add(new Coyote(new Vector2(1f, 1f), new Vector2(test[i], test[i]), coyoteTextures));
            }

            upgradeUI = new UpgradeUI(font, buttonText, hero);
            hero.Position = new Vector2(600f, 600f);
            collidables.Add(hero);
            backgroundRect = new Rectangle(0, 0, 32, 47);
            screen = new SplashScreen();

        }

        protected override void LoadContent()
        {
            heroTextures[0] = Content.Load<Texture2D>("heroText/Fixed/runUp");
            heroTextures[1] = Content.Load<Texture2D>("heroText/Fixed/runDown");
            heroTextures[2] = Content.Load<Texture2D>("heroText/Fixed/shootFront");
            heroTextures[3] = Content.Load<Texture2D>("heroText/Fixed/idleFacingFront");
            heroTextures[4] = Content.Load<Texture2D>("heroText/Fixed/idleFacingRight");
            heroTextures[5] = Content.Load<Texture2D>("heroText/Fixed/idleFacingUp");
            heroTextures[6] = Content.Load<Texture2D>("heroText/Fixed/runRight");
            heroTextures[7] = Content.Load<Texture2D>("heroText/Fixed/shootRight");
            heroTextures[8] = Content.Load<Texture2D>("heroText/Fixed/shootUp");
            heroTextures[9] = Content.Load<Texture2D>("heroText/Fixed/runDown");
            heroTextures[10] = Content.Load<Texture2D>("heroText/red_square");
            heroTextures[11] = Content.Load<Texture2D>("heroText/Fixed/runDownRight");
            heroTextures[12] = Content.Load<Texture2D>("heroText/Fixed/runUpRight");
            heroTextures[13] = Content.Load<Texture2D>("heroText/Fixed/shootDownRight");
            heroTextures[14] = Content.Load<Texture2D>("heroText/Fixed/shootUpRight");
            heroTextures[15] = Content.Load<Texture2D>("heroText/Fixed/idleFacingUpRight");
            heroTextures[16] = Content.Load<Texture2D>("heroText/Fixed/idleFacingDownRight");

            heroTextures[17] = Content.Load<Texture2D>("Projectiles/SpongeBullet");
            heroTextures[18] = Content.Load<Texture2D>("heroText/Fixed/heroHitAnimation");

            coffinTextures[0] = Content.Load<Texture2D>("coffinEn/coffinIdleFront");
            coffinTextures[1] = Content.Load<Texture2D>("coffinEn/coffinWalkRight");
            coffinTextures[2] = Content.Load<Texture2D>("coffinEn/coffinRunUp");
            coffinTextures[3] = Content.Load<Texture2D>("coffinEn/coffinIdleUp");
            coffinTextures[4] = Content.Load<Texture2D>("coffinEn/coffinIdleRight");
            coffinTextures[5] = Content.Load<Texture2D>("coffinEn/coffinRunDown");
            coffinTextures[6] = Content.Load<Texture2D>("coffinEn/coffinAttackFront");
            coffinTextures[7] = Content.Load<Texture2D>("coffinEn/coffinAttackRight");
            coffinTextures[8] = Content.Load<Texture2D>("coffinEn/coffinAttackUp");
            coffinTextures[9] = Content.Load<Texture2D>("heroText/red_square");
            coffinTextures[10] = Content.Load<Texture2D>("coffinEn/coffinHitAnimation");

            cactusTextures[0] = Content.Load<Texture2D>("cactusEn/cactusIdleFront");
            cactusTextures[1] = Content.Load<Texture2D>("cactusEn/cactusRunRight");
            cactusTextures[2] = Content.Load<Texture2D>("cactusEn/cactusRunUp");
            cactusTextures[3] = Content.Load<Texture2D>("cactusEn/cactusIdleUp");
            cactusTextures[4] = Content.Load<Texture2D>("cactusEn/cactusIdleRight");
            cactusTextures[5] = Content.Load<Texture2D>("cactusEn/cactusRunDown");
            cactusTextures[6] = Content.Load<Texture2D>("cactusEn/cactusAttackFront");
            cactusTextures[7] = Content.Load<Texture2D>("cactusEn/cactusAttackRight");
            cactusTextures[8] = Content.Load<Texture2D>("cactusEn/cactusAttackUp");
            cactusTextures[9] = Content.Load<Texture2D>("heroText/red_square");
            cactusTextures[10] = Content.Load<Texture2D>("cactusEn/cactusHitAnimation");
            cactusTextures[11] = Content.Load<Texture2D>("Projectiles/SpongeBullet");

            coyoteTextures[0] = Content.Load<Texture2D>("coyoteEn/coyoteIdleFront");
            coyoteTextures[1] = Content.Load<Texture2D>("coyoteEn/coyoteRunRight");
            coyoteTextures[2] = Content.Load<Texture2D>("coyoteEn/coyoteRunUp");
            coyoteTextures[3] = Content.Load<Texture2D>("coyoteEn/coyoteIdleUp");
            coyoteTextures[4] = Content.Load<Texture2D>("coyoteEn/coyoteIdleRight");
            coyoteTextures[5] = Content.Load<Texture2D>("coyoteEn/coyoteRunDown");
            coyoteTextures[6] = Content.Load<Texture2D>("coyoteEn/coyoteAttackFront");
            coyoteTextures[7] = Content.Load<Texture2D>("coyoteEn/coyoteAttackRight");
            coyoteTextures[8] = Content.Load<Texture2D>("coyoteEn/coyoteAttackUp");
            coyoteTextures[9] = Content.Load<Texture2D>("heroText/red_square");
            coyoteTextures[10] = Content.Load<Texture2D>("coyoteEn/coyoteHitAnimation");
            coyoteTextures[11] = Content.Load<Texture2D>("Projectiles/SpongeBullet");

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

            upgradeUI.Update(Mouse.GetState()) ;


            hero.Update(gameTime, collidables);


            foreach (ICollidable x in collidables.ToList())
            {

                if (x is Bullet)
                {
                    Bullet t = x as Bullet;
                    if (t.destroy == true)
                    {
                        collidables.Remove(t);
                    }
                }

                else if (x is Enemy)
                {
                    Enemy t = x as Enemy;
                    if (t.Remove == true)
                    {
                        collidables.Remove(t);
                    }
                }

                else if (x is Pickup)
                {
                    Pickup t = x as Pickup;
                    if (t.Despawn == true)
                    {
                        collidables.Remove(t);
                    }
                }
            }

            foreach (Bullet b in hero.bullets)
            {

                if (b.destroy == false)
                {
                    b.Update(gameTime, collidables);
                    if (collidables.Contains(b) == false) collidables.Add(b);
                }
            }


            foreach(Coyote co in coyotes)
            {
                if(co.Remove == false)
                {
                    co.Update(gameTime, hero, collidables);
                    if (collidables.Contains(co) == false) collidables.Add(co);
                }
            }

            foreach (Cactus c in cacti)
            {
                if (c.Remove == false)
                {
                    c.Update(gameTime, hero, collidables);
                    if (collidables.Contains(c) == false) collidables.Add(c);

                }

                foreach (Bullet b in c.cactusBullets)
                {
                    if(b.destroy == false)
                    {
                        b.Update(gameTime, collidables);
                        if (collidables.Contains(b) == false) collidables.Add(b);
                    }
                    
                }

            }

            foreach(Coffin c in coffins)
            {
                
                if(c.Remove == false)
                {
                    if(collidables.Contains(c) == false) collidables.Add(c);
                    c.Update(gameTime, hero, collidables);
                }
                
            }


            foreach (ICollidable c in collidables)
            {
                if (c is Coin)
                {
                    Coin p = c as Coin;
                    p.Update(gameTime, collidables);
                }
                else if (c is Health)
                {
                    Health h = c as Health;
                    h.Update(gameTime, collidables);
                }
            }


            




            ui.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();


            _spriteBatch.Draw(background, Vector2.Zero, null, Color.White, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0f);
            hero.Draw(_spriteBatch);

            
            if(!button.Destroy) button.Draw(_spriteBatch);

            foreach (Coyote co in coyotes)
            {
                if (co.Remove == false)
                {
                    co.Draw(_spriteBatch);
                }
            }


            foreach (Cactus c in cacti)
            {
                if (c.Remove == false)
                {
                    c.Draw(_spriteBatch);

                }

                foreach (Bullet b in c.cactusBullets)
                {
                    if (b.destroy == false)
                    {
                        b.Draw(_spriteBatch);
                    }

                }

            }

            foreach (Bullet b in hero.bullets)
            {
                if (b.destroy == false)
                {
                    b.Draw(_spriteBatch);
                }

            }

            foreach (Coffin c in coffins)
            {
                if (c.Remove == false)
                {
                    c.Draw(_spriteBatch);
                }

            }

            foreach (ICollidable c in collidables)
            {
                if(c is Coin)
                {
                    Coin p = c as Coin;
                    p.Draw(_spriteBatch);
                }
                else if(c is Health)
                {
                    Health h = c as Health;
                    h.Draw(_spriteBatch);
                }
            }

            upgradeUI.Draw(_spriteBatch);
            ui.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}