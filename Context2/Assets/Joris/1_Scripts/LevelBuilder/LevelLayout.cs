using System;
using System.Collections.Generic;
using UnityEngine;

namespace Joris
{
    [CreateAssetMenu(fileName = "Layout -", menuName = "Level/Layout")]
    public class LevelLayout : ScriptableObject
    {
        [field: SerializeField] public LevelTexture Texture { get; private set; }
        [field: SerializeField] public int GroundLevel { get; private set; }
        [field: SerializeField] public int TileLenght { get; private set; }

        private List<MeshFilter>[] _meshList;
        private MeshFilter[] _targetMesh;
        private Vector2Int _layoutSize;

        public void InitLayout()
        {
            _layoutSize = Texture.GetTextureSize();

            var variationAmount = Texture.GetTileSetVariationAmount();

            _meshList = new List<MeshFilter>[variationAmount];
            _targetMesh = new MeshFilter[variationAmount];

            for (int i = 0; i < variationAmount; i++)
            {
                _meshList[i] = new List<MeshFilter>();

                var combinedMeshInstance = new GameObject
                {
                    name = "CombinedMesh" + i
                };
                combinedMeshInstance.AddComponent<MeshFilter>();
                combinedMeshInstance.AddComponent<MeshRenderer>();

                combinedMeshInstance.GetComponent<MeshRenderer>().materials = Texture.GetTileSetVariationMaterial(i);

                _targetMesh[i] = combinedMeshInstance.GetComponent<MeshFilter>();
            }
        }

        public void AssignMesh(int ID, MeshFilter mesh)
        {
            _meshList[ID].Add(mesh);
        }

        public void CombineMeshes()
        {
            for (int i = 0; i < _targetMesh.Length; i++)
            {
                var combine = new CombineInstance[_meshList[i].Count];

                for (int m = 0; m < _meshList[i].Count; m++)
                {
                    combine[m].mesh = _meshList[i][m].sharedMesh;
                    combine[m].transform = _meshList[i][m].transform.localToWorldMatrix;
                }

                var mesh = new Mesh
                {
                    indexFormat = UnityEngine.Rendering.IndexFormat.UInt32
                };

                mesh.CombineMeshes(combine);

                _targetMesh[i].mesh = mesh;
            }
        }

        public void LoopLayout(Action<int, Vector2Int> action)
        {
            int l = 0;
            for (int y = 0; y < _layoutSize.y; y++)
            {
                for (int x = 0; x < _layoutSize.x; x++)
                {
                    action(l++, new Vector2Int(x, y));
                }
            }
        }

        public Vector3 TileToWorld(Vector2Int gridPos, float groundOffset = 0)
        {
            return new Vector3(
                -_layoutSize.x + TileLenght + gridPos.x * TileLenght - TileLenght / TileLenght,
                GroundLevel + groundOffset,
                _layoutSize.y - TileLenght - gridPos.y * TileLenght + TileLenght / TileLenght);
        }
    }
}
