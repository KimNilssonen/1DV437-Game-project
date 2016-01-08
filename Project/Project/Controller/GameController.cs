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
using Splitter;

namespace Project.Controller
{
    class GameController
    {
        // Model stuff.
        PlayerSimulation playerSimulation;
        List<Enemy> enemies = new List<Enemy>();

        // Input stuff.
        KeyboardState currentKeyboardState;

        // View stuff.
        PlayerView playerView;
        EnemyView enemyView;
        LevelSystem levelSystem;
        SplitterSystem splitterSystem;

        List<SplitterSystem> splitterList = new List<SplitterSystem>();
        List<SplitterSystem> splitterToBeRemoved = new List<SplitterSystem>();

        Camera camera;

        Font font;
        Font fontBorder;
        Font fontBorder1;
        Font fontBorder2;
        Font fontBorder3;
        
        string powerUpInfo; 

        // Textures.
        Texture2D playerTexture;
        Texture2D gameBackgroundTexture;

        ContentManager content;

        public enum PlayerForm
        {
            Square,
            Triangle,
            Circle,
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
            content = Content;
            Tiles.Content = Content;
            camera = new Camera(graphics.GraphicsDevice.Viewport);
        }
        

        public void LoadLevel()
        {

            currentPlayerForm = PlayerForm.Square;

            playerTexture = content.Load<Texture2D>("PlayerSquare");
            gameBackgroundTexture = content.Load<Texture2D>("GameBackground");
            

            playerSimulation = new PlayerSimulation();
            playerView = new PlayerView(camera, playerSimulation);
            playerSimulation.PlayerIsAlive();

            splitterSystem = new SplitterSystem(content, playerSimulation, camera);
            levelSystem = new LevelSystem(content, camera, selectedLevel);
            enemies.Clear();
            Enemy enemy;
            

            if(SelectedLevel != 0)
            {
                playerSimulation.PlayerGotJumpPowerUp();

                // Maybe should've put this in a separate "enemyController" class.
                if (SelectedLevel == 1)
                {
                    // ------ isSpecial parameter if I want to make one specific enemy move a certain way.
                    //-------------- (new position------------, new speed--------------, isSpecial)
                    enemy = new Enemy(new Vector2(1.3f, 0.65f), new Vector2(0.5f, 0.0f), false);
                    enemies.Add(enemy);
                    enemy = new Enemy(new Vector2(1.8f, 0.55f), new Vector2(-0.2f, 0.0f), false);
                    enemies.Add(enemy);
                }

                if(SelectedLevel == 2)
                {
                    enemy = new Enemy(new Vector2(0.99f, 0.1f), new Vector2(0.0f, 0.3f), false);
                    enemies.Add(enemy);
                    enemy = new Enemy(new Vector2(1.3f, 0.35f), new Vector2(-0.3f, 0.0f), false);
                    enemies.Add(enemy);
                    enemy = new Enemy(new Vector2(1.3f, 0.47f), new Vector2(0.3f, 0.0f), false);
                    enemies.Add(enemy);
                    enemy = new Enemy(new Vector2(1.9f, 0.33f), new Vector2(0.2f, 0.0f), false);
                    enemies.Add(enemy);
                    enemy = new Enemy(new Vector2(2.0f, 0.81f), new Vector2(0.15f, 0.0f), false);
                    enemies.Add(enemy);
                    enemy = new Enemy(new Vector2(2.075f, 0.81f), new Vector2(0.0f, 0.15f), false);
                    enemies.Add(enemy);
                }

                enemyView = new EnemyView(camera, enemies);
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
                if (currentPlayerForm != PlayerForm.Square)
                {
                    playerTexture = content.Load<Texture2D>("PlayerSquare");
                    currentPlayerForm = PlayerForm.Square;
                }
            }
            else if (currentKeyboardState.IsKeyDown(Keys.D2) && playerSimulation.PlayerHasJumpPowerUp())
            {
                if (currentPlayerForm != PlayerForm.Triangle)
                {
                    splitterList.Add(new SplitterSystem(content, playerSimulation, camera));
                    playerTexture = content.Load<Texture2D>("PlayerTriangle");
                    currentPlayerForm = PlayerForm.Triangle;
                }
            }
            else if(currentKeyboardState.IsKeyDown(Keys.D3) && playerSimulation.PlayerHasSprintPowerUp())
            {
                if (currentPlayerForm != PlayerForm.Circle)
                {
                    playerTexture = content.Load<Texture2D>("PlayerCircle");
                    currentPlayerForm = PlayerForm.Circle;
                }
            }
        }

