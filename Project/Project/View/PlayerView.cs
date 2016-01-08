using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Model;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Project.View
{
    class PlayerView : Observer
    {
        Texture2D playerTexture;
        PlayerSimulation playerSimulation;
        ContentManager content;
        Camera camera;

        SoundEffect soundEffect;

        public PlayerView(Camera newCamera, PlayerSimulation newPlayerSimulation, ContentManager newContent)
        {
            camera = newCamera;
            playerSimulation = newPlayerSimulation;
            content = newContent;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D newPlayerTexture)
        {
            playerTexture = newPlayerTexture;
            spriteBatch.Draw(playerTexture, camera.getVisualCoords(playerSimulation.getPosition()), Color.White);
        }


    // Observer stuff to play sound.
        public void playerJump()
        {
            soundEffect = content.Load<SoundEffect>("SoundFX/jumpSFX");
            soundEffect.Play(0.2f, 0.0f, 0.0f);
        }

        public void playerDied()
        {
            soundEffect = content.Load<SoundEffect>("SoundFX/loseSFX");
            soundEffect.Play(0.5f, 0.0f, 0.0f);
        }

        public void playerWon()
        {
            soundEffect = content.Load<SoundEffect>("SoundFX/winSFX");
            soundEffect.Play(0.5f, 0.0f, 0.0f);
        }

        public void playerTransformed()
        {
            soundEffect = content.Load<SoundEffect>("SoundFX/transformSFX");
            soundEffect.Play(0.15f, 0.0f, 0.0f);
        }

        public void playerLanded()
        {
            soundEffect = content.Load<SoundEffect>("SoundFX/impactSFX");
            soundEffect.Play(0.4f, 0.0f, 0.0f);
        }
    }
}
