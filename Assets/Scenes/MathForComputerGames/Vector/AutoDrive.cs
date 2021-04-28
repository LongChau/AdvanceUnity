﻿using System.Collections;
using System.Collections.Generic;
using UnityAdvance.Location;
using UnityEngine;

namespace UnityAdvance.SectionVector
{
    public class AutoDrive : MonoBehaviour
    {
        //public Vector2 dir = new Vector2(0.1f, 0.1f);
        public float speed;
        public float snap_speed;
        /// <summary>
        /// Use to stop the tank when it close to the tank.
        /// </summary>
        float stoppingDistance = 0.1f;
        Vector3 direction;

        Vector2 Up = new Vector2(0f, 1f);
        Vector2 Left = new Vector2(-1f, 0f);

        public Transform feul;

        // Start is called before the first frame update
        void Start()
        {
            direction = feul.position - transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            AutoMoveToFuel();
            //MoveWithSpeed();
            //SnappingEffect();
            //RandomSnappingEffect();
        }

        private void AutoMoveToFuel()
        {
            //var direction = ObjectManager.Fuel.transform.position - transform.position;
            //this.transform.position += direction;   // Jump directly to the feul position. Because we add the entire magtitude.
            //this.transform.position += direction * speed;   // Move slowly to the position along the vector. But the tank will move fast and then slowdown!.
            // because we have the different direction magnitude every update. 

            // If we want to move at constant speed. We have to re-calculate.
            // Let's have a stopping distance.
            if (Vector2.Distance(transform.position, feul.position) > stoppingDistance)
                this.transform.position += direction * speed;
            // The code above make the tank in same speed.
            // But problem is 2 tanks can move to the position at the same time.


        }

        private void MoveWithSpeed()
        {
            Vector3 position = this.transform.position;
            //position.x += dir.x;
            //position.y += dir.y;
            //this.transform.position = position;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                position.x += Up.x * speed;
                position.y += Up.y * speed;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                position.x += -Up.x * speed;
                position.y += -Up.y * speed;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                position.x += Left.x * speed;
                position.y += Left.y * speed;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                position.x += -Left.x * speed;
                position.y += -Left.y * speed;
            }
            this.transform.position = position;
        }

        private void SnappingEffect()
        {
            Vector3 position = this.transform.position;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                position.x += Up.x;
                position.y += Up.y;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                position.x += -Up.x;
                position.y += -Up.y;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                position.x += Left.x;
                position.y += Left.y;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                position.x += -Left.x;
                position.y += -Left.y;
            }
            // With speed < 1f the position is move when pressing key but closing to 0 when release.
            this.transform.position = position * speed;
            // Checking if nearly 0 we snap it to 0f.
            if (Vector2.Distance(transform.position, Vector2.zero) <= 0.01f)
                this.transform.position = Vector2.zero;
        }

        Vector3 rand_position;
        public int frameCount = 0;
        private void RandomSnappingEffect()
        {
            frameCount++;
            rand_position = this.transform.position;
            if (frameCount >= 100)
            {
                rand_position.x += Random.Range(-10, 10) * 2;
                rand_position.y += Random.Range(-10, 10) * 2;
                frameCount = 0;
            }
            // With snap_speed < 1f the position is move when pressing key but closing to 0 when release.
            this.transform.position = rand_position * snap_speed;
            // Checking if nearly 0 we snap it to 0f.
            if (Vector2.Distance(transform.position, Vector2.zero) <= 0.01f)
                this.transform.position = Vector2.zero;
        }

        [ContextMenu("CalculateVector")]
        private void CalculateVector()
        {
            var a = new Vector2(10, 5);
            var b = new Vector2(25, 20);
            var c = new Vector2(-15, 5);
            var d = new Vector2(10, -10);

            Distance(a, b);
            Distance(a, c);
            Distance(a, d);

            Distance(b, c);
            Distance(c, d);
            Distance(d, b);

            Distance(b, Vector2.zero);
            Distance(c, Vector2.zero);
            Distance(d, Vector2.zero);

            float Distance(Vector2 p1, Vector2 p2)
            {
                float result = Vector2.Distance(p1, p2);
                Debug.Log($"Distance: {result}");
                return result;
            }
        }
    }
}
