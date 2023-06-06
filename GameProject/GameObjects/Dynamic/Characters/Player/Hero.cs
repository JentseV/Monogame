using GameProject.Animations;
using GameProject.Characters;
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

namespace GameProject.GameObjects.Characters.Player
{
    internal class Hero : Character, IMovable, IGameComponent, ICollidable
    {

        public static float hitPoints2;

        private Texture2D hitboxText;

        public List<IGameComponent> bullets = new List<IGameComponent>();

        private Texture2D bulletTexture;
        private bool moving = false, canShoot = false;
        private Vector2 facing;

        public static float gold;

        public float Gold { get { return gold; } set { gold = value; } }

        private IInputReader inputReader;
        public new IInputReader InputReader
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

            Movable = true;
            Hitpoints = 3f;

            Tag = "Hero";

            Damage = 1f;
            Texture = textures[3];
            TextureIdle = textures[3];
            TextureUpRun = textures[0];
            TextureDownRun = textures[1];
            TextureAttackFront = textures[2];
            TextureIdleFacingUp = textures[5];
            TextureIdleFacingRight = textures[4];
            TextureIdleFacingFront = textures[3];
            TextureRunRight = textures[6];
            TextureAttackRight = textures[7];
            TextureAttackUp = textures[8];
            TextureRunDownRight = textures[11];
            TextureRunUpRight = textures[12];
            TextureAttackDownRight = textures[13];
            TextureAttackUpRight = textures[14];
            TextureIdleFacingUpRight = textures[15];
            TextureIdleFacingDownRight = textures[16];
            bulletTexture = textures[17];
            TextureIdling = TextureIdle;
            TextureRunning = TextureRunRight;
            TextureAttacking = TextureAttackUp;
            TextureHit = textures[18];

            hitboxText = textures[10];
            Position = new Vector2(2f, 2f);

            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, 32, 32);

            InvincibleTimer = 5f;

            AttackCooldown = 0.5f;
            TimeSinceLastAttack = AttackCooldown;
            Speed = new Vector2(3f, 3f);

            AnimationIdle = new Animation();
            AnimationRun = new Animation();
            AnimationAttacking = new Animation();
            AnimationHit = new Animation();

            AnimationHit.GetFramesFromTextureProperties(TextureHit.Width, TextureHit.Height, 1, 1);
            AnimationIdle.GetFramesFromTextureProperties(TextureIdle.Width, TextureIdle.Height, 6, 1);
            AnimationRun.GetFramesFromTextureProperties(TextureRunRight.Width, TextureRunRight.Height, 8, 1);
            AnimationAttacking.GetFramesFromTextureProperties(TextureAttacking.Width, TextureAttacking.Height, 6, 1);

