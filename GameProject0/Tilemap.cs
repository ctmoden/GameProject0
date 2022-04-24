using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameProject0
{
    /// <summary>
    /// Class definingn and rendering a tilemap.
    /// The code of this class is bui;t upon the Tilemap Exercise from class
    /// </summary>
    public class Tilemap
    {
        private int tileWidth, tileHeight, mapWidth, mapHeight;
        /// <summary>
        /// texture for tileset
        /// </summary>
        private Texture2D texture;

        private Vector2 position;

        private Rectangle[] tiles;
        /// <summary>
        /// map data for tilemap
        /// </summary>
        private int[] map;
        /// <summary>
        /// name of map file
        /// </summary>
        private string filename;

        private int _yOffset1;

        private int _yOffset2;

        private Random rand;

        private double transitionTimer;

        private int heightOffset;

        public Tilemap(string filename)
        {
            this.filename = filename;
            rand = new Random();
        }
        /// <summary>
        /// This method, structured from the class example, loads the content of the map data 
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            string data = File.ReadAllText(Path.Join(content.RootDirectory, filename));//PUT IN RELEASE FOLDER
            var lines = data.Split('\n');
            var tilesetFilename = lines[0].Trim();//extract filename
            texture = content.Load<Texture2D>(tilesetFilename);//load texture content from file name
            //second line is width and height (tile size)
            //second line = height and width of each tile
            var secondLine = lines[1].Split(',');
            tileWidth = int.Parse(secondLine[0]);
            tileHeight = int.Parse(secondLine[1]);
            int tilesetColumns = texture.Width / tileWidth;//tileset double the size of the tile
            int tilesetRows = texture.Height / tileHeight;
            tiles = new Rectangle[tilesetColumns * tilesetRows];
            //what bounds actually are, calculated
            for (int y = 0; y < tilesetColumns; y++)
            {
                for (int x = 0; x < tilesetRows; x++)
                {
                    int index = y * tilesetColumns + x;
                    tiles[index] = new Rectangle(
                        x * tileWidth, y * tileHeight,
                        tileWidth,
                        tileHeight
                        );//initializes all tiles to be right bounds
                }
            }
            //third line = map width
            //how many tiles high/wide the whole map is
            var thirdLine = lines[2].Split(',');
            mapWidth = int.Parse(thirdLine[0]);
            mapHeight = int.Parse(thirdLine[1]);
            loadMap();
        }
        /// <summary>
        /// scrolling tilemap, especially since starting at beginning of map data?  I could go backwards...
        /// tricks to loading the tilemap data?  Random nums and dont even need map data?
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //var translation = Matrix.CreateTranslation()
            //based off game time I could shift rows down by 
            //a height of one and draw another row to top or bottom
            //use tiled
            #region old
            for (int y = 0; y < mapHeight; y++)//for(int y = 0; y < mapHeight; y++) (int y = 0; y < mapHeight; y++)
            {
                for(int x = 0; x < mapWidth; x++)
                {
                    int i = y * mapWidth + x;
                    int index = map[i] - 1;//map actually starting at 1, but indexed starting at 0
                    if (index == -1) continue;//skip one increment through this particular loop
                    spriteBatch.Draw(texture, new Vector2(
                        x * tileWidth,//how many pixels over each block is
                        (y * -tileHeight) + heightOffset),
                        tiles[index], Color.White);
                }
            }
            #endregion old
            #region new
            /*for (int x = 0; x < mapWidth; x++)
            {
                int y = _yOffset2 - _yOffset1;
                int i = y * mapWidth + x;
                int index = map[i] - 1;//map actually starting at 1, but indexed starting at 0
                if (index == -1) continue;//skip one increment through this particular loop
                spriteBatch.Draw(texture, new Vector2(
                    x * tileWidth,//how many pixels over each block is
                    y * tileHeight
                    ),
                    tiles[index], Color.White);
            }*/
            #endregion new
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            
            //deal with timing here
            transitionTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(transitionTimer > 0.07)
            {
                heightOffset += 1;
                if (_yOffset2 < map.Length)
                {
                    _yOffset1++;
                    _yOffset2++;
                    
                }                
                transitionTimer -= 0.07;
            }
        }

        /// <summary>
        /// Manually loads map instead of reading from file
        /// </summary>
        private void loadMap()
        {
            map = new int[mapWidth * mapHeight];
            for (int i = 0; i < map.Length; i++)
            {
                int randInt = rand.Next(1, 5);
                if (randInt == 3) map[i] = 2;
                else map[i] = randInt;
            }
            heightOffset = mapHeight+Constants.GAME_HEIGHT;
        }
        private void initializeYOffsets()
        {
            _yOffset1 = 0;
            _yOffset2 = mapHeight;
        }

    }
}
