using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SortingAlgos.UI.UIElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgos.Sorting
{
    internal class SortingAlgorithm
    {
        public List<float> values
        {
            get;
            set;
        }
        public bool isSorted { get; set; }
        int length { get; set; }

        public List<int> indicesToHighlight;
        public int arrayAccesses = 0;
        public int comparisonCount = 0;
        public int ticks;
        public int swaps;
        public string name;
        Random random { get; set; }

        public virtual void Tick()
        {
            // This should run every tick for every algorithm
            ticks++;
            indicesToHighlight.Clear();
        }

        public void Initialise()
        { 
            // Adding all values to the list
            for (int i = 0; i < length; i++)
            {
                // Values that follow a sin curve
                //values.Add(500*((float)Math.Sin(3.14f * i)+1)/2);

                // Values that follow a linear ascent
                values.Add(i);
            }
            // Shuffle the array
            for (int i = 0; i > 2 * length; i++)
            {
                Swap(random.Next(length), random.Next(length));
                Swap(random.Next(length), random.Next(length));
            }
            swaps = 0;
        }

        public SortingAlgorithm(int length)
        {
            // Set up an algorithm with a random seed
            this.length = length;
            random = new Random();
            values = new List<float>();
            indicesToHighlight = new List<int>();
            Initialise();
        }
        public SortingAlgorithm(int length, int seed)
        {
            // Set up an algorithm with a given seed
            this.length = length;
            random = new Random(seed);
            values = new List<float>();
            indicesToHighlight = new List<int>();
            Initialise();
        }

        public void Swap(int i, int j)
        {
            // Swap two values in the values list
            swaps++;
            float temp = values[i];
            values[i] = values[j];
            values[j] = temp;
        }

        public virtual void Reset()
        {
            // Resets the sorting algorithm with new values
            isSorted = false;
            values.Clear();
            // Adding values
            for (int i = 0; i < length; i++)
            {
                values.Add(i);
            }
            // Shuffling
            for (int i = 0; i < 2 * length; i++)
            {
                Swap(random.Next(length), random.Next(length));
                Swap(random.Next(length), random.Next(length));
            }
            // Resesting stats
            arrayAccesses = 0;
            ticks = 0;
            comparisonCount = 0;
            swaps = 0;
        }

        public virtual void Draw(SpriteBatch spriteBatch, Game1 game, Window window)
        {
            List<int> ints = indicesToHighlight;
            // Calculate the scaling factor for all elements to keep them within the bounds of the window
            float scalar = (float)window.windowData.Height / values.Count;
            // Calculate the width of elements
            int width = (window.windowData.Width - 1) / values.Count;

            // Iterate over each element
            for (int i = 0; i < values.Count; i++)
            {
                // Get position and height for each element
                int height = (int)(values[i] * scalar);
                int xpos = width * i + window.windowData.X;
                int ypos = window.windowData.Y + window.windowData.Height - height;
                // Draw each element
                spriteBatch.Draw(game.pixelSquare, new Rectangle(xpos, ypos, width, height), ints.Contains(i) ? Color.White : new Color(game.HSVtoRGB(360 * values[i] / values.Count, 0.9f, 1f)));
            }

            // Draw statistics to the screen
            spriteBatch.DrawString(window.font, name, new Vector2(window.windowData.X, window.windowData.Y), Color.White);
            spriteBatch.DrawString(window.font, "Ticks " + ticks, new Vector2(window.windowData.X, window.windowData.Y + 30), Color.White);
            spriteBatch.DrawString(window.font, "Swaps " + swaps, new Vector2(window.windowData.X, window.windowData.Y + 60), Color.White);
            spriteBatch.DrawString(window.font, "Comparisons " + comparisonCount, new Vector2(window.windowData.X, window.windowData.Y + 90), Color.White);
            spriteBatch.DrawString(window.font, "Array Accesses " + arrayAccesses, new Vector2(window.windowData.X, window.windowData.Y + 120), Color.White);
        }
    }
}
