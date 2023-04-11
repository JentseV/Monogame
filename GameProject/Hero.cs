using GameProject.Animations;
using GameProject.Enemies;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace GameProject
{
    internal class Hero : Character, IMovable, IGameComponent, ICollidable
    {

        public static float hitPoints2;

        private Texture2D _texture, _textureRunning, _textureIdling, _textureShooting, _textureUpRun, _textureDownRun;
        private Texture2D _textureIdleFacingFront, _textureIdleFacingRight, _textureIdleFacingUp, _textureIdleFacingUpRight, _textureIdleFacingDownRight, _textureIdle;
        private Texture2D _textureShootUp, _textureShootFront, _textureShootRight, _textureShootUpRight, _textureShootDownRight;
        private Texture2D _textureRunRight, _textureRunUpRight, _textureRunDownRight;
        private Texture2D hitboxText;

        public List<IGameComponent> bullets = new List<IGameComponent>();

        private Texture2D bulletTexture;
        private bool moving = false, movable = true, shooting = false, canShoot = false;
        private Vector2 facing;
        private SpriteEffects flip = SpriteEffects.None;
        private Animation _animation;
        private Animation _animationRun;
        private Animation _animationShooting;

        public static float gold;

        public float Gold { get { return gold;  } set { gold = value; } }

        private IInputReader inputReader;
        public new  IInputReader InputReader
        {
            get { return inputReader; }
            set { inputReader = value; }
        }
        public new Rectangle Hitbox
        {
            get
            {
                return hitbox;
            }
            set
            {
                hitbox = value;
            }
        }

        private MovementManager movementManager;
        public Hero(Texture2D[] textures, IInputReader inputReader)
        {


            Hitpoints = 3f;

            Tag = "Hero";

            _texture = textures[3];
            _textureIdle = textures[3];
            _textureUpRun = textures[0];
            _textureDownRun = textures[1];
            _textureShootFront = textures[2];
            _textureIdleFacingUp = textures[5];
            _textureIdleFacingRight = textures[4];
            _textureIdleFacingFront = textures[3];
            _textureRunRight = textures[6];
            _textureShootRight = textures[7];
            _textureShootUp = textures[8];
            _textureRunDownRight = textures[11];
            _textureRunUpRight = textures[12];
            _textureShootDownRight = textures[13];
            _textureShootUpRight = textures[14];
            _textureIdleFacingUpRight = textures[15];
            _textureIdleFacingDownRight = textures[16];
            bulletTexture = textures[17];
            _textureIdling = _textureIdle;
            _textureRunning = _textureRunRight;
            _textureShooting = _textureShootUp;

            hitboxText = textures[10];
            Position = new Vector2(2f, 2f);

            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, 32, 32);


            AttackCooldown = 0.5f;
            TimeSinceLastAttack = AttackCooldown;
            Speed = new Vector2(3f, 3f);

            _animation = new Animation();
            _animationRun = new Animation();
            _animationShooting = new Animation();

            _animation.GetFramesFromTextureProperties(_texture.Width, _texture.Height, 6, 1);
            _animationRun.GetFramesFromTextureProperties(_textureRunning.Width, _textureRunning.Height, 8, 1);
            _animationShooting.GetFramesFromTextureProperties(_textureShooting.Width, _textureShooting.Height, 6, 1);

            movementManager = new MovementManager();
            this.inputReader = inputReader;
        }


        public new void Draw(SpriteBatch spriteBatch)
        {


            if (Keyboard.GetState().IsKeyDown(Keys.Space) && canShoot)
            {

                spriteBatch.Draw(_textureShooting, Position, _animationShooting.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0f, 0f), 1f, flip, 0f);
            }
            else if (moving)
            {

                spriteBatch.Draw(_textureRunning, Position, _animationRun.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0f, 0f), 1f, flip, 0f);
            }
            else
            {

                spriteBatch.Draw(_textureIdling, Position, _animation.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0f, 0f), 1f, flip, 0f);
            }

            //spriteBatch.Draw(hitboxText, Center, Hitbox, Color.White, 0f , new Vector2(0f,0f), 1f,SpriteEffects.None,0f);


        }


        public void Update(GameTime gameTime, List<ICollidable> collidables)
        {
            hitPoints2 = Hitpoints;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            TimeSinceLastAttack -= deltaTime;

            //Debug.WriteLine(Invincible);

            if (Invincible)
            {
                InvincibleTimer -= deltaTime;
                if (InvincibleTimer < 0)
                {
                    Invincible = false;
                    InvincibleTimer = 1f;
                }
            }
            Center = new Vector2(Position.X + 10, Position.Y + 10);
            hitbox.X = (int)Center.X;
            hitbox.Y = (int)Center.Y;

            Direction = inputReader.ReadInput();
            if (Direction.X > 0 || Direction.X < 0 || Direction.Y > 0 || Direction.Y < 0)
            {
                moving = true;
                shooting = false;
                canShoot = false;
            }
            else
            {
                moving = false;
                canShoot = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && canShoot)
            {
                Shoot();
            }

            CheckCollision(collidables);
            Move();
            GetFacingDirection();
            DecideAnimation();
            UpdateAnimations(gameTime);

        }

        private void GetFacingDirection()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                facing = new Vector2(1f, 0f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                facing = new Vector2(-1f, 0f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                facing = new Vector2(0f, -1f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                facing = new Vector2(0f, 1f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                facing = new Vector2(1f, -1f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                facing = new Vector2(-1f, -1f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                facing = new Vector2(-1f, 1f);

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                facing = new Vector2(1f, 1f);
            }

        }

        private void DecideAnimation()
        {

            if (moving)
            {
                switch (facing)
                {
                    case Vector2(1f, 0f):
                        {
                            flip = SpriteEffects.None;
                            _textureIdling = _textureIdleFacingRight;
                            _textureShooting = _textureShootRight;
                            _textureRunning = _textureRunRight;
                            break;
                        }

                    case Vector2(-1f, 0f):
                        {
                            flip = SpriteEffects.FlipHorizontally;
                            _textureRunning = _textureRunRight;
                            _textureIdling = _textureIdleFacingRight;
                            _textureShooting = _textureShootRight;
                            break;
                        }
                    case Vector2(0f, -1f):
                        {
                            flip = SpriteEffects.None;
                            _textureIdling = _textureIdleFacingUp;
                            _textureShooting = _textureShootUp;
                            _textureRunning = _textureUpRun;
                            break;
                        }
                    case Vector2(0f, 1f):
                        {
                            flip = SpriteEffects.None;
                            _textureIdling = _textureIdleFacingFront;
                            _textureShooting = _textureShootFront;
                            _textureRunning = _textureDownRun;
                            break;
                        }

                    case Vector2(1f, -1f):
                        {
                            flip = SpriteEffects.None;
                            _textureRunning = _textureRunUpRight;
                            _textureIdling = _textureIdleFacingUpRight;
                            _textureShooting = _textureShootUpRight;
                            break;
                        }

                    case Vector2(-1f, -1f):
                        {
                            flip = SpriteEffects.FlipHorizontally;
                            _textureRunning = _textureRunUpRight;
                            _textureIdling = _textureIdleFacingUpRight;
                            _textureShooting = _textureShootUpRight;
                            break;
                        }

                    case Vector2(1f, 1f):
                        {
                            flip = SpriteEffects.None;
                            _textureRunning = _textureRunDownRight;
                            _textureIdling = _textureIdleFacingDownRight;
                            _textureShooting = _textureShootDownRight;
                            break;
                        }

                    case Vector2(-1f, 1f):
                        {
                            flip = SpriteEffects.FlipHorizontally;
                            _textureRunning = _textureRunDownRight;
                            _textureIdling = _textureIdleFacingDownRight;
                            _textureShooting = _textureShootDownRight;
                            break;
                        }
                }
            }
        }
        public void ChangeInput(IInputReader inputReader)
        {
            this.inputReader = inputReader;
        }

        public new void Move()
        {

            if (movable) movementManager.Move(this);

        }

        public void UpdateAnimations(GameTime gameTime)
        {
            if (moving)
            {
                _animationRun.Update(gameTime);
            }
            else if (shooting)
            {
                _animationShooting.Update(gameTime);
            }
            else
            {
                _animation.Update(gameTime);
            }

        }

        protected new void CheckCollision(List<ICollidable> collidables)
        {
            foreach (ICollidable collidable in collidables)
            {
                if (this.Hitbox.Intersects(collidable.Hitbox) && !(collidable is Hero))
                {
                    
                    if(collidable is Coffin)
                    {
                        TakeDamage(1F);
                    }

                    if (collidable is Bullet)
                    {
                        Bullet b = collidable as Bullet;
                        if (b.Tag.Equals("CactusBullet")  && Invincible == false)
                        {
                            Debug.WriteLine("Shot by enemy");
                            TakeDamage(1f);
                        }
                    }
                }

            }
        }

        public void Shoot()
        {
            if (TimeSinceLastAttack <= 0)
            {
                shooting = true;
                Bullet bullet = new Bullet(bullets.Count, "BulletHero", new Vector2(Center.X + 2f, Center.Y + 12f), facing, new Vector2(2f, 2f), bulletTexture);
                bullets.Add(bullet);
                TimeSinceLastAttack = AttackCooldown;
            }
        }

        public void GainGold(float amount)
        {
            this.Gold += amount;
        }

        public void GainHealth(float amount)
        {
            this.Hitpoints += amount;
        }

    }
}
