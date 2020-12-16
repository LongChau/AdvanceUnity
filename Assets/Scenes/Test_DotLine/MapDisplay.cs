using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TestGPS.Manager;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace TestGPS.Component
{
    public class MapDisplay : MonoBehaviour
    {
        private string _googleApiKey = "AIzaSyCKTn_shtzn1ZiUovwAolxHz-9Cr1WcpC4";
        public string url = @"https://maps.googleapis.com/maps/api/staticmap?";
        public int mapSize = 640;
        public int zoom = 18;
        public Image mapImage;

        private IEnumerator _downloadMap;

        // Start is called before the first frame update
        private IEnumerator Start()
        {
            while (!GPSManager.Instance.IsFinishLocationRequest) yield return null;
            //yield return true;

            _downloadMap = DownloadingMap();
            yield return StartCoroutine(_downloadMap);

            Debug.Log("Finish MapDisplay.Start()");
        }

        private IEnumerator DownloadingMap()
        {
            url += "center=" + GPSManager.Instance.CurrentLocation.latitude + "," + GPSManager.Instance.CurrentLocation.longitude;
            url += "&zoom=" + zoom;
            url += "&size=" + mapSize + "x" + mapSize;
            // API Key（Google Maps Platform）
            url += "&key=" + _googleApiKey;

            // 地図画像をダウンロード
            url = UnityWebRequest.UnEscapeURL(url);
            UnityWebRequest req = UnityWebRequestTexture.GetTexture(url);
            yield return req.SendWebRequest();

            // テクスチャ生成
            if (req.error == null)
                yield return StartCoroutine(UpdateSprite(req.downloadHandler.data));
            else
            {
                Debug.LogError(req.error);
                yield break;
            }
        }

        private IEnumerator UpdateSprite(byte[] data)
        {
            // テクスチャ生成
            Texture2D tex = new Texture2D(mapSize, mapSize);
            tex.LoadImage(data);
            if (tex == null) yield break;
            // スプライト（インスタンス）を明示的に開放
            if (mapImage.sprite != null)
            {
                Destroy(mapImage.sprite);
                yield return null;
                mapImage.sprite = null;
                yield return null;
            }
            // スプライト（インスタンス）を動的に生成
            mapImage.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        }

        //[ContextMenu("Test")]
        //private void Test()
        //{
        //    A a = new A();
        //    string json = JsonUtility.ToJson(a);
        //    Debug.Log(json);
        //}

        //[Serializable]
        //public class A
        //{
        //    public List<string> listStr = new List<string>
        //    {
        //        "ABC", "LONG", "TERRY"
        //    };
        //    public Dictionary<string, string> dict = new Dictionary<string, string>
        //    {
        //        { "name",   "ABC" },
        //        { "age",    "9" },
        //        { "class",  "Knight" },
        //    };
        //}
    }
}
