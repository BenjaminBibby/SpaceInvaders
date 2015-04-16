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
        private bool isColliding;
        private string type;
        private int pointValue;

        /// <summary>
        /// Use e1, e2, e3 or ufo. as type.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="type"></param>
        public Enemy(Vector2 position, string type) : base(position)
        {
            CreateAnimation("MoveRight", 5, 0, 0, 65, 65, new Vector2(0, 0), 5);
            CreateAnimation("EnemyOne", 2, 0, 0, 32, 32, Vector2.Zero, 2);
            CreateAnimation("EnemyTwo", 2, 32, 0, 44, 32, Vector2.Zero, 2);
            CreateAnimation("EnemyThree", 2, 64, 0, 48, 32, Vector2.Zero, 2);
            CreateAnimation("UFO", 1, 96, 0, 96, 42, Vector2.Zero, 0);
            isColliding = false;
            this.type = type;

            switch (type.ToLower())
            {
                case "e1":
                    CurrentAnimation = "EnemyOne";
                        break;
                case "e2":
                        CurrentAnimation = "EnemyTwo";
                        break;
                case "e3":
                        CurrentAnimation = "EnemyThree";
                        break;
                case "ufo":
                        CurrentAnimation = "UFO";
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
            texture = content.Load<Texture2D>(@"EnemySheet");
            
            base.LoadContent(content);
        }

        protected override void AnimationRestart()
        {
         
        }

        protected override void OnCollision(SpriteObject obj)
        {
            isColliding = true;
            color = Color.Red;
        }
    }
}
