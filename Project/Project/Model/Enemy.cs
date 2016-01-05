using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Model
{
    class Enemy
    {
        public Vector2 position;
        public Vector2 acceleration = new Vector2(0.0f, 0.0f);
        public Vector2 speed = new Vector2(0.5f, 0.0f);

        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public Enemy(ContentManager Content, Rectangle _rectangle)
        {
            Rectangle = _rectangle;
            position = new Vector2(1.3f, 0.65f);
        }

        public bool playerGetsHitByEnemy(Rectangle player)
        {
            if (Rectangle.Intersects(player))
            {
                return true;
            }
            return false;
        }

        public void UpdatePosition(float gameTime)
        {
            
            speed = gameTime * acceleration + speed;
            position += speed * gameTime;
            Console.WriteLine("Update: " + position);

        }

        public void Collision(Rectangle newRectangle, Camera camera)
        {
            Vector2 newPos = position;
            newPos = camera.getVisualCoords(position);
            Rectangle = new Rectangle((int)newPos.X, (int)newPos.Y, 32, 32);

            //newRecPosition = camera.getLogicalCoords(new Vector2(newRectangle.X, newRectangle.Y));

            if (Rectangle.TouchLeft(newRectangle) || Rectangle.TouchRight(newRectangle))
            {
                speed.X = -speed.X;
                acceleration.X = -acceleration.X;
            }  
        }
    }
}
