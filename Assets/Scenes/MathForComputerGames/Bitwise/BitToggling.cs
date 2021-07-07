using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance.Bitwise
{
    public class BitToggling : MonoBehaviour
    {
        public BitwiseExample.EAttributes attributes;

        // Start is called before the first frame update
        void Start()
        {

        }

        [ContextMenu("ToggleAttribute")]
        public void ToggleAttribute()
        {
            attributes ^= BitwiseExample.EAttributes.Charisma;
        }
    }
}
