using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Model
{
    class PlayerSimulation
    {

        Player player = new Player();
        Rectangle rectangle;
        Vector2 position;
        Vector2 newRecPosition;

        public void UpdateMovement(float gameTime, KeyboardState currentKeyboardState, Enum currentPlayerForm)
        {
            
            if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                player.Jump(currentPlayerForm);   
            }

            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                player.setSpeedLeft(currentPlayerForm);
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                player.setSpeedRight(currentPlayerForm);
            }

            if(currentKeyboardState.IsKeyUp(Keys.Left) &&
                currentKeyboardState.IsKeyUp(Keys.Right) &&
                currentKeyboardState.IsKeyUp(Keys.Up))
            {
                player.setSpeedZero();
            }

            player.UpdatePosition(gameTime);

        }

        public void setStartPosition()
        {
            player.setStartPosition();
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset, Camera camera)
        {
            position = camera.getVisualCoords(player.position);
            rectangle = new Rectangle((int)position.X, (int)position.Y, 32, 32);

            newRecPosition = camera.getLogicalCoords(new Vector2(newRectangle.X, newRectangle.Y));


            if (rectangle.TouchTop(newRectangle))
            {
                if(rectangle.Bottom > newRectangle.Top)
                {
                    player.position.Y = player.position.Y - 0.003f;
                    player.speed.Y -= 0.01f;
                }
                
                player.TouchingFloor = true;
                player.CanJump = true;
                player.CanJumpAgain = true;
            }

            if (rectangle.TouchLeft(newRectangle))
            {
                player.speed.X = -0.06f;

            }
            if (rectangle.TouchRight(newRectangle))
            {
                player.speed.X = 0.05f;
            }

            if (rectangle.TouchBottom(newRectangle))
            {
                player.speed.Y = 0.1f;
            }

            if (position.X < 0)
            {
                position.X = 0 + rectangle.Width;
                player.speed.X = 0.05f;
            }

            if (position.X > xOffset - rectangle.Width)
            {
                position.X = xOffset - rectangle.Width;
                player.speed.X = -0.05f;
            }

            // If player falls, set IsAlive to false. 
            if (position.Y > yOffset - rectangle.Height)
            {
                player.IsAlive = false;
                position.Y = yOffset - rectangle.Height;
            }

            // If player leaves top of a rectangle.
            if (position.X > newRectangle.X + newRectangle.Width)
            {
                player.TouchingFloor = false;
                player.CanJump = false;
            }
        }

        public void PlayerGotJumpPowerUp()
        {
            player.PlayerGotJumpPowerUp = true;
        }

        public bool PlayerHasJumpPowerUp()
        {
            if (player.PlayerGotJumpPowerUp)
            {
                return true;
            }
            return false;
        }
        public void PlayerGotSprintPowerUp()
        {
            player.PlayerGotSprintPowerUp = true;
        }

        public bool PlayerHasSprintPowerUp()
        {
            if(player.PlayerGotSprintPowerUp)
            {
                return true;
            }
            return false;
        }

        public void PlayerGotToExit()
        {
            player.PlayerGotToExit = true;
        }

        public bool isPlayerAlive()
        {
            if(player.IsAlive)
            {
                return true;
            }
            return false;
        }

        public void PlayerIsAlive()
        {
            player.IsAlive = true;
        }
        public void PlayerIsDead()
        {
            player.IsAlive = false;
        }

        public Vector2 getPosition()
        {
            return player.position;
        }

        public Rectangle getRectangle()
        {
            return rectangle;
        }

        public float getSize()
        {
            return player.getSize();
        }
    }
}
