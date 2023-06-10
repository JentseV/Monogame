using GameProject.Animations;
using GameProject.Enemies;
using GameProject.GameObjects.Characters.Player;
using GameProject.Pickups;
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


namespace GameProject.GameObjects.Dynamic.DynamicCollidables.Characters.Enemies.Coyote
{
    internal class Coyote : Enemy, IEnemy
    {
        //SPRITESHEET 70x70

        private Texture2D BulletTexture;
        private bool heroInRange = false;

        private float channelTime = 0f;

        private float range;

        private Texture2D hitboxText;

        public List<Bullet> fireBalls = new List<Bullet>();

        public Coyote(Vector2 speed, Vector2 position, Texture2D[] textures)
        {

            Movable = true;
            Hitpoints = 3;
            Speed = speed;
            Position = position;
            Range = 400f;
            Center = new Vector2(50 + Position.X, 55 + Position.Y);
            Hitbox = new Rectangle((int)Center.X, (int)Center.Y, 45, 45);
            TextureIdle = textures[0];
            TextureRunRight = textures[1];
            TextureUpRun = textures[2];
            TextureIdleFacingUp = textures[3];
            HitboxText = textures[9];
            TextureRunLeft = textures[12];
            TextureIdleFacingRight = textures[4];
            TextureIdleFacingFront = textures[0];
            TextureDownRun = textures[5];

            TextureAttackRight = textures[7];
            TextureAttackUp = textures[8];
            TextureAttackFront = textures[6];
            TextureHit = textures[10];
            TextureAttackLeft = textures[13];

            BulletTexture = textures[11];

            TextureIdling = TextureIdle;
            TextureRunning = TextureRunRight;
            TextureAttacking = TextureAttackFront;

            AttackCooldown = 0.5f;

            TimeSinceLastAttack = AttackCooldown;

            AnimationIdle = new Animation();
            AnimationRun = new Animation();
            AnimationAttacking = new Animation();
            AnimationHit = new Animation();

            AnimationHit.GetFramesFromTextureProperties(TextureHit.Width, TextureHit.Height, 1, 1);
            AnimationIdle.GetFramesFromTextureProperties(TextureIdle.Width, TextureIdle.Height, 7, 1);
            AnimationRun.GetFramesFromTextureProperties(TextureRunRight.Width, TextureRunRight.Height, 14, 1);
            AnimationAttacking.GetFramesFromTextureProperties(TextureAttacking.Width, TextureAttacking.Height, 24, 1);
        }



        public new void Update(GameTime gameTime, Hero hero, List<ICollidable> collidables)
        {
            base.Update(gameTime, hero, collidables);
        }


        protected override void Attack()
        {

            if (TimeSinceLastAttack <= 0 && Invincible == false)
            {
                Attacking = true;

                TimeSinceLastAttack = AttackCooldown;
            }
        }


    }
}

