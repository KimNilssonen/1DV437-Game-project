using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Model;
using Project.Levels;
using Project.View;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Project.Controller
{
    class GameController
    {
        // Model stuff.
        PlayerSimulation playerSimulation;

        // Input stuff.
        KeyboardState currentKeyboardState;

        // View stuff.
        PlayerView playerView;
        LevelSystem levelSystem;
        Camera camera;

        // Textures.
        Texture2D playerTexture;

        ContentManager content;

        public enum PlayerForm
        {
            Square,
            Triangle
        }
        
        PlayerForm currentPlayerForm;

        private int selectedLevel = 0;
        private bool gameOver;
        public bool GameOver
        {
            get { return gameOver; }
            set { gameOver = value; }
        }

        public GameController(ContentManager Content, GraphicsDeviceManager graphics)
        {
            content = Content;
            currentPlayerForm = PlayerForm.Square;

            Tiles.Content = Content;

            camera = new Camera(graphics.GraphicsDevice.Viewport);
            levelSystem = new LevelSystem(content, camera, selectedLevel);
            

            // If new game, load this player texture.
            playerTexture = Content.Load<Texture2D>("PlayerSquare");

            playerSimulation = new PlayerSimulation();
            playerView = new PlayerView(camera, playerSimulation);
        }
        

        public void Play()
        {
            // Might need to load levels etc.
            
        }

        public void changePlayerTexture(KeyboardState currentKeyboardState)
        {
            // If player press 1, 2, etc. load new texture here...
            // if(input == 2) playerTexture = Content.Load<Texture2D>("PlayerTriangle");
            // if(input == 3) playerTexture = Content.Load<Texture2D>("PlayerCircle");
            // etc...


            if(currentKeyboardState.IsKeyDown(Keys.D1))
            {
                playerTexture = content.Load<Texture2D>("PlayerSquare");
                currentPlayerForm = PlayerForm.Square;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.D2) && playerSimulation.PlayerHasPowerUp())
            {
                playerTexture = content.Load<Texture2D>("PlayerTriangle");
                currentPlayerForm = PlayerForm.Triangle;
            }
        }

        public void Update(float gameTime)
        {
            if (playerSimulation.isPlayerAlive())
            {
                currentKeyboardState = Keyboard.GetState();
                changePlayerTexture(currentKeyboardState);

                playerSimulation.UpdateMovement(gameTime, currentKeyboardState, currentPlayerForm);

                foreach (CollisionTiles tile in levelSystem.CollisionTiles)
                {

                    // Using camera in playerSimulation.Collision to be able to use rectangles.
                    playerSimulation.Collision(tile.Rectangle, levelSystem.Width, levelSystem.Height, camera);
                    camera.Update(camera.getVisualCoords(playerSimulation.getPosition()), levelSystem.Width, levelSystem.Height);
                }

                if (levelSystem.playerGetsPowerUp(playerSimulation.getRectangle()))
                {
                    playerSimulation.PlayerGotPowerUp();
                }
            }
            else
            {
                GameOver = true;
            }         
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,
                              BlendState.AlphaBlend,
                              null, null, null, null,
                              camera.Transform);

            levelSystem.Draw(spriteBatch);
            
            playerView.Draw(spriteBatch, playerTexture);

            spriteBatch.End();
        }
    }
}
