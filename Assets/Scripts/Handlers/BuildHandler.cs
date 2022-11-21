using TKOU.SimAI.Interfaces;
using TKOU.SimAI.Levels.Buildings;
using TKOU.SimAI.Levels.Tiles;
using UnityEngine;

namespace TKOU.SimAI.Handlers
{
    /// <summary>
    /// Handles building in the game
    /// </summary>
    public class BuildHandler : IHaveBuildSelection
    {
        #region Variables

        private GameObject ghostGameObject;

        #endregion Variables

        #region Properties

        private IAmEntity buildTarget;

        public IAmEntity BuildTarget
        {
            get
            {
                return buildTarget;
            }

            set
            {
                if (buildTarget == value)
                {
                    return;
                }

                buildTarget = value;

                if (buildTarget is IHavePosition havePosition)
                {
                    UpdateGhostPosition(havePosition);
                }
            }
        }

        private IAmData buildSelection;

        public IAmData BuildSelection
        {
            get
            {
                return buildSelection;
            }

            set
            {
                if (buildSelection == value)
                {
                    return;
                }

                buildSelection = value;

                Debug.Log($"BuildSelection changed: {buildSelection}");

                UpdateGhostGO();
            }
        }

        #endregion Properties

        #region Public methods

        public void AttemptToBuildSelection()
        {
            if (ghostGameObject == null)
            {
                return;
            }

            if (BuildTarget == null)
            {
                return;
            }

            if (BuildSelection is BuildingData buildingData && BuildTarget is TileEntity tileEntity)
            {
                Building building = new Building(buildingData, tileEntity.Tile);

                tileEntity.Tile.AddObject(building);

                BuildingEntity.SpawnEntity(building);

                BuildSelection = null;
            }
        }

        #endregion Public methods

        #region Private methods

        private void UpdateGhostPosition(IHavePosition havePosition)
        {
            if (ghostGameObject == null)
            {
                return;
            }

            ghostGameObject.transform.position = havePosition.Position;
        }

        private void UpdateGhostGO()
        {
            if (ghostGameObject != null)
            {
                GameObject.Destroy(ghostGameObject);
            }

            if (buildSelection == null)
            {
                return;
            }

            CreateGhostGameObject(buildSelection);
        }

        private void CreateGhostGameObject(IAmData data)
        {
            if (data.EntityPrefab == null)
            {
                return;
            }

            Object prefab = data.EntityPrefab as Object;

            if (prefab == null)
            {
                return;
            }

            ghostGameObject = new GameObject();

            ghostGameObject.SetActive(false);

            Object instantiatedObject = GameObject.Instantiate(prefab, ghostGameObject.transform);

            Behaviour[] behaviours = ghostGameObject.GetComponentsInChildren<Behaviour>();

            for (int i = 0; i < behaviours.Length; i++)
            {
                GameObject.DestroyImmediate(behaviours[i]);
            }

            ghostGameObject.SetActive(true);
        }

        #endregion Private methods
    }
}