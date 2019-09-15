using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
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
            listLocalPos.Clear();

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
                var mesh = listChildMeshFilters[i].sharedMesh;

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

        private void CreateCombinedMaterial()
        {
            int width = 512 * listChildMeshFilters.Count;

            var sourceRender = model.GetComponentInChildren<MeshRenderer>();
            myMeshRenderer.sharedMaterial = sourceRender?.sharedMaterial;

            var sourceTex = sourceRender.sharedMaterial.mainTexture as Texture2D;
            Color[] sourcePixels = sourceTex.GetPixels();

            Material newMat = Instantiate(sourceRender?.sharedMaterial);
            Texture2D newTex = Instantiate(newMat.mainTexture) as Texture2D;

            Color[] pixels = newTex.GetPixels();

            for (int i = 0; i < listChildMeshFilters.Count; i++)
            {
                for (int y = 0; y < 512; y++)
                {
                    for (int x = 0; x < 512; x++)
                    {
                        int newCol = x + (i * 512);
                        pixels[y * width + newCol] = sourcePixels[x * i];
                    }
                }
            }

            var texContent = newTex.EncodeToPNG();
            File.WriteAllBytes(Application.dataPath + "/Combined.png", texContent);

            AssetDatabase.Refresh();
        }
    }
}