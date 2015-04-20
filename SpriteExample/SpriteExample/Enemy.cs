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

    class Enemy : SpriteObject
    {
        private string type;
        private int pointValue;

        /// <summary>
        /// Use e1, e2, e3 or ufo. as type.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="type"></param>
        public Enemy(Vector2 position, string type) : base(position)
        {
            CreateAnimation("EnemyOne", 2, 0, 0, 32, 32, Vector2.Zero, 1);
            CreateAnimation("EnemyTwo", 2, 32, 0, 44, 32, Vector2.Zero, 1);
            CreateAnimation("EnemyThree", 2, 64, 0, 48, 32, Vector2.Zero, 1);
            CreateAnimation("UFO", 1, 96, 0, 96, 42, Vector2.Zero, 0);
            this.type = type;

            Game1.AllObjects.Add(this);

            switch (type.ToLower())
            {
                case "e1":
                     CurrentAnimation = "EnemyOne";
                     this.pointValue = 40;
                        break;
                case "e2":
                     CurrentAnimation = "EnemyTwo";
                     this.pointValue = 20;
                        break;
                case "e3":
                     CurrentAnimation = "EnemyThree";
                     this.pointValue = 10;
                        break;
                case "ufo":
                     CurrentAnimation = "UFO";
                     this.pointValue = 200;
                        break;
                default:
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            velocity = Vector2.Zero;

            base.Update(gameTime);
        }

        public override void LoadContent(ContentManager content)
        {
            if(texture == null)
            texture = content.Load<Texture2D>(@"EnemySheet");

            base.LoadContent(content);
        }

        private void Move()
        {
            if(this.Position.X - this.speed < 0)
            {
            }
        }

        protected override void AnimationRestart()
        {
         
        }

        protected override void OnCollision(SpriteObject other)
        {
            if(other is Laser && (other as Laser).Direction == Orientation.UP)
            {
                //Destroy the enemy
            }
        }
    }
}
