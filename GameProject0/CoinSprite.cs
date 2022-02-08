using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
//using CollisionExample.Collisions;//using new namespace to access struct

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
        /// <summary>
        /// FIXME debug var for tracking coin pos
        /// </summary>
        public Vector2 Position => position;

        //private BoundingRectangle bounds;

        //private BoundingCircle bounds;

        public bool Collected { get; set; } = false;
        /// <summary>
        /// lamda syntax for a getter
        /// </summary>
        //public BoundingCircle Bounds => bounds;

        /// <summary>
        /// lamda syntax for a getter
        /// </summary>
        //public BoundingRectangle Bounds => bounds;
        /// <summary>
        /// Creates a new coin sprite
        /// </summary>
        /// <param name="position">The position of the sprite in the game</param>
        public CoinSprite(Vector2 position)
        {
            this.position = position;
            //recenters bounding circle backwards by 8.  Moves center down
            //not that expensive when in constructor, if they were moving like the ghost then update in drawing method
            //need to be -8 to shift backwards 
            //this.bounds = new BoundingCircle(position - new Vector2(-8, -8), 8);
            //this.bounds = new BoundingRectangle(position, 16,16);
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
            if (Collected) return;
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
        public void Update(GameTime gameTime, int yPos)
        {
            //FIXME change y component to be unique random num
            position += new Vector2(0, 2);
            if(yPos = )
        }
    }
}
