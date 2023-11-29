using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace SortingAlgos.Sorting.Algorithms
{
    internal class BubbleSort : SortingAlgorithm
    {
        int currentIndex;
        bool potentiallySorted = true;

        public BubbleSort(int length) : base(length)
        {
            name = "Bubble Sort";
            currentIndex = 0;
        }
        public BubbleSort(int length, int seed) : base(length, seed)
        {
            name = "Bubble Sort";
            currentIndex = 0;
        }

        public override void Reset()
        {
            base.Reset();
            currentIndex = 0;
            potentiallySorted = true;
        }

        public override void Tick()
        {
            if (isSorted)
            {
                return;
            }
            base.Tick();
            indicesToHighlight.Add(currentIndex);
            indicesToHighlight.Add(currentIndex + 1);
            if (currentIndex == values.Count - 1)
            {
                if (potentiallySorted)
                {
                    isSorted = true;
                    return;
                }
                currentIndex = 0;
                potentiallySorted = true;
            }
            comparisonCount++;
            arrayAccesses += 2;
            if (values[currentIndex] > values[currentIndex + 1])
            {
                Swap(currentIndex + 1, currentIndex);
                potentiallySorted = false;
            }
            currentIndex++;
        }


    }
}
