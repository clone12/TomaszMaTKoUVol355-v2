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

        #region Variables

        Income income;

        #endregion

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
            // Tutaj powinna znaleźć sie implementacja sumowania każdego postawionego budynku oraz przychodu. 
            //Sumując po każdym utworzonym budynku i zysk 5 sekundowy i dodawać koszt danego budynku do wydanych pieniedzy
            // ale przez brak wykonania wczesniejszego zadania do konca ciezko jest tutaj cos zrobic
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
            income = new Income(10,5);
            transform.position = building.Tile.Position;
        }

        #endregion Private methods
    }
}