using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Null-Conditional Operator (? And ?[])
/// LINQ
/// </summary>
public class Tutorial_2 : MonoBehaviour
{
    // Ứng dụng ? check null cho singleton
    private Tutorial_2 _instance;

    //public Tutorial_2 Instance
    //{
    //    get
    //    {
    //        return _instance ?? this;
    //    }

    //    set => _instance = value;
    //}

    // hoặc
    public Tutorial_2 Instance => _instance ?? (_instance = this);

    // ứng dụng cho delegate / action
    public Func<int> myAction;

    void Tut4()
    {
       int a = myAction?.Invoke() ?? HandleNull();
    }

    void Tut5()
    {
        List<int> myList = new List<int>();
        // dùng toán tử 3 ngôi
        //int elementCount = myList != null ? myList.Count : 0;
        // hoặc
        int elementCount = myList?.Count ?? 0;
    }

    int HandleNull()
    {
        return 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Training();

        //Tut8();

        #region Dynamic type
        // dynamic type
        //var result = AddDynamic("Hello ", " Long");
        //Debug.Log($"result: {result}");
        //result = AddDynamic("Hello ", " 44");
        //Debug.Log($"result: {result}");
        //result = AddDynamic("Hello ", 44);
        //Debug.Log($"result: {result}");
        //result = AddDynamic(1, 3);
        //Debug.Log($"result: {result}");
        //Tut7();
        //
        #endregion
    }

    //LINQ

    //training
    void Training()
    {
        int[] myNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 16, 19, 21 };
        var myQuerry =
            from num in myNumbers
            //where num % 2 == 0
            // sử dụng bitwise. Nếu = 1 thì đó là số lẻ (odd)
            where (num & 1) == 0
            select num;

        int result = myQuerry.Sum();
        Debug.Log(result);

        var a = 5 & 1;
        Debug.Log(a);

        var b = 4 & 1;
        Debug.Log(b);
    }

    void Tut8()
    {
        int[] myNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 16, 19, 21 };
        var myQuerry =
            from num in myNumbers
            where num % 2 == 0
            select num;

        var maxNum = myQuerry.Max();
        Debug.Log($"maxNum: {maxNum}");

        foreach (var num in myQuerry)
        {
            Debug.Log(num);
        }

        Debug.Log("");

        // not executed this new array
        // querry capture arr instance not the pointer
        //myNumbers = new int[] { 55, 12, 66, 78, 80, 34, 67 };

        // this runs
        myNumbers[0] = 88;
        foreach (var num in myQuerry)
        {
            Debug.Log(num);
        }

        var maxNum2 = myQuerry.Max();
        Debug.Log($"maxNum: {maxNum2}");


        var myNumbers2 = new int[] { 55, 12, 66, 78, 80, 34, 67 };
        //int[] sortedArr = new int[myNumbers2.Length];
        //int max = myNumbers2[0];
        //for (int i = 0; i < myNumbers2.Length; i++)
        //{
        //    for (int j = 0; j < myNumbers2.Length - 1; i++)
        //    {
        //        if ()
        //    }
        //}

        // hoặc sort tang8
        Array.Sort(myNumbers2);
        Array.ForEach(myNumbers2, num => Debug.Log($"{num}"));

        // sort giảm
        Array.Sort(myNumbers2);
        Array.ForEach(myNumbers2, num => Debug.Log($"{num}"));
        Array.Reverse(myNumbers2);

        // cách 2 sort giảm
        Array.Sort( myNumbers2, 
                    (a, b) => b.CompareTo(a) );
        Array.ForEach(myNumbers2, num => Debug.Log($"{num}"));
    }
    //

    // dynamic type
    dynamic AddDynamic(dynamic first, dynamic second) => first + second;

    void Tut7()
    {
        // gán vô là cái gì tự hiểu là cái đó
        dynamic myVal = 4;
        Debug.Log($"myVal: {myVal}");
        myVal = "Hello";
        Debug.Log($"myVal: {myVal}");
        // Unity ko cho xài thàng này, phải nâng API compatiple (trong player setting lên 4.0x)

    }
    //

    void Tut1()
    {
        // cho phép = null 
        int? myNumber = null;

        List<int> myList = null;
        myNumber = myList?.Count; // có count thì trả về, còn không có thì trả về null
    }

    void Tut2()
    {
        List<int> myList = new List<int> { 5, 6 };
        int? firstMember = myList?[0];  // chưa chác phần từ [0] ko null
        Debug.Log($"firstMember: {firstMember}");
    }

    void Tut3()
    {
        List<int> myList = new List<int> { 5, 6 };

        // nếu phần tử [0] = null thì trả về giá trị default = 5
        int? firstMember = myList?[0] ?? 5; 

        Debug.Log($"firstMember: {firstMember}");
    }

    #region Extension method
    // Extension method
    void Tut6()
    {
        transform.ResetPosition();
    }
    //
    #endregion
}

public static class ExtensionTransformMethod
{
    #region Extension method
    // Extension method
    public static void ResetPosition(this Transform target)
    {
        target.position = Vector3.zero;
    }
    //
    #endregion

}

#region PartialClass
public partial class DemoPatialClass : MonoBehaviour
{

    // Start is called just before any of the Update methods is called the first time
    private void Start()
    {
        //Debug.Log("This is DemoPatialClass ---- 1");
        PrintString("This is DemoPatialClass ---- 1");
    }

    // ... see more in tutorial_1

    // Một đoạn code mà chưa nghĩ ra
    // strict: 
    // 1: method phải trả về void
    // 2: mặc định phải là private
    // 3: cả 2 bên partial tên phải giống nhau
    // partial methods
    partial void PrintString(string myString);
}
#endregion

public class AnonymouseClass : MonoBehaviour
{
    private void Awake()
    {
        // anonymous class
        var myData = new
        {
            first = 5,
            second = "second"
        };

        Debug.Log(myData.ToString());
    }
}

// Turple
public class TurpleClass : MonoBehaviour
{
    private void Awake()
    {
        // turple: unnamed turple
        var myData = (5, "Hello");

        Debug.Log($"myData.Item1: {myData.Item1}");

        // cho phép sửa dữ liệu được
        myData.Item1 = 7;

        Debug.Log($"myData.Item1: {myData.Item1}");
        Debug.Log($"myData.Item2: {myData.Item2}");

        // named turple
        var myData2 = (myAge: 5, myName: "Hello");

        int myAge = 10;
        string myName = "AAA";
        var myData3 = (myAge, myName);

        // old-style turple
        Tuple <int, string, string> person = 
            new Tuple<int, string, string>(1, "Steve", "Job");
    }
}