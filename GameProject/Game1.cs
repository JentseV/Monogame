using GameProject.Content;
using GameProject.Enemies;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Hero hero;
        private Coffin coffin;
        private int[] test = { 10, 50, 100, 300, 200 };
        private List<Coffin> coffins = new List<Coffin>();
        private Texture2D[] coffinTextures = new Texture2D[10];
        private Texture2D[] heroTextures = new Texture2D[18];
        private Texture2D bulletText;

        private Bullet bullet;

        private Rectangle[] testHitboxes = new Rectangle[2];
        private Rectangle testHitbox, testHitbox2;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            hero = new Hero(heroTextures, new KeyboardReader());
            coffin = new Coffin(new Vector2(1f,1f),new Vector2(50f,50f),coffinTextures);
            for(int i = 0; i < 3; i++)
            {
                coffins.Add(new Coffin(new Vector2(1f, 1f), new Vector2(test[i], test[i]), coffinTextures));
            }
            testHitbox = new Rectangle(250, 100, 32, 32);
            testHitbox2 = new Rectangle(550, 100, 32, 32);
            testHitboxes[0] = testHitbox;
            testHitboxes[1] = testHitbox2;
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




            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            hero.Update(gameTime);
            
            foreach(Coffin c in coffins)
            {
                if(c.dead == false)
                {
                    c.Update(gameTime, hero, hero.bullets);
                }
                
            }
            hero.CheckCollision(testHitboxes);
            
             
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            hero.Draw(_spriteBatch);
            

            foreach (Coffin c in coffins)
            {
                if(c.dead == false)
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