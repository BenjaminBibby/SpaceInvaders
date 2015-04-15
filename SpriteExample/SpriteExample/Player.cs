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
    enum Direction { Up, Down, Right, Left};

    class Player : SpriteObject
    {
        private Direction dir;
        private bool attack;
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
            this.Position = new Vector2((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width/2 - 26), 625);
            this.speed = 250;
            attack = false;
            dir = Direction.Down;
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"SpaceInvader");

            CreateAnimation("Player", 1, 0, 0, texture.Width, texture.Height, Vector2.Zero, 0);

            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            CurrentAnimation = "Player";

            velocity = Vector2.Zero;

            if(attack == false)
            {
                HandleInput(Keyboard.GetState());
            }
            
            velocity *= speed;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Position += (velocity * deltaTime);

            base.Update(gameTime);
        }

        private void HandleInput(KeyboardState keyState)
        {           
            if (keyState.IsKeyDown(Keys.A) && this.Position.X - velocity.X > 0)
            {
                dir = Direction.Left;
                velocity += new Vector2(-1, 0);
            }
            if (keyState.IsKeyDown(Keys.D) && this.Position.X + velocity.X + this.CollisionRect.Width < GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
            {
                dir = Direction.Right;
                velocity += new Vector2(1, 0);
            }
            if(keyState.IsKeyDown(Keys.Space))
            {
                currentIndex = 0;
                timeElapsed = 0;
                Attack();
                attack = true;
            }
        }

        private void Attack()
        {

        }

        protected override void AnimationRestart()
        {
            attack = false;
        }

        protected override void OnCollision(SpriteObject other)
        {
        }
    }
}
