using GameProject.Animations;
using GameProject.Enemies;
using GameProject.GameObjects.Characters.Player;
using GameProject.GameObjects.Static;
using GameProject.GameObjects.Static.StaticCollidable;
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
using IGameComponent = GameProject.Interfaces.IGameComponent;

namespace GameProject.Pickups
{
    internal abstract class Pickup : StaticCollidable , ICollidable, IGameComponent
    {

        private List<IPickupObserver> observers = new List<IPickupObserver>();

        private Texture2D healthTexture, coinTexture;
        private float timeTillDespawn;
        private Animation animation;
        private int id;
        private string tag;
        public Animation Animation { get { return animation; } set { animation = value; } }
        public Texture2D HealthTexture { get { return healthTexture; } set { healthTexture = value; } }
        public Texture2D CoinTexture { get { return coinTexture; } set { coinTexture = value; } }
        public float TimeTillDespawn { get { return timeTillDespawn; } set { timeTillDespawn = value; } }



        public int ID { get { return id; } set { id = value; } }
        public string Tag { get { return tag; } set { tag = value; } }

        public Pickup(int idIn, string tagIn, Vector2 postionIn,float timeTillDespawnIn)
        {
            ID = idIn;
            Tag = tagIn;
            Position = postionIn;
            Hitbox = new Rectangle((int)Position.X,(int)Position.Y,25,25);
            Remove = false;
            TimeTillDespawn = timeTillDespawnIn;
            this.Animation = new Animation();
        }

        public override void CheckCollision(ICollidable collidable)
        {
                if(this.Hitbox.Intersects(collidable.Hitbox) && !(collidable is Pickup) && !(collidable is Bullet))
                {
                    if(collidable is Hero)
                    {
                        
                        Hero h = collidable as Hero;
                        OnPickup(h);
                    }
                }
        }

        public void OnPickup(Hero h)
        {
            
            NotifyObservers();
            this.Remove = true;
        }


        public void AttachObserver(IPickupObserver observer)
        {
            observers.Add(observer);
        }

        public void DetachObserver(IPickupObserver observer)
        {
            observers.Remove(observer);
        }

        protected virtual void NotifyObservers()
        {
            Debug.WriteLine("Notify observer");
            foreach (var observer in observers)
            {
                observer.OnPickup(this);
            }
        }


        public void Update(GameTime gameTime)
        {
            this.Animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Animation.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0f);

        }
    }
}
