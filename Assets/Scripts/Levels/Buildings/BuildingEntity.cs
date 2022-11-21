using TKOU.SimAI.Interfaces;
using UnityEngine;

namespace TKOU.SimAI.Levels.Buildings
{
    /// <summary>
    /// Visualisation of the <see cref="Building"/> object.
    /// </summary>
    public class BuildingEntity : MonoBehaviour, IAmEntity
    {
        #region Properties

        public Building Building
        {
            get;
            private set;
        }

        #endregion Properties

        #region Public methods

        public static BuildingEntity SpawnEntity(Building ownerBuilding)
        {
            BuildingData buildingData = ownerBuilding.BuildingData;

            if (buildingData == null)
            {
                Debug.LogError($"Can't spawn a building, {nameof(buildingData.BuildingEntityPrefab)} is null!");

                return null;
            }

            BuildingEntity buildingEntity = Instantiate(buildingData.BuildingEntityPrefab);

            buildingEntity.Initialize(ownerBuilding);

            return buildingEntity;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        #endregion Public methods

        #region Private methods

        private void Initialize(Building building)
        {
            Building = building;

            transform.position = building.Tile.Position;
        }

        #endregion Private methods
    }
}