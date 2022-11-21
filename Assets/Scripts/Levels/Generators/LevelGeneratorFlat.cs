using TKOU.SimAI.Levels.Tiles;
using UnityEngine;

namespace TKOU.SimAI.Levels.Generators
{
    /// <summary>
    /// Generates a flat tiled level.
    /// </summary>
    [CreateAssetMenu(fileName = nameof(LevelGeneratorFlat), menuName = nameof(SimAI) + "/" + nameof(SimAI.Levels) + "/" + nameof(LevelGeneratorFlat))]
    public class LevelGeneratorFlat : LevelGenerator
    {
        #region Variables

        [SerializeField, HideInInspector]
        private LevelGeneratorBasicLogic generationBasicLogic;

        [SerializeField, Tooltip("Tile to use for the terrain")]
        private TileData tileData;

        #endregion Variables

        #region Properties

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override int MinLevelWidth
        {
            get;
        } = 8;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override int MinLevelHeight
        {
            get;
        } = 8;

        #endregion Properties

        #region Unity methods

        private void Awake()
        {
            generationBasicLogic = new LevelGeneratorBasicLogic(MinLevelWidth, MinLevelHeight);
        }

        #endregion Unity methods

        #region Public methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="level"></param>
        public override void GenerateLevel(Level level)
        {
            Tile[] tiles = level.Tiles;

            for (int i = 0; i < tiles.Length; i++)
            {
                level.SetTile(i, tileData);
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="level"></param>
        public override bool IsLevelValid(Level level)
        {
            return generationBasicLogic.IsLevelValid(level);
        }

        #endregion Public methods
    }
}