using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using IrrKlang;

namespace SpriteExample
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int attackTimer;
        private int numOfEnemies = 0;
        private bool roundOver;
        Text scoreText;
        public static ISoundEngine soundEngine;
        private EnemyFormation formation;
        private int spawnTimer;

        private static List<SpriteObject> tmpObjects = new List<SpriteObject>();
        internal static List<SpriteObject> TmpObjects
        {
            get { return tmpObjects; }
            set { tmpObjects = value; }
        }
        private Texture2D lives;

        private static List<SpriteObject> allObjects = new List<SpriteObject>();
        internal static List<SpriteObject> AllObjects
        {
            get { return allObjects; }
            set { allObjects = value; }
        }

        public Game1()
            : base()
        {
            soundEngine = new ISoundEngine();
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 675;
            graphics.PreferredBackBufferWidth = 800;
            Window.Title = "Space Invaders";
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            roundOver = false;
            spawnTimer = 0;
            attackTimer = 0;
            // TODO: Add your initialization logic here
            for (int x = 0; x < 4; x++)
            {
                int xOffset = 210 * x;
                for (int i = 1; i < 7; i++)
                {
                    if (i < 4)
                        new Shield(new Vector2(xOffset + i * 32, 500), i);
                    else
                    {
                        new Shield(new Vector2(xOffset + ((i - 3) * 32), 532), i);
                    }
                }
            }

            scoreText = new Text(new Vector2(195, 30), "" + Player.Instance.Score.ToString(), new Color(76, 255, 0),0);
            new Text(new Vector2(80, 30), "SCORE", Color.White,0);
            new Text(new Vector2(500, 30), "LIVES", Color.White,0);
            Player player = Player.Instance;
            lives = Content.Load<Texture2D>("SpaceInvader");

            formation = new EnemyFormation(8, 5, new Vector2(50, 50), 10f, 1f);


            foreach (SpriteObject enemy in allObjects)
            {
                if(enemy is Enemy)
                numOfEnemies++;
            }
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            if(spriteBatch == null)
            spriteBatch = new SpriteBatch(GraphicsDevice);
           
            foreach (SpriteObject obj in tmpObjects)
            {
                obj.LoadContent(Content);
            }
            // TODO: use this.Content to load your game content here

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            spawnTimer++;
            attackTimer++;

            if(attackTimer >= 120)
            {
                formation.Attack();
                attackTimer = 0;
            }

            numOfEnemies = 0;

            foreach(SpriteObject enemy in allObjects)
            {
                if(enemy is Enemy)
                numOfEnemies++;
            }
            
            if (numOfEnemies <= 0)
            {
                formation = new EnemyFormation(8, 5, new Vector2(50, 50), 10f, 1f);
                Player.Instance.Lives++;
            }

            if(spawnTimer > 300)
            {
                spawnTimer = 0;
                Random ufoSpawn = new Random(Guid.NewGuid().GetHashCode());
                if (ufoSpawn.Next(3) == 1)
                {
                    new Enemy(Vector2.Zero, "ufo");
                }
            }

            scoreText.FontOutput = "" + Player.Instance.Score.ToString();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Adds new objects to objects
            if(TmpObjects != AllObjects)
            {
                TmpObjects.Clear();
                foreach(SpriteObject obj in allObjects)
                {
                    TmpObjects.Add(obj);
                }
                LoadContent();
            }

            // TODO: Add your update logic here
            foreach(SpriteObject obj in tmpObjects)
            {
                obj.Update(gameTime);
            }

            formation.MoveFormation();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            for (int i = 0; i < Player.Instance.Lives; i++)
            {
                spriteBatch.Draw(lives, new Rectangle(575 + (i * 75), 10, 52, 32), Color.White);                
            }

            foreach(SpriteObject obj in tmpObjects)
            {
                obj.Draw(spriteBatch);
            }

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
