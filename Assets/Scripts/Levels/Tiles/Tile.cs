using System.Collections.Generic;
using System.Text;
using TKOU.SimAI.Interfaces;
using UnityEngine;

namespace TKOU.SimAI.Levels.Tiles
{
    /// <summary>
    /// Single tile of the level.
    /// </summary>
    public class Tile
    {
        #region Variables

        private List<IAmTileObject> tileObjects;

        #endregion Variables

        #region Properties

        public TileData TileData
        {
            get;
            private set;
        }

        public int X
        {
            get;
            private set;
        }

        public int Y
        {
            get;
            private set;
        }

        public Vector3 Position
        {
            get;
            private set;
        }

        #endregion Properties

        #region Constructors

        public Tile(TileData tileData, int x, int y, Vector3 position)
        {
            TileData = tileData;

            X = x;

            Y = y;

            Position = position;

            tileObjects = new List<IAmTileObject>();
        }

        #endregion Constructors

        #region Public methods

        public void AddObject(IAmTileObject tileObject)
        {
            tileObjects.Add(tileObject);
        }

        public void RemoveObject(IAmTileObject tileObject)
        {
            if (!tileObjects.Remove(tileObject))
            {
                Debug.LogError($"Failed to remove object : {tileObject.GetType()}. It doesn't exist on this tile!");
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"data:{TileData} x:{X} y:{Y} pos:{Position} tileObjects:");

            for (int i = 0; i < tileObjects.Count; i++)
            {
                stringBuilder.AppendLine($"[{i}] - {tileObjects[i]}");
            }

            return stringBuilder.ToString();
        }

        #endregion Public methods
    }
}