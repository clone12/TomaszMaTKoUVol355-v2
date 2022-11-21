namespace TKOU.SimAI.Interfaces
{
    /// <summary>
    /// An object that have a building data selected for building.
    /// </summary>
    public interface IHaveBuildSelection
    {
        #region Properties

        public IAmData BuildSelection
        {
            get;
            set;
        }

        #endregion Properties
    }
}