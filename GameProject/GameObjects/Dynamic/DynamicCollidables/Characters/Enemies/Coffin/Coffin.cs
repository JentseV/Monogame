using GameProject.Animations;
using GameProject.Enemies;
using GameProject.GameObjects.Characters.Player;
using GameProject.Interfaces;
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
    internal class Coffin : Enemy, IEnemy , IAttack
    {

        public Coffin(Vector2 speed, Vector2 position, Texture2D[] textures,Hero hero) : base(speed, position, textures,hero)
        {
            Idling = false;
            Hitpoints = 3;
            Range = 32f;
            Movable = true;

            AttackCooldown = 3f;
            Center = new Vector2(50 + Position.X, 55 + Position.Y);
            Hitbox = new Rectangle((int)Center.X, (int)Center.Y, 45, 45);

            TimeSinceLastAttack = AttackCooldown;

            AnimationHit.GetFramesFromTextureProperties(TextureHit.Width, TextureHit.Height, 1, 1);
            AnimationIdle.GetFramesFromTextureProperties(TextureIdle.Width, TextureIdle.Height, 6, 1);
            AnimationRun.GetFramesFromTextureProperties(TextureRunRight.Width, TextureRunRight.Height, 14, 1);
            AnimationAttacking.GetFramesFromTextureProperties(TextureAttacking.Width, TextureAttacking.Height, 18, 1);
        }

        public override void Attack()
        {

            if (TimeSinceLastAttack <= 0 && Invincible == false)
            {
                Attacking = true;
                TimeSinceLastAttack = AttackCooldown;
            }
           
        }

    }
}
