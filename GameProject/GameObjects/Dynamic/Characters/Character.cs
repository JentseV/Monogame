﻿using GameProject.Animations;
using GameProject.GameObjects;
using GameProject.GameObjects.Dynamic;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GameProject.Characters
{
    internal abstract class Character : DynamicGO, IGameComponent, IMovable, ICollidable
    {

        private float damage;
        public float Damage { get { return damage; } set { damage = value; } }
        private float invincibleTimer;

        private bool moving, attacking, dead, hit, invincible, movable, remove;

        public bool Remove { get { return remove; } set { remove = value; } }
        public bool Movable { get { return movable; } set { movable = value; } }
        public bool Invincible { get { return invincible; } set { invincible = value; } }
        public bool Hit { get { return hit; } set { hit = value; } }
        public bool Moving { get { return moving; } set { moving = value; } }
        public bool Attacking { get { return attacking; } set { attacking = value; } }
        public bool Dead { get { return dead; } set { dead = value; } }

        private float attackCooldown = 2f;
        private float timeSinceLastAttack;

        public float InvincibleTimer { get { return invincibleTimer; } set { invincibleTimer = value; } }
        public float AttackCooldown { get { return attackCooldown; } set { attackCooldown = value; } }

        public float TimeSinceLastAttack { get { return timeSinceLastAttack; } set { timeSinceLastAttack = value; } }

        private int id;

        public int ID { get { return id; } set { id = value; } }

        private string tag;

        public string Tag { get { return tag; } set { tag = value; } }

        private float hitpoints;
        public float Hitpoints { get { return hitpoints; } set { hitpoints = value; } }


        private Animation _animationIdle;
        private Animation _animationRun;
        private Animation _animationAttacking;
        private Animation _animationHit;

        public Animation AnimationIdle { get { return _animationIdle; } set { _animationIdle = value; } }
        public Animation AnimationRun { get { return _animationRun; } set { _animationRun = value; } }

        public Animation AnimationAttacking { get { return _animationAttacking; } set { _animationAttacking = value; } }

        public Animation AnimationHit { get { return _animationHit; } set { _animationHit = value; } }




        #region 
        //Textures
        private Texture2D _hitboxText;
        private Texture2D _textureRunLeft;
        private Texture2D _textureAttackRight, _textureAttackUp, _textureAttackFront;
        private Texture2D _textureAttacking;
        private Texture2D _textureHit;
        private Texture2D _texture, _textureRunning, _textureIdling;
        private Texture2D _textureIdleFacingFront, _textureIdleFacingRight, _textureIdleFacingUp, _textureIdle;
        private Texture2D _textureRunRight, _textureUpRun, _textureDownRun;
        private Texture2D _textureIdleFacingUpRight, _textureIdleFacingDownRight;
        private Texture2D _textureAttackUpRight, _textureAttackDownRight;
        private Texture2D _textureRunUpRight, _textureRunDownRight;
        private Texture2D _textureAttackLeft;


        public Texture2D TextureAttackLeft { get { return _textureAttackLeft; } set { _textureAttackLeft = value; } }
        public Texture2D TextureRunLeft { get { return _textureRunLeft; } set { _textureRunLeft = value; } }
        public Texture2D HitboxText { get { return _hitboxText; } set { _hitboxText = value; } }
        public Texture2D TextureIdleFacingUpRight { get { return _textureIdleFacingUpRight; } set { _textureIdleFacingUpRight = value; } }
        public Texture2D TextureIdleFacingDownRight { get { return _textureIdleFacingDownRight; } set { _textureIdleFacingDownRight = value; } }
        public Texture2D TextureAttackUpRight { get { return _textureAttackUpRight; } set { _textureAttackUpRight = value; } }
        public Texture2D TextureAttackDownRight { get { return _textureAttackDownRight; } set { _textureAttackDownRight = value; } }
        public Texture2D TextureRunUpRight { get { return _textureRunUpRight; } set { _textureRunUpRight = value; } }
        public Texture2D TextureRunDownRight { get { return _textureRunDownRight; } set { _textureRunDownRight = value; } }
        public Texture2D TextureHit { get { return _textureHit; } set { _textureHit = value; } }
        public Texture2D Texture { get { return _texture; } set { _texture = value; } }
        public Texture2D TextureRunning { get { return _textureRunning; } set { _textureRunning = value; } }
        public Texture2D TextureIdling { get { return _textureIdling; } set { _textureIdling = value; } }
        public Texture2D TextureUpRun { get { return _textureUpRun; } set { _textureUpRun = value; } }
        public Texture2D TextureIdleFacingFront { get { return _textureIdleFacingFront; } set { _textureIdleFacingFront = value; } }
        public Texture2D TextureIdleFacingRight { get { return _textureIdleFacingRight; } set { _textureIdleFacingRight = value; } }
        public Texture2D TextureIdleFacingUp { get { return _textureIdleFacingUp; } set { _textureIdleFacingUp = value; } }
        public Texture2D TextureRunRight { get { return _textureRunRight; } set { _textureRunRight = value; } }
        public Texture2D TextureIdle { get { return _textureIdle; } set { _textureIdle = value; } }
        public Texture2D TextureDownRun { get { return _textureDownRun; } set { _textureDownRun = value; } }
        public Texture2D TextureAttackRight { get { return _textureAttackRight; } set { _textureAttackRight = value; } }
        public Texture2D TextureAttacking { get { return _textureAttacking; } set { _textureAttacking = value; } }
        public Texture2D TextureAttackUp { get { return _textureAttackUp; } set { _textureAttackUp = value; } }
        public Texture2D TextureAttackFront { get { return _textureAttackFront; } set { _textureAttackFront = value; } }

        #endregion

        #region
        
        public IInputReader InputReader { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        protected void TakeDamage(float amountOfDamage)
        {
            if (Hitpoints > 0 && Invincible == false)
            {
                Invincible = true;
                Hit = true;
                Hitpoints -= amountOfDamage;
            }
            else
            {
                Dead = true;
            }

        }

        public virtual void CheckCollision(List<ICollidable> collidables)
        {

        }


        public void Move()
        {
            Moving = true;
            Position += Direction * Speed;
        }
    }
}