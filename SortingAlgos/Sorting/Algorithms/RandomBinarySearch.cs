using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SortingAlgos.UI.UIElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgos.Sorting.Algorithms
{
    internal class RandomBinarySearch : SortingAlgorithm
    {
        Random nextIdxRand;
        Random numToFindRand;
        int numToFind;
        int currentIndex;
        int lowIdx;
        int highIdx;

        public RandomBinarySearch(int length) : base(length)
        {
            name = "Random Binary Search";
            numToFindRand = new Random();
            nextIdxRand = new Random();
            numToFind = numToFindRand.Next(length - 1);
            currentIndex = 0;
            lowIdx = 0;
            highIdx = length - 1;
            for (int i = 0; i < length; i++)
            {
                values[i] = i;
            }
        }

        public RandomBinarySearch(int length, int seed) : base(length, seed)
        {
            name = "Random Binary Search";
            numToFindRand = new Random(seed);
            nextIdxRand = new Random(seed);
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
            base.Draw(spriteBatch, game, window);

            spriteBatch.DrawString(window.font, "Searching for: ", new Vector2(window.windowData.X + 300, window.windowData.Y), Color.White);
            spriteBatch.Draw(game.pixelSquare, new Rectangle(window.windowData.X + 480, window.windowData.Y, 30, 30), new Color(game.HSVtoRGB(360 * numToFind / values.Count, 0.9f, 1f)));
            spriteBatch.DrawString(window.font, isSorted ? "Found at index: " + currentIndex : "Searching", new Vector2(window.windowData.X + 300, window.windowData.Y + 30), isSorted ? Color.Green : Color.Red);
            spriteBatch.DrawString(window.font, "Current index: " + currentIndex, new Vector2(window.windowData.X + 300, window.windowData.Y + 60), isSorted ? Color.Green : Color.Red);
        }

        public override void Tick()
        {
            if (isSorted)
                return;
            base.Tick();

            comparisonCount++;
            arrayAccesses++;
            if (values[currentIndex] < numToFind)
            {
                lowIdx = currentIndex;
                currentIndex = currentIndex + nextIdxRand.Next(1, (highIdx - lowIdx) / 2);
            }
            else if (values[currentIndex] > numToFind)
            {
                comparisonCount++;
                arrayAccesses++;
                highIdx = currentIndex;
                currentIndex = currentIndex - nextIdxRand.Next(1, (highIdx - lowIdx) / 2);
            }
            comparisonCount++;
            arrayAccesses++;
            indicesToHighlight.Add(currentIndex);
            if (values[currentIndex] == numToFind)
            {
                isSorted = true;
                return;
            }
        }

        public override void Reset()
        {
            base.Reset();
            currentIndex = 0;
            lowIdx = 0;
            highIdx = values.Count;
            numToFind = numToFindRand.Next(values.Count);
            isSorted = false;
            for (int i = 0; i < values.Count; i++)
            {
                values[i] = i;
            }
        }
    }
}
