using TKOU.SimAI.Interfaces;
using UnityEngine;

namespace TKOU.SimAI.Levels.Tiles
{
    /// <summary>
    /// Data of a single tile.
    /// </summary>
    [CreateAssetMenu(fileName = nameof(TileData), menuName = nameof(SimAI) + "/" + nameof(SimAI.Levels) + "/" + nameof(TileData))]
    public class TileData : ScriptableObject, IAmData
    {
        #region Properties

        [field: SerializeField]
        public string TileName
        {
            get;
            private set;
        }

        [field: SerializeField]
        public Sprite TileIcon
        {
            get;
            private set;
        }

        [field: SerializeField]
        public TileEntity TileEntityPrefab
        {
            get;
            private set;
        }

        string IAmData.DataName
        {
            get
            {
                return TileName;
            }
        }

        Sprite IAmData.DataIcon
        {
            get
            {
                return TileIcon;
            }
        }

        IAmEntity IAmData.EntityPrefab
        {
            get
            {
                return TileEntityPrefab;
            }
        }

        #endregion Properties
    }
}