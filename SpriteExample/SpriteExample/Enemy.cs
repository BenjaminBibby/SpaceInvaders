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

        public Enemy(Vector2 position) : base(position)
        {
            isColliding = false;

            CurrentAnimation = "MoveRight";
        }

        public override void Update(GameTime gameTime)
        {
            velocity = Vector2.Zero;

            velocity = Player.Instance.Position - this.Position;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(!isColliding)
            {
                Position += (velocity * deltaTime);
            }

            if(this.velocity.X < 0)
            {
                CurrentAnimation = "MoveLeft";
            }
            else
            {
                CurrentAnimation = "MoveRight";
            }

            isColliding = false;
            color = Color.White;

            base.Update(gameTime);
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"sponge");

            CreateAnimation("MoveRight", 5, 0, 0, 65, 65, new Vector2(0, 0), 5);
            CreateAnimation("MoveLeft", 5, 65, 0, 65, 65, new Vector2(0, 0), 5);
            CreateAnimation("DmgRight", 4, 130, 0, 65, 65, new Vector2(0, 0), 5);
            CreateAnimation("DmgLeft", 4, 185, 0, 65, 65, new Vector2(0, 0), 5);
            CreateAnimation("Explode", 5, 260, 0, 100, 100, new Vector2(-10, -20), 5);

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
