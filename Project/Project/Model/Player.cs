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

        // X movement stuff.
        float maxSpeed = 0.175f;
        float deAccelerate = 0.03f;
        float accelerate = 0.01f;

        float standardGravity = 1.5f;
        float size = 0.025f;


        private Rectangle rectangle;
        private bool isAlive = true;
        private bool playerGotPowerUp;
        private bool canJumpAgain;
        private bool canJump;
        private bool touchingFloor;
        private bool isFalling;


        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        public bool PlayerGotPowerUp
        {
            get { return playerGotPowerUp; }
            set { playerGotPowerUp = true; }
        }

        public bool CanJumpAgain
        {
            get { return canJumpAgain; }
            set { canJumpAgain = value; }
        }

        public bool CanJump
        {
            get { return canJump; }
            set { canJump = value; }
        }

        public bool TouchingFloor
        {
            get { return touchingFloor; }
            set { touchingFloor = value; }
        }

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
