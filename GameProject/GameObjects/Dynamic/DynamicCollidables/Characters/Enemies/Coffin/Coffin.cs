using GameProject.Animations;
using GameProject.Enemies;
using GameProject.GameObjects.Characters.Player;
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


namespace GameProject.GameObjects.Dynamic.DynamicCollidables.Characters.Enemies.Coffin
{
    internal class Coffin : Enemy, IEnemy
    {

        public Coffin(Vector2 speed, Vector2 position, Texture2D[] textures)
        {
            Hitpoints = 3;
            Range = 32f;
            Movable = true;
            //SPRITESHEET 74x55
            Speed = speed;
            Position = position;

            Center = new Vector2(50 + Position.X, 55 + Position.Y);
            Hitbox = new Rectangle((int)Center.X, (int)Center.Y, 45, 45);
            TextureIdle = textures[1];
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

            TimeSinceLastAttack = AttackCooldown;

            AnimationIdle = new Animation();
            AnimationRun = new Animation();
            AnimationAttacking = new Animation();
            AnimationHit = new Animation();

            AnimationHit.GetFramesFromTextureProperties(TextureHit.Width, TextureHit.Height, 1, 1);
            AnimationIdle.GetFramesFromTextureProperties(TextureIdle.Width, TextureIdle.Height, 6, 1);
            AnimationRun.GetFramesFromTextureProperties(TextureRunRight.Width, TextureRunRight.Height, 14, 1);
            AnimationAttacking.GetFramesFromTextureProperties(TextureAttacking.Width, TextureAttacking.Height, 18, 1);
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
                hitbox.Width = 55;
            }
            else
            {
                Attacking = false;
            }
        }

    }
}
