using UnityEngine;

namespace TKOU.SimAI.Interfaces
{
    /// <summary>
    /// All datas should use this interface.
    /// </summary>
    public interface IAmData
    {
        #region Properties

        /// <summary>
        /// Sprite representing this data for UI.
        /// </summary>
        public Sprite DataIcon
        {
            get;
        }

        /// <summary>
        /// Name representing this data.
        /// </summary>
        public string DataName
        {
            get;
        }

        /// <summary>
        /// The prefab for this data, if any
        /// </summary>
        public IAmEntity EntityPrefab
        {
            get;
        }

        #endregion Properties
    }
}