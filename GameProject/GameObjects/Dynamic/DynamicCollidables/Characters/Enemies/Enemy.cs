using GameProject.Animations;
using GameProject.Characters;
using GameProject.Factories;
using GameProject.GameObjects;
using GameProject.GameObjects.Characters.Player;
using GameProject.GameObjects.Dynamic.DynamicCollidables;
using GameProject.GameObjects.Dynamic.DynamicCollidables.Characters.Enemies.Cactus;
using GameProject.Interfaces;
using GameProject.Managers;
using GameProject.Pickups;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace GameProject.Enemies
{
    internal abstract class Enemy : Character, ICollidable, IAnimated , IAttack
    {

        private Hero heroL;
        public Hero HeroL { get { return heroL; } set { heroL = value; } }
        private Vector2 heroPos, facing;
        private float range;
        private bool heroInRange = false;
        public Vector2 Facing { get { return facing; } set { facing = value; } }
        public Vector2 HeroPos { get { return heroPos; } set { heroPos = value; } }
        
        public float Range { get { return range; } set { range = value; } }

        
        public Enemy(Vector2 speed, Vector2 position, Texture2D[] textures, Hero hero) : base(textures)
        {
            HeroL = hero;
            Speed = speed;
            Position = position;
            InitializeAnimations();
            InitializeTextures(textures);
        }


        public override void Update(GameTime gameTime, List<ICollidable> collidables)
        {
            Idling = false;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            TimeSinceLastAttack -= deltaTime;
            
            if (Hit)
            {
                CheckInvincible(deltaTime);
            }
            else
            {
                DecideAction();
            }

            HeroPos = HeroL.Position;
            UpdateHitbox();
            OnDeath();
            GetFacingDirection();
            DecideAnimation();
            UpdateAnimations(gameTime);

        }

        public void CheckInvincible(float delta)
        {
            Invincible = true;
            Moving = false;
            Attacking = false;
            InvincibleTimer += delta;
            if (InvincibleTimer > 1.5f)
            {
                Invincible = false;
                Hit = false;
                InvincibleTimer = 0f;
            }
        }
        public void DecideAction()
        {

            if (Vector2.Distance(HeroPos, this.Position) < Range)
            {
                heroInRange = true;
                Attacking = true;
                Moving = false;
            }
            else
            {
                Moving = true;
                Attacking = false;
                heroInRange = false;
            }

            if (!Attacking && Invincible == false && heroInRange == false )
            {
                Move();
            }
            else
            {
                Attack();
            }
        }

        protected void DecideAnimation()
        {

            switch (Facing)
            {
                case Vector2(1f, 0f):
                    {
                        Flip = SpriteEffects.None;
                        TextureIdling = TextureIdleFacingRight;
                        TextureRunning = TextureRunRight;
                        TextureAttacking = TextureAttackRight;
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
                        TextureRunning = TextureUpRun;
                        TextureAttacking = TextureAttackUp;
                        break;
                    }
                case Vector2(0f, 1f):
                    {
                        Flip = SpriteEffects.None;
                        TextureIdling = TextureIdleFacingFront;
                        TextureRunning = TextureDownRun;
                        TextureAttacking = TextureAttackFront;
                        break;
                    }
            }

        }

        protected void GetFacingDirection()
        {
            Direction = Vector2.Normalize(HeroPos - Position);
            if (Direction.X > 0f && Direction.X > Direction.Y)
            {

                Facing = new Vector2(1f, 0f);
            }
            else if (Direction.X < 0 && Direction.X < Direction.Y)
            {
                Facing = new Vector2(-1f, 0f);
            }

            if (Direction.Y > 0 && Direction.Y > Direction.X)
            {
                Facing = new Vector2(0f, 1f);
            }
            else if (Direction.Y < 0 && Direction.Y < Direction.X)
            {
                Facing = new Vector2(0f, -1f);
            }

        }

        
        public void OnDeath()
        {
            if (Dead)
            {
                Moving = false;
                Attacking = false;

                SpawnPickup();
                Remove = true;
            }
        }

        public override void CheckCollision(ICollidable collidables)
        {
                if (this.Hitbox.Intersects(collidables.Hitbox) && !(collidables is Cactus))
                {
                    if (collidables is Bullet)
                    {
                        Bullet b = collidables as Bullet;
                        if (b.Tag == "BulletHero" && Invincible == false)
                        {
                            TakeDamage(b.Damage);
                        }
                    }

                }
        }

        public void SpawnPickup()
        {
            Pickup pickup = PickupFactory.SpawnPickup(PickupManager.CountPickups(), "Pickup", this.Center, 5f, HeroL);
            PickupManager.AddPickup(pickup);  
        }

        public override void InitializeTextures(Texture2D[] textures)
        {
            
            TextureIdle = textures[0];
            TextureRunRight = textures[1];
            TextureUpRun = textures[2];
            TextureIdleFacingUp = textures[3];
            HitboxText = textures[9];
            TextureIdleFacingRight = textures[4];
            TextureIdleFacingFront = textures[0];
            TextureDownRun = textures[5];
            TextureAttackRight = textures[7];
            TextureAttackUp = textures[8];
            TextureAttackFront = textures[6];
            TextureHit = textures[10];

            TextureIdling = TextureIdle;
            TextureRunning = TextureRunRight;
            TextureAttacking = TextureAttackFront;

        }
    }
}
