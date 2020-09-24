using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Scenes;

namespace TestPerformance
{
    public class SubSceneReferences : MonoBehaviour
    {
        public static SubSceneReferences Instance { get; private set; }

        public SubScene map1;

        private void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
