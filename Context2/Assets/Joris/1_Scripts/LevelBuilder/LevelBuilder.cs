using UnityEngine;

namespace Joris 
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private LevelLayout _layout;

        private void Awake()
        {
            _layout.InitLayout();

            _layout.LoopLayout((i, gridPos) =>
            {
                var tileData = _layout.Texture.GetTileData(gridPos);

                var tile = Instantiate(tileData.Model, transform).transform;
                    tile.localPosition = _layout.TileToWorld(gridPos);
                    tile.localRotation = Quaternion.identity;
                    tile.parent = null;
            });
        }
    }
}
