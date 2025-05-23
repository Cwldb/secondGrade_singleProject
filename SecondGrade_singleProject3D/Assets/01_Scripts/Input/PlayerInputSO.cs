using System;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "PlayerInput", menuName = "SO/PlayerInput", order = 0)]
public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
{
    [SerializeField] private LayerMask whatIsGround;
    //public event Action<Vector2> OnMovementChange;
    public event Action OnAttackPressed;
    public event Action OnRollingPressed;

    public Vector2 MovementKey { get; private set; }

    private Controls _controls;
    private Vector2 _mousePosition;
    private Vector2 _screenPosition;
    private Vector3 _worldPosition;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
        }
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementKey = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnAttackPressed?.Invoke();
    }

    public void OnRolling(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnRollingPressed?.Invoke();
    }

    public void OnPointer(InputAction.CallbackContext context)
    {
        _screenPosition = context.ReadValue<Vector2>();
    }

    public Vector3 GetWorldPosition()
    {
        Camera mainCam = Camera.main;
        Debug.Assert(mainCam != null, "No main camera in this scene");
        Ray camRay = mainCam.ScreenPointToRay(_screenPosition);
        if (Physics.Raycast(camRay, out RaycastHit hit, mainCam.farClipPlane, whatIsGround))
        {
            _worldPosition = hit.point;
        }
        return _worldPosition;
    }

    public Vector3 GetEntityPos(out RaycastHit hit)
    {
        Camera mainCam = Camera.main;
        Ray camRay = mainCam.ScreenPointToRay(_screenPosition);
        bool isHit = Physics.Raycast(camRay, out hit, mainCam.farClipPlane, whatIsGround);
        if (isHit)
        {
            _worldPosition = hit.point;
        }
        return _worldPosition;
    }
}

