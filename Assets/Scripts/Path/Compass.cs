using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance
{
    public class Compass : MonoBehaviour
    {
        [SerializeField]
        private float _strength = 10;

        // Start is called before the first frame update
        void Start()
        {

        }

        //private void Update()
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawLine(transform.position, transform.forward * _strength);
        //}

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, 
                new Vector3(transform.position.x, transform.position.y, transform.position.z + _strength));
        }
    }
}