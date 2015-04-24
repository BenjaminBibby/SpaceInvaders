using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Audio;
using IrrKlang;

namespace SpriteExample
{
    class Player : SpriteObject
    {
        private bool alive;
        private int lives = 3;
        private int timer = 0;
        private int score = 0;

        public int Score
        {
            get { return score; }
            set { score = value; }
        }
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


        private Player(Vector2 position)
            : base(position)
        {
            alive = true;
            this.Position = new Vector2(400 - (52 * 0.5f) , 625);
            this.speed = 250;
            Game1.AllObjects.Add(this);
        }

        public override void LoadContent(ContentManager content)
        {
            if (texture == null)
            {
                texture = content.Load<Texture2D>(@"playerSheet");
                CreateAnimation("Player", 1, 0, 0, 52, 32, Vector2.Zero, 0);
                CreateAnimation("Explode", 3, 32, 0, 52, 32, Vector2.Zero, 12);
            }
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            timer++;

            if(!alive && timer >= 50)
            {
                alive = true;
                CurrentAnimation = "Player";
            }

            if(CurrentAnimation != "Explode")
            {
                CurrentAnimation = "Player";
            }
            if(timer >= 60)
            {
                alive = true;
            }

            if(alive)
            {
                CurrentAnimation = "Player";
            }
            else
            {
                CurrentAnimation = "Explode";
            }

            velocity = Vector2.Zero;

            if(alive)
            {
                HandleInput(Keyboard.GetState());
            }
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

                if (keyState.IsKeyDown(Keys.Space) && timer >= 30)
                {
                    new Laser(Orientation.UP, "LaserSheet", new Vector2(this.Position.X + (this.CollisionRect.Width / 2) - 6, this.Position.Y - 10));
                    Game1.soundEngine.Play2D(@"Content\Shoot.wav", false);
                    Game1.soundEngine.Play2D(@"Content\shoot.wav", false);
                    timer = 0;
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
            if (other is Laser)
            {
                if((other as Laser).Direction == Orientation.DOWN)
                {
                    Destroy(other);
                    alive = false;
                    this.CurrentAnimation = "Explode";
                    Game1.soundEngine.Play2D(@"Content\explosion.wav", false);
                    alive = false;
                    timer = 0;                    
                    this.lives--;
                    Game1.soundEngine.Play2D(@"Content\explosion.wav", false);
                    CurrentAnimation = "Explode";
                }        
            }
        }
    }
}
