using Microsoft.Xna.Framework;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Splitter
{
    class SplitterParticle
    {
        float maxSpeed = 0.05f;
        public Vector2 randomDirection;
        Vector2 velocity;
        Vector2 acceleration = new Vector2(0.0f, 0.3f);
        public Vector2 position;


        public SplitterParticle(Random rand, Vector2 playerPosition) 
        {
            randomDirection = new Vector2((float)rand.NextDouble() - 0.5f, (float)rand.NextDouble() - 0.5f);
            //normalize to get it spherical vector with length 1.0
            randomDirection.Normalize();
            //add random length between 0 to maxSpeed
            randomDirection = randomDirection * ((float)rand.NextDouble() * maxSpeed);
            velocity = randomDirection;
            position = playerPosition;
        }

        public void Update(float gameTime)
        {
            velocity = gameTime * acceleration + velocity;
            position = gameTime * velocity + position;
        }
    }
}
