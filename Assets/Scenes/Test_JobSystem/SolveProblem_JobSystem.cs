using UnityEngine;
using System.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Burst;

namespace TestPerformance
{
    /// <summary>
    /// We solve problem with the JobSystem.
    /// </summary>
    public class SolveProblem_JobSystem : MonoBehaviour
    {
        public bool enableCollection;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            float startTime = Time.realtimeSinceStartup; // Get the time this ToughTask begin.
            if (enableCollection)
            {
                //NativeArray<JobHandle> listJobHandle = new NativeArray<JobHandle>(, Allocator.Temp);
                NativeList<JobHandle> listJobHandle = new NativeList<JobHandle>(Allocator.Temp);
                for (int i = 0; i < 10; i++)    // => 10 job to handle 10 work
                {
                    var jobHandle = ReallyToughTaskJob();   // Declare the JobHandle here.
                    listJobHandle.Add(jobHandle);
                }

                // wait for the job all complete
                JobHandle.CompleteAll(listJobHandle);   // tell JobHandle to complete all the jobs.
                listJobHandle.Dispose();    // must have to Dispose to return to main thread. => this frees the memory allocated
            }
            else
            {
                // 1 job handle 10 work.
                DoHeavyWorkEachJob();   // => PROBLEM INSIDE.
            }
            Debug.Log($"{(Time.realtimeSinceStartup - startTime) * 1000f} ms"); // Get the time ToughTask is done.
        }

        private void DoHeavyWorkEachJob()
        {
            for (int i = 0; i < 10; i++)    // => PROBLEM IN HERE: these jobs (10 jobs) is running on the single thread.
            {
                var jobHandle = ReallyToughTaskJob();   // Declare the JobHandle here.
                jobHandle.Complete();   // Wait the job till it complete
            }
        }

        private JobHandle ReallyToughTaskJob()
        {
            var job = new ReallyToughTask();
            return job.Schedule();  // schedule the job. => this code allow job runs. When job run, it cannot be stopped.
        }
    }

    [BurstCompile]  // using this allow to speed up the job.
    /// <summary>
    /// Define a struct with interface IJob
    /// </summary>
    public struct ReallyToughTask : IJob
    {
        //public float someValue; // In case we need some value;

        // Implement the function from IJob.
        public void Execute()
        {
            // Represent a tough task such as PathFinding or serious complex calculation.
            // Same as the the calculation in BaseProblem_HeavyCalculation
            float value = 0f;
            for (int i = 0; i < 50000; i++)
            {
                value = math.exp10(math.sqrt(value));
            }
        }
    }
}