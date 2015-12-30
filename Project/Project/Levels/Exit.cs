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
        Rectangle rectangle;

        public Exit(ContentManager Content, Rectangle _rectangle)
        {
            texture = Content.Load<Texture2D>("Tile9");
            rectangle = _rectangle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
