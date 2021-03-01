using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance.AssetDatabaseTest
{
    [Serializable]
    [CreateAssetMenu(fileName = "UserData", menuName = "AssetDatabase/UserData")]
    public class UserData : ScriptableObject
    {
        public string user_name;
        public int age;
        public int level;

        public UserData(string user_name, int age, int level)
        {
            this.user_name = user_name;
            this.age = age;
            this.level = level;
        }

        public UserData(User data)
        {
            this.user_name = data.user_name;
            this.age = data.age;
            this.level = data.level;
        }
    }

    [Serializable]
    public class User
    {
        public string user_name;
        public int age;
        public int level;

        public User(User data)
        {
            this.user_name = data.user_name;
            this.age = data.age;
            this.level = data.level;
        }
    }
}
