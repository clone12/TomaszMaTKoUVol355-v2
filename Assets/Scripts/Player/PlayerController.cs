using System;
using System.Collections.Generic;
using TKOU.SimAI.Handlers;
using TKOU.SimAI.Highlights;
using TKOU.SimAI.Interfaces;
using TKOU.SimAI.Levels;
using TKOU.SimAI.Levels.Buildings;
using TKOU.SimAI.Levels.Tiles;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TKOU.SimAI.Player
{
    /// <summary>
    /// Handles the player and his input.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        #region Events

        public event Action<IAmEntity> OnHoverEntity;
        public event Action<IAmEntity> OnSelectEntity;

        #endregion Events

        #region Variables

        [SerializeField]
        private PlayerInput playerInput;

        [SerializeField]
        private HighlightController highlightController;

        [SerializeField]
        private UIBuildingController uiBuildingController;

        private new IAmCamera camera;
        private IAmEntity selectedEntity;
        private IAmEntity hoveredEntity;
        private IAmEntity underCursorEntity;

        private bool isContextPressed = false;

        private const float moveScale = 0.04f;
        private const float zoomScale = 5f;

        private Dictionary<Type, Action<IAmEntity>> typeToSelectionHandlers;

        private bool shouldUpdateEntityUnderCursor = false;

        private BuildHandler buildHandler;

        #endregion Variables

        #region Unity methods

        private void Awake()
        {
            //Initialize input.
            playerInput.moveMouseInput.action.performed += PlayerInput_OnMoveMouse;
            playerInput.moveCameraInput.action.performed += PlayerInput_OnMoveCamera;
            playerInput.contextInput.action.performed += PlayerInput_OnContext;
            playerInput.returnInput.action.performed += PlayerInput_OnReturn;
            playerInput.selectInput.action.performed += PlayerInput_OnSelect;
            playerInput.zoomCameraInput.action.performed += PlayerInput_OnZoomCamera;

            playerInput.moveMouseInput.action.Enable();
            playerInput.moveCameraInput.action.Enable();
            playerInput.contextInput.action.Enable();
            playerInput.returnInput.action.Enable();
            playerInput.selectInput.action.Enable();
            playerInput.zoomCameraInput.action.Enable();

            //Initialize logic handling.
            typeToSelectionHandlers = new Dictionary<Type, Action<IAmEntity>>();

            typeToSelectionHandlers.Add(typeof(TileEntity), OnSelectEntity_TileEntity);
            typeToSelectionHandlers.Add(typeof(BuildingEntity), OnSelectEntity_BuildingEntity);

            buildHandler = new BuildHandler();
        }

        private void Update()
        {
            if (shouldUpdateEntityUnderCursor)
            {
                UpdateEntityUnderCursor();
                UpdateHoveredEntity();
                UpdateBuildHandler();
                shouldUpdateEntityUnderCursor = false;
            }
        }

        private void OnDestroy()
        {
            playerInput.moveMouseInput.action.performed -= PlayerInput_OnMoveMouse;
            playerInput.moveCameraInput.action.performed -= PlayerInput_OnMoveCamera;
            playerInput.contextInput.action.performed -= PlayerInput_OnContext;
            playerInput.returnInput.action.performed -= PlayerInput_OnReturn;
            playerInput.selectInput.action.performed -= PlayerInput_OnSelect;
            playerInput.zoomCameraInput.action.performed -= PlayerInput_OnZoomCamera;
        }

        #endregion Unity methods

        #region Public methods

        /// <summary>
        /// Initialize <see cref="PlayerController"/>.
        /// </summary>
        /// <param name="camera"></param>
        public void Initialize(IAmCamera camera)
        {
            this.camera = camera;

            uiBuildingController.buildSelectionTarget = buildHandler;
        }

        #endregion Public methods

        #region Private methods

        private void UpdateEntityUnderCursor()
        {
            Ray ray = camera.Camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            RaycastHit[] hits = Physics.RaycastAll(ray);

            underCursorEntity = null;

            if (hits.Length == 0)
            {
                return;
            }

            for (int i = 0; i < hits.Length; i++)
            {
                underCursorEntity = hits[i].collider.GetComponentInChildren<IAmEntity>();

                if (underCursorEntity != null)
                {
                    return;
                }

                Rigidbody body = hits[i].rigidbody;

                if (body == null)
                {
                    continue;
                }

                underCursorEntity = body.GetComponentInChildren<IAmEntity>();

                if (underCursorEntity != null)
                {
                    return;
                }
            }
        }

        private void UpdateHoveredEntity()
        {
            HoverEntity(underCursorEntity);
        }

        private void UpdateBuildHandler()
        {
            if (underCursorEntity == null)
            {
                return;
            }

            buildHandler.BuildTarget = underCursorEntity;
        }

        private void SelectEntity(IAmEntity entity)
        {
            if (selectedEntity == entity)
            {
                return;
            }

            selectedEntity = entity;

            if (selectedEntity == null)
            {
                return;
            }

            if (typeToSelectionHandlers.TryGetValue(selectedEntity.GetType(), out System.Action<IAmEntity> logicAction))
            {
                logicAction(selectedEntity);
            }
            else
            {
                Debug.LogError($"Unhandled entity pressed: {selectedEntity.GetType()}");
            }
        }

        private void HoverEntity(IAmEntity entity)
        {
            if (hoveredEntity == entity)
            {
                return;
            }

            hoveredEntity = entity;

            highlightController.ClearAllHighlights();

            if (hoveredEntity != null)
            {
                highlightController.Highlight(entity);
            }

            OnHoverEntity?.Invoke(hoveredEntity);

            Debug.Log($"Entity hovered: {hoveredEntity}");
        }

        private void PlayerInput_OnZoomCamera(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.phase != InputActionPhase.Performed)
            {
                return;
            }

            float zoom = callbackContext.ReadValue<float>() * zoomScale * Time.deltaTime; ;

            camera?.Zoom(zoom);

            shouldUpdateEntityUnderCursor = true;
        }

        private void PlayerInput_OnSelect(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.phase != InputActionPhase.Performed)
            {
                return;
            }

            if (callbackContext.ReadValueAsButton())
            {
                if (buildHandler.BuildSelection != null)
                {
                    buildHandler.AttemptToBuildSelection();
                }
                else
                {
                    SelectEntity(hoveredEntity);
                }
            }
        }

        private void PlayerInput_OnReturn(InputAction.CallbackContext callbackContext)
        {
            if (buildHandler.BuildSelection != null)
            {
                buildHandler.BuildSelection = null;
            }
        }

        private void PlayerInput_OnContext(InputAction.CallbackContext callbackContext)
        {

            if (callbackContext.phase != InputActionPhase.Performed)
            {
                return;
            }

            isContextPressed = callbackContext.ReadValueAsButton();
        }

        private void PlayerInput_OnMoveCamera(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.phase != InputActionPhase.Performed)
            {
                return;
            }

            if (!isContextPressed)
            {
                return;
            }

            Vector2 delta = callbackContext.ReadValue<Vector2>() * moveScale;

            camera?.MoveBy(-delta);

            shouldUpdateEntityUnderCursor = true;
        }

        private void PlayerInput_OnMoveMouse(InputAction.CallbackContext callbackContext)
        {

            if (callbackContext.phase != InputActionPhase.Performed)
            {
                return;
            }

            shouldUpdateEntityUnderCursor = true;
        }

        private void OnSelectEntity_TileEntity(IAmEntity entity)
        {
            TileEntity tileEntity = (TileEntity)entity;

            OnSelectEntity?.Invoke(tileEntity);

            Debug.Log($"Tile pressed! {tileEntity}");
        }

        private void OnSelectEntity_BuildingEntity(IAmEntity entity)
        {
            BuildingEntity buildingEntity = (BuildingEntity)entity;

            OnSelectEntity?.Invoke(buildingEntity);

            Debug.Log($"Building pressed! {buildingEntity}");
        }

        #endregion Private methods
    }
}