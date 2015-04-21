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
    abstract class SpriteObject
    {
        private Vector2 origin;
        private float layer;
        protected Color color;
        private Rectangle rectangle;
        private Rectangle collisionRect;
        private Rectangle[] rectangles;
        protected int currentIndex;
        protected float timeElapsed;
        private float animationSpeed;
        private Dictionary<string, Animation> animations;
        private Texture2D boxTexture;

        protected float scale;
        protected SpriteEffects effect;
        protected Texture2D texture;
        private Vector2 position;
        protected Vector2 velocity;
        protected float speed;
        private string currentAnimation;
        protected Rectangle[] Rectangles
        {
            get { return rectangles; }
        }
        protected string CurrentAnimation
        {
            get { return currentAnimation; }
            set { currentAnimation = value; }
        }
        
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Rectangle CollisionRect
        {
            get 
            { 
                return new Rectangle 
                ( 
                    (int)(position.X - origin.X),
                    (int)(position.Y - origin.Y),
                    rectangles[0].Width, rectangles[0].Height
                ); 
            }
        }

        public SpriteObject(Vector2 position)
        {
            rectangles = new Rectangle[1];
            animations = new Dictionary<string, Animation>();
            this.scale = 1;
            this.color = new Color(255, 255, 255, 255);
            this.position = position;
            this.origin = new Vector2(0, 0);
            this.animationSpeed = 10;
            this.effect = SpriteEffects.None;
        }

        public virtual void LoadContent(ContentManager content)
        {
            if(boxTexture == null)
            boxTexture = content.Load<Texture2D>(@"pixel");
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectangles[currentIndex], color, 0, origin, scale, effect, layer);

            Rectangle topBorder = new Rectangle(0, 0, 800, 5);
            Rectangle bottomBorder = new Rectangle(0, 670, 800, 5);
            Rectangle rightBorder = new Rectangle(795, 0, 5,800);
            Rectangle leftBorder = new Rectangle(0, 0, 5, 800);

            spriteBatch.Draw(boxTexture, topBorder, new Color(76, 255,0));
            spriteBatch.Draw(boxTexture, bottomBorder, new Color(76, 255, 0));
            spriteBatch.Draw(boxTexture, rightBorder, new Color(76, 255, 0));
            spriteBatch.Draw(boxTexture, leftBorder, new Color(76, 255, 0));

#if DEBUG
            Rectangle topLine = new Rectangle(CollisionRect.X, CollisionRect.Y, CollisionRect.Width, 1);
            Rectangle bottomLine = new Rectangle(CollisionRect.X, CollisionRect.Y + CollisionRect.Height, CollisionRect.Width, 1);
            Rectangle rightLine = new Rectangle(CollisionRect.X + CollisionRect.Width, CollisionRect.Y, 1, CollisionRect.Height);
            Rectangle leftLine = new Rectangle(CollisionRect.X, CollisionRect.Y, 1, CollisionRect.Height);

            spriteBatch.Draw(boxTexture, topLine, Color.Red);
            spriteBatch.Draw(boxTexture, bottomLine, Color.Red);
            spriteBatch.Draw(boxTexture, rightLine, Color.Red);
            spriteBatch.Draw(boxTexture, leftLine, Color.Red);
#endif
        }

        public virtual void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(animations[currentAnimation].Fps != 0)
            currentIndex = (int)(timeElapsed * animationSpeed);

            PlayAnimation(currentAnimation);

            if(currentIndex > rectangles.Length - 1)
            {
                timeElapsed = 0;
                currentIndex = 0;
                AnimationRestart();
            }

            CheckCollision();
        }

        private void CheckCollision()
        {
            foreach (SpriteObject obj in Game1.TmpObjects)
            {
                if (obj != this)
                {
                    if(this.IsCollidingWith(obj))
                    {
                        OnCollision(obj);
                    }
                }
            }
        }

        public bool IsCollidingWith(SpriteObject other)
        {
            //Uses intersectswith to determine if a collision is taking place
            return CollisionRect.Intersects(other.CollisionRect);
        }

        protected void CreateAnimation(string name, int frames, int yPos, int xStartFrame, int width, int height, Vector2 offset, float fps)
        {
            animations.Add(name, new Animation(frames, yPos, xStartFrame, width, height, offset, fps, texture));
        }

        protected void PlayAnimation(string animationName)
        {
            rectangles = animations[animationName].Rectangle;
            origin = animations[animationName].Offset;
            animationSpeed = animations[animationName].Fps;
        }
        public void Destroy(SpriteObject sprObj)
        {
            Game1.AllObjects.Remove(sprObj);
        }

        protected abstract void AnimationRestart();

        protected abstract void OnCollision(SpriteObject other);
    }
}
