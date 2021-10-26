using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test_ServiceLocator
{
    public class AndroidGameService : IGameService
    {
        public void Login()
        {
            Debug.Log("Android login");
        }
    }
}
