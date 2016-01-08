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
        private Vector2 position;
        private Vector2 newPos;
        public Vector2 acceleration = new Vector2(0.0f, 0.0f);
        public Vector2 speed;

        private bool isSpecial;
        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Enemy(Vector2 newPosition, Vector2 newSpeed, bool special)
        {
            Position = newPosition;
            speed = newSpeed;
            isSpecial = special;
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
        }


        public void Collision(Rectangle newRectangle, Camera camera)
        {
            newPos = camera.getVisualCoords(Position);
            Rectangle = new Rectangle((int)newPos.X, (int)newPos.Y, 32, 32);

            //newRecPosition = camera.getLogicalCoords(new Vector2(newRectangle.X, newRectangle.Y));

            if (!isSpecial)
            {
                if (Rectangle.TouchLeft(newRectangle) || Rectangle.TouchRight(newRectangle))
                {
                    speed.X = -speed.X;
                    acceleration.X = -acceleration.X;
                }
                if (Rectangle.TouchTop(newRectangle) || Rectangle.TouchBottom(newRectangle))
                {
                    speed.Y = -speed.Y;
                    acceleration.Y = -acceleration.Y;
                }
            }
        /*----Tried to make the enemy go around a special part of the map on lvl 2 with this-------*/
            else
            {
                if(Rectangle.TouchRight(newRectangle) && 
                    speed.X < 0 && speed.Y == 0.0f)
                {
                    
                    speed.Y = 0.3f;
                    speed.X = 0.0f;
                }
                if(Rectangle.TouchTop(newRectangle) &&
                        speed.Y > 0 && speed.X == 0.0f)
                {
                    speed.Y = 0.0f;
                    speed.X = 0.3f;
                }
                if(Rectangle.TouchLeft(newRectangle) &&
                        speed.X > 0 && speed.Y == 0.0f)
                {
                    speed.Y = -0.3f;
                    speed.X = 0.0f;
                }
                if(Rectangle.TouchBottom(newRectangle) &&
                        speed.Y < 0 && speed.X == 0.0f)
                {
                    speed.Y = 0.0f;
                    speed.X = -0.3f;
                }               
            }
        /*----------------------------------------------------------------------------------*/
        }
    }
}
