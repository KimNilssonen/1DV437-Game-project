
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project.Model;
using Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Levels
{
    class LevelSystem
    {
        Exit exit;
        PowerUp powerUp;
        Vector2 powerUpPosition;
        //Enemy enemy;

        ContentManager content;
        Camera camera;

        private List<int[,]> levels = new List<int[,]>();

        private List<CollisionTiles> collisionTiles = new List<CollisionTiles>();
        public List<CollisionTiles> CollisionTiles
        {
            get { return collisionTiles; }
        }

        private int width, height;

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public LevelSystem(ContentManager _content, Camera _camera, int selectedLevel)
        {
            content = _content;
            camera = _camera;

            LevelCreator levelCreator = new LevelCreator();
            levels = levelCreator.getLevels();

            Generate(selectedLevel);
        }


        public void Generate(int selectedLevel)
        {
            int size = 48;
            for (int x = 0; x < levels[selectedLevel].GetLength(1); x++)
            {
                for (int y = 0; y < levels[selectedLevel].GetLength(0); y++)
                {
                    int textureIndex = levels[selectedLevel][y, x];
                    
                    if (textureIndex > 0)
                    {
                        if (textureIndex == 9)
                        {
                            exit = new Exit(content, new Rectangle(x*size, y*size, size, size));
                        }
                        else if(textureIndex == 5)
                        {
                            powerUp = new PowerUp(content, new Rectangle(x * size+12, y * size+12, size-12, size-12));
                        }
                        else
                        {
                            collisionTiles.Add(new CollisionTiles(textureIndex, new Rectangle(x * size, y * size, size, size)));
                        }
                        width = (x + 1) * size;
                        height = (x + 1) * size;
                    }

                }
            }
        }

        public bool PlayerGetsPowerUp(Rectangle player)
        {
            if (powerUp != null)
            {
                if (powerUp.playerGetsPowerUp(player))
                {
                    powerUpPosition = new Vector2(powerUp.Rectangle.X-powerUp.Rectangle.Width, powerUp.Rectangle.Y-powerUp.Rectangle.Height*3);
                    powerUp = null;
                    return true;
                }
            }
            return false;
        }

        public bool PlayerGotToExit(Rectangle player)
        {
            if(exit.PlayerGotToExit(player))
            {
                return true;
            }
            return false;
        }

        public Vector2 getPowerUpPosition()
        {
            return new Vector2(powerUpPosition.X, powerUpPosition.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(CollisionTiles tile in collisionTiles)
            {
                tile.Draw(spriteBatch);
            }

            if(powerUp != null)
            {
                powerUp.Draw(spriteBatch);
            }

            exit.Draw(spriteBatch);
        }
    }
}
