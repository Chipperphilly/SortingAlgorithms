using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgos.Sorting.Algorithms
{
    internal class QuickerBubbleSort : SortingAlgorithm
    {
        int currentIndex;
        bool potentiallySorted = true;
        int sortedIndex;

        public override void Reset()
        {
            base.Reset();
            currentIndex = 0;
            sortedIndex = values.Count;
            potentiallySorted = true;
        }
        public QuickerBubbleSort(int length, int seed) : base(length, seed)
        {
            name = "Quicker Bubble Sort";
            currentIndex = 0;
            sortedIndex = values.Count;
        }

        public QuickerBubbleSort(int length) : base(length)
        {
            name = "Quicker Bubble Sort";
            currentIndex = 0;
            sortedIndex = values.Count;
        }

        public override void Tick()
        {
            // Don't continue sorting if already sorted
            if (isSorted)
            {
                indicesToHighlight.Clear();
                return;
            }
            base.Tick();

            // Adding indices to highlight
            indicesToHighlight.Add(currentIndex);
            indicesToHighlight.Add(currentIndex + 1);

            // Checking if the current index is the last index needing to be checked for this iteration
            if (currentIndex == values.Count - 1 || currentIndex == sortedIndex - 1)
            {
                // If no swaps have occured throughout the entire iteration then the array is sorted
                if (potentiallySorted)
                {
                    isSorted = true;
                    return;
                }

                // Changing variables needed to start the new iteration
                currentIndex = 0;
                sortedIndex--;
                potentiallySorted = true;
            }


            comparisonCount++;
            arrayAccesses += 2;
            // Swapping the two values being checked if the first is greater than the second
            if (values[currentIndex] > values[currentIndex + 1])
            {
                Swap(currentIndex + 1, currentIndex);
                // A swap has occured so the array isn't definetely sorted
                potentiallySorted = false;
            }
            currentIndex++;
        }
    }
}
