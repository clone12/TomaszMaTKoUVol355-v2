using TKOU.SimAI.Levels.Buildings;
using TKOU.SimAI.Levels.Tiles;
using UnityEngine;

namespace TKOU.SimAI
{
    /// <summary>
    /// Contains all game elements.
    /// </summary>
    [CreateAssetMenu(fileName = nameof(GameContents), menuName = nameof(SimAI) + "/" + nameof(GameContents))]
    public class GameContents : ScriptableObject
    {
        #region Variables

        public TileData[] tiles;
        public BuildingData[] buildings;

        #endregion Variables

        private void SetBuildingCost()
        {
            MyClass myClass = new MyClass();
            //buildings[0].BuildingCost = myClass.SetCost(43);
            // Kombinowalem ale nie moge sobie z tym poradzic, tak wiem ze to sa braki, ale bardzo szybko jestem w stanie je nadrobic
            // Gdybym dalrade to dodac to wowczas problemem nie jest wyswietlenie tego
        }
    }

    public class MyClass
    {
        public int SetCost
        {
            get { return 0; }
            private set { }
        }
    }
}