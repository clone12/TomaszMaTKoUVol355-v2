using System;
using TKOU.SimAI.Camera;
using TKOU.SimAI.Interfaces;
using TKOU.SimAI.Levels;
using TKOU.SimAI.Levels.Generators;
using TKOU.SimAI.Player;
using UnityEngine;

namespace TKOU.SimAI
{
    /// <summary>
    /// Main controller of the game.
    /// </summary>
    public class GameController : MonoBehaviour, IGameController
    {
        #region Events

        /// <summary>
        /// Called when the game is being run.
        /// </summary>
        public event Action OnGameRun;

        /// <summary>
        /// Called when the game is stopped.
        /// </summary>
        public event Action OnGameEnd;

        #endregion Events

        #region Variables

        [SerializeField, Tooltip("If set to true will start the game automatically.")]
        private bool autorun = true;

        [Header("Controllers")]

        [SerializeField]
        private GameCamera gameCamera;

        [SerializeField]
        private PlayerController playerController;

        [Header("Level")]
        [SerializeField, Tooltip("Level controller to use by the game.")]
        private LevelController levelController;

        [SerializeField, Tooltip("Level generators to use for the generation.")]
        private LevelGenerator[] levelGenerators;

        [SerializeField]
        private Vector3 singleTileSize = new Vector3(30.0f, 5.0f, 30.0f);

        [SerializeField]
        private int startLevelWidth;

        [SerializeField]
        private int startLevelHeight;

        #endregion Variables

        #region Properties

        /// <summary>
        /// Game content database.
        /// </summary>
        [field: SerializeField]
        public GameContents GameContents
        {
            get;
            private set;
        }

        /// <summary>
        /// True if game is currently running.
        /// </summary>
        public bool IsGameRunning
        {
            get;
            private set;
        }

        #endregion Properties

        #region Unity methods

        private void Start()
        {
            if (autorun)
            {
                RunGame();
            }
        }

        #endregion Unity methods

        #region Public methods

        public void RunGame()
        {
            if (IsGameRunning)
            {
                Debug.LogError("Tried to run game that is already running!");

                return;
            }

            levelController.SetLevelGeneratorConfig(new LevelGeneratorConfig(singleTileSize, -singleTileSize * 0.5f, startLevelWidth, startLevelHeight));

            levelController.SetLevelGenerators(levelGenerators);

            levelController.RunLevel(levelController.GenerateLevel());

            gameCamera.SetBounds(levelController.LevelGeneratorConfig.LevelMinPosition, levelController.LevelGeneratorConfig.LevelMaxPosition);

            playerController.Initialize(gameCamera);

            IsGameRunning = true;

            OnGameRun?.Invoke();
        }

        public void StopGame()
        {
            if (!IsGameRunning)
            {
                Debug.LogError("Tried to stop game that is not running!");

                return;
            }

            levelController.StopLevel();

            IsGameRunning = false;

            OnGameEnd?.Invoke();
        }

        #endregion Public methods
    }
}