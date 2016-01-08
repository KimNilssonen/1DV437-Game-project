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
        List<Enemy> enemies;
        Camera camera;

        public EnemyView(Camera newCamera, List<Enemy> newEnemyList)
        {
            camera = newCamera;
            enemies = newEnemyList;

        }

        public void Draw(SpriteBatch spriteBatch, Texture2D newTexture)
        {
            if (enemies.Count != 0)
            {
                foreach (Enemy enemy in enemies)
                {
                    enemyTexture = newTexture;
                    spriteBatch.Draw(enemyTexture, camera.getVisualCoords(enemy.Position), Color.White);
                }
            }
        }

    }
}
