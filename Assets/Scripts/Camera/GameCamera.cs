using Cinemachine;
using TKOU.SimAI.Interfaces;
using UnityEngine;

namespace TKOU.SimAI.Camera
{
    public class GameCamera : MonoBehaviour, IAmCamera
    {
        #region Variables

        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;
        private CinemachineGroupComposer cameraGroupComposer;

        [SerializeField]
        private UnityEngine.Camera camera;
        private Transform cameraTarget;
        private Transform cameraTransform;

        [Header("Zoom")]
        private float minZoomValue = 10;
        private float maxZoomValue = 100;

        [Header("Ranges")]
        private Vector2 minPosition;
        private Vector2 maxPosition;

        #endregion Variables

        #region Properties

        UnityEngine.Camera IAmCamera.Camera
        {
            get
            {
                return camera;
            }
        }

        #endregion Properties

        #region Unity methods

        private void Awake()
        {
            cameraTransform = virtualCamera.transform;
            cameraGroupComposer = virtualCamera.GetCinemachineComponent<CinemachineGroupComposer>();
            cameraTarget = cameraGroupComposer.FollowTarget;
        }

        #endregion Unity methods

        #region Public methods

        public void SetBounds(Vector2 minPosition, Vector2 maxPosition)
        {
            this.minPosition = minPosition;
            this.maxPosition = maxPosition;
        }

        public void Zoom(float value)
        {
            float newOrthoSize = Mathf.Clamp(cameraGroupComposer.m_MinimumOrthoSize - value, minZoomValue, maxZoomValue);

            cameraGroupComposer.m_MinimumOrthoSize = newOrthoSize;
            cameraGroupComposer.m_MaximumOrthoSize = newOrthoSize;
        }

        public void MoveBy(Vector2 delta)
        {
            Vector3 rotation = cameraTransform.eulerAngles;

            rotation.x = 0.0f;
            rotation.z = 0.0f;

            Vector3 delta3D = new Vector3(delta.x, 0.0f, delta.y);

            delta3D = Quaternion.Euler(rotation) * delta3D;

            Vector3 newPosition = cameraTarget.position;

            newPosition.x += delta3D.x;
            newPosition.z += delta3D.z;

            newPosition.x = Mathf.Clamp(newPosition.x, minPosition.x, maxPosition.x);
            newPosition.z = Mathf.Clamp(newPosition.z, minPosition.y, maxPosition.y);

            cameraTarget.position = newPosition;
        }

        public void MoveTo(Vector2 position)
        {
            position.x = Mathf.Clamp(position.x, minPosition.x, maxPosition.x);
            position.y = Mathf.Clamp(position.y, minPosition.y, maxPosition.y);

            cameraTarget.position = new Vector3(position.x, 0.0f, position.y);
        }

        #endregion Public methods
    }
}
