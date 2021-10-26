using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test_ServiceLocator
{
    public class IOSGameService : IGameService
    {
        public void Login()
        {
            Debug.Log("IOS login");
        }
    }
}
