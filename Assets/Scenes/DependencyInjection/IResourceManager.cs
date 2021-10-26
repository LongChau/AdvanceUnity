using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance.DI
{
    public interface IResourceManager
    {
        void Constructor(IMainResourceView view);
        void UpgradeTeaCup();
        void UpgradeCoffeeCup();
    }
}
