using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Scenes;
using UnityEngine;

namespace TestPerformance
{
    public class SubSceneLoader : ComponentSystem
    {
        private SceneSystem _sceneSystem;

        protected override void OnCreate()
        {
            base.OnCreate();
            _sceneSystem = World.GetOrCreateSystem<SceneSystem>();
        }

        protected override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadSubScene(SubSceneReferences.Instance.map1);
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                UnloadSubScene(SubSceneReferences.Instance.map1);
            }
        }

        private void LoadSubScene(SubScene scene)
        {
            _sceneSystem.LoadSceneAsync(scene.SceneGUID);
        }

        private void UnloadSubScene(SubScene scene)
        {
            _sceneSystem.UnloadScene(scene.SceneGUID);
        }
    }
}
