using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityAdvance
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class CubeBuilder : MonoBehaviour
    {
        private Mesh myMesh;

        [SerializeField]
        private List<Vector3> _listVertices = new List<Vector3>();

        [SerializeField]
        private int[] _arrIndices;

        [SerializeField]
        private MeshTopology _meshTopology;

        [SerializeField]
        private Material _material;

        [SerializeField]
        private Color _gizmoColor;

        [SerializeField]
        private MeshFilter meshFilter;
        [SerializeField]
        private MeshRenderer meshRenderer;

        [SerializeField]
        private Vector2[] _arrUV;

        // This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only)
        private void OnValidate()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();

            BuildAndShowCube();
        }

        // Start is called before the first frame update
        void Start()
        {
            BuildAndShowCube();
        }

        // có thể sử dụng bàng cách click chuột phải vào component
        [ContextMenu("BuildAndShowCube")]
        private void BuildAndShowCube()
        {
            BuildCube();
            ShowCube();
        }

        private void BuildCube()
        {
            myMesh = new Mesh();
            myMesh.name = "MyMesh";

            // bottom
            // top
            // front
            // back
            // left
            // right

            myMesh.SetVertices(_listVertices);

            // also this can be used
            //myMesh.triangles = new int[]
            //{
            //    0, 1, 2,
            //    0, 2, 3,
            //};

            myMesh.SetIndices(_arrIndices, _meshTopology, 0);

            myMesh.uv = _arrUV;

            //myMesh.RecalculateNormals();
        }

        private void ShowCube()
        {
            //var meshFilter = gameObject.AddComponent<MeshFilter>();
            meshFilter.mesh = myMesh;
            //var meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshRenderer.material = _material;
        }

        private void OnDrawGizmos()
        {
            if (_listVertices == null)
                return;
            Gizmos.color = _gizmoColor;

            // cách 2 chuyển từ local => world. Chỉ chạy trên Editor thôi
            Handles.matrix = transform.localToWorldMatrix;
            //

            for (int i = 0; i < _listVertices.Count; i++)
            {
                // Cthuc chuyển từ local => global.  Cách 1: có thể dùng cho runtime
                //var globalPosition = transform.TransformPoint(_listVertices[i]);
                //Handles.Label(globalPosition, i.ToString());
                //

                // cách 2 chuyển từ local => world
                Handles.Label(_listVertices[i], i.ToString());
                //
            }

            // cách 2: phải nhả transform
            Handles.matrix = Matrix4x4.identity;
            //
        }
    }
}
