﻿
using GameProject.Enemies;
using GameProject.Projectiles;
using GameProject.Screens;
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
        private Rectangle backgroundRect;
        private Texture2D background;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Hero hero;
        private Coffin coffin;
        private int[] test = { 10, 50, 100, 300, 200 };
        private List<Coffin> coffins = new List<Coffin>();
        private List<Cactus> cacti = new List<Cactus>();
        private List<ICollidable> collidables = new List<ICollidable>();
        private Texture2D[] coffinTextures = new Texture2D[11];
        private Texture2D[] heroTextures = new Texture2D[18];
        private Texture2D[] cactusTextures = new Texture2D[12];
        
        private Rectangle[] testHitboxes = new Rectangle[2];
        private Rectangle testHitbox, testHitbox2;

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
            hero = new Hero(heroTextures, new KeyboardReader());
            
            for(int i = 0; i < 1; i++)
            {
                coffins.Add(new Coffin(new Vector2(1f, 1f), new Vector2(test[i], test[i]), coffinTextures));
            }

            for (int i = 0; i < 1; i++)
            {
                cacti.Add(new Cactus(new Vector2(1f, 1f), new Vector2(test[i+3], test[i+3]), cactusTextures));
            }


            hero.Position = new Vector2(600f, 600f);
            collidables.Add(hero);
            testHitbox = new Rectangle(250, 100, 32, 32);
            testHitbox2 = new Rectangle(550, 100, 32, 32);
            testHitboxes[0] = testHitbox;
            testHitboxes[1] = testHitbox2;
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

            background = Content.Load<Texture2D>("World/background");


            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            screen.Update(gameTime);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here


            hero.Update(gameTime, collidables);

            foreach (Bullet b in hero.bullets)
            {

                if (b.destroy == false)
                {
                    b.Update(gameTime, collidables);
                    if (collidables.Contains(b) == false) collidables.Add(b);
                }
            }

            foreach (Cactus c in cacti)
            {
                if (c.Dead == false)
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
                
                if(c.Dead == false)
                {
                    if(collidables.Contains(c) == false) collidables.Add(c);
                    c.Update(gameTime, hero, collidables);
                }
                
            }

            foreach (ICollidable x in collidables.ToList())
            {
                
                if(x is Bullet)
                {
                    Bullet t = x as Bullet;
                    if(t.destroy == true)
                    {
                        collidables.Remove(t);
                    }
                }

                else if (x is Enemy)
                {
                    Enemy t = x as Enemy;
                    if (t.Dead == true)
                    {
                        collidables.Remove(t);
                    }
                }
            }

            
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            

            _spriteBatch.Draw(background, Vector2.Zero , null, Color.White, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0f);
            hero.Draw(_spriteBatch);


            foreach(Cactus c in cacti)
            {
                if(c.Dead == false)
                {
                    c.Draw(_spriteBatch);
                    foreach (Bullet b in c.cactusBullets)
                    {
                        if(b.destroy == false)
                        {
                            b.Draw(_spriteBatch);
                        }
                       
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
                if(c.Dead == false)
                {
                    c.Draw(_spriteBatch);
                }
                
            }

            
            //_spriteBatch.Draw(coffinTextures[0], new Vector2(50f, 50f), Color.White);
            //_spriteBatch.Draw(heroTextures[10], testHitbox,Color.Green);
            //_spriteBatch.Draw(heroTextures[10], testHitbox2, Color.Green);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}