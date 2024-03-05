using System;
using UnityEngine;

namespace Joris
{
    [Serializable]
    public class LayoutTexture
    {
        [SerializeField] private LevelTexture _tileMap;

        public Vector2Int GetTextureSize()
        {
            return _tileMap.GetTextureSize();
        }

        public TileData GetTileData(Vector2Int gridPos)
        {
            var pixelColor = _tileMap.GetPixelColor(gridPos.x, gridPos.y);
            var variationData = _tileMap.GetTileData(pixelColor);
            return variationData;
        }
    }

    [CreateAssetMenu(fileName = "Texture -", menuName = "Level/Texture")]
    public class LevelTexture : ScriptableObject
    {
        [SerializeField] private Texture2D _colorMap;
        [SerializeField] private LevelTileSet _tileSet;

        public Vector2Int GetTextureSize()
        {
            return new Vector2Int(_colorMap.width, _colorMap.height);
        }

        public TileData GetTileData(Color colorCode)
        {
            if (_tileSet.TileVariations.TryGetValue(colorCode, out var tileData))
                return tileData;

            throw new Exception("VariationData could not be extracted from dictionary");
        }

        public Color GetPixelColor(int x, int y)
        {
            return _colorMap.GetPixel(x, y);
        }
    }
}
