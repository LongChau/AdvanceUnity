using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace UnityAdvance.TMPExample
{
    public class InputFieldExample : MonoBehaviour
    {
        [SerializeField] TMP_InputField _inputField;

        // Start is called before the first frame update
        void Start()
        {
            _inputField.onValueChanged.AddListener(Handle_ValueChanged);
        }

        private void Handle_ValueChanged(string input)
        {
            Debug.Log(input);
        }
    }
}
