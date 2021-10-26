using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance.DI
{
    public class RegisterDependencies : MonoBehaviour
    {
        [SerializeField]
        public ResourceManager _resourceManager;
        [SerializeField]
        public ResourceView _resourceView;

        private void Awake()
        {
            // Resolve/Inject the dependencies.
            _resourceManager.Constructor(_resourceView);
            _resourceView.Constructor(_resourceManager);
        }
    }
}
