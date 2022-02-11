using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;

namespace GameProject0
{
    /// <summary>
    /// Static class to provide methods that return random 2d vectors to support sprite positioning
    /// and velocity
    /// </summary>
    public static class HelperMethods
    {
        /// <summary>
        /// returns new random vector for positioning of sprites on game screen 
        /// </summary>
        /// <returns></returns>
        public static Vector2 RandomVectGenerator()
        {
            Random rand = new Random();
            return new Vector2((float)rand.NextDouble() * Constants.GAME_WIDTH, (float)rand.NextDouble() * Constants.GAME_WIDTH - Constants.GAME_WIDTH);

        }
        /// <summary>
        /// Generates a vector with a randomly generated y component between min and max
        /// for modulating the y component velocity of a sprite
        /// </summary>
        /// <param name="min">min for random number gen</param>
        /// <param name="max">max for random number gen</param>
        /// <returns></returns>
        public static Vector2 RandomYVelGenerator(int min, int max)
        {
            Random rand = new Random();
            //chooses random y velocity of missile
            int randVel = rand.Next(min, max);
            return new Vector2(0, randVel);
        }
    }
}
