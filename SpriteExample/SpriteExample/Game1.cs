﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace SpriteExample
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

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
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 675;
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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            lives = Content.Load<Texture2D>("SpaceInvader");

            for (int i = 1; i < 9; i++)
            {
               
                if(i < 3)
                allObjects.Add(new Enemy(new Vector2(i * 60, 0), "e1"));

                if (i >= 3 && i < 6)
                allObjects.Add(new Enemy(new Vector2(i * 60, 45), "e2"));

                if(i < 9 && i >= 6)
                allObjects.Add(new Enemy(new Vector2(i * 60, 90), "e3"));
            }

            allObjects.Add(Player.Instance);



            for (int i = 0; i < allObjects.Count; i++)
            {
                allObjects[i].LoadContent(Content);
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
            //TmpObjects = AllObjects
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            for (int i = 0; i < allObjects.Count; i++)
            {
                allObjects[i].Update(gameTime);
            }

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

            for (int i = 0; i < allObjects.Count; i++)
            {
                allObjects[i].Draw(spriteBatch);
            }

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
