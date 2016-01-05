using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.View
{
    class EnemyView
    {

        Texture2D enemyTexture;
        Enemy enemy;
        Camera camera;

        public EnemyView(Camera newCamera, Enemy newEnemy)
        {
            camera = newCamera;
            enemy = newEnemy;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D newTexture)
        {
            if (enemy != null)
            {
                enemyTexture = newTexture;
                //float playerScale = camera.getTextureScale(playerTexture.Width, playerSimulation.getSize());
                spriteBatch.Draw(enemyTexture, camera.getVisualCoords(enemy.position), Color.White);
            }
        }

    }
}
