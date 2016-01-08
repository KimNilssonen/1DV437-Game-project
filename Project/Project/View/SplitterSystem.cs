using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project.Model;
using Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Splitter
{
    class SplitterSystem
    {
        PlayerSimulation _playerSimulation;
        Camera _camera;
        Texture2D sparkTexture;

        int maxParticles = 25;
        public List<SplitterParticle> particleList;
        Random rand = new Random();

        public SplitterSystem(ContentManager content, PlayerSimulation playerSimulation, Camera camera)
        {
            sparkTexture = content.Load<Texture2D>("spark");
            _playerSimulation = playerSimulation;
            _camera = camera;

            particleList = new List<SplitterParticle>();

            for (int i = 0; i < maxParticles; i++)
            {
                particleList.Add(new SplitterParticle(rand, _playerSimulation.getPosition() + new Vector2(_playerSimulation.getSize() / 2, _playerSimulation.getSize())));
            }
        }

        public bool isParticleOutOfScreen()
        {
            if (particleList.TrueForAll(isOutOfScreen))
            {
                return true;
            }
            return false;
        }

        private bool isOutOfScreen(SplitterParticle particle)
        {
            return particle.position.Y >= 10;
        }

        public void Update(float gameTime)
        {
            foreach(SplitterParticle particle in particleList)
            {
                particle.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (SplitterParticle particle in particleList)
            {
                spriteBatch.Draw(sparkTexture, _camera.getVisualCoords(particle.position), null, Color.White, 0, Vector2.Zero, 0.2f, SpriteEffects.None, 0);
            }
        }

    }
}
