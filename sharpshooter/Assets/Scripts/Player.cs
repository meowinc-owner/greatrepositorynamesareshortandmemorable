using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Soldier
{ 
    //       ⛧°。 ⋆༺♱༻⋆。 °⛧
    [Header("︻╦̵̵̿╤── VARIABLES")] 
    private Camera cam;
    

    [Header("—⟪=====> Input")] 
    
    public InputActionAsset actions;

    private InputAction moveAction;
    private InputAction aimAction;
    private InputAction shootAction;

    
    // ⫘⫘⫘⫘⫘⫘⫘⫘⫘⫘ START FUNCTION ⫘⫘⫘⫘⫘⫘⫘⫘⫘⫘
    protected override void Start()
    {
        base.Start();
        moveAction = actions.FindAction("Player/Move");
        aimAction = actions.FindAction("Player/Aim");
        shootAction = actions.FindAction("Player/Attack");
        cam = Camera.main;
        
    }
    
    // ⫘⫘⫘⫘⫘⫘⫘⫘⫘⫘ UPDATE ⫘⫘⫘⫘⫘⫘⫘⫘⫘⫘
    protected override void Update()
    {
        aimPosition = cam.ScreenToWorldPoint(aimAction.ReadValue<Vector2>());
        movementinput = moveAction.ReadValue<Vector2>();
        isFiring = shootAction.inProgress;
        base.Update();
    }

    

    

    
}
