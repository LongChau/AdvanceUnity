using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGPS.Manager
{
    public class GPSManager : MonoBehaviour
    {
        private GPS_Point _currentLocation;

        private static GPSManager _instance;
        public static GPSManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GPSManager>();
                    if (_instance == null)
                    {
                        
                    }

                    return _instance;
                }
                else return _instance;
            }
        }

        public GPS_Point CurrentLocation => _currentLocation;

        public bool IsDontDestroy;

        [HideInInspector] public bool IsFinishLocationRequest;

        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            _instance = this;
            if (IsDontDestroy) DontDestroyOnLoad(gameObject);
        }

        IEnumerator Start()
        {
#if UNITY_EDITOR
            _currentLocation = new GPS_Point
            {
                latitude = 10.770359792327454f,
                longitude = 106.6879248095558f,
                altitude = 0.0f,
                horizontalAccuracy = 0.0f,
                timestamp = 0.0f
            };

            IsFinishLocationRequest = true;
            Debug.Log("Finish GPSManager.Start()");
            yield return true;
#else
            // First, check if user has location service enabled
            if (!Input.location.isEnabledByUser) yield break;

            // Start service before querying location
            Input.location.Start();

            // Wait until service initializes
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            // Service didn't initialize in 20 seconds
            if (maxWait < 1)
            {
                print("Timed out");
                IsFinishLocationRequest = false;
                yield break;
            }

            // Connection has failed
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                print("Unable to determine device location");
                IsFinishLocationRequest = false;
                yield break;
            }
            else
            {
                // Access granted and location value could be retrieved
                _currentLocation = new GPS_Point
                {
                    latitude            = Input.location.lastData.latitude,
                    longitude           = Input.location.lastData.longitude,
                    altitude            = Input.location.lastData.altitude,
                    horizontalAccuracy  = Input.location.lastData.horizontalAccuracy,
                    timestamp           = Input.location.lastData.timestamp
                };

                print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
                IsFinishLocationRequest = true;
            }

            // Stop service if there is no need to query location updates continuously
            Input.location.Stop();
            Debug.Log("Finish GPSManager.Start()");
#endif
        }
    }

    public struct GPS_Point
    {
        public float    latitude;
        public float    longitude;
        public float    altitude;
        public float    horizontalAccuracy;
        public double   timestamp;
    }
}
