﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test_ServiceLocator
{
    public class TestGameService : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GameServiceLocator.Instance.Get<IOSGameService>();
            GameServiceLocator.Instance.Get<AndroidGameService>();
            GameServiceLocator.Instance.Get<FacebookGameService>();

            // #if IOS
            GameServiceLocator.Instance.Get<IOSGameService>().Login();
            // #if Android
            GameServiceLocator.Instance.Get<AndroidGameService>().Login();
            // #if Facebook
            GameServiceLocator.Instance.Get<FacebookGameService>().Login();
        }
    }
}