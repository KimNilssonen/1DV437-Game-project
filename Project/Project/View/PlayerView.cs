using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Model;

namespace Project.View
{
    class PlayerView
    {
        Texture2D playerTexture;
        PlayerSimulation playerSimulation;
        Camera camera;

        public PlayerView(Camera newCamera, PlayerSimulation newPlayerSimulation)
        {
            camera = newCamera;
            playerSimulation = newPlayerSimulation;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D newPlayerTexture)
        {
            playerTexture = newPlayerTexture;

            //float playerScale = camera.getTextureScale(playerTexture.Width, playerSimulation.getSize());
            spriteBatch.Draw(playerTexture, camera.getVisualCoords(playerSimulation.getPosition()), Color.White);
        }
    }
}
