using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Player : Soldier
{ 
    //       ⛧°。 ⋆༺♱༻⋆。 °⛧
    [Header("︻╦̵̵̿╤── VARIABLES")] 
    private Camera cam;
    private bool onGamepad = false;
    private Vector2 lastMouseInput;
    private Vector2 aimDirection;
    public GameObject pointerPrefab;
    private List<GameObject> pointers = new List<GameObject>();
    public float minPointerDistance = 3f;
    public float maxPointerDistance = 4f;
    public float minEnemyDistance = 10f;
    public float maxEnemyDistance = 35f;
    

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
        
        InvokeRepeating(nameof(HealMePlease),0, 1);
    }
    
    // ⫘⫘⫘⫘⫘⫘⫘⫘⫘⫘ UPDATE ⫘⫘⫘⫘⫘⫘⫘⫘⫘⫘
    protected override void Update()
    {
        ProcessAim();
        aimPosition = transform.position + (Vector3) aimDirection;
        movementinput = moveAction.ReadValue<Vector2>();
        isFiring = shootAction.inProgress;
        UpdatePointers();
        base.Update();

        float temphp = hp;
        temphp *= 2f;
        int inthp = Mathf.RoundToInt(temphp);
        hp = Mathf.Clamp((float) inthp / 2f, 0f, 5f);
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

    private void UpdatePointers()
    {
        while (pointers.Count < GameManager.enemies.Count)
        {
            pointers.Add(Instantiate(pointerPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity));
        }

        while (pointers.Count > GameManager.enemies.Count)
        {
            DestroyImmediate(pointers[pointers.Count - 1]);
            pointers.RemoveAt(pointers.Count - 1);
        }

        for (int i = 0; i < pointers.Count; i++)
        {
            Vector2 enemyPosition = GameManager.enemies[i].transform.position;
            float distanceToEnemy =  Mathf.Clamp(Vector2.Distance(transform.position, enemyPosition), minEnemyDistance, maxEnemyDistance);
            float distanceToPointer = MathUtils.Remap(minEnemyDistance, maxEnemyDistance, minPointerDistance, maxPointerDistance, distanceToEnemy, minPointerDistance, maxPointerDistance);
            Vector2 direction = (enemyPosition - (Vector2) transform.position).normalized;
            
            pointers[i].transform.position = (Vector2) transform.position + direction * distanceToPointer;
            pointers[i].transform.rotation = Quaternion.Euler(new Vector3(0,0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
        }
    }

    private void HealMePlease()
    {
        if (hp < 5f)
        {
            hp += 0.5f;
            hp = Mathf.Clamp(hp, 0, 5);
        }
    }
}
