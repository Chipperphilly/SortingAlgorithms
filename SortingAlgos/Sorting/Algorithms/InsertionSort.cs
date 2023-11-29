using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgos.Sorting.Algorithms
{
    internal class InsertionSort : SortingAlgorithm
    {
        int currentIndex;
        int insertionIndex;

        bool inserting;
        
        public InsertionSort(int length) : base(length) 
        {
            name = "Insertion Sort";
            currentIndex = 1;
            insertionIndex = 0;
            inserting = false;
        }
        public InsertionSort(int length, int seed) : base(length, seed)
        {
            name = "Insertion Sort";
            currentIndex = 1;
            insertionIndex = 0;
            inserting = false;
        }

        public override void Reset()
        {
            base.Reset();
            currentIndex = 1;
            insertionIndex = 0;
            inserting = false;
        }

        public override void Tick()
        {
            if (isSorted)
            {
                return;
            }
            base.Tick();
            if (currentIndex >= values.Count)
            {
                isSorted = true;
                return;
            }
            indicesToHighlight.Add(currentIndex);

            if (inserting)
            {
                indicesToHighlight.Add(insertionIndex);
                if (insertionIndex == 0)
                {
                    inserting = false;
                    currentIndex++;
                    return;
                }

                comparisonCount++;
                arrayAccesses += 2;
                if (values[insertionIndex] < values[insertionIndex - 1])
                {
                    Swap(insertionIndex, insertionIndex - 1);
                    insertionIndex--;
                    return;
                }
                else
                {
                    inserting = false;
                    currentIndex++;
                    return;
                }
            }
            comparisonCount++;
            arrayAccesses += 2;
            if (values[currentIndex] < values[currentIndex - 1])
            {
                Swap(currentIndex, currentIndex - 1);
                insertionIndex = currentIndex - 1;
                inserting = true;
            }
            else
            {
                currentIndex++;
            }
        }
    }
}
