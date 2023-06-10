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
       
        private int TeleportCount = 0;
        
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


            TextureIdling = TextureIdle;
            TextureRunning = TextureRunRight;
            TextureAttacking = TextureAttackFront;

            AttackCooldown = 2f;

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



        protected override void Attack()
        {

            if (TimeSinceLastAttack <= 0 && Invincible == false)
            {
                Attacking = true;

                
                Vector2 teleportPosition = CalculateTeleportPosition();

                
                Position = teleportPosition;

                
                TeleportCount++;

                TimeSinceLastAttack = AttackCooldown;
            }
        }


        private Vector2 CalculateTeleportPosition()
        {
            
            Vector2 playerPosition = HeroPos;

            
            float teleportAngle = RandomAngle();

            
            float initialTeleportDistance = 200f; 
            float teleportDistance = initialTeleportDistance * (1f / (TeleportCount + 1));

            
            Vector2 teleportOffset = new Vector2(
                (float)Math.Cos(teleportAngle) * teleportDistance,
                (float)Math.Sin(teleportAngle) * teleportDistance
            );
            Vector2 teleportPosition = playerPosition + teleportOffset;

            
            Vector2 direction = Vector2.Normalize(teleportPosition - playerPosition);
            teleportPosition += direction * 100f;

            
            teleportPosition = ClampToGameBoundaries(teleportPosition);

            return teleportPosition;
        }




        private Random random = new Random();

        private float RandomAngle()
        {
            float angle = (float)(random.NextDouble() * Math.PI * 2);
            return angle;
        }

        private Vector2 ClampToGameBoundaries(Vector2 position)
        {
            
            float minX = 0;
            float minY = 0;
            float maxX = 1200;
            float maxY = 800;

          
            float clampedX = MathHelper.Clamp(position.X, minX, maxX);
            float clampedY = MathHelper.Clamp(position.Y, minY, maxY);

            return new Vector2(clampedX, clampedY);
        }


    }
}

