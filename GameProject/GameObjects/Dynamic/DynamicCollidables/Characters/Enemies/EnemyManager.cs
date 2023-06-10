using GameProject.GameObjects.Characters.Player;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject.Characters;
using GameProject.Enemies;

namespace GameProject.GameObjects.Dynamic.Characters.Enemies
{
    internal class EnemyManager
    {
        public List<Enemy> enemies = new List<Enemy>();

        public EnemyManager(List<Enemy> enemies)
        {
            this.enemies = enemies;
        }

        public void Update(GameTime gameTime, Hero hero, List<ICollidable> collidables)
        {
            foreach (Enemy enemy in enemies)
            {
                
                if (!enemy.Remove)
                {
                    enemy.Update(gameTime, hero, collidables);

                    if (enemy is IRangedAttacker rangedEnemy)
                        rangedEnemy.UpdateBullets(gameTime, collidables);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in enemies)
            {
                if (!enemy.Remove)
                {
                    enemy.Draw(spriteBatch);

                    if (enemy is IRangedAttacker rangedEnemy)
                        rangedEnemy.DrawBullets(spriteBatch);
                }
            }
        }


    }
}
