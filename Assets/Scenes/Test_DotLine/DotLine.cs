using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGPS.Manager
{
    public class DotLine : MonoBehaviour
    {
        public LineRenderer lineRenderer;
        public float width = 0.2f;
        public float intervalCheckTime;
        public float thresholdLine = 0.5f;

        public Vector3 previousPosition;
        public Vector3 CurrentPosition => transform.position;

        public float _minorDistance = 0.0f;

        public GameObject dotPrefab;    // temp
        public List<Vector3> listDots = new List<Vector3>();

        private IEnumerator _checkDot;
        private int _dotCount = 0;

        // Start is called before the first frame update
        void Start()
        {
            previousPosition = transform.position;
            listDots.Add(previousPosition);
            lineRenderer.SetPosition(_dotCount, previousPosition);

            _checkDot = IECheckDot();
            StartCoroutine(_checkDot);
        }

        private IEnumerator IECheckDot()
        {
            var waitInterval = new WaitForSeconds(intervalCheckTime);
            var waitDots = new WaitForSecondsRealtime(thresholdLine);
            while (true)
            {
                float distance = Vector2.Distance(CurrentPosition, previousPosition);
                bool isSimilar = Mathf.Approximately(distance, _minorDistance);

                if (!isSimilar)
                {
                    Debug.Log($"Check at {CurrentPosition}");

                    previousPosition = CurrentPosition;

                    var dot = Instantiate(dotPrefab);
                    dot.transform.position = CurrentPosition;
                    _dotCount++;
                    
                    listDots.Add(CurrentPosition);

                    var posCount = _dotCount + 1;
                    lineRenderer.startWidth = width;
                    lineRenderer.endWidth = width;
                    lineRenderer.positionCount = posCount;
                    lineRenderer.SetPosition(_dotCount, CurrentPosition);
                    //lineRenderer.SetPositions(listDots.ToArray());

                    yield return waitInterval;

                    if (_dotCount % 3 == 0) yield return waitDots;
                }

                yield return null;
            }
        }

        private void OnDestroy()
        {
            if (_checkDot != null) StopCoroutine(_checkDot);
        }
    }
}
