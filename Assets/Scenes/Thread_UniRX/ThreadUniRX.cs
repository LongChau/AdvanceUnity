using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using UniRx;
using UnityEngine;

public class ThreadUniRX : MonoBehaviour
{
    public string filePath;
    public string thread1_Text = "Hello I'm thread 1";

    static string finalPath;

    // Start is called before the first frame update
    void Start()
    {
        //finalPath = Path.Combine(Application.persistentDataPath, filePath);
        //finalPath.Replace('/', '\\');
        var convertPath = Path.GetFullPath(Application.persistentDataPath);
        finalPath = Path.Combine(convertPath, filePath);
        Debug.Log("finalPath: " + finalPath);

        //SaveFile();
    }

    [ContextMenu("SaveFile")]
    private void SaveFile()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(finalPath);
            bf.Serialize(file, thread1_Text);
            file.Close();
        }
        catch (Exception Ex)
        {
            Debug.Log(Ex.ToString());
        }
    }

    [ContextMenu("DoHeavyThread")]
    private void DoHeavyThread()
    {
        Debug.Log("DoHeavyThread()");
        var heavyMethod = Observable.Start(() =>
        {
            // heavy method...
            //Thread.Sleep(TimeSpan.FromSeconds(1));

            Thread writeThread = new Thread(() =>
            {
                if (!File.Exists(finalPath))
                {
                    //try
                    //{
                    //    // Create a new file     
                    //    using (FileStream fs = File.Create(finalPath))
                    //    {
                    //        // Add some text to file    
                    //        //Byte[] title = new UTF8Encoding(true).GetBytes("New Text File");
                    //        //fs.Write(title, 0, title.Length);
                    //        //byte[] author = new UTF8Encoding(true).GetBytes("Mahesh Chand");
                    //        //fs.Write(author, 0, author.Length);
                    //        byte[] author = new UTF8Encoding(true).GetBytes(thread1_Text);
                    //        fs.Write(author, 0, author.Length);
                    //    }
                    //}

                    //catch (Exception Ex)
                    //{
                    //    Console.WriteLine(Ex.ToString());
                    //}

                    //File.Create(finalPath);

                    SaveFile();

                    //BinaryFormatter bf = new BinaryFormatter();
                    //FileStream file = File.Create(finalPath);
                    //bf.Serialize(file, thread1_Text);
                    //file.Close();

                    Debug.Log("Finished save path: " + finalPath);
                }
            });

            writeThread.Start();

            return true;
        });

        var heavyMethod2 = Observable.Start(() =>
        {
            // heavy method...
            Thread.Sleep(TimeSpan.FromSeconds(3));



            return true;
        });

        // Join and await two other thread values
        Observable.WhenAll(heavyMethod, heavyMethod2)
            .ObserveOnMainThread() // return to main thread
            .Subscribe(xs =>
            {
                // Unity can't touch GameObject from other thread
                // but use ObserveOnMainThread, you can touch GameObject naturally.
                //var result = xs[0] + ":" + xs[1];
                //Debug.Log(result);

                var threadSucceed = xs[0] == xs[1];
                if (threadSucceed)
                    Debug.Log("2 Thread has completed!");
            });
    }
}
