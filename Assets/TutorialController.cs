using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class MyEvent : UnityEvent<int, string> { }

/// <summary>
/// Event, delegates, action
/// </summary>
/// 
public class TutorialController : MonoBehaviour
{
    public Text text;
    public Button btn1;
    public Button btn2;
    public Button btn3;

    // không serialize được nên không hiện được lên inspector
    public UnityEvent<int> myEvent;

    // serialize được nên hiện lên inspector
    public MyEvent myEvent2;

    private delegate void TutorialCallBack();

    // Start is called before the first frame update
    void Start()
    {
        Button[] arrBtns = new Button[] { btn1, btn2, btn3 };

        for (int buttonIndex = 0; buttonIndex < arrBtns.Length; buttonIndex++)
        {
            int countIndex = buttonIndex;
            arrBtns[countIndex].onClick.AddListener( () =>
            {
                Debug.Log($"Button {countIndex} clicked!");
            });
        }

        //   TutorialCallBack callBack = PrintTut orialComplete;
        //   callBack += PrintTutorialComplete2;
        //   StartCoroutine(DoTutorial(callBack));

        //Sài Action thay vì tự khái báo delegate
        //Action callBackAction = PrintTutorialComplete;
        //callBackAction += PrintTutorialComplete2;

        //StartCoroutine(DoTutorialAction(PrintRelease, callBackAction));

        //StartCoroutine(DoTutorialFunc(PrintRelease, WaitForButton, callBackAction));

        // lambda
        Action callBackAction = PrintTutorialComplete;
        callBackAction += PrintTutorialComplete2;

        //string msg = "AAAAA";
        StartCoroutine(DoTutorialFunc( 
            msg => text.text = msg, 
            WaitForButton, 
            PrintTutorialComplete2));
    }

    void PrintTutorialComplete()
    {
        Debug.Log("Tutorail complete");
    }

    void PrintTutorialComplete2()
    {
        Debug.Log("Tutorail complete 2");
    }

    void PrintDebug(string msg)
    {
        Debug.Log(msg);
    }

    void PrintRelease(string msg)
    {
        text.text = msg;
    }

    private IEnumerator DoTutorial(TutorialCallBack callBack)
    {
        text.text = "Press Button 1";
        yield return WaitForButton(btn1);
        text.text = "Press Button 2";
        yield return WaitForButton(btn2);
        text.text = "Press Button 3";
        yield return WaitForButton(btn3);

        text.text = "tutorial done";

        callBack();
    }
    /// <summary>
    /// dùng Action không phải khai báo biến delegate
    /// </summary>
    /// <param name="callBack"></param>
    /// <returns></returns>
    private IEnumerator DoTutorialAction(Action<string> print, Action callBack)
    {
        print?.Invoke("Press Button 1");
        yield return WaitForButton(btn1);
        print?.Invoke("Press Button 2");
        yield return WaitForButton(btn2);
        print?.Invoke("Press Button 3");
        yield return WaitForButton(btn3);
        print?.Invoke("tutorial done");

        callBack?.Invoke(); // ? là kiểm tra khác null thì mới gọi lại thay vì dùng if kiểm tra khác null
    }

    private IEnumerator DoTutorialFunc(Action<string> print,
        Func<Button, IEnumerator> wait, Action callBack)
    {
        print?.Invoke("Press Button 1");
        yield return wait?.Invoke(btn1);
        print?.Invoke("Press Button 2");
        yield return wait?.Invoke(btn2);
        print?.Invoke("Press Button 3");
        yield return wait?.Invoke(btn3);
        print?.Invoke("tutorial done");

        callBack?.Invoke(); // ? là kiểm tra khác null thì mới gọi lại thay vì dùng if kiểm tra khác null
    }

    private IEnumerator WaitForButton(Button button)
    {
        bool isWaiting = true;
        UnityAction action = () => isWaiting = false;
        button.onClick.AddListener(action);
        yield return new WaitWhile(() => isWaiting);
        button.onClick.RemoveListener(action);
    }

}
