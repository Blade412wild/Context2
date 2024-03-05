using System;
using UnityEngine;

namespace Joris
{
    [CreateAssetMenu(fileName = "Layout -", menuName = "Level/Layout")]
    public class LevelLayout : ScriptableObject
    {
        [field: SerializeField] public LayoutTexture Texture { get; private set; }
        private Vector2Int _layoutSize;

        public int GroundLevel;
        public int TileLenght;

        public void InitLayout()
        {
            _layoutSize = Texture.GetTextureSize();
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

        public Vector3 TileToWorld(Vector2Int gridPos)
        {
            return new Vector3Int(
                -_layoutSize.x + TileLenght + gridPos.x * TileLenght - TileLenght / TileLenght,
                GroundLevel,
                _layoutSize.y - TileLenght - gridPos.y * TileLenght + TileLenght / TileLenght);
        }
    }
}
