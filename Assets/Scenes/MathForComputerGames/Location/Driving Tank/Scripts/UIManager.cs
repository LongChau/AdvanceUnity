using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UnityAdvance.Location
{
    public class UIManager : MonoBehaviour
    {
        public TextMeshProUGUI _txtTankPos;
        public TextMeshProUGUI _txtFuelPos;
        public TextMeshProUGUI _txtEnergyPos;
        public TMP_InputField _inputField;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        public void AddFuel(string amt)
        {
            _txtEnergyPos.SetText(amt);
            Drive.Fuel = int.Parse(amt);
        }

        // Update is called once per frame
        void Update()
        {
            _txtTankPos.SetText($"{Drive.TankPosition}");
            _txtFuelPos.SetText($"{ObjectManager.Fuel.position}");
        }
    }
}
