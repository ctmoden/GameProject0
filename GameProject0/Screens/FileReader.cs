using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace GameProject0
{
    public static class FileReader
    {
        private static string data;
        private static string fileName;
        private static string[] line;
        private static int highScore;

        /// <summary>
        /// Sets filename to be read from
        /// </summary>
        /// <param name="name"></param>
        public static void SetFileName(string name)
        {
            fileName = name;
        }
        /// <summary>
        /// Reads in high score: max number of cois collected in a single run
        /// </summary>
        /// <returns></returns>
        public static int GetHighScore()
        {
            return highScore;
        }
        
        /// <summary>
        /// Reads in high score data
        /// </summary>
        public static void ReadFile(ContentManager content)
        {
            data = File.ReadAllText(Path.Join(content.RootDirectory, fileName));
            highScore = Int32.Parse(data);
        }
        /// <summary>
        /// Writes new high score data to the file
        /// </summary>
        /// <param name="newHighScore"></param>
        /// <param name="newGameTime"></param>
        public static void WriteHighScoreInfo(ContentManager content, int newCoinCount)
        {
            string writeText = newCoinCount.ToString();
            File.WriteAllText(Path.Join(content.RootDirectory, fileName), writeText);
        }
    }
}
