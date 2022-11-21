using TKOU.SimAI.Levels.Buildings;
using TKOU.SimAI.Levels.Tiles;
using UnityEngine;

namespace TKOU.SimAI
{
    /// <summary>
    /// Contains all game elements.
    /// </summary>
    [CreateAssetMenu(fileName = nameof(GameContents), menuName = nameof(SimAI) + "/" + nameof(GameContents))]
    public class GameContents : ScriptableObject
    {
        #region Variables

        public TileData[] tiles;
        public BuildingData[] buildings;

        #endregion Variables
    }
}