using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project.View
{
    class Camera
    {
        private Matrix transform;
        public Matrix Transform
        {
            get
            {
                return transform;
            }
        }

        private Vector2 center;
        private Viewport viewPort;

        private float screenWidth;
        private float screenHeight;

        public Camera(Viewport newViewPort)
        {
            viewPort = newViewPort;
            screenWidth = viewPort.Width;
            screenHeight = viewPort.Height;
        }

        public Vector2 getVisualCoords(Vector2 logicalCoords)
        {

            float visualX = (logicalCoords.X * screenWidth);
            float visualY = (logicalCoords.Y * screenHeight);

            return new Vector2(visualX, visualY);
        }

        public Vector2 getLogicalCoords(Vector2 visualCoords)
        {
            float logicalX = visualCoords.X / screenWidth;
            float logicalY = visualCoords.Y / screenHeight;

            return new Vector2(logicalX, logicalY);
        }


        public void Update(Vector2 position, int xOffset, int yOffset)
        {
            
            // X
            if (position.X < viewPort.Width / 2)
            {
                center.X = viewPort.Width / 2;
            }
            else if (position.X > xOffset - (viewPort.Width / 2))
            {
                center.X = xOffset - (viewPort.Width / 2);
            }
            else
            {
                center.X = position.X;
            }

            //Console.WriteLine("pos: " + position.Y + ", port: " + viewPort.Height + ", yOffset: " + yOffset);
            // Y
            if (position.Y < viewPort.Height)
            {
                // +40 so the platform rise a bit from the bottom.
                center.Y = (viewPort.Height / 4);
            }
            else if (position.Y > yOffset - (viewPort.Height / 2))
            {
                center.Y = yOffset - (viewPort.Height / 2);
            }
            else
            {
                center.Y = position.Y;
            }

            transform = Matrix.CreateTranslation(new Vector3(-center.X + (viewPort.Width/2),
                                                             -center.Y + (viewPort.Height/2),0));
        }

        //float scaleX;
        //float scaleY;

        //// Used for resizing all textures.
        //float overallSize = 1.0f;

        //public Camera(Viewport viewPort)
        //{
        //    scaleX = viewPort.Width;
        //    scaleY = viewPort.Height;

        //}

        //public Vector2 getVisualCoords(Vector2 logicalCoords, float textureWidth, float textureHeight)
        //{

        //    float visualX = (logicalCoords.X * scaleX) - textureWidth / 2;
        //    float visualY = (logicalCoords.Y * scaleY) - textureHeight / 2;

        //    return new Vector2(visualX, visualY);
        //}


        //public Vector2 getLogicalCoords(Vector2 visualCoords)
        //{
        //    float logicalX = (visualCoords.X) / scaleX;
        //    float logicalY = (visualCoords.Y) / scaleY;
        //    //Console.WriteLine(logicalX + " " + logicalY);
        //    return new Vector2(logicalX, logicalY);
        //}

        //public float getTextureScale(float textureWidth, float size)
        //{
        //    return scaleX * (size * overallSize) / textureWidth;
        //}

        //public Rectangle getGameArea()
        //{
        //    return new Rectangle(0, 0, (int)scaleX, (int)scaleY);
        //}

        //public Vector2 getViewport()
        //{
        //    return new Vector2(scaleX, scaleY);
        //}
    }
}