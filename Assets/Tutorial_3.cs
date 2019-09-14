using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

/// <summary>
/// Property.
/// Indexer.
/// </summary>
/// 
public class Tutorial_3 : MonoBehaviour
{
    public int MyNumber
    {
        get { return 50; }
        set { Debug.Log($"int value = {value}"); }
    }

    [SerializeField]
    private string myEmail;
    public string MyEmail
    {
        get => myEmail ?? "default@gmail.com";
        set
        {
            if (IsValidEmail(value))
                myEmail = value;
        }
    }

    public int EmailLength => MyEmail?.Length ?? 0;

    private bool IsValidEmail(string email)
    {
        if (!email.Contains("@"))
        {
            return false;
        }

        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Tut_4();
        //Tut_3();
        //Tut_2();
        //Tut_1();
    }

    #region Tut4
    void Tut_4()
    {
        string[] str = new string[] { "A thousand slendid Suns",
                                        "The one", "The two", "third The",
                                        "longchau@gmail", "aaaaGMail",
                                        "longchau@gmail.com", "longchau@@gmail.com",
                                        "longchau1210@gmail.com" };
        //Debug.Log("Matching words start with 'The' ");
        ShowMatch(str, @"^[\w\.\d]+@[\w\.\d]+$");
    }

    void ShowMatch(string[] input, string matchPattern)
    {
        foreach (var item in input)
        {
            if (Regex.IsMatch(item, matchPattern))
            {
                Debug.Log(item);
            }
        }
    }

    void ShowMatch(string[] input, string matchPattern, string exceptPattern = "@{2}", string secondMatchPattern = "")
    {
        foreach (var item in input)
        {
            //MatchCollection matchCollection = Regex.Matches(item, pattern);
            //MatchCollection matchCollection = Regex.IsMatch(item, pattern);
            if (Regex.IsMatch(item, matchPattern) && 
                !Regex.IsMatch(item, exceptPattern) &&
                Regex.IsMatch(item, secondMatchPattern))
            {
                Debug.Log(item);
            }
            //foreach (Match match in matchCollection)
            //{
            //    Debug.Log(match);
            //}
        }
        //MatchCollection mc = Regex.Matches(input, exprs);
        //foreach (Match m in mc)
        //{
        //    Debug.Log(m);
        //}
    }
    #endregion

    void Tut_3()
    {
        MyCollection myCollection = new MyCollection();
        //Debug.Log(myCollection[3]);

        Debug.Log(myCollection["first"]);
    }

    void Tut_2()
    {
        MyEmail = "InvalidEmail";
        Debug.Log(MyEmail);

        Debug.Log(EmailLength);
    }

    void Tut_1()
    {
        MyNumber = 22;
        Debug.Log($"My number = {MyNumber}");
    }
}

/// <summary>
/// Indexer
/// </summary>
public class MyCollection
{
    private readonly int[] myArray = new int[] { 2, 4, 5, 8 };

    private readonly string[] names = new string[] { "first", "second", "third", "fourth" };

    public int this[int index] => myArray[index];
    //{
    //    get
    //    {
    //        return myArray[index];
    //    }
    //}

    // overload
    //public int this[string key]
    //{
    //    get
    //    {
    //        switch (key)
    //        {
    //            case "first":
    //                return myArray[0];
    //            case "second":
    //                return myArray[1];
    //            case "third":
    //                return myArray[2];
    //            default:
    //                return myArray[0];
    //        }
    //    }
    //}

    public int this[string key] => myArray[Array.IndexOf(names, key)];
    //{
    //    get
    //    {
    //        var result = Array.IndexOf(names, key);
    //        return myArray[result];
    //    }
    //}
}

