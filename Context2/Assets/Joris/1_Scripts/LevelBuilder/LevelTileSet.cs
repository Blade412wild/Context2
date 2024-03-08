using System;
using System.Collections.Generic;
using UnityEngine;

namespace Joris
{
    [CreateAssetMenu(fileName = "TileSet -", menuName = "Level/TileSet")]
    public class LevelTileSet : ScriptableObject
    {
        public TileSetDictionary TileVariations;

        [HideInInspector]
        public List<Color> keyIndex;

        private void OnValidate()
        {
            keyIndex = new List<Color>();

            var id = 0;

            foreach(var tileset in TileVariations)
            {
                keyIndex.Add(tileset.Key);
                tileset.Value.MeshID = id;
                id++;
            }
        }
    }

    [Serializable]
    public class TileData
    {
        [HideInInspector]
        public int MeshID;

        public GameObject Model;
        public float GroundOffset;
    }

    [Serializable]
    public class ColorVariationDataTuple : SerializableKeyValuePair<Color, TileData>
    {
        public ColorVariationDataTuple(Color item1, TileData item2) : base(item1, item2) { }
    }

    [Serializable]
    public class TileSetDictionary : SerializableDictionary<Color, TileData>
    {
        [SerializeField] private List<ColorVariationDataTuple> _pairs = new List<ColorVariationDataTuple>();

        protected override List<SerializableKeyValuePair<Color, TileData>> _keyValuePairs
        {
            get
            {
                var list = new List<SerializableKeyValuePair<Color, TileData>>();
                foreach (var pair in _pairs)
                {
                    list.Add(new SerializableKeyValuePair<Color, TileData>(pair.Key, pair.Value));
                }
                return list;
            }

            set
            {
                _pairs.Clear();
                foreach (var kvp in value)
                {
                    _pairs.Add(new ColorVariationDataTuple(kvp.Key, kvp.Value));
                }
            }
        }
    }
}
