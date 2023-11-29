using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgos.Sorting.Algorithms
{
    internal class SelectionSort : SortingAlgorithm
    {
        int currentIndex;
        int sortedIndex;
        int minValIndex;

        public override void Reset()
        {
            base.Reset();
            currentIndex = 0;
            sortedIndex = -1;
            minValIndex = 0;
        }

        public SelectionSort(int length) : base(length)
        {
            name = "Selection Sort";
            currentIndex = 0;
            sortedIndex = -1;
            minValIndex = 0;
        }
        public SelectionSort(int length, int seed) : base(length, seed)
        {
            name = "Selection Sort";
            currentIndex = 0;
            sortedIndex = -1;
            minValIndex = 0;
        }

        public override void Tick()
        {
            if (isSorted)
                return;
            base.Tick();
            if (currentIndex > values.Count - 1)
            {
                Swap(sortedIndex + 1, minValIndex);
                
                sortedIndex++;
                minValIndex = sortedIndex + 1;
                currentIndex = sortedIndex + 1;
                if (sortedIndex >= values.Count - 1)
                {
                    isSorted = true;
                    return;
                }
                return;
            }
            indicesToHighlight.Add(sortedIndex + 1);
            indicesToHighlight.Add(currentIndex);

            comparisonCount++;
            arrayAccesses += 2;
            if (values[currentIndex] < values[minValIndex])
            {
                minValIndex = currentIndex;
            }
            currentIndex++;

        }



    }
}
