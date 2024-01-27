using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : Singleton<PlayerInputHandler>
{
    InputActionList inputActions;
    public Vector2 movementInput { get; private set; }
    public bool PickUpPressed { get; private set; }
    Coroutine PickUpPressWindow;
    public bool UsePressed { get; private set; }
    Coroutine UsePressWindow;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        inputActions = new();

        inputActions.Player.Movement.performed += ctx =>
        {
            movementInput = ctx.ReadValue<Vector2>();
        };

        inputActions.Player.Movement.canceled += ctx =>
        {
            movementInput = Vector2.zero;
        };

        inputActions.Player.pickUp.performed += ctx =>
        {
            PickUpPressed = true;
            PickUpPressWindow = StartCoroutine(PickUpInteractListenWindow());
        };

        inputActions.Player.pickUp.canceled += ctx =>
        {
            ActionPickUpUsed();
        };


        inputActions.Player.Use.performed += ctx =>
        {
            UsePressed = true;
            UsePressWindow = StartCoroutine(UseInteractListenWindow());
        };

        inputActions.Player.Use.canceled += ctx =>
        {
            ActionUseUsed();
        };
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void OnEnable()
    {
        if (inputActions != null)
        {
            inputActions.Enable();
        }
    }

    public void ActionUseUsed()
    {
        UsePressed = false;
        StopCoroutine(UsePressWindow);
    }

    public void ActionPickUpUsed()
    {
        PickUpPressed = false;
        StopCoroutine(PickUpPressWindow);
    }

    IEnumerator PickUpInteractListenWindow()
    {
        yield return new WaitForSeconds(0.4f);
        PickUpPressed = false;
    }
    IEnumerator UseInteractListenWindow()
    {
        yield return new WaitForSeconds(0.4f);
        UsePressed = false;
    }
}
