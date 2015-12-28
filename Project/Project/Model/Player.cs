using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Model
{
    class Player
    {
        // TODO: Fix start position.
        Vector2 position = new Vector2(0.1f, 0.5f);

        public Vector2 acceleration;

        public Vector2 speed = Vector2.Zero;
       
        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        // X movement stuff.
        float maxSpeed = 0.175f;
        float deAccelerate = 0.03f;
        float accelerate = 0.01f;

        float standardGravity = 1.5f;
        float size = 0.025f;

        private bool isAlive;
        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        private bool canJumpAgain;
        public bool CanJumpAgain
        {
            get { return canJumpAgain; }
            set { canJumpAgain = value; }
        }

        private bool canJump;
        public bool CanJump
        {
            get { return canJump; }
            set { canJump = value; }
        }

        private bool touchingFloor;
        public bool TouchingFloor
        {
            get { return touchingFloor; }
            set { touchingFloor = value; }
        }

        private bool isFalling;
        public bool IsFalling
        {
            get { return isFalling; }
            set { isFalling = value; }
        }

        public void UpdatePosition(float gameTime)
        {

            if(TouchingFloor && IsFalling)
            {
                StandOnGround();
            }
            else if(!TouchingFloor && !IsFalling)
            {
                Fall();
            }
            
                
            speed = gameTime * acceleration + speed;
            position += speed * gameTime;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public void setSpeedLeft()
        {
            speed.X -= accelerate;
            if(speed.X <= -maxSpeed)
            {
                speed.X = -maxSpeed;
            }
        }

        public void setSpeedRight()
        {
            speed.X += accelerate;
            if (speed.X >= maxSpeed)
            {
                speed.X = maxSpeed;
            }
        }

        public void setSpeedZero()
        {
            
            if (speed.X > 0)
            {
                speed.X -= deAccelerate;

                if (speed.X <= 0)
                {
                    speed.X = 0;
                }
            }
            if (speed.X < 0)
            {
                speed.X += deAccelerate;

                if (speed.X >= 0)
                {
                    speed.X = 0;
                }
            }
        }

        public void Jump(Enum currentPlayerForm)
        {
            if(CanJump && CanJumpAgain)
            {
                speed.Y = -0.6f;
                CanJump = false;
            }
            else if(!CanJump && CanJumpAgain)
            {
                if (currentPlayerForm.ToString() == "Triangle")
                {
                    speed.Y = -0.6f;
                    CanJumpAgain = false;
                }
            }
        }

        public void Fall()
        {
            acceleration.Y = standardGravity;
            IsFalling = true;
        }

        public void StandOnGround()
        {
            acceleration.Y = 0;
            speed.Y = 0;
            IsFalling = false;
        }

        public float getSize()
        {
            return size;
        }
    }
}
