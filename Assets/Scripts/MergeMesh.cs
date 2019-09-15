using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance
{
    public class MergeMesh : MonoBehaviour
    {
        public GameObject model;

        public List<MeshFilter> listChildMeshFilters = new List<MeshFilter>();
        public List<MeshRenderer> listChildMeshRenderers = new List<MeshRenderer>();

        public Mesh myMesh;
        public MeshFilter myMeshFilter;
        public MeshRenderer myMeshRenderer;

        private List<Vector3> listLocalPos = new List<Vector3>();

        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        void Init()
        {
            GetData();
        }

        [ContextMenu("GetData")]
        private void GetData()
        {
            listChildMeshFilters.Clear();
            listChildMeshRenderers.Clear();

            for (int i = 0; i < model.transform.childCount; i++)
            {
                var child = model.transform.GetChild(i);

                var childMeshFilter = child.GetComponent<MeshFilter>();
                listChildMeshFilters.Add(childMeshFilter);

                var childMeshRenderer = child.GetComponent<MeshRenderer>();
                listChildMeshRenderers.Add(childMeshRenderer);

                listLocalPos.Add(
                    // Chuyển từ local position => target position
                    // nếu childMeshFilter.transform là con của model.transform thì
                    // sẽ có position ra sao.
                    model.transform.InverseTransformPoint(childMeshFilter.transform.position) );
            }
        }

        [ContextMenu("MergeMeshTogether")]
        private void MergeMeshTogether()
        {
            myMesh = new Mesh();

            List<Vector3> listVertices = new List<Vector3>();
            List<int> listIndices = new List<int>();
            List<Vector2> listUVs = new List<Vector2>();
            List<Vector3> listNormals = new List<Vector3>();

            int totalIndices = 0;

            int posIndex = 0;

            for (int i = 0; i < listChildMeshFilters.Count; i++)
            {
                var mesh = listChildMeshFilters[i].mesh;

                foreach (var vertice in mesh.vertices)
                {
                    listVertices.Add(vertice + listLocalPos[posIndex]);
                }
                
                foreach (var index in mesh.GetIndices(0))
                {
                    int correctIndex = index + totalIndices;
                    listIndices.Add(correctIndex);
                }

                listUVs.AddRange(mesh.uv);
                listNormals.AddRange(mesh.normals);

                //totalIndices += (int)mesh.GetIndexCount(0);
                totalIndices += (int)mesh.vertices.Length;

                posIndex++;
            }

            myMesh.SetVertices(listVertices);
            //myMesh.SetIndices(listIndices.ToArray(), MeshTopology.Triangles, 0);
            myMesh.triangles = listIndices.ToArray();

            //myMesh.SetUVs(0, listUVs);
            myMesh.uv = listUVs.ToArray();
            myMesh.SetNormals(listNormals);
            //myMesh.RecalculateNormals();

            myMeshFilter.mesh = myMesh;
        }
    }
}