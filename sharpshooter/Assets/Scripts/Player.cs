using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Soldier
{ 
    //       ⛧°。 ⋆༺♱༻⋆。 °⛧
    [Header("︻╦̵̵̿╤── VARIABLES")] 
    private Camera cam;
    private bool onGamepad = false;
    private Vector2 lastMouseInput;
    private Vector2 aimDirection;
    

    [Header("—⟪=====> Input")] 
    
    public InputActionAsset actions;

    private InputAction moveAction;
    private InputAction aimAction;
    private InputAction shootAction;
    private InputAction aimStickAction;

    
    // ⫘⫘⫘⫘⫘⫘⫘⫘⫘⫘ START FUNCTION ⫘⫘⫘⫘⫘⫘⫘⫘⫘⫘
    protected override void Start()
    {
        base.Start();
        
        cam = Camera.main;
        
    }
    
    // ⫘⫘⫘⫘⫘⫘⫘⫘⫘⫘ UPDATE ⫘⫘⫘⫘⫘⫘⫘⫘⫘⫘
    protected override void Update()
    {
        ProcessAim();
        aimPosition = transform.position + (Vector3) aimDirection;
        movementinput = moveAction.ReadValue<Vector2>();
        isFiring = shootAction.inProgress;
        base.Update();
    }

    // ⫘⫘⫘⫘⫘⫘⫘⫘⫘⫘ EEEEE ⫘⫘⫘⫘⫘⫘⫘⫘⫘⫘

    private void OnEnable()
    {
        moveAction = actions.FindAction("Player/Move");
        aimAction = actions.FindAction("Player/Aim");
        shootAction = actions.FindAction("Player/Attack");
        aimStickAction = actions.FindAction("Player/AimStick");
        aimStickAction.performed += UpdateAimStick;
    }

    private void OnDisable()
    {
        aimStickAction.performed -= UpdateAimStick;
    }

    private void UpdateAimStick(InputAction.CallbackContext context)
    {
        Vector2 newInput = context.ReadValue<Vector2>();
        if (newInput.sqrMagnitude <= 0.01f)
        {
            return;
        }

        onGamepad = true;
        aimDirection = newInput.normalized;
    }

    private void ProcessAim()
    {
        Vector2 newInput = aimAction.ReadValue<Vector2>();
        if (newInput == lastMouseInput && onGamepad)
        {
            return;
        }
        onGamepad = false;
        Vector3 mousePosition = cam.ScreenToWorldPoint(newInput);
        aimDirection = ((Vector2)mousePosition - (Vector2) transform.position).normalized;
        lastMouseInput =  newInput;
    }
}
