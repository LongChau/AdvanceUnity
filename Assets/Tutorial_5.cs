using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// GC optimize
/// </summary>
namespace UnityAdvance
{
    public class Tutorial_5 : MonoBehaviour
    {
        public Text txtHP;

        [SerializeField]
        private int _healthPoint = 5;

        private string hpString;
        public int HealthPoint
        {
            get => _healthPoint;
            set
            {
                if (_healthPoint != value)
                {
                    _healthPoint = value;
                    //Debug.Log(_healthPoint);
                    hpString = $"HP: {_healthPoint}";
                    txtHP.text = hpString;
                    //txtHP.text = "HP: " + _healthPoint;
                }
            }
        }

        private Vector3[] arrNormals;
        [SerializeField]
        private Mesh myMesh;

        // Start is called before the first frame update
        void Start()
        {
            arrNormals = myMesh.normals;
        }

        // Update is called once per frame
        void Update()
        {
            Tut_3();
            //Tut_2();
            //Tut_1();
        }

        private void Tut_3()
        {
            for (int i = 0; i < arrNormals.Length; i++)
            {
                var normal = arrNormals[i];
            }
        }
        
        private void Tut_2()
        {
            // will not trigger Property
            //_healthPoint++;
            HealthPoint++;
        }

        private void Tut_1()
        {
            string myString = "Hello";

        }
    }
}