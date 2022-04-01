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

        private Rectangle[] tiles;
        /// <summary>
        /// map data for tilemap
        /// </summary>
        private int[] map;
        /// <summary>
        /// name of map file
        /// </summary>
        private string filename;

        private int _yOffset;

        private double transitionTimer;

        public Tilemap(string filename)
        {
            this.filename = filename;
        }
        /// <summary>
        /// This method, structured from the class example
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            string data = File.ReadAllText(Path.Join(content.RootDirectory, filename));
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
            _yOffset = mapHeight - 1;
            //create our map
            //each number in this long list represents the index of a tile in the tileset
            var fourthLine = lines[3].Split(',');
            map = new int[mapWidth * mapHeight];
            //iterate through across y then x to avoid cach misses
            //2d array => 1d
            //imagine it as a 2d array with all tileset targets arranged to mirror the map
            //except spread out across a 1d array
            //iterate up to the area of the map to load 2d into 1d
            for (int i = 0; i < mapWidth * mapHeight; i++)
            {
                map[i] = int.Parse(fourthLine[i]);
            }
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
            //monogame extended for loading tmx
            //load a GIANT ass tilemap (like 10000 blocks high)
            //every ~.25 seconds increment
            for (int y = _yOffset; y >= 0; y--)//for(int y = 0; y < _mapHeight; y++)
            {
                for (int x = mapWidth - 1; x >= 0; x--)//for(int x = 0; x < _mapWidth; x++)
                {
                    int i = y * mapWidth + x;
                    int index = map[i] - 1;//map actually starting at 1, but indexed starting at 0
                    if (index == -1) continue;//skip one increment through this particular loop
                    spriteBatch.Draw(texture, new Vector2(
                        x * tileWidth,//how many pixels over each block is
                        y * tileHeight
                        ),
                        tiles[index], Color.White);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //deal with timing here
            transitionTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(transitionTimer > 1.0)
            {
                _yOffset--;
                transitionTimer -= 1.0;
            }
        }

    }
}
