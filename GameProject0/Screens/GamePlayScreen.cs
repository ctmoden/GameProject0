using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
/// <summary>
/// TO-DO: Pass graphics and Content
/// </summary>
namespace GameProject0.Screens
{
    public class GamePlayScreen
    {
        private SpriteBatch spriteBatch;
        private GameController controller;
        private KeyboardState keyboardState;
        private SpriteFont bangers;
        private ChopperSprite chopper;
        private CoinSprite[] coins;
        private MissileSprite[] missiles;
        private CloudSprite[] clouds;
        private Texture2D ball;
        private Texture2D explosion;
        private Texture2D rec;
        private static int coinCount;
        private static int hitCount;
        private string playTime = "";
        /// <summary>
        /// Constructor for game screen, sets parent game controller
        /// </summary>
        /// <param name="controller"></param>
        public GamePlayScreen(GameController controller)
        {
            this.controller = controller;
            coinCount = 0;//FIXME may want to reinitialize values after game is complete
            hitCount = 0;
            
        }
        public void Initialize()
        {
            chopper = new ChopperSprite();
            //spriteBatch = new SpriteBatch(controller.GraphicsDevice);//FIXME is this right?
            coins = new CoinSprite[]
            {
                new CoinSprite(),
                new CoinSprite(),
                new CoinSprite(),
                new CoinSprite(),
                new CoinSprite()
            };
            missiles = new MissileSprite[]
            {
                new MissileSprite(),
                new MissileSprite(),
                new MissileSprite(),
                new MissileSprite(),
                new MissileSprite(),
                new MissileSprite()
            };
            clouds = new CloudSprite[]
            {
                new CloudSprite(),
                new CloudSprite(),
                new CloudSprite(),
                new CloudSprite(),
                new CloudSprite(),
                new CloudSprite()
            };
        }
        public void LoadContent()
        {
            chopper.LoadContent(controller.Content);//FIXME is this right?
            spriteBatch = new SpriteBatch(controller.GraphicsDevice);//FIXME is this right?
            bangers = controller.Content.Load<SpriteFont>("bangers");
            foreach (var coin in coins) coin.LoadContent(controller.Content);
            foreach (var missile in missiles) missile.LoadContent(controller.Content);
            chopper.LoadContent(controller.Content);
            foreach (var cloud in clouds) cloud.LoadContent(controller.Content);
            bangers = controller.Content.Load<SpriteFont>("bangers");
            ball = controller.Content.Load<Texture2D>("ball");
            rec = controller.Content.Load<Texture2D>("Water32Frames8x4");
            explosion = controller.Content.Load<Texture2D>("explosion0");
        }

        public void Update(GameTime gameTime, out bool switchScreen)
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape) || keyboardState.IsKeyDown(Keys.Q))
            {
                switchScreen = true;
                restartGame();
                return;
            }
            else
            {
                switchScreen = false;
            }
            chopper.Update();
            foreach (var coin in coins)
            {
                if (coin.Bounds.CollidesWith(chopper.Bounds))
                {
                    coin.Collided = true;
                    coinCount++;
                }
                coin.Update(chopper.Hit);
            }
            foreach (var missile in missiles)
            {
                if (missile.Bounds.CollidesWith(chopper.Bounds))
                {
                    hitCount++;
                    chopper.Hit = true;
                }
                missile.Update(chopper.Hit);
            }
            foreach (var cloud in clouds) cloud.Update(chopper.Hit);
        }
        /// <summary>
        /// Draws gameplay
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime)
        {
            controller.GraphicsDevice.Clear(Color.CornflowerBlue);//this will draw, but string will not
            spriteBatch.Begin();
            foreach (var coin in coins)
            {
            #region coin bounding region debugging
            /*
            var rect = new Rectangle((int)(coin.Bounds.Center.X - coin.Bounds.Radius),
                (int)(coin.Bounds.Center.Y - coin.Bounds.Radius),
                (int)(2*coin.Bounds.Radius), (int)(2*coin.Bounds.Radius));

            spriteBatch.Draw(ball, rect, Color.White);
            */
            #endregion coin bounding region debugging
                coin.Draw(gameTime, spriteBatch);                
            }
            foreach (var missile in missiles) missile.Draw(gameTime, spriteBatch);
            foreach (var cloud in clouds) cloud.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(bangers, $"Coins Collected: {coinCount}", new Vector2(10, 10), Color.Gold, 0f, new Vector2(), .5f, SpriteEffects.None, 0);
            //if chopper is hit, game time is hidden.  If not, make text shows gametime
            switch (chopper.Hit)
            {
                case false:
                    spriteBatch.DrawString(bangers, $"Game Time: {gameTime.TotalGameTime.TotalSeconds}", new Vector2(30, 30), Color.Gold, 0f, new Vector2(), .5f, SpriteEffects.None, 0);
                    break;
                case true:
                    spriteBatch.DrawString(bangers, $"Game Time: {gameTime.TotalGameTime.TotalSeconds}", new Vector2(30, 30), Color.Transparent, 0f, new Vector2(), .5f, SpriteEffects.None, 0);
                    break;
            }
            chopper.Draw(gameTime, spriteBatch, false);
            if (chopper.Hit)
            {
                if (playTime.Length == 0) playTime = Math.Round(gameTime.TotalGameTime.TotalSeconds, 2).ToString();
                spriteBatch.Draw(explosion, new Vector2(chopper.Position.X - 64, chopper.Position.Y - 64), new Rectangle(0, 0, 128, 128), Color.White);
                //spriteBatch.Draw(explosion, new Vector2(chopper.Position.X, chopper.Position.Y),
                //rectE, Color.White, 0f, new Vector2(64, 64), 10f, SpriteEffects.None, 0);
                spriteBatch.DrawString(bangers, $"You got shot down!!  Press ecs to restart game", new Vector2(200, 200), Color.DarkRed, 0f, new Vector2(), .5f, SpriteEffects.None, 0);
                spriteBatch.DrawString(bangers, $"Survived: {playTime} seconds!", new Vector2(30, 30), Color.Gold, 0f, new Vector2(), .5f, SpriteEffects.None, 0);
            }
            #region chopper bounding region debugging
            /*
            var rectC = new Rectangle((int)chopper.Bounds.X,(int)chopper.Bounds.Y,
                    (int)chopper.Bounds.Height, (int)chopper.Bounds.Width);
            spriteBatch.Draw(rec, rectC, Color.White);*/
            #endregion chopper bounding region debugging
            //spriteBatch.DrawString(bangers, "Test Screen", new Vector2(250, 250), Color.Black);           
            spriteBatch.End();
        }

        private void restartGame()
        {

        }
    }
}
