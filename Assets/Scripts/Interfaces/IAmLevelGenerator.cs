using TKOU.SimAI.Levels;

namespace TKOU.SimAI.Interfaces
{
    /// <summary>
    /// Basic inteface for all level generators.
    /// </summary>
    public interface IAmLevelGenerator
    {
        #region Properties

        /// <summary>
        /// Min level width.
        /// </summary>
        public int MinLevelWidth
        {
            get;
        }

        /// <summary>
        /// Min level height.
        /// </summary>
        public int MinLevelHeight
        {
            get;
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Does a generation pass on the level using this generator.
        /// </summary>
        /// <param name="level"></param>
        public void GenerateLevel(Level level);

        /// <summary>
        /// Checks if the level is valid for the given generator.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool IsLevelValid(Level level);

        #endregion Public methods
    }
}