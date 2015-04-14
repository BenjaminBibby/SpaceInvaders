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
    class Animation
    {
        private Vector2 offset;
        private float fps;
        private Rectangle[] rectangles;
        private Texture2D texture;
        private Color[][] colors;

        public Color[][] Colors
        {
            get { return colors; }
            set { colors = value; }
        }

        #region properties
        public Vector2 Offset
        {
            get { return offset; }
            set { offset = value; }
        }
        public float Fps
        {
            get { return fps; }
            set { fps = value; }
        }
        public Rectangle[] Rectangle
        {
            get { return rectangles; }
            set { rectangles = value; }
        }
        #endregion

        public Animation(int frames, int yPos, int xStartFrame, int width, int height, Vector2 offset, float fps, Texture2D texture)
        {
            colors = new Color[frames][];
            rectangles = new Rectangle[frames];

            for (int i = 0; i < frames; i++)
            {
                colors[i] = new Color[width * height];

                rectangles[i] = new Rectangle((i + xStartFrame) * width, yPos, width, height);

                texture.GetData<Color>(0, rectangles[i], colors[i], 0, width * height);
            }

            this.fps = fps;
            this.offset = offset;
        }
    }
}
