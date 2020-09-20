using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestPerformance
{
    /// <summary>
    /// This show how the different GC run for struct and class.
    /// </summary>
    public class TestGC : MonoBehaviour
    {
        internal class Another_Class_1
        {
            public string myName;
            public int myId;

            public Another_Class_1(string name, int id)
            {
                myName = name;
                myId = id;
            }

            public void Print() => print($"name: {myName}, id: {myId}");
            public void IncreaseMyID() => myId++;
        }

        internal struct Another_Struct_1
        {
            public string myName;
            public int myId;

            public Another_Struct_1(string name, int id)
            {
                myName = name;
                myId = id;
            }

            public void Print() => print($"name: {myName}, id: {myId}");
            public void IncreaseMyID() => myId++;
        }

        private Another_Struct_1[] arrStructs = new Another_Struct_1[5]
        {
            new Another_Struct_1("A", 0),
            new Another_Struct_1("B", 1),
            new Another_Struct_1("C", 2),
            new Another_Struct_1("D", 3),
            new Another_Struct_1("E", 4),
        };

        private Another_Class_1[] arrClasses = new Another_Class_1[5]
      {
            new Another_Class_1("A", 0),
            new Another_Class_1("B", 1),
            new Another_Class_1("C", 2),
            new Another_Class_1("D", 3),
            new Another_Class_1("E", 4),
      };

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // --- STRUCT ---
            // NO GC
            //for (int i = 0; i < arrStructs.Length; i++)
            //{
            //    arrStructs[i].IncreaseMyID();
            //}

            for (int i = 0; i < 1000; i++)
            {
                var newStruct = new Another_Struct_1("A", i);
            }

            // ---

            // --- CLASS ---
            // NO GC
            //for (int i = 0; i < arrClasses.Length; i++)
            //{
            //    arrClasses[i].IncreaseMyID();
            //}

            //for (int i = 0; i < 1000; i++)
            //{
            //    var newClass = new Another_Class_1("A", i);
            //}
            // ---
        }
    }
}