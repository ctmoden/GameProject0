using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameProject0
{

    public enum Direction
    {
        Right = 0,
        Left = 1
    }
    /// <summary>
    /// Class representing animated helicopter sprite.
    /// The rotors of the helicopter will spin when animated
    /// </summary>
    public class ChopperSprite
    {
        private const int PIXEL_SPEED = 150;
        /// <summary>
        /// Texture for helicopter
        /// </summary>
        private Texture2D texture;
        /// <summary>
        /// direction timer.  Times how long chopper moves in certian direction.
        /// </summary>
        private double directionTimer;
        /// <summary>
        /// Controls timing of animation
        /// </summary>
        private double animationTimer;
        /// <summary>
        /// animation frame in image grid
        /// x component
        /// </summary>
        private short animationFrame = 0;//start at one to avoid dead bat frame...
        /// <summary>
        /// row of current animation 
        /// y component
        /// </summary>
        private short animationRow = 0;//like direction 

        public Direction Direction;
        /// <summary>
        /// Position of chopper
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// loads texture
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("8x8-helo-sprite");
        }
        /// <summary>
        /// Update chopper animation
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            directionTimer += gameTime.ElapsedGameTime.TotalSeconds;
            //switch direction every 1.5 seconds
            if(directionTimer > 1.5)
            {
                switch (Direction)
                {
                    case Direction.Left:
                        Direction = Direction.Right;
                        break;
                    case Direction.Right:
                        Direction = Direction.Left;
                        break;
                }
                //reset direction timer otherwise direction would change every frame
                directionTimer -= 1.5;
            }
            //move chopper in current direction it's set to (across x-plane)
            switch (Direction)
            {
                //FIXME: 150 = pixels/frame, pixels/second, or what?
                case Direction.Left:
                    Position += new Vector2(1, 0) * PIXEL_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Right:
                    Position += new Vector2(-1, 0) * PIXEL_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
            }
        }
        /// <summary>
        /// draws animated helicopter sprite
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //update timer based on elapsed time in game
            //elapsed time = elapsed time since last update
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (animationTimer > 0.2)
            {
                animationFrame++;
                //reached end of current row, reset to first pos in next row
                if(animationFrame > 7)
                {
                    animationFrame = 0;
                    animationRow++;
                    if (animationRow > 7) animationRow = 0;
                }
                //reset timer
                animationTimer -= .2;
                //rectangle updated between spriteBatch begin and end
                //rectangle is new "chunk" of helo image that represents a different part 
                //of the animation
                var sourceRectangle = new Rectangle(animationFrame * 8, animationRow * 8, 8, 8);
                //draw with upadted position and source rectangle
                spriteBatch.Draw(texture, Position, sourceRectangle, Color.White);
            }
        }
    }
}
