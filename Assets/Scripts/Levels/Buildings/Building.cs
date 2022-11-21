using System;
using TKOU.SimAI.Interfaces;
using TKOU.SimAI.Levels.Tiles;

namespace TKOU.SimAI.Levels.Buildings
{
    /// <summary>
    /// Single building that can be placed on a tile
    /// </summary>
    public class Building : IAmTileObject
    {
        #region Properties

        public Type Type
        {
            get
            {
                return typeof(Building);
            }
        }

        public Tile Tile
        {
            get;
            private set;
        }

        public BuildingData BuildingData
        {
            get;
            private set;
        }

        #endregion Properties

        #region Constructors

        public Building(BuildingData buildingData, Tile tile)
        {
            BuildingData = buildingData;

            Tile = tile;
        }

        #endregion Constructors

        #region Private methods

        public override string ToString()
        {
            return $"data:{BuildingData}";
        }

        #endregion Private methods
    }
}