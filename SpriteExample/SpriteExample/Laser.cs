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
    enum Orientation {UP, DOWN}
    class Laser : SpriteObject
    {
        private Orientation direction;
        private string imgPath;

        public Laser(Orientation direction, string imagePath, Vector2 position) : base(position)
        {
            this.direction = direction;
            this.imgPath = imagePath;
            this.speed = 5;
            CreateAnimation("Laser", 1, 0, 0, 6, 12, Vector2.Zero, 0);
            Game1.AllObjects.Add(this);
        }

        public override void LoadContent(ContentManager content)
        {
            if(texture == null)
            texture = content.Load<Texture2D>(@"" + imgPath);
            
            base.LoadContent(content);
        }
        public override void Update(GameTime gameTime)
        {
            CurrentAnimation = "Laser";
            switch (direction)
            {
                case Orientation.UP:
                    Position -= new Vector2(0, speed);
                    break;
                case Orientation.DOWN:
                    Position += new Vector2(0, speed);
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void AnimationRestart()
        {

        }

        protected override void OnCollision(SpriteObject other)
        {
            if(other is Enemy)
            {
                Destroy(this);
            }
        }
    }
}
