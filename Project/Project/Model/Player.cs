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
        public Vector2 position;

        public Vector2 acceleration;

        public Vector2 speed = Vector2.Zero;

        // X movement stuff.
        float deAccelerate = 0.03f;
        float accelerate = 0.01f;
        float standardMaxSpeed = 0.175f;
        float maxSpeed;

        float jumpSpeed = 0.6f;

        float standardGravity = 1.5f;
        float size = 0.025f;

        // Privates.
        private Rectangle rectangle;
        private bool isAlive = true;
        private bool playerGotJumpPowerUp;
        private bool playerGotSprintPowerUp;
        private bool playerGotToExit;
        private bool canJumpAgain;
        private bool canJump;
        private bool touchingFloor;
        private bool isFalling;

        // Properties.
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

        public bool PlayerGotJumpPowerUp
        {
            get { return playerGotJumpPowerUp; }
            set { playerGotJumpPowerUp = value; }
        }

        public bool PlayerGotSprintPowerUp
        {
            get { return playerGotSprintPowerUp; }
            set { playerGotSprintPowerUp = value; }
        }

        public bool PlayerGotToExit
        {
            get { return playerGotToExit; }
            set { playerGotToExit = value; }
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

        // Constructor
        public Player()
        {
            maxSpeed = standardMaxSpeed;
        }

        public void setStartPosition()
        {
             position = new Vector2(0.1f, 0.1f);
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

        public void setSpeedLeft(Enum currentPlayerForm)
        {
            speed.X -= accelerate;

            if (currentPlayerForm.ToString() == "Circle")
            {
                maxSpeed = standardMaxSpeed * 2;
            }
            else
            {
                if(TouchingFloor)
                {
                    maxSpeed = standardMaxSpeed;
                }
            }

            if(speed.X <= -maxSpeed)
            {
                speed.X = -maxSpeed;
            }
        }

        public void setSpeedRight(Enum currentPlayerForm)
        {
            speed.X += accelerate;

            if (currentPlayerForm.ToString() == "Circle")
            {
                maxSpeed = 0.35f;
            }
            else
            {
                if (TouchingFloor)
                {
                    maxSpeed = standardMaxSpeed;
                }
            }

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
                if(currentPlayerForm.ToString() == "Circle")
                {
                    speed.Y = -jumpSpeed + 0.2f;
                }
                else
                {
                    speed.Y = -jumpSpeed;
                }
                CanJump = false;
            }
            else if(!CanJump && CanJumpAgain)
            {
                if (currentPlayerForm.ToString() == "Triangle")
                {
                    speed.Y = -jumpSpeed;
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
