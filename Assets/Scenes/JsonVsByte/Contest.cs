using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestPerformance
{
    public class Contest : MonoBehaviour
    {
        public PlayerData playerData;

        private string serialized_data;
        private string deserialized_data;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            serialized_data = playerData.Serialize();
            deserialized_data = playerData.Deserialize(serialized_data);
        }
    }

    [Serializable]
    public class PlayerData
    {
        public int playerName;
        public int level;
        public float exp;
        public int gold;

        public PlayerData() { }

        public PlayerData(int playerName, int level, float exp, int gold)
        {
            this.playerName = playerName;
            this.level = level;
            this.exp = exp;
            this.gold = gold;
        }

        public string Serialize() => JsonUtility.ToJson(this);
        public string Deserialize(string json)
        {
            JsonUtility.FromJsonOverwrite(json, this);
            return json;
        }
    }
}