            movementManager = new MovementManager();
            this.inputReader = inputReader;
        }


        public void Update(GameTime gameTime, List<ICollidable> collidables)
        {
            hitPoints2 = Hitpoints;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            TimeSinceLastAttack -= deltaTime;

            UpdateHitbox();
            CheckInvincible(deltaTime);
            DecideAction();
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
            switch (facing)
            {
                case Vector2(1f, 0f):
                    {
                        Flip = SpriteEffects.None;
                        TextureIdling = TextureIdleFacingRight;
                        TextureAttacking = TextureAttackRight;
                        TextureRunning = TextureRunRight;
                        break;
                    }

                case Vector2(-1f, 0f):
                    {
                        Flip = SpriteEffects.FlipHorizontally;
                        TextureRunning = TextureRunRight;
                        TextureIdling = TextureIdleFacingRight;
                        TextureAttacking = TextureAttackRight;
                        break;
                    }
                case Vector2(0f, -1f):
                    {
                        Flip = SpriteEffects.None;
                        TextureIdling = TextureIdleFacingUp;
                        TextureAttacking = TextureAttackUp;
                        TextureRunning = TextureUpRun;
                        break;
                    }
                case Vector2(0f, 1f):
                    {
                        Flip = SpriteEffects.None;
                        TextureIdling = TextureIdleFacingFront;
                        TextureAttacking = TextureAttackFront;
                        TextureRunning = TextureDownRun;
                        break;
                    }

                case Vector2(1f, -1f):
                    {
                        Flip = SpriteEffects.None;
                        TextureRunning = TextureRunUpRight;
                        TextureIdling = TextureIdleFacingUpRight;
                        TextureAttacking = TextureAttackUpRight;
                        break;
                    }

                case Vector2(-1f, -1f):
                    {
                        Flip = SpriteEffects.FlipHorizontally;
                        TextureRunning = TextureRunUpRight;
                        TextureIdling = TextureIdleFacingUpRight;
                        TextureAttacking = TextureAttackUpRight;
                        break;
                    }

                case Vector2(1f, 1f):
                    {
                        Flip = SpriteEffects.None;
                        TextureRunning = TextureRunDownRight;
                        TextureIdling = TextureIdleFacingDownRight;
                        TextureAttacking = TextureAttackDownRight;
                        break;
                    }

                case Vector2(-1f, 1f):
                    {
                        Flip = SpriteEffects.FlipHorizontally;
                        TextureRunning = TextureRunDownRight;
                        TextureIdling = TextureIdleFacingDownRight;
                        TextureAttacking = TextureAttackDownRight;
                        break;
                    }
            }

        }
        public void ChangeInput(IInputReader inputReader)
        {
            this.inputReader = inputReader;
        }

        public new void Move()
        {

            if (Movable) movementManager.Move(this);

        }


        public void UpdateHitbox()
        {
            Center = new Vector2(Position.X + 10, Position.Y + 10);
            hitbox.X = (int)Center.X;
            hitbox.Y = (int)Center.Y;
        }
        public void CheckInvincible(float deltaTime)
        {
            if (Invincible)
            {
                InvincibleTimer -= deltaTime;
                Movable = false;
                if (InvincibleTimer < 4f)
                {
                    Hit = false;
                    Movable = true;
                }
                if (InvincibleTimer < 0)
                {
                    Invincible = false;
                    InvincibleTimer = 5f;

                }
            }
        }
        public void UpdateAnimations(GameTime gameTime)
        {
            if (Moving)
            {
                AnimationRun.Update(gameTime);
            }
            else if (Attacking)
            {
                AnimationAttacking.Update(gameTime);
            }
            else
            {
                AnimationIdle.Update(gameTime);
            }

        }

        public void DecideAction()
        {
            Direction = inputReader.ReadInput();
            if (Direction.X > 0 || Direction.X < 0 || Direction.Y > 0 || Direction.Y < 0)
            {
                Moving = true;
                Attacking = false;
                canShoot = false;
            }
            else
            {
                Moving = false;
                canShoot = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && canShoot)
            {
                Shoot();
            }
            else
            {
                Attacking = false;
            }

        }

        protected new void CheckCollision(List<ICollidable> collidables)
        {
            foreach (ICollidable collidable in collidables)
            {
                if (Hitbox.Intersects(collidable.Hitbox) && !(collidable is Hero))
                {

                    if (collidable is Coffin)
                    {
                        TakeDamage(1F);
                    }

                    if (collidable is Bullet)
                    {
                        Bullet b = collidable as Bullet;
                        if (b.Tag == "CactusBullet" && Invincible == false)
                        {
                            TakeDamage(b.Damage);
                        }
                    }
                }

            }
        }

        public void Shoot()
        {
            if (TimeSinceLastAttack <= 0)
            {
                Attacking = true;
                Bullet bullet = new Bullet(bullets.Count, "BulletHero", new Vector2(Center.X + 2f, Center.Y + 12f), facing, new Vector2(2f, 2f), bulletTexture, Damage);
                bullets.Add(bullet);
                TimeSinceLastAttack = AttackCooldown;
            }
        }

        public void GainGold(float amount)
        {
            Gold += amount;
        }

        public void GainHealth(float amount)
        {
            if (Hitpoints < 3)
            {
                Hitpoints += amount;
            }
            else
            {
                GainGold(1f);
            }

        }


        public void IncreaseSpeed(Vector2 amount)
        {
            Speed += amount;
        }

        public void IncreaseDamage(float amount)
        {
            Damage += amount;
        }


    }
}
