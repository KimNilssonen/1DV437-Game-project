using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Model;
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
        Level level;
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

        public GameController(ContentManager Content, GraphicsDeviceManager graphics)
        {

            camera = new Camera(graphics.GraphicsDevice.Viewport);
            level = new Level();
            content = Content;
            currentPlayerForm = PlayerForm.Square;

            Tiles.Content = Content;
            
            level.Generate(new int[,]
            {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,1,1,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0},
                {0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,9},
                {1,1,1,1,1,1,1,1,0,0,0,1,1,1,1,1,0,1,1,0,0,1,1,0,0,1,1,0,0,0,1,1,1,0,1,1,1,1,1,0,0,0,1,1},
            }, 48);

            

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
            else if (currentKeyboardState.IsKeyDown(Keys.D2))
            {
                playerTexture = content.Load<Texture2D>("PlayerTriangle");
                currentPlayerForm = PlayerForm.Triangle;
            }
        }

        public void Update(float gameTime)
        {

            currentKeyboardState = Keyboard.GetState();

            changePlayerTexture(currentKeyboardState);

            playerSimulation.UpdateMovement(gameTime, currentKeyboardState, currentPlayerForm);
            
            foreach (CollisionTiles tile in level.CollisionTiles)
            {
                // Using camera in playerSimulation to be able to use rectangles.
                playerSimulation.Collision(tile.Rectangle, level.Width, level.Height, camera);
                camera.Update(camera.getVisualCoords(playerSimulation.getPosition()), level.Width, level.Height);
                
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,
                              BlendState.AlphaBlend,
                              null, null, null, null,
                              camera.Transform);

            level.Draw(spriteBatch);
            playerView.Draw(spriteBatch, playerTexture);

            spriteBatch.End();
        }
    }
}
