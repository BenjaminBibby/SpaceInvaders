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
    class Shield : SpriteObject
    {
        private int shieldHP;
        private int type;
        public int ShieldHP
        {
            get { return shieldHP; }
            set { shieldHP = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="type"></param>
        public Shield(Vector2 position, int type)
            : base(position)
        {
            this.type = type;
            #region Switch for type
            switch (type)
            {
                case 1:
                    CurrentAnimation = "LeftTop";
                    break;
                case 2:
                    CurrentAnimation = "MidTop";
                    break;
                case 3:
                    CurrentAnimation = "RightTop";
                    break;
                case 4:
                    CurrentAnimation = "LeftBottom";
                    break;
                case 5:
                    CurrentAnimation = "MidBottom";
                    break;
                case 6:
                    CurrentAnimation = "RightBottom";
                    break;
                default:
                    CurrentAnimation = "RightTop";
                    break;
            }
            #endregion

            Game1.AllObjects.Add(this);
        }
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"SheetShield");

            CreateAnimation("LeftTop", 4, 0, 0, 32, 32, Vector2.Zero, 0);
            CreateAnimation("MidTop", 4, 32, 0, 32, 32, Vector2.Zero, 0);
            CreateAnimation("RightTop", 4, 64, 0, 32, 32, Vector2.Zero, 0);
            CreateAnimation("LeftBottom", 4, 96, 0, 32, 32, Vector2.Zero, 0);
            CreateAnimation("MidBottom", 4, 128, 0, 32, 32, Vector2.Zero, 0);
            CreateAnimation("RightBottom", 4, 160, 0, 32, 32, Vector2.Zero, 0);
            base.LoadContent(content);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        protected override void OnCollision(SpriteObject other)
        {

        }
        protected override void AnimationRestart()
        {

        }
    }
}
