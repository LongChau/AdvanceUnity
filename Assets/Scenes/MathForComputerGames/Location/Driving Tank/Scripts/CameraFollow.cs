using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance.Location
{
    public class CameraFollow : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            this.transform.position = new Vector3(Drive.TankPosition.x, Drive.TankPosition.y, transform.position.z);
        }
    }
}
