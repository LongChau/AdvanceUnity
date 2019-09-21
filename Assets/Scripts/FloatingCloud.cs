using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance
{
    public class FloatingCloud : MonoBehaviour
    {
        [SerializeField]
        private float _rotateSpeed;
        [SerializeField]
        private float _offset = 1;

        [SerializeField]
        private Material _cloudBox;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            _rotateSpeed += _offset;
            _cloudBox.SetFloat("_Rotation", _rotateSpeed);
            if (_rotateSpeed >= 360)
                _rotateSpeed = 0;
        }
    }
}