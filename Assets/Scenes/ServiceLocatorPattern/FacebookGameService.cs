using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test_ServiceLocator
{
    public class FacebookGameService : IGameService
    {
        public void Login()
        {
            Debug.Log("Facebook login");
        }
    }
}
