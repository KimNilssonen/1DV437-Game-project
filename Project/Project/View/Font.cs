using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.View
{
    class Font
    {
        SpriteFont _font;
        Vector2 _position;
        string _text;
        Color _color;

        public Font(SpriteFont font, Vector2 position, string text, Color color)
        {

            _font = font;
            _position = position;
            _text = text;
            _color = color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, _text, _position, _color);
        }
    }
}
