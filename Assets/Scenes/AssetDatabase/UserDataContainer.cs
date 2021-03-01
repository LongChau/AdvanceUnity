using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityAdvance.AssetDatabaseTest
{
    [CreateAssetMenu(fileName = "UserDataContainer", menuName = "AssetDatabase/UserDataContainer")]
    public class UserDataContainer : ScriptableObject
    {
        public List<UserData> listUserData = new List<UserData>();

        public User user;

        [HideInInspector]
        public int count;

        public string GetAssetPath()
        {
            Debug.Log(AssetDatabase.GetAssetPath(this));
            return AssetDatabase.GetAssetPath(this);
        }

        public void CreateUserData()
        {
            UserData newUserData = new UserData(user);
            newUserData.name = $"UserData_{count}";
            AssetDatabase.AddObjectToAsset(newUserData, this);
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newUserData));

            listUserData.Add(newUserData);
            count++;

            //AssetDatabase.ImportAsset(GetAssetPath());
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public void ClearUserData()
        {
            for (int i = listUserData.Count - 1; i >= 0; i--)
            {
                DestroyImmediate(listUserData[i], true);
            }

            count = 0;
            listUserData = new List<UserData>();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    [CustomEditor(typeof(UserDataContainer))]
    public class TestScriptableEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (UserDataContainer)target;

            if (GUILayout.Button("Get Asset Path", GUILayout.Height(20)))
            {
                script.GetAssetPath();
            }

            if (GUILayout.Button("Create User Data", GUILayout.Height(20)))
            {
                script.CreateUserData();
            }

            if (GUILayout.Button("Clear User Data", GUILayout.Height(20)))
            {
                script.ClearUserData();
            }
        }
    }
}
