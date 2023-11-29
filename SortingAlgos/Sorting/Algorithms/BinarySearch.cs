using Microsoft.Xna.Framework.Graphics;
using SortingAlgos.UI.UIElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SortingAlgos.Sorting.Algorithms
{
    internal class BinarySearch : SortingAlgorithm
    {
        Random numToFindRand;
        int numToFind;
        int currentIndex;
        int lowIdx;
        int highIdx;

        public BinarySearch(int length) : base(length)
        {
            name = "Binary Search";
            numToFindRand = new Random();
            numToFind = numToFindRand.Next(length - 1);
            currentIndex = 0;
            lowIdx = 0;
            highIdx = length - 1;
            for (int i = 0; i < length; i++)
            {
                values[i] = i;
            }
        }

        public BinarySearch(int length, int seed) : base(length, seed)
        {
            name = "Binary Search";
            numToFindRand = new Random(seed);
            numToFind = numToFindRand.Next(length - 1);
            currentIndex = 0;
            lowIdx = 0;
            highIdx = length - 1;
            for (int i = 0; i < length; i++)
            {
                values[i] = i;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Game1 game, Window window)
        {
            // Drawing the necesary info for a search algorithm
            base.Draw(spriteBatch, game, window);

            spriteBatch.DrawString(window.font, "Searching for: ", new Vector2(window.windowData.X + 300, window.windowData.Y), Color.White);
            spriteBatch.Draw(game.pixelSquare, new Rectangle(window.windowData.X + 480, window.windowData.Y, 30, 30), new Color(game.HSVtoRGB(360 * numToFind / values.Count, 0.9f, 1f)));
            spriteBatch.DrawString(window.font, isSorted ? "Found at index: " + currentIndex : "Searching", new Vector2(window.windowData.X + 300, window.windowData.Y + 30), isSorted ? Color.Green : Color.Red);
        }

        public override void Tick()
        {
            if (isSorted)
                return;
            base.Tick();

            // If the current value is too small only search values above this index
            if (values[currentIndex] < numToFind)
            {
                lowIdx = currentIndex;
                currentIndex = currentIndex + (highIdx - lowIdx) / 2;
            }
            // If the current value is too big only search values below this index
            else if (values[currentIndex] > numToFind)
            {
                highIdx = currentIndex;
                currentIndex = currentIndex - (highIdx - lowIdx) / 2;
            }
            comparisonCount++;
            arrayAccesses++;
            indicesToHighlight.Add(currentIndex);
            // If the current value is the number to find the algorithm is done
            if (values[currentIndex] == numToFind)
            {
                isSorted = true;
                return;
            }
        }

        public override void Reset()
        {
            base.Reset();
            // Reseting all data 
            currentIndex = 0;
            lowIdx = 0;
            highIdx = values.Count;
            numToFind = numToFindRand.Next(values.Count);
            isSorted = false;
            // Making sure the list is sorted
            for (int i = 0; i < values.Count; i++)
            {
                values[i] = i;
            }
        }
    }
}
