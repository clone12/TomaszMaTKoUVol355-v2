using System;
using TKOU.SimAI.Interfaces;
using UnityEngine;

namespace TKOU.SimAI.Levels.Generators
{
    /// <summary>
    /// Base class for level generators that contains some of the basic functionalities.
    /// </summary>
    [Serializable]
    public class LevelGeneratorBasicLogic : IAmLevelGenerator
    {
        #region Properties

        [field: SerializeField]
        public int MinLevelWidth
        {
            get;
        }

        [field: SerializeField]
        public int MinLevelHeight
        {
            get;
        }

        #endregion Properties

        #region Constructors

        public LevelGeneratorBasicLogic(int minLevelWidth, int minLevelHeight)
        {
            MinLevelWidth = minLevelWidth;

            MinLevelHeight = minLevelHeight;
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// <inheritdoc>/>
        /// </summary>
        /// <param name="level"></param>
        public void GenerateLevel(Level level)
        {
            if (level == null)
            {
                Debug.LogError($"{nameof(level)} is null!");

                return;
            }
            else
            {
                if (!IsLevelValid(level))
                {
                    Debug.LogError($"{nameof(level.LevelGeneratorConfig)} is not valid!");

                    return;
                }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool IsLevelValid(Level level)
        {
            if (level.LevelGeneratorConfig.Width < MinLevelWidth)
            {
                Debug.LogError($"{nameof(level.LevelGeneratorConfig.Width)} must be at least {MinLevelWidth}");

                return false;
            }

            if (level.LevelGeneratorConfig.Height < MinLevelHeight)
            {
                Debug.LogError($"{nameof(level.LevelGeneratorConfig.Height)} must be at least {MinLevelHeight}");

                return false;
            }

            return true;
        }

        #endregion Public methods
    }
}