        public void Update(float gameTime)
        {

            if (playerSimulation.isPlayerAlive())
            {
                currentKeyboardState = Keyboard.GetState();
                changePlayerTexture(currentKeyboardState);

                if (splitterList.Count != 0)
                {
                    foreach (SplitterSystem splitter in splitterList)
                    {
                        splitter.Update(gameTime);
                        if (splitter.isParticleOutOfScreen())
                        {
                            splitterToBeRemoved.Add(splitter);
                        }
                    }
                    foreach (SplitterSystem splitter in splitterToBeRemoved)
                    {
                        splitterList.Remove(splitter);
                    }

                    
                }
                if (currentPlayerForm == PlayerForm.Circle && currentKeyboardState.IsKeyDown(Keys.Left) ||
                    currentPlayerForm == PlayerForm.Circle && currentKeyboardState.IsKeyDown(Keys.Right))
                {
                    splitterList.Add(new SplitterSystem(content, playerSimulation, camera));
                }

                playerSimulation.UpdateMovement(gameTime, currentKeyboardState, currentPlayerForm);
                

                foreach (CollisionTiles tile in levelSystem.CollisionTiles)
                {
                    
                    // Using camera in playerSimulation.Collision to be able to use rectangles.
                    playerSimulation.Collision(tile.Rectangle, levelSystem.Width, levelSystem.Height, camera);
                    if (enemies.Count != 0)
                    {
                        foreach (Enemy enemy in enemies)
                        {
                            enemy.Collision(tile.Rectangle, camera);
                        }
                    }
                    camera.Update(camera.getVisualCoords(playerSimulation.getPosition()), levelSystem.Width, levelSystem.Height);
                }

                if (levelSystem.PlayerGetsPowerUp(playerSimulation.getRectangle()))
                {
                    if (SelectedLevel == 0)
                    {
                        playerSimulation.PlayerGotJumpPowerUp();
                    }
                    if(SelectedLevel == 2)
                    {
                        playerSimulation.PlayerGotSprintPowerUp();
                    }
                }
                if (enemies.Count != 0)
                {
                    foreach (Enemy enemy in enemies)
                    {
                        enemy.UpdatePosition(gameTime);
                        if (enemy.playerGetsHitByEnemy(playerSimulation.getRectangle()))
                        {
                            playerSimulation.PlayerIsDead();
                        }
                    }
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
            spriteBatch.Draw(gameBackgroundTexture, new Vector2(camera.Center.X / 10, camera.Center.Y / 20), Color.White);
            levelSystem.Draw(spriteBatch);

        /* FONTS -----------------------------------------------------------------------------------------------------------------*/
            if (playerSimulation.PlayerHasJumpPowerUp() && selectedLevel == 0)
            {
                powerUpInfo = "                    Awesome!\nI can now doublejump by switching to\ntriangle form (2) in mid-air.I must reset the\njump by go back to a square again.";
                Vector2 fontPosition = new Vector2(levelSystem.getPowerUpPosition().X - levelSystem.getPowerUpPosition().X / 8, levelSystem.getPowerUpPosition().Y);

                /*-- Used to get a border around the font-----------------------------------------------------------*/
                fontBorder = new Font(content.Load<SpriteFont>("Info"), new Vector2(fontPosition.X, fontPosition.Y - 2),
                    powerUpInfo, Color.Black);
                fontBorder.Draw(spriteBatch);

                fontBorder1 = new Font(content.Load<SpriteFont>("Info"), new Vector2(fontPosition.X, fontPosition.Y + 2),
                    powerUpInfo, Color.Black);
                fontBorder1.Draw(spriteBatch);

                fontBorder2 = new Font(content.Load<SpriteFont>("Info"), new Vector2(fontPosition.X - 2, fontPosition.Y),
                    powerUpInfo, Color.Black);
                fontBorder2.Draw(spriteBatch);

                fontBorder3 = new Font(content.Load<SpriteFont>("Info"), new Vector2(fontPosition.X + 2, fontPosition.Y),
                    powerUpInfo, Color.Black);
                fontBorder3.Draw(spriteBatch);
                /*-------------------------------------------------------------------------------------------------*/

                font = new Font(content.Load<SpriteFont>("Info"), new Vector2(levelSystem.getPowerUpPosition().X - levelSystem.getPowerUpPosition().X / 8, levelSystem.getPowerUpPosition().Y),
                    powerUpInfo, Color.White);
                font.Draw(spriteBatch);
            }

            if (playerSimulation.PlayerHasSprintPowerUp() && selectedLevel == 2)
            {
                powerUpInfo = "                    Amazing!\nI can now sprint by switching to\ncircle form (3). The jump is not as good though.";
                /*-- Used to get a border around the font-----------------------------------------------------------*/
                fontBorder = new Font(content.Load<SpriteFont>("Info"), new Vector2(levelSystem.getPowerUpPosition().X, levelSystem.getPowerUpPosition().Y - 2),
                    powerUpInfo, Color.Black);
                fontBorder.Draw(spriteBatch);

                fontBorder1 = new Font(content.Load<SpriteFont>("Info"), new Vector2(levelSystem.getPowerUpPosition().X, levelSystem.getPowerUpPosition().Y + 2),
                    powerUpInfo, Color.Black);
                fontBorder1.Draw(spriteBatch);

                fontBorder2 = new Font(content.Load<SpriteFont>("Info"), new Vector2(levelSystem.getPowerUpPosition().X - 2, levelSystem.getPowerUpPosition().Y),
                    powerUpInfo, Color.Black);
                fontBorder2.Draw(spriteBatch);

                fontBorder3 = new Font(content.Load<SpriteFont>("Info"), new Vector2(levelSystem.getPowerUpPosition().X + 2, levelSystem.getPowerUpPosition().Y),
                    powerUpInfo, Color.Black);
                fontBorder3.Draw(spriteBatch);
                /*--------------------------------------------------------------------------------------------------*/

                font = new Font(content.Load<SpriteFont>("Info"), levelSystem.getPowerUpPosition(),
                    powerUpInfo, Color.White);
                font.Draw(spriteBatch);
            }
        /* FONTS END--------------------------------------------------------------------------------------------------------------*/

            if (splitterList.Count != 0)
            {
                foreach(SplitterSystem splitter in splitterList)
                {
                    splitter.Draw(spriteBatch);
                }
                
            }

            playerView.Draw(spriteBatch, playerTexture);

            if (enemies.Count != 0)
            {
                foreach (Enemy enemy in enemies)
                {
                    enemyView.Draw(spriteBatch, content.Load<Texture2D>("Tile6"));
                }
            }            

            spriteBatch.End();
        }
    }
}
