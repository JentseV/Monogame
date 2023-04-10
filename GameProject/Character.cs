
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


namespace GameProject
{
    internal abstract class Character : IGameComponent ,IMovable, ICollidable
    {


        private float invincibleTimer;

        private bool moving, attacking, dead, hit , invincible, movable;

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




        #region 
        //Textures

        private Texture2D _textureAttackRight, _textureAttackUp, _textureAttackFront;
        private Texture2D _textureAttacking;
        private Texture2D _textureHit;
        private Texture2D _texture, _textureRunning, _textureIdling;
        private Texture2D _textureIdleFacingFront, _textureIdleFacingRight, _textureIdleFacingUp, _textureIdle;
        private Texture2D _textureRunRight, _textureUpRun, _textureDownRun;
        public Texture2D TextureHit { get { return _textureHit; } set { _textureHit = value; } }
        public Texture2D Texture{get { return _texture; }set { _texture = value; }}
        public Texture2D TextureRunning { get { return _textureRunning; } set { _textureRunning = value;  } }
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
        protected Rectangle hitbox;
        // Movement en collision
        private Vector2 position;
        private Vector2 direction;
        private Vector2 speed;
        public Vector2 Position { get { return position;  } set { position = value; } }
        public Vector2 Direction { get { return direction; } set { direction = value; } }
        public Vector2 Speed { get { return speed; } set { speed = value; } }
        public Vector2 Center { get ; set; }
        public Rectangle Hitbox { get { return hitbox; } set { hitbox = value; }  }

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
            if (this.Hitpoints > 0 && Invincible == false)
            {
                this.Invincible = true;
                this.Hit = true;
                this.Hitpoints -= amountOfDamage;
            }
            else
            {
                Dead = true;
            }

        }

        public void CheckCollision(List<ICollidable> collidables)
        {
            
        }

        public void Move()
        {
            Moving = true;
            Position += Direction * Speed;
        }
    }
}
