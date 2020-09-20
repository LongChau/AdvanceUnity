using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

namespace TestPerformance
{
    /// <summary>
    /// The base problem we often face. This class has problem in performance when 
    /// calulate heavy process.
    /// </summary>
    public class BaseProblem_HeavyCalculation : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            float startTime = Time.realtimeSinceStartup;    // Get the time this ToughTask begin.
            for (int i = 0; i < 10; i++)
            {
                ReallyToughTask();
            }
            Debug.Log($"{(Time.realtimeSinceStartup - startTime) * 1000f} ms"); // Get the time ToughTask is done.
        }

        private void ReallyToughTask()
        {
            // Represent a tough task such as PathFinding or serious complex calculation.
            float value = 0f;
            for (int i = 0; i < 50000; i++)
            {
                value = math.exp10(math.sqrt(value));
            }
        }
    }
}