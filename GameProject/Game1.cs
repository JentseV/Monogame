using GameProject.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Hero hero;
        private Texture2D[] heroTextures = new Texture2D[11];

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
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            hero.Update(gameTime);
            hero.CheckCollision(testHitboxes);
            
             
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            hero.Draw(_spriteBatch);
            _spriteBatch.Draw(heroTextures[10], testHitbox,Color.Green);
            _spriteBatch.Draw(heroTextures[10], testHitbox2, Color.Green);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}