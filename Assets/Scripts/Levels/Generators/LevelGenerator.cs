using TKOU.SimAI.Interfaces;
using UnityEngine;

namespace TKOU.SimAI.Levels.Generators
{
    /// <summary>
    /// Base class for level generators.
    /// </summary>
    public abstract class LevelGenerator : ScriptableObject, IAmLevelGenerator
    {
        #region Properties

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public abstract int MinLevelWidth { get; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public abstract int MinLevelHeight { get; }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public abstract bool IsLevelValid(Level level);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public abstract void GenerateLevel(Level level);

        #endregion Public methods
    }
}