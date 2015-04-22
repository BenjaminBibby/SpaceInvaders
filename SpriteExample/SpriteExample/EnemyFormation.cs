using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace SpriteExample
{
    class EnemyFormation
    {
        private int width, height;
        private Vector2 offset;
        private float spaceBetween;
        private Enemy[, ] enemies;
        private float speed;

        public EnemyFormation(int width, int height, Vector2 offset, float spaceBetween, float speed)
        {
            this.width = width;
            this.height = height;
            this.offset = offset;
            this.spaceBetween = spaceBetween;
            this.speed = speed;
            CreateFormation(width, height, offset, spaceBetween);
        }

        public void CreateFormation(int width, int height, Vector2 offset, float spaceBetween)
        {
            enemies = new Enemy[width, height];
            // The enemyy formation
            string eType = "e1";
            for (int y = 0; y < height; y++)
            {
                if (y > 2)
                {
                    eType = "e2";
                }
                else if (y > 3)
                {
                    eType = "e3";
                }
                for (int x = 0; x < width; x++)
                {
                    enemies[x, y] = new Enemy(new Vector2(0 + (50 + spaceBetween) * x, 0 + (50 + spaceBetween) * y) + offset, eType);
                }
            }
        }
        /// <summary>
        /// Finds and return the closest position of the enemies
        /// </summary>
        /// <returns></returns>
        private float GetStartOfRow()
        {
            float[] rows = new float[height];
            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    if(enemies[x, y] != null)
                    {
                        rows[y] = enemies[x, y].Position.X;
                        break;
                    }
                }
            }
            return rows.Min();
        }
        /// <summary>
        /// Finds and return the position of the farest right enemy
        /// </summary>
        /// <returns></returns>
        private float GetWidthOfFormation()
        {
            float[] rows = new float[height];
            for(int y = 0; y < height; y++)
            {
                /*for(int x = 0; x < width; x++)
                {
                    if(enemies[x, y] != null)
                    {
                        rows[y] = enemies[x, y].Position.X + enemies[x, y].CollisionRect.Width;
                    }
                }*/
                for(int x = width - 1; x >= 0; x--)
                {
                    if(enemies[x, y] != null)
                    {
                        rows[y] = (enemies[x, y].Position.X + enemies[x, y].CollisionRect.Width);
                        break;
                    }
                }
            }
            /*float largest = rows[0];
            for (int i = 0; i < rows.Length; i++)
            {
               if(rows[i] > largest)
               {
                   largest = rows[i];
               }
            }
            return largest;*/
            return rows.Max();
                
        }
        /// <summary>
        /// Finds and return the bottem enemy of the formation
        /// </summary>
        /// <returns></returns>
        private float GetHeightOfFormation()
        {
            float[] rows = new float[width];
            for (int x = 0; x < width; x++)
            {
                for (int y = height - 1; y > 0; y--)
                {
                    if (enemies[x, y] != null)
                    {
                        rows[x] = enemies[x, y].Position.Y + enemies[x, y].CollisionRect.Height;
                        break;
                    }
                }
            }
            return rows.Max();
        }
        /// <summary>
        /// Calls the attack function of a random bottom-line enemy
        /// </summary>
        public void Attack()
        {
            Random rnd = new Random();
            int x = rnd.Next(0, width);
            // Search for the lowest positioned enemy in a random row
            for(int y = height - 1; y > 0; y--)
            {
                if(enemies[x, y] != null)
                {
                    enemies[x, y].Attack();
                    break;
                }
            }
        }
        public void MoveFormation()
        {
            if (GetWidthOfFormation() >= 800 || GetStartOfRow() <= 0)
            {
                speed = -1 * speed;
                foreach(Enemy e in enemies)
                {
                    e.Position += new Vector2(0, e.CollisionRect.Height);
                }
            }
            for (int y = 0; y < height; y++) 
            {
                for (int x = 0; x < width; x++)
                {
                    // Move the current enemy in the formation
                    if (enemies[x, y] != null)
                    {
                        enemies[x, y].Position += new Vector2(speed, 0);
                    }
                }
            }
            
        }
    }
}
