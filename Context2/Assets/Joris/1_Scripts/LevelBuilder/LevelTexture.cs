using System;
using UnityEngine;

namespace Joris
{
    [CreateAssetMenu(fileName = "Texture -", menuName = "Level/Texture")]
    public class LevelTexture : ScriptableObject
    {
        [SerializeField] private Texture2D _colorMap;
        [SerializeField] private LevelTileSet _tileSet;

        public Vector2Int GetTextureSize()
        {
            return new Vector2Int(_colorMap.width, _colorMap.height);
        }

        public int GetTileSetVariationAmount()
        {
            return _tileSet.TileVariations.Count;
        }

        public Material[] GetTileSetVariationMaterial(int index)
        {
            var key = _tileSet.keyIndex[index];

            if (_tileSet.TileVariations.TryGetValue(key, out var tileData))
                return tileData.Model.GetComponent<MeshRenderer>().sharedMaterials;

            throw new Exception("VariationData could not be extracted from dictionary");
        }

        public TileData GetTileData(Vector2Int pixelPos)
        {
            var colorCode = _colorMap.GetPixel(pixelPos.x, pixelPos.y);

            if (_tileSet.TileVariations.TryGetValue(colorCode, out var tileData))
                return tileData;

            throw new Exception("VariationData could not be extracted from dictionary");
        }
    }
}
