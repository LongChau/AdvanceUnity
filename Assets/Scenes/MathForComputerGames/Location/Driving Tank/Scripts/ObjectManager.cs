using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance.Location
{
    public class ObjectManager : MonoBehaviour
    {
        public GameObject objPrefab;
        public static Transform Fuel { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            var obj = Instantiate(objPrefab,
                new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), objPrefab.transform.position.z),
                Quaternion.identity);
            Fuel = obj.transform;
            Debug.Log($"Fuel location: {obj.transform.position}");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
