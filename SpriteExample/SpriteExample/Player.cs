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
            this.Position = new Vector2(25, 425);
            this.speed = 250;
            attack = false;
            dir = Direction.Down;
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"playerSheet");
            

            //Creates the player animations
            CreateAnimation("IdleUp", 1, 50, 0, 50, 50, Vector2.Zero, 1);
            CreateAnimation("RunUp", 12, 50, 0, 50, 50, Vector2.Zero, 12);
            CreateAnimation("AttackUp", 9, 230, 0, 70, 80, new Vector2(13, 27), 27);

            CreateAnimation("IdleDown", 1, 0, 0, 50, 50, Vector2.Zero, 1);
            CreateAnimation("RunDown", 12, 0, 0, 50, 50, Vector2.Zero, 12);
            CreateAnimation("AttackDown", 9, 150, 0, 70, 80, new Vector2(-4, -2), 27);

            CreateAnimation("IdleRight", 1, 100, 8, 50, 50, Vector2.Zero, 1);
            CreateAnimation("RunRight", 8, 100, 8, 50, 50, Vector2.Zero, 8);
            CreateAnimation("AttackRight", 9, 380, 0, 70, 70, new Vector2(-9, 7), 27);

            CreateAnimation("IdleLeft", 1, 100, 0, 50, 50, Vector2.Zero, 1);
            CreateAnimation("RunLeft", 8, 100, 0, 50, 50, Vector2.Zero, 8);
            CreateAnimation("AttackLeft", 9, 310, 0, 70, 70, new Vector2(30, 5), 27);

            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            velocity = Vector2.Zero;

            if(attack == false)
            {
                Idle();
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
                CurrentAnimation = "RunLeft";
                velocity += new Vector2(-1, 0);
            }
            if (keyState.IsKeyDown(Keys.D) && this.Position.X + velocity.X + this.CollisionRect.Width < GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
            {
                dir = Direction.Right;
                CurrentAnimation = "RunRight";
                velocity += new Vector2(1, 0);
            }
            if(keyState.IsKeyDown(Keys.Space))
            {
                currentIndex = 0;
                timeElapsed = 0;
                Attack();
                attack = true;
            }

            //if(keyState.IsKeyDown(Keys.W))
            //{
            //    dir = Direction.Up;
            //    CurrentAnimation = "RunUp";
            //    velocity += new Vector2(0, -1);
            //}

            //if (keyState.IsKeyDown(Keys.S))
            //{
            //    dir = Direction.Down;
            //    CurrentAnimation = "RunDown";
            //    velocity += new Vector2(0, 1);
            //}
        }

        private void Idle()
        {
            switch (dir)
            {
                case Direction.Up:
                    CurrentAnimation = "IdleUp";
                    break;
                case Direction.Down:
                    CurrentAnimation = "IdleDown";
                    break;
                case Direction.Right:
                    CurrentAnimation = "IdleRight";
                    break;
                case Direction.Left:
                    CurrentAnimation = "IdleLeft";
                    break;
                default:
                    break;
            }
        }

        private void Attack()
        {
            switch (dir)
            {
                case Direction.Up:
                    CurrentAnimation = "AttackUp";
                    break;
                case Direction.Down: 
                    CurrentAnimation = "AttackDown";
                    break;
                case Direction.Right:
                    CurrentAnimation = "AttackRight";
                    break;
                case Direction.Left:
                    CurrentAnimation = "AttackLeft";
                    break;
                default:
                    break;
            }
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
