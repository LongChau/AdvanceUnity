using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance.DI
{
    public class ResourceManager : MonoBehaviour, IResourceManager
    {
        public void UpgradeTeaCup()
        {
            Debug.Log("UpgradeTeaCup");

        }

        public void UpgradeCoffeeCup()
        {
            Debug.Log("UpgradeCoffeeCup");

        }

        public void Constructor(IMainResourceView mainResourceView)
        {

        }
    }
}
