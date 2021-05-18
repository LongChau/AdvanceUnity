using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

namespace TestGoogleCloud
{
    public class GoogleCloudStorage : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        private async void AsyncUploadFile(string filePath)
        {
            //Uri uri = new Uri(FTPHost + new FileInfo(filePath).Name);

            //Debug.Log(uri);

            //FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(uri);
            //ftp.Credentials = new NetworkCredential(FTPUserName, FTPPassword);

            //ftp.KeepAlive = true;
            //ftp.UseBinary = true;
            //ftp.Method = WebRequestMethods.Ftp.UploadFile;

            //FileStream fs = File.OpenRead(filePath);
            //byte[] buffer = new byte[fs.Length];
            //await fs.ReadAsync(buffer, 0, buffer.Length);
            //fs.Close();

            //Stream ftpstream = ftp.GetRequestStream();
            //await ftpstream.WriteAsync(buffer, 0, buffer.Length);
            //ftpstream.Close();

            string path = "https://console.cloud.google.com/storage/browser/test-storage-advance-unity/Sounds";
            byte[] myData = System.Text.Encoding.UTF8.GetBytes("This is some test data");
            FileStream fs = File.OpenRead(path);
            await fs.ReadAsync(myData, 0, myData.Length);
            fs.Close();
        }

        [ContextMenu("WriteFile")]
        private void WriteFile()
        {
            string fileName = "test.txt";
            string path = "https://console.cloud.google.com/storage/browser/test-storage-advance-unity/Sounds/" + fileName;
            byte[] myData = System.Text.Encoding.UTF8.GetBytes("This is some test data");
            //FileStream fs = File.OpenRead(path);
            //await fs.WriteAsync(myData, 0, myData.Length);
            //fs.Close();
            File.WriteAllBytes(path, myData);
        }

        [ContextMenu("Upload")]
        private void Upload()
        {
            StartCoroutine(IEUpload());
        }

        IEnumerator IEUpload()
        {
            byte[] myData = System.Text.Encoding.UTF8.GetBytes("This is some test data");
            string fileName = "test.txt";
            string path = "https://console.cloud.google.com/storage/browser/test-storage-advance-unity/Sounds/" + fileName;

            UnityWebRequest www = UnityWebRequest.Put(path, myData);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Upload complete!");
            }
        }
    }
}
