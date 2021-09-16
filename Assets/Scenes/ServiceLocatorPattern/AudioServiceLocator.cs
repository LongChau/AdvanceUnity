using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestUniRx
{
    /// <summary>
    /// THe locator that ties audios together.
    /// </summary>
    public class AudioServiceLocator : MonoBehaviour
    {
        private static IAudio _service;
        public static IAudio GetAudio() => _service;
        public static void Provide(IAudio service)
        {
            _service = service;
        }

        // Start is called before the first frame update
        void Start()
        {

        }
    }
}
