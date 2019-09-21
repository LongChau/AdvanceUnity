using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace UnityAdvance
{
    public class MergeMeshTutorial : MonoBehaviour
    {
        public GameObject model;

        public List<MeshFilter> listChildMeshFilters = new List<MeshFilter>();
        public List<MeshRenderer> listChildMeshRenderers = new List<MeshRenderer>();

        public Mesh myMesh;
        public MeshFilter myMeshFilter;
        public MeshRenderer myMeshRenderer;

        private List<Vector3> listLocalPos = new List<Vector3>();

        public List<Texture2D> _listTexs = new List<Texture2D>();

        public Material sourceMaterial;

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
                    model.transform.InverseTransformPoint(childMeshFilter.transform.position));
            }

            sourceMaterial = GetComponent<Material>();
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

        private string _textureCombinedFilePath = "CombinedTexture.png";
        private string _materialCombinedFilePath = "CombinedMaterial.mat";

        [ContextMenu("CreateCombinedTexture")]
        private void CreateCombinedTexture()
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
            File.WriteAllBytes(Application.dataPath + _textureCombinedFilePath, texContent);

            AssetDatabase.Refresh();
        }

        [ContextMenu("CreateCombineMaterial")]
        private void CreateCombineMaterial()
        {
            //Material mat = new Material(mat);
            // load từ dưới asset lên.
            
        }

        private void ApplyCombinedMesh()
        {
            //myMeshFilter.mesh = 

        }

        [ContextMenu("MyCombineTex")]
        private void MyCombineTex()
        {
            int width = 512 * _listTexs.Count;
            int height = 512;

            Texture2D newTex = new Texture2D(width, height);
            Color[] pixels = newTex.GetPixels();

            for (int texIndex = 0; texIndex < _listTexs.Count; texIndex++)
            {
                var sourceTex = _listTexs[texIndex];
                Color[] sourcePixels = sourceTex.GetPixels();

                for (int y = 0; y < 512; y++)
                {
                    for (int x = 0; x < 512; x++)
                    {
                        int newCol = x + (texIndex * 512);
                        pixels[y * width + newCol] = sourcePixels[y * height + x];
                    }
                }

                newTex.SetPixels(pixels);
            }

            var texContent = newTex.EncodeToPNG();
            string filePath = Path.Combine("Assets", _textureCombinedFilePath);
            Debug.Log($"{filePath}");
            File.WriteAllBytes(filePath, texContent );

            AssetDatabase.Refresh();
        }

        [ContextMenu("MyCombineMaterial")]
        private void MyCombineMaterial()
        {
            string filePath = Path.Combine("Assets", _textureCombinedFilePath);

            var combinedMaterial = new Material(sourceMaterial);
            var combinedTex = AssetDatabase.LoadAssetAtPath<Texture2D>(filePath);
            combinedMaterial.mainTexture = combinedTex;

            string materialFilePath = Path.Combine("Assets", _materialCombinedFilePath);

            AssetDatabase.CreateAsset(combinedMaterial, materialFilePath);
            AssetDatabase.Refresh();
        }

        private List<Vector2> uv = new List<Vector2>();

        [ContextMenu("MyApplyUV")]
        private void MyApplyUV(Mesh mesh, int textureIndex)
        {
            var sourceUV = mesh.uv;

            for (int i = 0; i < sourceUV.Length; i++)
            {
                var newUV = sourceUV[i];
                newUV.x = (textureIndex + newUV.x) / _listTexs.Count;
                uv.Add(newUV);
            }
        }
    }
}