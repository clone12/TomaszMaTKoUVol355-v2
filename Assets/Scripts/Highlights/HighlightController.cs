using System.Collections.Generic;
using TKOU.SimAI.Interfaces;
using UnityEngine;

namespace TKOU.SimAI.Highlights
{
    /// <summary>
    /// Handles highlighting objects
    /// </summary>
    public class HighlightController : MonoBehaviour
    {
        #region Variables

        [SerializeField]
        private Material defaultHighlightMaterial;

        private Dictionary<IAmEntity, Highlight> highlights;

        #endregion Variables

        #region Unity methods

        private void Awake()
        {
            highlights = new Dictionary<IAmEntity, Highlight>();
        }

        #endregion Unity methods

        #region Public methods

        public bool IsHighlighted(IAmEntity entity)
        {
            return highlights.ContainsKey(entity);
        }

        public void Highlight(IAmEntity entity)
        {
            MonoBehaviour monoBehaviour = entity as MonoBehaviour;

            if (monoBehaviour == null)
            {
                Debug.LogError($"Tried to highlight unsupported entity: {entity}");
                return;
            }

            Highlight highlight = Highlights.Highlight.CreateHighlight(transform, DefaultMaterialToHighlightMaterial);

            highlight.Target = monoBehaviour.transform;

            highlights.Add(entity, highlight);
        }

        public void ClearHighlight(IAmEntity entity)
        {
            if (highlights.TryGetValue(entity, out Highlight highlight))
            {
                highlight.Destroy();
                highlights.Remove(entity);
            }
        }

        public void ClearAllHighlights()
        {
            foreach (KeyValuePair<IAmEntity, Highlight> pair in highlights)
            {
                pair.Value.Destroy();
            }

            highlights.Clear();
        }

        #endregion Public methods

        #region Private methods

        private Material DefaultMaterialToHighlightMaterial(Material materialToMap)
        {
            return defaultHighlightMaterial;
        }

        #endregion Private methods
    }
}