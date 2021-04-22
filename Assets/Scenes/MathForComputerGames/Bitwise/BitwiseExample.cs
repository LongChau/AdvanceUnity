using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance.Bitwise
{
    public class BitwiseExample : MonoBehaviour
    {
        [Flags]
        public enum EAttributes : int
        {
            //None        = 1 << 0,
            Invisible   = 1 << 1,
            Fly         = 1 << 2,
            Charisma    = 1 << 3,
            Intelligent = 1 << 4,
            Magic       = 1 << 5,
        }

        public EAttributes attributes;

        // Start is called before the first frame update
        void Start()
        {

        }

        [ContextMenu("Test_AddAttributes")]
        public void Test_AddAttributes()
        {
            attributes |= EAttributes.Charisma | EAttributes.Intelligent | EAttributes.Invisible;
        }

        [ContextMenu("Test_RemoveAttributes")]
        public void Test_RemoveAttributes()
        {
            attributes &= ~EAttributes.Charisma & ~EAttributes.Invisible;
        }

        [ContextMenu("Test_CheckAttributes")]
        public void Test_CheckAttributes()
        {
            Debug.Log($"Has Invisible {HasAttribute(EAttributes.Invisible)}");
            Debug.Log($"Has Fly {HasAttribute(EAttributes.Fly)}");
            Debug.Log($"Has Charisma {HasAttribute(EAttributes.Charisma)}");
            Debug.Log($"Has Intelligent {HasAttribute(EAttributes.Intelligent)}");
            Debug.Log($"Has Magic {HasAttribute(EAttributes.Magic)}");
        }

        public bool HasAttribute(EAttributes att)
        {
            return (attributes & att) != 0;
        }

        //[ContextMenu("AddAttribute")]
        public void AddAttribute(EAttributes att)
        {
            attributes |= att;
        }

        //[ContextMenu("RemoveAttribute")]
        public void RemoveAttribute(EAttributes att)
        {
            attributes &= ~att;
        }
    }
}
