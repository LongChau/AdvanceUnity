using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace UnityAdvance.DI
{
    public class ResourceView : MonoBehaviour, IMainResourceView
    {
        [SerializeField]
        private Slider _coffeeSlider;
        [SerializeField]
        private Slider _teaSlider;

        //private ResourceManager _resourceManager;
        private IResourceManager _resourceManager;

        private void Awake()
        {
            //_resourceManager = FindObjectOfType<ResourceManager>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        public void Constructor(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public void OnBtnUpgradeTeaCupClicked()
        {
            _resourceManager.UpgradeTeaCup();
        }

        public void OnBtnUpgradeCoffeeCupClicked()
        {
            _resourceManager.UpgradeCoffeeCup();
        }
    }
}
