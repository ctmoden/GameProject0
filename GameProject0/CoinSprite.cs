using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameProject0.Collisions;//using new namespace to access struct

namespace GameProject0
{

    public class CoinSprite
    {
        
        private const float ANIMATION_SPEED = 0.1f;

        private double animationTimer;

        private int animationFrame;

        private Vector2 position;

        private Texture2D texture;

        private const int PIXEL_SPEED = 150;
        

        //private BoundingRectangle bounds;

        private BoundingCircle bounds;

        public bool Collided { get; set; } = false;
        /// <summary>
        /// lamda syntax for a getter
        /// </summary>
        public BoundingCircle Bounds => bounds;
        /// <summary>
        /// Creates a new coin sprite
        /// </summary>
        /// <param name="position">The position of the sprite in the game</param>
        public CoinSprite()
        {
            position = HelperMethods.RandomVectGenerator();
            //recenters bounding circle backwards by 8.  Moves center down
            //not that expensive when in constructor, if they were moving like the ghost then update in drawing method
            //need to be -8 to shift backwards 
            bounds = new BoundingCircle(position - new Vector2(-8, -8), 8);
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("coins");
        }

        /// <summary>
        /// Draws the animated sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (animationTimer > ANIMATION_SPEED)
            {
                animationFrame++;
                if (animationFrame > 7) animationFrame = 0;
                animationTimer -= ANIMATION_SPEED;
            }
            //16 pixels in w and h
            var source = new Rectangle(animationFrame * 16, 0, 16, 16);
            //drawing with upper left hand corner, circle will be up and to the left each direction, soft origin backwards by 8
            spriteBatch.Draw(texture, position, source, Color.White);
        }
        /// <summary>
        /// Update velocity and whether it is touching the bottom of the screen
        /// Constants class for viewport dimensions?
        /// other param options: graphics device viewport info:
        ///     if the viewport is 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(bool stop)
        {
            if (!stop)
            {
                position += HelperMethods.RandomYVelGenerator(2, 4);
                bounds.Center = position;
                //FIXME had to add offsets to coins.  Why is center not aligned with coin?
                bounds.Center.X += 8;
                bounds.Center.Y += 9;
                if (position.Y > Constants.GAME_HEIGHT || Collided)
                {
                    position = HelperMethods.RandomVectGenerator();
                    bounds.Center = position;
                    Collided = false;
                }

            }
            else
            {
                position += new Vector2(0, 0);
            }
        }
    }
}
