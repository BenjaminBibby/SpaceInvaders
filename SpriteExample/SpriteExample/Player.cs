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
    class Player : SpriteObject
    {
        private int lives = 3;
                public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }

        private static Player instance;

        internal static Player Instance
        {
            get
            {
                if (instance != null)
                {
                    return Player.instance;
                }
                else
                {
                    instance = new Player(new Vector2(0, 0));
                    return instance;
                }
            }                
        }      


        private Player(Vector2 position) : base(position)
        {
            this.Position = new Vector2((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * .5f - 26), 625);
            this.speed = 250;
            Game1.AllObjects.Add(this);
        }

        public override void LoadContent(ContentManager content)
        {
            if (texture == null)
            {
                texture = content.Load<Texture2D>(@"SpaceInvader");
                CreateAnimation("Player", 1, 0, 0, texture.Width, texture.Height, Vector2.Zero, 0);
            }
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            CurrentAnimation = "Player";

            velocity = Vector2.Zero;

            HandleInput(Keyboard.GetState());
                        
            velocity *= speed;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Position += (velocity * deltaTime);

            base.Update(gameTime);
        }

        private void HandleInput(KeyboardState keyState)
        {           
            if (keyState.IsKeyDown(Keys.A) && (this.Position.X - velocity.X) > 0)
            {
                velocity += new Vector2(-1, 0);
            }
            if (keyState.IsKeyDown(Keys.D) && (this.Position.X + this.CollisionRect.Width) < GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
            {
                velocity += new Vector2(1, 0);
            }
            if(keyState.IsKeyDown(Keys.Space))
            {
                new Laser(Orientation.UP, "LaserSprite", new Vector2(this.Position.X+(this.CollisionRect.Width/2)-2,this.Position.Y-5));
                //new Laser(new Vector2(50, 50), "LaserSprite");
                this.lives = 2;
            }
        }

        private void Attack()
        {

        }

        protected override void AnimationRestart()
        {
        }

        protected override void OnCollision(SpriteObject other)
        {
        }
    }
}
