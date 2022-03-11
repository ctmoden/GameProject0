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
        private Random random;
        private float rotation;
        private const int AlphaBlendDrawOrder = 100;
        BlendState blendState; 

        /// <summary>
        /// Constructor for cloud sprite, generates random position
        /// </summary>
        public CloudSprite()
        {
            position = HelperMethods.RandomVectGenerator();
            random = new Random();
            //blendstate for transparency
            blendState = BlendState.AlphaBlend;
            setRotation();
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
        /// <param name="stop">bool param to indicate if game has stopped</param>
        public void Update(bool stop)
        {
            if (!stop)
            {
                position += HelperMethods.RandomYVelGenerator(1, 3);//2,5
                if (position.Y > Constants.GAME_HEIGHT)
                {
                    position = HelperMethods.RandomVectGenerator();
                    setRotation();
                }
            }
            else
            {
                position += new Vector2(0, 0);
            }
        }
        /// <summary>
        /// Draw sprite from atlas
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin(blendState: blendState);
           
            spriteBatch.Draw(atlas, position, new Rectangle(80, 32, 16, 16), Color.White*.7f, rotation, new Vector2(8, 8), 7, SpriteEffects.None, 0);
            //spriteBatch.End();
        }

        private void setRotation()
        {
            rotation = (float) random.NextDouble();
        }
    }
}
