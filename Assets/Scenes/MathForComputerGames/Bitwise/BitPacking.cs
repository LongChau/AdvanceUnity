using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance.Bitwise
{
    public class BitPacking : MonoBehaviour
    {
        private string A = "110111";
        private string B = "10001";
        private string C = "1101";

        // Start is called before the first frame update
        void Start()
        {

        }

        [ContextMenu("CheckBitPacking")]
        private void CheckBitPacking()
        {
            int aBits = Convert.ToInt32(A, 2);
            int bBits = Convert.ToInt32(B, 2);
            int cBits = Convert.ToInt32(C, 2);

            int packed = 0;

            Debug.Log(Convert.ToString(packed, 2).PadLeft(32, '0'));
            packed = packed | (aBits << 26);
            Debug.Log(Convert.ToString(packed, 2).PadLeft(32, '0'));
            packed = packed | (bBits << 21);
            Debug.Log(Convert.ToString(packed, 2).PadLeft(32, '0'));
            packed = packed | (cBits << 17);
            Debug.Log(Convert.ToString(packed, 2).PadLeft(32, '0'));

            Debug.Log($"Final {Convert.ToString(packed, 2).PadLeft(32, '0')}");
        }
    }
}
