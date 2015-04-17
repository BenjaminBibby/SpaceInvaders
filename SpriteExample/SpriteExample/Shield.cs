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

            CreateAnimation("LeftBottom", 4, 0, 0, 34, 32, Vector2.Zero, 0);
            CreateAnimation("LeftTop", 4, 32, 0, 30, 32, Vector2.Zero, 0);
            CreateAnimation("RightBottom", 4, 64, 0, 27, 30, Vector2.Zero, 0);
            CreateAnimation("MidBottom", 4, 94, 0, 27, 30, Vector2.Zero, 0);
            CreateAnimation("RightTop", 4, 124, 0, 33, 28, Vector2.Zero, 0);
            CreateAnimation("MidTop", 4, 152, 0, 34, 27, Vector2.Zero, 0);
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
                    break;
            }
            #endregion
            Game1.AllObjects.Add(this);
        }
        public override void LoadContent(ContentManager content)
        {
            if(texture == null)
            texture = content.Load<Texture2D>(@"ShieldSheet");

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
