using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Levels
{
    class PowerUp
    {
        Texture2D texture;
        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public PowerUp(ContentManager Content, Rectangle _rectangle)
        {
            texture = Content.Load<Texture2D>("Tile5");
            Rectangle = _rectangle;
        }

        public bool playerGetsPowerUp(Rectangle player)
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
