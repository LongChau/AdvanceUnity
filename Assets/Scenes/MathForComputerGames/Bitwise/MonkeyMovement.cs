using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance.Bitwise
{
    public class MonkeyMovement : MonoBehaviour
    {
        public float speed = 2.5f;
        public SpriteRenderer render;
        public BitwiseExample.EAttributes attributes;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                render.flipX = true;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                render.flipX = false;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var attributeCtrl = collision.GetComponent<AttributeController>();
            if (attributeCtrl != null)
            {
                attributes ^= attributeCtrl.attributes;
            }
        }

        [ContextMenu("PrintAttributes")]
        private void PrintAttributes()
        {
            // With this we can iterate the flag values.
            foreach (Enum value in Enum.GetValues(attributes.GetType()))
            {
                if (attributes.HasFlag(value))
                    Debug.Log($"I have attributes: {value}");
            }
        }
    }
}
