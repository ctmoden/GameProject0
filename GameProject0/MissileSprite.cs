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
    public class MissileSprite 
    {
        private Texture2D texture;
        private Vector2 position;
        /// <summary>
        /// length is 32 pixels, rad = 16 pixels
        /// </summary>
        private BoundingRectangle bounds;
        public BoundingRectangle Bounds => bounds;
        /// <summary>
        /// Constructor for missile
        /// </summary>
        /// <param name="position"></param>
        public MissileSprite()
        {
            position = HelperMethods.RandomVectGenerator();
            bounds = new BoundingRectangle(position.X, position.Y,32,14);
        }
        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("missile00");
        }
        /// <summary>
        /// QUESTIONS: how to change timing of missiles firing at chopper?  Game time snapshots + random offset?
        ///FIXME add param to stop game?
        /// </summary>
        /// <param name="gameTime"></param>
        
        public void Update(bool stop)//needs to take in position on update
        {
            if (!stop)
            {
                //position += HelperMethods.RandomYVelGenerator(7, 8);
                position += new Vector2(0, 1);
                if (position.Y > Constants.GAME_HEIGHT)
                {
                    position = HelperMethods.RandomVectGenerator();
                    //position = new Vector2(100f, 100f);
                }
                bounds.X = position.X - 7;
                bounds.Y = position.Y - 19;
            }
            else
            {
                position += new Vector2(0, 0);
            }
        }
        /// <summary>
        /// Draws sprite using supplied sprite batch
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var source = new Rectangle(0, 0, 32, 32);
            spriteBatch.Draw(texture, position, source, Color.White, 0, new Vector2(16, 16), 1f, SpriteEffects.FlipVertically, 0);
        }
    }
}
