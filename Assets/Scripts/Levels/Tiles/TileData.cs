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

        public int TileCost
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

        int IAmData.DataCost
        {
            get
            {
                return TileCost;
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