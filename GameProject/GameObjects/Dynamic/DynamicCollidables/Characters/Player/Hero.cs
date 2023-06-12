using GameProject.Animations;
using GameProject.Characters;
using GameProject.Enemies;
using GameProject.GameObjects.Dynamic.Characters.Enemies;
using GameProject.GameObjects.Dynamic.DynamicCollidables;
using GameProject.GameObjects.Dynamic.DynamicCollidables.Characters.Enemies;
using GameProject.GameObjects.Dynamic.DynamicCollidables.Characters.Enemies.Coffin;
using GameProject.GameObjects.Dynamic.DynamicCollidables.Characters.Enemies.Coyote;
using GameProject.GameObjects.Static.StaticCollidable;
using GameProject.GameObjects.Static.StaticCollidable.Buildings;
using GameProject.Interfaces;
using GameProject.Pickups;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGameComponent = GameProject.Interfaces.IGameComponent;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace GameProject.GameObjects.Characters.Player
{
    internal class Hero : Character, IMovable, IGameComponent, ICollidable, IRangedAttacker, IPickupObserver, IAttack
    {

        private List<SoundEffect> soundEffects;

        public static float hitPoints2;
        private List<Bullet> bullets = new List<Bullet>();
        public List<Bullet> Bullets { get { return bullets; } set { bullets = value; } }

        private Texture2D bulletTexture;
        private bool canShoot = false;
        private Vector2 facing;

        public static float gold;
        private Vector2 previousPosition;
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
        public Hero(Texture2D[] textures, IInputReader inputReader, List<SoundEffect> sounds)
        {

            this.soundEffects = sounds;
            Movable = true;
            Hitpoints = 3f;

            Tag = "Hero";
            this.Position = new Vector2(500F, 500F);
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
            TextureHit = textures[18];
            HitboxText = textures[10];


            TextureIdling = TextureIdle;
            TextureRunning = TextureRunRight;
            TextureAttacking = TextureAttackUp;
            

            
            Position = new Vector2(589f, 703f);

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
            previousPosition = Position;

            if (Hit)
            {
                Invincible = true;
                CheckInvincible(deltaTime);
            }
            
            UpdateHitbox();
            CheckInvincible(deltaTime);
            DecideAction();
            UpdateBullets(gameTime,collidables);
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

        public void CheckInvincible(float deltaTime)
        {
            if (Invincible)
            {
                InvincibleTimer -= deltaTime;
                Movable = false;
                if (InvincibleTimer < 3f)
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


        public void DecideAction()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                this.Speed += new Vector2(0.1f, 0.1f); // momentum + acceleratie
            }
            else
            {
                this.Speed = new Vector2(3f, 3f);
            }

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
                Attack();
            }
            else
            {
                Attacking = false;
            }

        }

        public override void CheckCollision(ICollidable collidable)
        {
            
                if (Hitbox.Intersects(collidable.Hitbox) && !(collidable is Hero) && !Invincible)
                {

                    if (collidable is Enemy)
                    {
                        
                        TakeDamage(1);
                    }

                    if (collidable is Bullet)
                    {
                        Bullet b = collidable as Bullet;
                        if (b.Tag == "CactusBullet" && !Invincible && !b.Remove)
                        {
                            TakeDamage(b.Damage);
                        }
                    }

                    if(collidable is Building)
                    {
                        Rectangle intersection = Rectangle.Intersect(Hitbox, collidable.Hitbox);

                        
                        float depthX = intersection.Width;
                        float depthY = intersection.Height;

                        
                        float signX = Math.Sign(Position.X - collidable.Position.X);
                        float signY = Math.Sign(Position.Y - collidable.Position.Y);

                        if (depthX < depthY)
                        {
                            
                            float displacementX = depthX * signX;
                            Position = new Vector2(Position.X + displacementX, Position.Y);
                        }
                        else
                        {
                            float displacementY = depthY * signY;
                            Position = new Vector2(Position.X, Position.Y + displacementY);
                        }

                        UpdateHitbox();
                    }
                }

            
        }
        private Random random = new Random();
        public override void Attack()
        {
            if (TimeSinceLastAttack <= 0)
            {
                Attacking = true;
                Bullet bullet = new Bullet(bullets.Count, "BulletHero", new Vector2(Center.X + 2f, Center.Y + 12f), facing, new Vector2(2f, 2f), bulletTexture, Damage);
                bullets.Add(bullet);
                TimeSinceLastAttack = AttackCooldown;

                int randomIndex = random.Next(0, 2); 
                PlaySound(soundEffects[randomIndex]);
            }
        }

        private void PlaySound(SoundEffect soundEffect)
        {
            SoundEffectInstance instance = soundEffect.CreateInstance();
            instance.Stop();
            instance.Play();
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

        public void UpdateBullets(GameTime gameTime, List<ICollidable> collidables)
        {
            foreach (Bullet b in Bullets)
            {
                if (!b.Remove)
                {
                    b.Update(gameTime);
                    if (!collidables.Contains(b))
                        collidables.Add(b);
                }
               
            }
        }

        public void DrawBullets(SpriteBatch spriteBatch)
        {
            
            foreach (Bullet b in bullets)
            {
                if (!b.Remove) b.Draw(spriteBatch);
            }
            
        }

        public void OnPickup(Pickup pickup) // hardcoded values, change later
        {
            if(pickup is Health && !pickup.Remove)
            {
                GainHealth(1f);
            }else if(pickup is Coin && !pickup.Remove)
            {
                GainGold(1f);
            }
        }
    }
}
