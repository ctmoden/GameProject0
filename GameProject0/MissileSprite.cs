﻿using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GameProject0
{
    public class MissileSprite
    {
        private Texture2D texture;
        private Vector2 position;
        /// <summary>
        /// Constructor for missile
        /// </summary>
        /// <param name="position"></param>
        public MissileSprite(Vector2 position)
        {
            this.position = position;
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
        /// </summary>
        /// <param name="gameTime"></param>
        
        public void Update(GameTime gameTime)//needs to take in position on update
        {
            //FIXME change y component to be unique random num
            Random rand = new Random();
            //chooses random y velocity of missile
            int randVel = rand.Next(5, 10);
            position += new Vector2(0, randVel);
            if (position.Y > Constants.GAME_HEIGHT)
            {
                position = new Vector2((float)rand.NextDouble() * Constants.GAME_WIDTH, (float)rand.NextDouble() * Constants.GAME_HEIGHT - Constants.GAME_HEIGHT);

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