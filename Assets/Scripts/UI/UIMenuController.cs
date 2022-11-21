using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace TKOU.SimAI.UI
{
    /// <summary>
    /// Menu controller.
    /// </summary>
    public class UIMenuController : MonoBehaviour
    {
        #region Variables

        [SerializeField]
        private GameController gameController;

        [SerializeField]
        private Button playButton;

        [SerializeField]
        private Button quitButton;

        [SerializeField]
        private GameObject mainMenuObject;

        [SerializeField]
        [Tooltip("Set to TRUE to automatically start the game on play. Useful for testing.")]
        private bool autostart = false;

        [SerializeField] MyButton[] buttonList;
        public int selectedButton = 0;

        public delegate void ButtonAction();

        #endregion Variables

        #region Unity methods

        private void Awake()
        {
            buttonList = new MyButton[2];

            buttonList[0].image = GameObject.Find("UI-Button-Play").GetComponent<Image>();
            buttonList[0].image.color = Color.yellow;
            buttonList[0].action = PlayButton_OnClick;

            buttonList[1].image = GameObject.Find("UI-Button-Quit").GetComponent<Image>();
            buttonList[1].image.color = Color.white;
            buttonList[1].action = QuitButton_OnClick;

            //Problem z tymi Inputami nie rozumiem do konca tego

            playButton.onClick.AddListener(PlayButton_OnClick);
            quitButton.onClick.AddListener(QuitButton_OnClick);

            gameController.OnGameRun += GameController_OnGameRun;
            gameController.OnGameEnd += GameController_OnGameEnd;
        }

        private void Start()
        {
            if (autostart)
            {
                gameController.RunGame();
            }
        }

        private void Update()
        {
            if (UnityEngine.Input.GetAxis("Vertical") > 0)
            {
                MoveToNextButton();
            }
            else if (UnityEngine.Input.GetAxis("Vertical") < 0)
            {
                MoveToPreviousButton();
            }
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                buttonList[selectedButton].action();
            }
        }

        #endregion Unity methods

        #region Private methods

        void MoveToNextButton()
        {
            buttonList[selectedButton].image.color = Color.white;
            selectedButton++;
            if (selectedButton >= buttonList.Length)
            {
                selectedButton = 0;
            }

            buttonList[selectedButton].image.color = Color.yellow;
        }

        void MoveToPreviousButton()
        {
            buttonList[selectedButton].image.color = Color.white;
            selectedButton--;
            if (selectedButton < 0)
            {
                selectedButton = (buttonList.Length - 1);
            }
            buttonList[selectedButton].image.color = Color.yellow;
        }

        #region Event callbacks

        private void QuitButton_OnClick()
        {
            Application.Quit();
        }

        private void PlayButton_OnClick()
        {
            gameController.RunGame();
        }

        private void GameController_OnGameRun()
        {
            UpdateVisibility();
        }

        private void GameController_OnGameEnd()
        {
            UpdateVisibility();
        }

        #endregion Event callbacks

        private void UpdateVisibility()
        {
            mainMenuObject.SetActive(!gameController.IsGameRunning);
        }


        #endregion Private methods

        [System.Serializable]
        public struct MyButton
        {
            public Image image;
            public ButtonAction action;
        }
    }
}