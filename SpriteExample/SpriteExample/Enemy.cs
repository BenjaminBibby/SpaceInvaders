using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;


namespace SpriteExample
{   

    class Enemy : SpriteObject
    {
        private bool isColliding;
        private string type;
        private int pointValue;

        public Enemy(Vector2 position, string type) : base(position)
        {
            isColliding = false;
            this.type = type;

            CurrentAnimation = "MoveRight";
        }

        public override void Update(GameTime gameTime)
        {
            velocity = Vector2.Zero;

            base.Update(gameTime);
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"sponge");

            CreateAnimation("MoveRight", 5, 0, 0, 65, 65, new Vector2(0, 0), 5);

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
