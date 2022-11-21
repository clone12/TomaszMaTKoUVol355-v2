namespace TKOU.SimAI.Interfaces
{
    public interface IAmCamera : IAmMovable, IAmZoomable
    {
        #region Properties

        public UnityEngine.Camera Camera
        {
            get;
        }

        #endregion Properties
    }
}