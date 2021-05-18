using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.IO;
using UnityEngine.UI;
using UniRx;
using UnityEngine.Networking;
using UnityEngine.Events;
using System.Threading;
using System.Threading.Tasks;
using TMPro;

/// <summary>
/// Handle write/read file and connection to FTP server.
/// </summary>
public class Uploader : MonoBehaviour
{
    //ftp://158.199.222.16/html/sanrio/
    // 158.199.234.86
    public string FTPHost = "ftp://158.199.234.86/html/sanrio/";
    public string FTPUserName = "oita";
    public string FTPPassword = "muhareke999";
    public string FilePath;

    [Space][Tooltip("Wait time for uploading.")]
    public float intervalTime = 120f;
    private bool _isUploading;
    [Space]

    public Text txtDebug;
    public GameObject notiFailed;
    public GameObject notiSucceed;
    public TextMeshProUGUI txtFailed;
    public Canvas canvasCapture;
    public Canvas canvasUpload;

    public UnityEvent OnUploadCompleted;
    public UnityEvent OnUploadFailed;
    public UnityEvent OnEndIntervalUploadTime;

    private void AddDebug(string debug)
    {
        if (txtDebug != null)
            txtDebug.text += $"\n {debug}";

        Debug.Log($"{debug}");
    }

    /// <summary>
    /// Open the FTP and upload the file asynchronously.
    /// Async help the main process run smoothly, not to be blocked by this heavy process.
    /// </summary>
    /// <param name="filePath"></param>
    private async void AsyncUploadFile(string filePath)
    {
        AddDebug($"UploadFile({filePath})");

        try
        {
            Uri uri = new Uri(FTPHost + new FileInfo(filePath).Name);

            Debug.Log(uri);
            AddDebug($"{uri}");

            FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(uri);
            ftp.Credentials = new NetworkCredential(FTPUserName, FTPPassword);

            ftp.KeepAlive = true;
            ftp.UseBinary = true;
            ftp.Method = WebRequestMethods.Ftp.UploadFile;

            FileStream fs = File.OpenRead(filePath);
            byte[] buffer = new byte[fs.Length];
            await fs.ReadAsync(buffer, 0, buffer.Length);
            fs.Close();

            Stream ftpstream = ftp.GetRequestStream();
            await ftpstream.WriteAsync(buffer, 0, buffer.Length);
            ftpstream.Close();

            await StartCoroutine(PostAPIScreenShot(uri.ToString()));

            //#if UNITY_EDITOR
            //EditorCoroutineUtility.StartCoroutineOwnerless(Editor_PostAPIScreenShot(uri.ToString()));
            //#else
            //    //await StartCoroutine(PostAPIScreenShot(uri.ToString()));
            //    StartCoroutine(PostAPIScreenShot(uri.ToString()));
            //#endif
        }
        catch (Exception ex)
        {
            txtFailed.SetText($"Failed to upload the file. {ex.Message}");
            ShowNotiFailed();
        }
    }

    /// <summary>
    /// This one can use directly from the editor.
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    IEnumerator Editor_PostAPIScreenShot(string uri)
    {
        int indexFilePath = uri.IndexOf("picture");
        WWWForm form = new WWWForm();
        form.AddField("image_url", "http://cinnamon-qr.sakura.ne.jp/" + uri.Substring(indexFilePath));

        // using (UnityWebRequest www = UnityWebRequest.Post("https://us-central1-cinnamon-qr.cloudfunctions.net/new", form))
        using (UnityWebRequest www = UnityWebRequest.Post("https://asia-northeast1-cinnamon-qr.cloudfunctions.net/new", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                txtFailed.SetText($"Failed to upload the file. {www.error}");
                ShowNotiFailed();
            }
            else
            {
                Debug.Log("Form upload complete! " + uri);
                ShowNotiSucceed();
            }
        }
    }

    /// <summary>
    /// Post the API with uri.
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    IEnumerator PostAPIScreenShot(string uri)
    {
        Debug.Log($"Uploader.PostAPIScreenShot({uri})");
        int indexFilePath = uri.IndexOf("picture");
        WWWForm form = new WWWForm();
        form.AddField("image_url", "http://cinnamon-qr.sakura.ne.jp/" + uri.Substring(indexFilePath));

        // using (UnityWebRequest www = UnityWebRequest.Post("https://us-central1-cinnamon-qr.cloudfunctions.net/new", form))
        using (UnityWebRequest www = UnityWebRequest.Post("https://asia-northeast1-cinnamon-qr.cloudfunctions.net/new", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                
                txtFailed.SetText($"Failed to upload the file. {www.error}");
                ShowNotiFailed();
            }
            else
            {
                Debug.Log("Form upload complete! " + uri);
                ShowNotiSucceed();
                _isUploading = false;
            }
        }
    }

