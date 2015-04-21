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
    enum Orientation { UP, DOWN }
    class Laser : SpriteObject
    {
        private Orientation direction;
        private bool hasCollided = false;

        public Orientation Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        private string imgPath;

        public Laser(Orientation direction, string imagePath, Vector2 position)
            : base(position)
        {
            this.direction = direction;
            this.imgPath = imagePath;
            this.speed = 5;
            hasCollided = false;
            CreateAnimation("Laser", 1, 0, 0, 12, 23, Vector2.Zero, 0);
            CreateAnimation("Explosion", 1, 23, 0, 50, 31, new Vector2(25, 15), 0);
            Game1.AllObjects.Add(this);
        }

        public override void LoadContent(ContentManager content)
        {
            if (texture == null)
                texture = content.Load<Texture2D>(@"" + imgPath);

            base.LoadContent(content);
        }
        public override void Update(GameTime gameTime)
        {
            if (this.Position.Y < 0 || this.Position.Y > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)
            {
                Destroy(this);
            }

            if (hasCollided == false)
            {
                CurrentAnimation = "Laser";
            }
            else
            {
                CurrentAnimation = "Explosion";
            }

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
            if (other is Enemy || other is Shield)
            {
                this.speed = 0;
                hasCollided = true;

                if (other is Enemy)
                {
                    Destroy(other);
                    Destroy(this);
                }
            }
        }
    }
}
