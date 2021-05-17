using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance.SectionVector
{
    public class AirPlaneController : MonoBehaviour
    {
        public float rotationSpeed = 1.0f;
        public float speed = 1.0f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            float rotationX, translateX, rotationY, translateY, rotationZ, translateZ;
            //translateX = Input.GetAxis("Horizontal") * speed;
            //translateY = Input.GetAxis("VerticalY") * speed;
            //translateZ = Input.GetAxis("Vertical") * speed;

            rotationX = Input.GetAxis("Vertical") * speed;
            rotationY = Input.GetAxis("Horizontal") * speed;
            rotationZ = Input.GetAxis("HorizontalZ") * speed;
            translateZ = Input.GetAxis("VerticalY") * speed;

            //transform.Translate(translateX, 0 , 0);
            //transform.Translate(0, translateY, 0);
            //transform.Translate(0, 0, translateZ);

            // Merge code together:
            //transform.position = new Vector3(transform.position.x + translateX,
            //                                transform.position.y + translateY,
            //                                transform.position.z + translateZ);

            transform.Rotate(rotationX, 0, 0);
            transform.Rotate(0, rotationY, 0);
            transform.Rotate(0, 0, rotationZ);
            //transform.Rotate(rotationX, rotationY, rotationZ);
            transform.Translate(0, 0, translateZ);
        }
    }
}
