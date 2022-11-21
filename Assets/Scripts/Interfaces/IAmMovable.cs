using UnityEngine;

namespace TKOU.SimAI.Interfaces
{
    public interface IAmMovable
    {
        #region Public methods

        public void MoveBy(Vector2 delta);

        public void MoveTo(Vector2 position);

        #endregion Public methods
    }
}