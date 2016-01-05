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
        Enemy enemy;

        // Input stuff.
        KeyboardState currentKeyboardState;

        // View stuff.
        PlayerView playerView;
        EnemyView enemyView;
        LevelSystem levelSystem;
        Camera camera;
        Font font;
        Font fontBorder;
        Font fontBorder1;
        Font fontBorder2;
        Font fontBorder3;
        string doubleJumpPowerUpInfo = "                    Awesome!\nI can now doublejump by switching to\ntriangle form in mid-air.I must reset the\njump by go back to a square again.";

        // Textures.
        Texture2D playerTexture;
        Texture2D gameBackgroundTexture;

        ContentManager content;

        public enum PlayerForm
        {
            Square,
            Triangle
        }
        
        PlayerForm currentPlayerForm;

        private int selectedLevel;
        public int SelectedLevel
        {
            get { return selectedLevel; }
            set { selectedLevel = value; }
        }

        private bool gameOver;
        public bool GameOver
        {
            get { return gameOver; }
            set { gameOver = value; }
        }

        private bool finishedLevel;
        public bool FinishedLevel
        {
            get { return finishedLevel; }
            set { finishedLevel = value; }
        }

        public GameController(ContentManager Content, GraphicsDeviceManager graphics)
        {
            SelectedLevel = 0;
            content = Content;
            currentPlayerForm = PlayerForm.Square;

            Tiles.Content = Content;

            camera = new Camera(graphics.GraphicsDevice.Viewport);
        }
        

        public void LoadLevel()
        {
            playerTexture = content.Load<Texture2D>("PlayerSquare");
            gameBackgroundTexture = content.Load<Texture2D>("GameBackground");

            playerSimulation = new PlayerSimulation();
            playerView = new PlayerView(camera, playerSimulation);
            playerSimulation.PlayerIsAlive();

            levelSystem = new LevelSystem(content, camera, selectedLevel);
            enemy = levelSystem.getEnemy();
            enemyView = new EnemyView(camera, enemy);

            if(SelectedLevel != 0)
            {
                playerSimulation.PlayerGotPowerUp();
            }

            playerSimulation.setStartPosition();
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
                levelSystem.Update(gameTime);

                foreach (CollisionTiles tile in levelSystem.CollisionTiles)
                {
                    
                    // Using camera in playerSimulation.Collision to be able to use rectangles.
                    playerSimulation.Collision(tile.Rectangle, levelSystem.Width, levelSystem.Height, camera);
                    if (enemy != null)
                    {
                        enemy.Collision(tile.Rectangle, camera);
                    }
                    camera.Update(camera.getVisualCoords(playerSimulation.getPosition()), levelSystem.Width, levelSystem.Height);
                }

                if (levelSystem.PlayerGetsPowerUp(playerSimulation.getRectangle()))
                {
                    playerSimulation.PlayerGotPowerUp();
                }
                if (levelSystem.PlayerGetsHitByEnemy(playerSimulation.getRectangle()))
                {
                    playerSimulation.PlayerIsDead();
                }

                if(levelSystem.PlayerGotToExit(playerSimulation.getRectangle()))
                {
                    FinishedLevel = true;
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
            // new Vector2(camera.Center.X/10, camera.Center.Y/20)... gives kind off a parallax scrolling background.
            // Messed around values and ended up with this.
            spriteBatch.Draw(gameBackgroundTexture, new Vector2(camera.Center.X/10, camera.Center.Y/20), Color.White);
            levelSystem.Draw(spriteBatch);
            playerView.Draw(spriteBatch, playerTexture);
            enemyView.Draw(spriteBatch, content.Load<Texture2D>("Tile6"));

            if(playerSimulation.PlayerHasPowerUp() && selectedLevel == 0)
            {

            /*-- Used to get a border around the font-------------------------------------------------------------------*/
                fontBorder = new Font(content.Load<SpriteFont>("Info"), new Vector2(levelSystem.getPowerUpPosition().X, levelSystem.getPowerUpPosition().Y - 2),
                    doubleJumpPowerUpInfo, Color.Black);
                fontBorder.Draw(spriteBatch);

                fontBorder1 = new Font(content.Load<SpriteFont>("Info"), new Vector2(levelSystem.getPowerUpPosition().X, levelSystem.getPowerUpPosition().Y + 2),
                    doubleJumpPowerUpInfo, Color.Black);
                fontBorder1.Draw(spriteBatch);

                fontBorder2 = new Font(content.Load<SpriteFont>("Info"), new Vector2(levelSystem.getPowerUpPosition().X - 2, levelSystem.getPowerUpPosition().Y),
                    doubleJumpPowerUpInfo, Color.Black);
                fontBorder2.Draw(spriteBatch);

                fontBorder3 = new Font(content.Load<SpriteFont>("Info"), new Vector2(levelSystem.getPowerUpPosition().X + 2, levelSystem.getPowerUpPosition().Y),
                    doubleJumpPowerUpInfo, Color.Black);
                fontBorder3.Draw(spriteBatch);
            /*---------------------------------------------------------------------------------------------------------*/

                font = new Font(content.Load<SpriteFont>("Info"), levelSystem.getPowerUpPosition(),
                    doubleJumpPowerUpInfo, Color.White);
                font.Draw(spriteBatch);
            }

            spriteBatch.End();
        }
    }
}
