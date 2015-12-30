using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Project.Levels
{

    class Tiles
    {
        protected Texture2D texture;

        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        private static ContentManager content;
        public static ContentManager Content
        {
            get { return content; }
            set { content = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }

    class CollisionTiles : Tiles
    {
        
        public CollisionTiles(int i, Rectangle newRectangle)
        {
            
            texture = Content.Load<Texture2D>("Tile" + i);
            this.Rectangle = newRectangle;
        }
    }
}

    // Old code. Remove later...
    ///*
    // * Found this code here: http://xnatd.blogspot.se/2009/02/ok-so-first-part-of-our-tower-defence.html
    // * Seems like a good way to build up the map.
    // */
    //class Level
    //{
    //    Camera camera;

    //    int[,] map = new int[,]
    //    {
    //        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    //        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    //        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    //        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    //        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    //        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    //        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    //        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    //        {1,1,1,1,1,1,1,1,1,0,0,1,1,1,1,1},
    //    };

    //    public Level(Camera newCamera)
    //    {
    //        camera = newCamera;
    //    }

    //    private List<Texture2D> tileTextures = new List<Texture2D>();

    //    public void AddTexture(Texture2D texture)
    //    {
    //        tileTextures.Add(texture);
    //    }

    //    public int Width
    //    {
    //        get { return map.GetLength(1); }
    //    }
    //    public int Height
    //    {
    //        get { return map.GetLength(0); }
    //    }

    //    public void Draw(SpriteBatch spriteBatch)
    //    {
    //        for (int x = 0; x < Width; x++)
    //        {
    //            for (int y = 0; y < Height; y++)
    //            {
    //                int textureIndex = map[y, x];
    //                if(textureIndex == -1)
    //                {
    //                    continue;
    //                }

    //                Texture2D texture = tileTextures[textureIndex];
    //                float tileScale = camera.getTextureScale(texture.Width, 0.025f);
    //                spriteBatch.Draw(texture, new Rectangle(x*32, y*32, 32, 32), Color.White);
    //            }
    //        }
    //    }

    //}
