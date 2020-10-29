using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace TestPerformance
{
    public class Contest : MonoBehaviour
    {
        public PlayerData playerData;

        [SerializeField]
        private string serialized_data;
        [SerializeField]
        private string deserialized_data;

        [SerializeField]
        private byte[] serialized_data_byte;
        [SerializeField]
        private byte[] deserialized_data_byte;

        [SerializeField]
        private BinaryFormatter bf;
        [SerializeField]
        private MemoryStream ms;

        // Start is called before the first frame update
        void Start()
        {
            bf = new BinaryFormatter();
            ms = new MemoryStream();
        }

        // Update is called once per frame
        void Update()
        {
            //serialized_data = playerData.JSON_Serialize();
            //deserialized_data = playerData.JSON_Deserialize(serialized_data);

            //serialized_data_byte = playerData.BYTE_Serialize(bf, ms);
            //serialized_data_byte = playerData.ConvertToBytes();
        }
    }

    [Serializable]
    public class PlayerData
    {
        public string playerName;
        public int level;
        public float exp;
        public int gold;

        public PlayerData() { }

        public PlayerData(string playerName, int level, float exp, int gold)
        {
            this.playerName = playerName;
            this.level = level;
            this.exp = exp;
            this.gold = gold;
        }

        public string JSON_Serialize() => JsonUtility.ToJson(this);
        public string JSON_Deserialize(string json)
        {
            JsonUtility.FromJsonOverwrite(json, this);
            return json;
        }

        public byte[] ConvertToBytes() => Encoding.UTF8.GetBytes(this.ToString());
        public void ConvertToPlayerDataFromBytes(byte[] buffer)
        {
            var serialized = Encoding.UTF8.GetString(buffer);

        }

        public byte[] BYTE_Serialize(BinaryFormatter bf, MemoryStream ms)
        {
            bf.Serialize(ms, this);
            return ms.ToArray();
        }

        public void BYTE_Deserialize(byte[] buffer, BinaryFormatter bf, MemoryStream ms)
        {
            var obj = bf.Deserialize(ms);
            var newData = obj as PlayerData;
            this.playerName = newData.playerName;
            this.level = newData.level;
            this.exp = newData.exp;
            this.gold = newData.gold;
        }
    }
}
