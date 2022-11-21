using TKOU.SimAI.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TKOU.SimAI
{
    /// <summary>
    /// A generic game button.
    /// </summary>
    public class UIButtonGame : MonoBehaviour
    {
        #region Events

        public event System.Action<UIButtonGame> OnClick;

        #endregion Events

        #region Variables

        [SerializeField]
        private Button button;

        [SerializeField]
        private TextMeshProUGUI nameText;

        [SerializeField]
        private Image iconImage;

        private const string nullDataName = "Empty";

        private const Sprite nullDataSprite = null;

        #endregion Variables

        #region Properties

        public IAmData Data
        {
            get;
            private set;
        }

        #endregion Properties

        #region Unity methods

        private void Awake()
        {
            button.onClick.AddListener(Button_OnClick);
        }

        #endregion Unity methods

        #region Public methods

        public void SetData(IAmData data)
        {
            Data = data;

            UpdateUI();
        }

        #endregion Public methods

        #region Private methods

        #region Event callbacks

        private void Button_OnClick()
        {
            OnClick?.Invoke(this);
        }

        #endregion Event callbacks

        public void UpdateUI()
        {
            if (Data != null)
            {
                nameText.text = Data.DataName;

                iconImage.sprite = Data.DataIcon;

                nameText.gameObject.SetActive(string.IsNullOrEmpty(nameText.text) == false);

                iconImage.gameObject.SetActive(iconImage.sprite != null);
            }
            else
            {
                nameText.text = nullDataName;

                iconImage.sprite = nullDataSprite;

                nameText.gameObject.SetActive(true);

                iconImage.gameObject.SetActive(false);
            }
        }

        #endregion Private methods
    }
}