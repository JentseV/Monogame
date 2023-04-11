using GameProject.Animations;
using GameProject.Pickups;
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
    internal abstract class Enemy : Character, ICollidable
    {


        private Animation _animationIdle;
        private Animation _animationRun;
        private Animation _animationAttacking;
        private Animation _animationHit;

        public Animation AnimationIdle { get { return _animationIdle; } set { _animationIdle = value; } }
        public Animation AnimationRun { get { return _animationRun; } set { _animationRun = value; } }

        public Animation AnimationAttacking { get { return _animationAttacking; } set { _animationAttacking = value; } }

        public Animation AnimationHit { get { return _animationHit; } set { _animationHit = value; } }


        public void OnDeath()
        {
            if (Dead)
            {
                Moving = false;
                Attacking = false;
                Random r = new Random();
                Pickup.SpawnPickup(r.Next(),"Pickup",this.Position,5f);
            }
        }


    }
}