    void OnFileUploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
    {
        Debug.Log("Uploading Progreess: " + e.ProgressPercentage);
        //txtDebug.text = "Uploading Progreess: ";
        AddDebug($"Uploading Progreess: {e.ProgressPercentage}");
    }

    void OnFileUploadCompleted(object sender, UploadFileCompletedEventArgs e)
    {
        Debug.Log("File Uploaded");
        //txtDebug.text = "File Uploaded";
        AddDebug($"File Uploaded");
    }

    private void Test_EncodeTexture(Texture2D texture) => MakeTextureToFile(texture);

    private void Test_GetDate() => Debug.Log($"{RandomStringByInt()}");

    /// <summary>
    /// Randome string by int.
    /// </summary>
    /// <returns></returns>
    private string RandomStringByInt()
    {
        int seed = (int)System.DateTime.Now.Ticks;
        UnityEngine.Random.InitState(seed);
        int rand = UnityEngine.Random.Range(1, 100000);
        var result = $"{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}_{rand}";
        return result; 
    }

    /// <summary>
    /// Encode the texture into buffer and write the file.
    /// </summary>
    /// <param name="texture"></param>
    public void MakeTextureToFile(Texture2D texture)
    {
        Debug.Log($"MakeTextureToFile({texture.name})");
        AddDebug($"MakeTextureToFile({texture.name})");

        _isUploading = true;
        Start_IEIntervalUploadTime();
        Debug.unityLogger.logEnabled = true;

        string folderPath = "";
        string filePath = "";
        string randomString = RandomStringByInt();

        folderPath = Path.Combine(Application.persistentDataPath, "Images");

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        Debug.Log($"Path: {folderPath}");
        AddDebug($"Path: {folderPath}");

        // Get PNG bytes
        var dataBuffer = texture.EncodeToPNG();

        // Get filepath
        filePath = $"{folderPath}/screenshot_{randomString}.png";
        AddDebug($"filePath: {filePath}");

        // Start image encode thread.
        var threadWriteFile = Observable.Start(() =>
        {
            var taskWriteFile = WriteFileAsync(filePath, dataBuffer);
            //taskWriteFile.Wait(); // Does not need since we are using Observable.WhenAll...
            return taskWriteFile.Result;
        });

        // await thread values
        Observable.WhenAll(threadWriteFile) // when write file complete.
            .ObserveOnMainThread() // return to main thread
            .Subscribe(result =>
            {
                if (result != null)
                {
                    bool writeResult = (bool)result.GetValue(0);
                    Debug.Log($"Write file result: {writeResult}");
                    AddDebug($"Write file result: {writeResult}");

                    if (writeResult)
                    {
                        Debug.Log($"Upload file at path: {filePath}");
                        AsyncUploadFile(filePath);
                    }
                    else
                    {
                        Debug.Log("Write file failed. Failed to upload file too.");
                        AddDebug("Write file failed. Failed to upload file too.");
                    }
                }
            });
    }

    /// <summary>
    /// In here we use write file async to make the main thread not to be blocked.
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    private async Task<bool> WriteFileAsync(string filePath, byte[] data)
    {
        using (FileStream sourceStream = new FileStream(filePath,
            FileMode.Append, FileAccess.Write, FileShare.None,
            bufferSize: data.Length, useAsync: true))
        {
            await sourceStream.WriteAsync(data, 0, data.Length);
            Debug.Log("WriteFileAsync finished");
            return true;
        };
    }

    public void ShowNotiFailed()
    {
        notiFailed.SetActive(true);
        StartCoroutine(HideNoti(notiFailed, () =>
        {
            Debug.Log("Failed ran");
            canvasCapture.gameObject.SetActive(false);
            canvasUpload.gameObject.SetActive(true);
            OnUploadFailed?.Invoke();
            Debug.unityLogger.logEnabled = false;
        }));
    }

    public void ShowNotiSucceed()
    {
        notiSucceed.SetActive(true);
        StartCoroutine(HideNoti(notiSucceed, () =>
        {
            Debug.Log("Succeed ran");
            canvasCapture.gameObject.SetActive(true);
            canvasUpload.gameObject.SetActive(false);
            OnUploadCompleted?.Invoke();
            Debug.unityLogger.logEnabled = false;
        }));
    }

    IEnumerator HideNoti(GameObject target, Action callback = null)
    {
        yield return new WaitForSecondsRealtime(3.0f);
        target.SetActive(false);
        callback?.Invoke();
    }

    private void Start_IEIntervalUploadTime() => StartCoroutine(IEIntervalUploadTime());

    /// <summary>
    /// Wait for upload time out.
    /// If the upload process is too long. We will cancel and go back to the DeveloperScreen.
    /// </summary>
    /// <returns></returns>
    IEnumerator IEIntervalUploadTime()
    {
        var wait = new WaitForSecondsRealtime(1f);
        var currentTime = intervalTime;
        while (_isUploading)
        {
            currentTime--;
            yield return wait;
        }

        OnEndIntervalUploadTime?.Invoke();
    }
}