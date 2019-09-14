using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Reflection;
using System;

/// <summary>
/// Reflection
/// Attribute
/// </summary>
namespace UnityAdvance
{
    public class DontCall : Attribute
    {
        public string DontCallIt { get; set; }
    }

    class UnknowClass
    {
        private string myName = "";
        public string MyName => myName;

        private int myNumber = 5;
        public int MyNumber => myNumber;

        public void PrintHello()
        {
            Debug.Log("UnknowClass:PrintHello()");
        }

        public void PrintHello(string data)
        {
            Debug.Log($"UnknowClass:PrintHello({data} : {myNumber})");
        }

        private void PrintHello(int data)
        {
            Debug.Log($"UnknowClass:PrintHello({data})");
        }

        private void PrivateHello()
        {
            Debug.Log("UnknowClass:PrivateHello()");
        }

        [DontCall(DontCallIt = "Oooopsss")]
        private void PrintChao()
        {
            Debug.Log("UnknowClass:PrintChao()");
        }

        private void PrintNihao()
        {
            Debug.Log("UnknowClass:PrintNihao()");
        }
    }

    [SelectionBase]
    public class Tutorial_4 : MonoBehaviour
    {
        public Tutorial_3 tut3;

        private void OnValidate()
        {
            tut3 = GetComponent<Tutorial_3>();
        }

        // Start is called before the first frame update
        void Start()
        {
            Tut_2();
            //Tut_1();
        }

        private void Tut_1()
        {
            var unknowClass = new UnknowClass();
            CallMethod(unknowClass, "PrintHello");
        }

        private void Tut_2()
        {
            var unknowClass = new UnknowClass();
            CallMethod2(unknowClass, "PrintHello");
        }

        private void CallMethod2(object obj, string methodName)
        {
            var type = obj.GetType();
            var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (var methodInfo in methods)
            {
                var attribute = methodInfo.GetCustomAttribute(typeof(DontCall));
                if (attribute != null)
                { 
                    Debug.Log(attribute.GetType().GetProperty("DontCallIt").GetValue(attribute, null));
                    methodInfo.Invoke(obj, null);
                }
            }
        }

        private void CallMethod(object obj, string methodName)
        {
            var type = obj.GetType();

            //var arrParam = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            var param = type.GetField("myNumber", BindingFlags.NonPublic | BindingFlags.Instance);
            Debug.Log($"Param: {param}");
            if (param != null)
                param.SetValue(obj, 1000);
            else
                Debug.Log("Param is null");

            //var method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic);
            var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            //method.Invoke(obj, null);

            foreach (var methodInfo in methods)
            {
                if (IsMethodMatched(methodInfo))
                {
                    methodInfo.Invoke(obj, new object[] { "L" });
                }
                //Debug.Log($"{methodInfo}");
                //methodInfo.Invoke(obj, null);
            }

        }

        private bool IsMethodMatched(MethodInfo info)
        {
            if (!info.Name.Equals("PrintHello"))
                return false;

            var parameter = info.GetParameters();

            if (parameter == null || parameter.Length != 1)
                return false;

            var param = parameter[0];
            if (param.ParameterType != typeof(string))
                return false;

            return true;
        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }
    }

    public static class ObjectExtensions
    {
        public static void Dump(this object obj)
        {
            if (obj == null)
            {
                Debug.Log("obj is null");
                return;
            }

            Debug.Log("Hash: ");
            Debug.Log(obj.GetHashCode());
            Debug.Log("Type: ");
            Debug.Log(obj.GetType());

            var props = GetProperties(obj);

            if (props.Count > 0)
            {
                Debug.Log("-------------------------");
            }

            foreach (var prop in props)
            {
                Debug.Log(prop.Key);
                Debug.Log(": ");
                Debug.Log(prop.Value);
            }
        }

        private static Dictionary<string, string> GetProperties(object obj)
        {
            var props = new Dictionary<string, string>();
            if (obj == null)
                return props;

            var type = obj.GetType();
            foreach (var prop in type.GetProperties())
            {
                var val = prop.GetValue(obj, new object[] { });
                var valStr = val == null ? "" : val.ToString();
                props.Add(prop.Name, valStr);
            }

            return props;
        }
    }
}