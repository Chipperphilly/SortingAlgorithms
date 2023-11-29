using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgos.Sorting.Algorithms
{
    internal class MergeSort : SortingAlgorithm
    {
        MergeSort upper;
        MergeSort lower;
        bool createdSorts = false;
        int startIdx;
        int length;
        int lowerIdx;
        int upperIdx;
        int currentIdx;

        bool merging = false;
        List<float> merger;

        bool combining = false;

        public override void Reset()
        {
            base.Reset();
            createdSorts = false;
            startIdx = 0;
            merging = false;
            combining = false;
        }

        public MergeSort(int length) : base(length)
        {
            name = "Merge Sort";
            this.length = length;
            startIdx = 0;
        }

        public MergeSort(int length, int startIndex) : base(length) 
        {
            name = "Merge Sort";
            this.length = length;
            startIdx = startIndex;
        }

        public MergeSort(int length, int startIndex, List<float> arr) : base(length)
        {
            name = "Merge Sort";
            values = arr;
            this.length = length;
            startIdx = startIndex;
        }

        public MergeSort(int length, int startIndex, int seed) : base(length, seed)
        {
            name = "Merge Sort";
            this.length = length;
            startIdx = startIndex;
        }

        public override void Tick()
        {
            if (isSorted)
            {
                return;
            }
            base.Tick();
            if (length == 1)
            {
                isSorted = true;
                return;
            }

            if (combining)
            {
                indicesToHighlight.Add(currentIdx);
                indicesToHighlight.Add(startIdx);
                if (currentIdx > startIdx + length - 1)
                {
                    isSorted = true;
                    indicesToHighlight.Clear();
                    return;
                }

                values[currentIdx] = merger[currentIdx - startIdx];
                currentIdx++;
                return;
            }

            if (merging)
            {
                // Should the value from the lower or upper segment be placed into the next spot
                if (upperIdx >= startIdx + length && lowerIdx >= startIdx + length / 2)
                {
                    merging = false;
                    combining = true;
                    currentIdx = startIdx;
                    return;
                }
                comparisonCount++;
                arrayAccesses += 2;
                if ((upperIdx >= startIdx + length && lowerIdx < startIdx + length/2) || (values[lowerIdx] < values[upperIdx] && lowerIdx < startIdx + length / 2))
                {
                    merger.Add(values[lowerIdx]);
                    lowerIdx++;
                }
                else if (upperIdx < startIdx + length)
                {
                    merger.Add(values[upperIdx]);
                    upperIdx++;
                }
                indicesToHighlight.Add(lowerIdx); 
                indicesToHighlight.Add(upperIdx);
                return;
            }

            if (!createdSorts)
            {
                createdSorts = true;
                upper = new MergeSort(length - length / 2, startIdx + length / 2, values);
                lower = new MergeSort(length / 2, startIdx, values);
            }

            if (lower.isSorted && upper.isSorted)
            {
                merging = true;
                lowerIdx = startIdx;
                upperIdx = startIdx + length / 2;
                merger = new List<float>();
                arrayAccesses = lower.arrayAccesses + upper.arrayAccesses;
                comparisonCount = lower.comparisonCount + upper.comparisonCount;
                return;
            }

            /*tickUpper = !tickUpper;
            if (!upper.isSorted)
            {
                upper.Tick();
                indicesToHighlight.AddRange(upper.indicesToHighlight);
            }
            else
            {
                lower.Tick();
                indicesToHighlight.AddRange(lower.indicesToHighlight);
            }*/
            upper.Tick();
            lower.Tick();
            indicesToHighlight.AddRange(lower.indicesToHighlight);
            indicesToHighlight.AddRange(upper.indicesToHighlight);
        }
    }
}
