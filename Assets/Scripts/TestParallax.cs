using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance
{
    public class TestParallax : MonoBehaviour
    {
        [SerializeField]
        private float _runSpeed;
        [SerializeField]
        private float _offset = 1;

        [SerializeField]
        private Material _parallaxMat;

        private void OnValidate()
        {
            //_parallaxMat = GetComponent<Material>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            _offset = Time.time * _runSpeed;
            _parallaxMat.mainTextureOffset = new Vector2(_offset, 0);
        }
    }
}