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
    class Text : SpriteObject
    {
        SpriteFont font1;
        Vector2 position;
        string fontOutput;
        float duration;
        bool noDuration;

        /// <summary>
        /// Set duration = 0, for no duration (the object will stay in the game until manually removed).
        /// Otherwise the duration will be converted to seconds.
        /// </summary>
        public string FontOutput
        {
            get { return fontOutput; }
            set { fontOutput = value; }
        }
        Color fontColor;
        public Text(Vector2 position, string fontOutput, Color fontColor, float duration)
            : base(position)
        {
            CreateAnimation("FontImage",1,0,0,5,5, Vector2.Zero, 0);
            CurrentAnimation = "FontImage";

            if(duration != 0)
            {
                noDuration = false;
                this.duration = duration * 60;
            }
            else
            {
                noDuration = true;
            }

            this.fontColor = fontColor;
            this.fontOutput = fontOutput;
            this.position = position;
            Game1.AllObjects.Add(this);
        }
        public override void LoadContent(ContentManager content)
        {

            texture = content.Load<Texture2D>("FontImage");
            font1 = content.Load<SpriteFont>("RetroComputer");

            base.LoadContent(content);
        }
        public override void Update(GameTime gameTime)
        { 
            if(duration > 0)
            {
                duration--;
                if(duration == 0 && !noDuration)
                {
                    Destroy(this);
                }
            }

            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            string output = "" + fontOutput + "";
            Vector2 fontOrigin = font1.MeasureString(output) / 2;
            spriteBatch.DrawString(font1, output, position, fontColor, 0, fontOrigin, 1.0f, SpriteEffects.None, 1.0f);
            
            base.Draw(spriteBatch);
        }
        protected override void AnimationRestart(){}
        protected override void OnCollision(SpriteObject other) { }
    }
}
