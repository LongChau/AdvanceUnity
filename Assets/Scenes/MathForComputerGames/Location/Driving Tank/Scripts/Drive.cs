using UnityEngine;
using System.Collections;

// A very simplistic car driving on the x-z plane.
namespace UnityAdvance.Location
{
    public class Drive : MonoBehaviour
    {
        public float speed = 10.0f;
        public float rotationSpeed = 100.0f;

        public static Vector2 TankPosition { get; private set; }
        public static int Fuel { get; set; }

        public UIManager uiManager;

        private void Start()
        {
            TankPosition = transform.position;
        }

        void Update()
        {
            if (Fuel > 0)
            {
                var newFuel = Fuel - (int)Vector2.Distance(TankPosition, transform.position);
                uiManager._txtEnergyPos.SetText(newFuel.ToString());
            }

            TankPosition = transform.position;

            // Get the horizontal and vertical axis.
            // By default they are mapped to the arrow keys.
            // The value is in the range -1 to 1
            float translation = Input.GetAxis("Vertical") * speed;
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

            // Make it move 10 meters per second instead of 10 meters per frame...
            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;

            // Move translation along the object's z-axis
            transform.Translate(0, translation, 0);

            // Rotate around our y-axis
            transform.Rotate(0, 0, -rotation);
        }

        [ContextMenu("CalculateDistance")]
        private void CalculateDistance()
        {
            Vector2 a = Vector2.zero;
            Vector2 b = new Vector2(3f, 3f);
            Vector2 c = new Vector2(-2f, -1f);
            Vector2 d = new Vector2(-1f, 2f);
            Debug.Log($"Distance: {Vector2.Distance(a, b)}");
            Debug.Log($"Distance: {Vector2.Distance(c, d)}");
        }

        [ContextMenu("CalculateVector")]
        private void CalculateVector()
        {
            Vector2 a = new Vector2(3f, 5f);
            Debug.Log($"Distance: {a.magnitude}");
            Vector2 b = new Vector2(0, 1f);
            Debug.Log($"Distance: {b.magnitude}");
            b = new Vector2(-5, 11f);
            Debug.Log($"Distance: {b.magnitude}");
            b = new Vector2(-7, 10f);
            Debug.Log($"Distance: {b.magnitude}");
            b = new Vector2(6, -15);
            Debug.Log($"Distance: {b.magnitude}");
        }
    }
}
