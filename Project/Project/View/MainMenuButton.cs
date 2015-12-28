using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.View
{
    class MainMenuButton
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;

        Color color = new Color(255, 255, 255, 255);
        byte fading = 5;

        public Vector2 size;

        bool down;
        public bool isClicked;

        public MainMenuButton(Texture2D newTexture, GraphicsDevice graphics)
        {
            texture = newTexture;
            size = new Vector2(graphics.Viewport.Width / 6, graphics.Viewport.Height / 20);

        }

        public void Update(MouseState mouseState)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            Rectangle mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                if(color.A == 255)
                {
                    down = false;
                }

                if (color.A == 0)
                {
                    down = true;
                }

                if(down)
                {
                    color.A += fading;
                }
                else
                {
                    color.A -= fading;
                }

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    isClicked = true;
                }
            }
            else if(color.A < 255)
            {
                color.A += fading;
                isClicked = false;
            }
        }
        
        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, color);
        }
    }
}
