using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    [CreateAssetMenu(fileName = "BasicInfo", menuName = "AssetData/BasicInfo")]
    public class BasicInfo : ScriptableObject
    {
        public int Health;
        public int Damage;
        public int Mana;
        public int SacrificeHp;
    }
}
