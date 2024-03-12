using UnityEngine;

namespace Joris 
{
    //[ExecuteInEditMode]
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private LevelLayout _layout;

        private void Awake()
        {
            _layout.InitLayout();

            _layout.LoopLayout((i, gridPos) =>
            {
                var tileData = _layout.Texture.GetTileData(gridPos);

                var tile = Instantiate(tileData.Model, transform);
                tile.SetActive(false);

                _layout.AssignMesh(tileData.MeshID, tile.GetComponent<MeshFilter>());

                var tileTRS = tile.transform;
                tileTRS.localPosition = _layout.TileToWorld(gridPos, tileData.GroundOffset);
                tileTRS.localRotation = Quaternion.identity;
            });

            _layout.CombineMeshes();
        }
    }
}
