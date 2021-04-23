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
            None        = 0,
            Invisible   = 1 << 0,
            Fly         = 1 << 1,
            Charisma    = 1 << 2,
            Intelligent = 1 << 3,
            Magic       = 1 << 4,
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
            //attributes &= ~EAttributes.Charisma & ~EAttributes.Invisible;
            // Or:
            attributes &= ~(EAttributes.Charisma | EAttributes.Invisible);
        }

        [ContextMenu("Test_ToggleAttributes")]
        public void Test_ToggleAttributes()
        {
            attributes ^= EAttributes.Intelligent;
        }

        [ContextMenu("ResetAttributes")]
        public void ResetAttributes()
        {
            Debug.Log(Convert.ToInt32(EAttributes.None));
            attributes = EAttributes.None;
        }

        [ContextMenu("Test_CheckAttributes")]
        public void Test_CheckAttributes()
        {
            Debug.Log($"Has Invisible {attributes.HasFlag(EAttributes.Invisible)}");
            Debug.Log($"Has Fly {attributes.HasFlag(EAttributes.Fly)}");
            Debug.Log($"Has Charisma {attributes.HasFlag(EAttributes.Charisma)}");
            Debug.Log($"Has Intelligent {attributes.HasFlag(EAttributes.Intelligent)}");
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
