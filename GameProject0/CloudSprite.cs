using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameProject0.Collisions;

namespace GameProject0
{
    public class CloudSprite
    {
        private Texture2D atlas;
        private Vector2 position;

      /// <summary>
      /// Constructor for cloud sprite, generates random position
      /// </summary>
        public CloudSprite()
        {
            position = HelperMethods.RandomVectGenerator();
        }
        /// <summary>
        /// Loads atlas texture with cloud in it
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            atlas = content.Load<Texture2D>("colored_packed");
        }
        /// <summary>
        /// When clouds hit the bottom of the screen, they are redrawn above screen
        /// in a random position and start moving down at a random velocity
        /// </summary>
        /// <param name="stop"></param>
        public void Update(bool stop)
        {
            if (!stop)
            {
                position += HelperMethods.RandomYVelGenerator(2, 5);
                if (position.Y > Constants.GAME_HEIGHT)
                {
                    position = HelperMethods.RandomVectGenerator();
                }
            }
            else
            {
                position += new Vector2(0, 0);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(atlas, position, new Rectangle(80, 32, 16, 16), Color.White, 0f, new Vector2(8, 8), 7, SpriteEffects.None, 0);//, 1, new Vector2(100,100), 100,SpriteEffects.None, 1);

        }
    }
}
