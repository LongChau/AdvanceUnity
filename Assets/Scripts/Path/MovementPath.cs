using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance
{
    /// <summary>
    /// Tong hop cac cach di chuyen
    /// </summary>
    public class MovementPath : MonoBehaviour
    {
        [SerializeField]
        private List<WaypointPath> _listPathPoints = new List<WaypointPath>();

        // 2 cach duoi deu duoc
        //public Vector3 this[int index] => _listPathPoints[index].transform.position;
        public Vector3 this[int index] => _pointPosition(index);
        private Vector3 _pointPosition(int index) => _listPathPoints[index].transform.position;

        public int PointCount => _listPathPoints.Count;

        public Vector3 LastPoint => _pointPosition(PointCount - 1);

        public List<WaypointPath> ListPathPoints { get => _listPathPoints; set => _listPathPoints = value; }
        public float Wide { get => wide; set => wide = value; }

        // Start is called before the first frame update
        void Start()
        {
        }

        [ContextMenu("GetAllWaypoints")]
        private void GetAllWaypoints()
        {
            Debug.Log("GetAllWaypoints()");

            _listPathPoints.Clear();

            //for (int pointIndex = 0; pointIndex < transform.childCount; pointIndex++)
            //{
            //    var waypoint = transform.GetChild(pointIndex).GetComponent<WaypointPath>();
            //    if (waypoint != null)
            //        _listPathPoints.Add(waypoint);
            //}

            var waypoints = GetComponentsInChildren<WaypointPath>();
            if (waypoints != null)
                _listPathPoints.AddRange(waypoints);
        }

        private void DrawPathLine()
        {
            Gizmos.color = Color.green;
            for (int pointIndex = 0; pointIndex < _listPathPoints.Count - 1; pointIndex++)
            {
                DrawSection(pointIndex);
            }
        }

        //private void DrawSection(int pointIndex)
        //{
        //    Gizmos.DrawLine(PointPosition(pointIndex), PointPosition(pointIndex + 1));
        //}

        private void OnDrawGizmos()
        {
            if (_listPathPoints != null)
                DrawPathLine();
        }

        [SerializeField]
        private float wide;
        private void DrawSection(int startIndex)
        {
            Vector3 startPoint = _pointPosition(startIndex);
            Vector3 endPoint = _pointPosition(startIndex + 1);
            Vector3 direction = (endPoint - startPoint).normalized;

            Gizmos.color = Color.white;

            DrawBound(startPoint, endPoint, direction, -wide);
            DrawBound(startPoint, endPoint, direction, wide);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(startPoint, endPoint);
        }

        private void DrawBound(Vector3 startPoint, Vector3 endPoint, Vector3 forward, float offset)
        {
            Vector3 right = Vector3.Cross(forward, Vector3.up);

            startPoint += right * offset;
            endPoint += right * offset;

            Gizmos.DrawLine(startPoint, endPoint);
        }
    }
}