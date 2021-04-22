using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

#if UNITY_EDITOR
using UnityEngine;
#endif

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

        [ContextMenu("AddAttribute")]
        public void AddAttribute(EAttributes att)
        {
            attributes |= att;
        }

        [ContextMenu("RemoveAttribute")]
        public void RemoveAttribute(EAttributes att)
        {
            attributes &= ~att;
        }

#if UNITY_EDITOR
        private void OnGUI(Rect position,
                            SerializedProperty property,
                            GUIContent label)
        {
            EditorGUI.BeginChangeCheck();
            uint a = (uint)(EditorGUI.MaskField(position, label, property.intValue, property.enumNames));
            if (EditorGUI.EndChangeCheck())
            {
                property.intValue = (int)a;
            }
        }
#endif
    }
}
