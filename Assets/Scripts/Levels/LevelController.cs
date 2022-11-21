using TKOU.SimAI.Interfaces;
using TKOU.SimAI.Levels.Generators;
using UnityEngine;

namespace TKOU.SimAI.Levels
{
    /// <summary>
    /// High-level controller for game levels, which uses <see cref="LevelGeneratorBasicLogic"/> 
    /// </summary>
    public class LevelController : MonoBehaviour
    {
        #region Variables

        private const int minLevelWidth = 1;
        private const int minLevelHeight = 1;

        #endregion Variables

        #region Properties

        /// <summary>
        /// Level that is currently running.
        /// </summary>
        [field: Header("State")]
        public Level CurrentLevel
        {
            get;
            private set;
        }

        /// <summary>
        /// True if a level is running. 
        /// False otherwise.
        /// </summary>
        public bool IsLevelRunning
        {
            get
            {
                return CurrentLevel != null;
            }
        }

        /// <summary>
        /// Current level generator to be used when a new level is created.
        /// </summary>
        [field: Header("Generation")]
        public IAmLevelGenerator[] LevelGenerators
        {
            get;
            private set;
        }

        public LevelGeneratorConfig LevelGeneratorConfig
        {
            get;
            private set;
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Set <see cref="LevelGeneratorConfig"/>.
        /// </summary>
        /// <param name="levelGeneratorConfig"></param>
        public void SetLevelGeneratorConfig(LevelGeneratorConfig levelGeneratorConfig)
        {
            if (levelGeneratorConfig == null)
            {
                Debug.LogError($"{nameof(levelGeneratorConfig)} can't be null!");

                return;
            }

            LevelGeneratorConfig = levelGeneratorConfig;
        }

        /// <summary>
        /// Set collection of <see cref="LevelGenerator"/>.
        /// </summary>
        /// <param name="levelGenerators"></param>
        public void SetLevelGenerators(LevelGenerator[] levelGenerators)
        {
            if (levelGenerators == null)
            {
                Debug.LogError($"{nameof(levelGenerators)} can't be null!");

                return;
            }

            LevelGenerators = levelGenerators;
        }

        /// <summary>
        ///  Creates a level using the configured <see cref="LevelGeneratorConfig"/> and <see cref="LevelGenerators"/>
        /// </summary>
        /// <returns>FALSE if generation failed. TRUE if it succeed</returns>
        public Level GenerateLevel()
        {
            if (LevelGenerators == null)
            {
                Debug.LogError($"{nameof(LevelGenerators)} can't be null!");

                return null;
            }

            Level level = CreateLevelBase(LevelGeneratorConfig);

            if (level == null)
            {
                Debug.LogError("Failed to create a level!");

                return null;
            }

            for (int i = 0; i < LevelGenerators.Length; i++)
            {
                IAmLevelGenerator levelGenerator = LevelGenerators[i];

                if (levelGenerator.IsLevelValid(level))
                {
                    levelGenerator.GenerateLevel(level);
                }
                else
                {
                    Debug.LogError($"current Level is not valid for the generator: {levelGenerator} !");
                }
            }

            return level;
        }

        public void RunLevel(Level level)
        {
            if (CurrentLevel != null)
            {
                Debug.LogError($"Can't run a level when one is already running! Call {nameof(StopLevel)} first.");

                return;
            }

            CurrentLevel = level;

            CurrentLevel.GenerateEntities();
        }

        public void StopLevel()
        {
            if (CurrentLevel == null)
            {
                Debug.LogError("Tried to stop level that is not running!");

                return;
            }

            CurrentLevel.Dispose();

            CurrentLevel = null;
        }

        #endregion Public methods

        #region Private methods

        private Level CreateLevelBase(LevelGeneratorConfig levelGeneratorConfig)
        {
            if (IsConfigurationValid(levelGeneratorConfig))
            {
                return new Level(levelGeneratorConfig);
            }
            else
            {
                Debug.LogError($"{nameof(levelGeneratorConfig)} is not valid!");

                return null;
            }
        }

        private bool IsConfigurationValid(LevelGeneratorConfig levelGeneratorConfig)
        {
            if (levelGeneratorConfig.Width < minLevelWidth)
            {
                Debug.LogError($"{nameof(levelGeneratorConfig.Width)} must be at least {minLevelWidth}");

                return false;
            }

            if (levelGeneratorConfig.Height < minLevelHeight)
            {
                Debug.LogError($"{nameof(levelGeneratorConfig.Height)} must be at least {minLevelHeight}");

                return false;
            }

            return true;
        }

        #endregion Private methods
    }
}