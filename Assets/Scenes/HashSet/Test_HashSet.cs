using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_HashSet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HashSet<int> evenNumbers = new HashSet<int>();
        HashSet<int> oddNumbers = new HashSet<int>();

        for (int i = 0; i < 5; i++)
        {
            // Populate numbers with just even numbers.
            evenNumbers.Add(i * 2);

            // Populate oddNumbers with just odd numbers.
            oddNumbers.Add((i * 2) + 1);
        }

        Debug.Log($"evenNumbers contains {evenNumbers.Count} elements: ");
        DisplaySet(evenNumbers);

        Debug.Log($"oddNumbers contains {oddNumbers.Count} elements: ");
        DisplaySet(oddNumbers);

        // Create a new HashSet populated with even numbers.
        HashSet<int> numbers = new HashSet<int>(evenNumbers);
        Debug.Log("\n numbers UnionWith oddNumbers...");
        numbers.UnionWith(oddNumbers);

        Debug.Log($"numbers contains {numbers.Count} elements: ");
        DisplaySet(numbers);

        void DisplaySet(HashSet<int> collection)
        {
            foreach (int i in collection)
            {
                Debug.Log($" {i}");
            }
        }

        /* This example produces output similar to the following:
        * evenNumbers contains 5 elements: { 0 2 4 6 8 }
        * oddNumbers contains 5 elements: { 1 3 5 7 9 }
        * numbers UnionWith oddNumbers...
        * numbers contains 10 elements: { 0 2 4 6 8 1 3 5 7 9 }
        */
    }
}
