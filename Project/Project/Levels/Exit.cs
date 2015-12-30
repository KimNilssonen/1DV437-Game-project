using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Levels
{
    class Exit
    {
        Texture2D texture;
        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public Exit(ContentManager Content, Rectangle _rectangle)
        {
            texture = Content.Load<Texture2D>("Tile9");
            rectangle = _rectangle;
        }

        public bool PlayerGotToExit(Rectangle player)
        {
            if (Rectangle.Intersects(player))
            {
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
