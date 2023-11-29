using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgos.Sorting.Algorithms
{
    internal class Quicksort : SortingAlgorithm
    {
        int low;
        int high;
        float pivot;
        int newPivotIndex;
        int currentIndex;
        bool partitioned;
        Quicksort lower;
        Quicksort upper;
        bool createdNewSorts = false;

        public override void Reset()
        {
            base.Reset();
            pivot = values[high];
            newPivotIndex = low - 1;
            currentIndex = low;
            partitioned = false;
            createdNewSorts = false;
        }

        public Quicksort(int length, int low, int high) : base(length)
        {
            name = "Quick Sort";
            pivot = values[high];
            newPivotIndex = low - 1;
            currentIndex = low;
            this.low = low;
            this.high = high;
            partitioned = false;
        }
        public Quicksort(int length, int seed) : base(length, seed)
        {
            name = "Quick Sort";
            low = 0;
            high = length - 1;
            pivot = values[high];
            newPivotIndex = low - 1;
            currentIndex = low;
            this.low = 0;
            this.high = length - 1;
            partitioned = false;
        }
        public Quicksort(int length, int low, int high, int seed) : base(length, seed)
        {
            name = "Quick Sort";
            pivot = values[high];
            newPivotIndex = low - 1;
            currentIndex = low;
            this.low = low;
            this.high = high;
            partitioned = false;
        }

        public Quicksort(int length, int low, int high, List<float> arr) : base(length)
        {
            name = "Quick Sort";
            values = arr;
            if (high < low)
            {
                isSorted = true;
            }
            else
            {
                pivot = values[high];
            }
            newPivotIndex = low - 1;
            currentIndex = low;
            this.low = low;
            this.high = high;
            partitioned = false;
        }

        public override void Tick()
        {
            if (isSorted)
            {
                return;
            }
            base.Tick();
            if (partitioned)
            {
                if (!createdNewSorts)
                {
                    lower = new Quicksort(values.Count, low, newPivotIndex, values);
                    upper = new Quicksort(values.Count, newPivotIndex + 2, high, values);
                    createdNewSorts = true;
                }
                /*if (!lower.isSorted)
                {
                    lower.Tick();
                    indicesToHighlight.AddRange(lower.indicesToHighlight);
                }
                else
                {
                    upper.Tick();
                    indicesToHighlight.AddRange(upper.indicesToHighlight);
                }*/
                upper.Tick();
                lower.Tick();
                indicesToHighlight.AddRange(upper.indicesToHighlight);
                indicesToHighlight.AddRange(lower.indicesToHighlight);

                isSorted = lower.isSorted && upper.isSorted;
                if (isSorted)
                {
                    arrayAccesses += lower.arrayAccesses + upper.arrayAccesses;
                    swaps += lower.swaps + upper.swaps;
                    comparisonCount += lower.comparisonCount + upper.comparisonCount;
                }
                return;
            }

            if (currentIndex >= high)
            {
                Swap(high, newPivotIndex + 1);
                indicesToHighlight.Add(high);
                indicesToHighlight.Add(newPivotIndex + 1);
                partitioned = true;
                return;
            }

            comparisonCount++;
            arrayAccesses++;
            if (values[currentIndex] < pivot)
            {
                newPivotIndex++;
                Swap(currentIndex, newPivotIndex);
                indicesToHighlight.Add(currentIndex);
                indicesToHighlight.Add(newPivotIndex);
            }

            currentIndex++;
        }
    }
}
