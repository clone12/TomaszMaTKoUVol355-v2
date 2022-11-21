using System.Collections.Generic;
using UnityEngine;

namespace TKOU.SimAI.Highlights
{
    /// <summary>
    /// Highlights a target.
    /// </summary>
    public class Highlight : MonoBehaviour
    {
        #region Types

        /// <summary>
        /// Converter function that maps a given material to a highlight material to be used.
        /// </summary>
        /// <param name="materialToMap"></param>
        /// <returns></returns>
        public delegate Material MaterialToHighlightMaterialMapper(Material materialToMap);

        #endregion Types

        #region Variables

        private const string highlightDefaultName = "Highlight";

        private const string meshRendererDefaultName = "MeshRenderer";

        private Transform transf;

        private List<MeshFilter> meshFilters;

        private List<MeshRenderer> meshRenderers;

        private MaterialToHighlightMaterialMapper materialMapper;

        #endregion Variables

        #region Properties

        private Transform target;

        /// <summary>
        /// Target of the highlight
        /// </summary>
        public Transform Target
        {
            get
            {
                return target;
            }

            set
            {
                if (target == value)
                {
                    return;
                }

                target = value;

                if (target == null)
                {
                    ClearHighlightRenderers();
                }

                UpdateHighlightRenderers(target);
            }
        }

        #endregion Properties

        #region Unity methods

        private void Awake()
        {
            transf = transform;

            meshFilters = new List<MeshFilter>();

            meshRenderers = new List<MeshRenderer>();
        }

        private void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            transf.position = target.position;

            transf.rotation = target.rotation;
        }

        #endregion Unity methods

        #region Public methods

        /// <summary>
        /// Creates a highlight GameObject.
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static Highlight CreateHighlight(Transform parent, MaterialToHighlightMaterialMapper materialMapper)
        {
            GameObject gameObject = new GameObject(highlightDefaultName);

            gameObject.transform.SetParent(parent);

            Highlight highlight = gameObject.AddComponent<Highlight>();

            highlight.materialMapper = materialMapper;

            return highlight;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        #endregion Public methods

        #region Private methods

        private void ClearHighlightRenderers()
        {
            for (int i = 0; i < meshRenderers.Count; i++)
            {
                Destroy(meshRenderers[i].gameObject);
            }

            meshRenderers.Clear();
        }

        private void UpdateHighlightRenderers(Transform target)
        {
            MeshRenderer[] targetMeshRenderers = target.GetComponentsInChildren<MeshRenderer>();

            AddOrRemoveMeshRendersToAmount(targetMeshRenderers.Length);

            transf.position = target.position;

            transf.rotation = target.rotation;

            for (int i = 0; i < targetMeshRenderers.Length; i++)
            {
                MeshFilter targetMeshFilter = targetMeshRenderers[i].GetComponent<MeshFilter>();

                Transform targetTransform = targetMeshRenderers[i].transform;

                Transform meshFilterTransform = meshFilters[i].transform;
                meshFilters[i].sharedMesh = targetMeshFilter.sharedMesh;
                meshRenderers[i].sharedMaterial = materialMapper(targetMeshRenderers[i].sharedMaterial);

                meshFilterTransform.localPosition = targetTransform.localPosition;
                meshFilterTransform.localRotation = targetTransform.localRotation;
            }
        }

        private void AddOrRemoveMeshRendersToAmount(int amountNeeded)
        {
            //Adding
            int amountToAdd = amountNeeded - meshRenderers.Count;

            if (amountToAdd > 0)
            {
                for (int i = 0; i < amountToAdd; i++)
                {
                    GameObject gameObject = new GameObject(meshRendererDefaultName);

                    gameObject.transform.SetParent(transform);

                    MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

                    MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

                    meshFilters.Add(meshFilter);

                    meshRenderers.Add(meshRenderer);
                }

                //We can return, since it is impossible to need removal when we needed new mesh renderers.
                return;
            }

            //Removing
            int amountToRemove = meshRenderers.Count - amountNeeded;

            if (amountToRemove > 0)
            {
                for (int i = 0; i < amountToRemove; i++)
                {
                    Destroy(meshFilters[i].gameObject);
                }

                meshFilters.RemoveRange(0, amountToRemove);

                meshRenderers.RemoveRange(0, amountToRemove);
            }
        }

        #endregion Private methods
    }
}