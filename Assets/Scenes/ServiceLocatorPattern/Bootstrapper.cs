using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Test_ServiceLocator
{
    /// <summary>
    /// Loading in boot scene. After the scene is loaded this script runs.
    /// </summary>
    public static class Bootstrapper
    {
        //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Initialized()
        {
            // Init default service locator.
            GameServiceLocator.Initialized();

            // Register all services next.
            GameServiceLocator.Instance.Register<IGameService>(new IOSGameService());
            GameServiceLocator.Instance.Register<IGameService>(new FacebookGameService());
            GameServiceLocator.Instance.Register<IGameService>(new AndroidGameService());

            // Application is ready to start. Move to the main scene.
            //SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        }
    }
}
