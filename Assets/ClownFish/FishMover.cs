using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FishMover : MonoBehaviour
{
    InputActionList inputActions;

    Vector2 movementInput;

    bool PickUpPressed;
    Coroutine PickUpPressWindow;
    bool UsePressed;
    Coroutine UsePressWindow;
    Rigidbody2D rb2;
    private float maxVelocity;
    [SerializeField]
    private float actionMaxVelocity;

    [SerializeField]
    private Transform characterRenderer;
    private float characterYsize;

    [SerializeField]
    private float speedMod;


    



    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        inputActions = new();

        characterYsize = characterRenderer.localScale.y;

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
            PickUpPressed = false;
            StopCoroutine(PickUpPressWindow);
        };


        inputActions.Player.Use.performed += ctx =>
        {
            UsePressed = true;
            UsePressWindow = StartCoroutine(PickUpInteractListenWindow());
        };

        inputActions.Player.Use.canceled += ctx =>
        {
            UsePressed = false;
            StopCoroutine(UsePressWindow);
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

    // Update is called once per frame
    void Update()
    {
        if (maxVelocity > actionMaxVelocity && maxVelocity > 0)
        {
            maxVelocity -= Time.deltaTime * 3;
        }
        if (movementInput == Vector2.zero && maxVelocity > 0)
        {
            maxVelocity -= Time.deltaTime * 12;
        }
        else
        {
            if (maxVelocity < actionMaxVelocity)
            {
                maxVelocity = actionMaxVelocity;
            }
        }
        if(maxVelocity < 0)
        {
            maxVelocity = 0;
        }
    }


    private void FixedUpdate()
    {
        HandelMovement();
        VelocityClamping();
        Rotation();
    }

    private void Rotation()
    {
        if(movementInput == Vector2.zero) { return; }

        //characterRenderer.forward = Vector2.Lerp(characterRenderer.forward, movementInput, 0.3f);
        float angle = Mathf.Atan2(movementInput.y, -movementInput.x) * Mathf.Rad2Deg;
        Quaternion newRot = Quaternion.Euler(new Vector3(0, 0, -angle));
        Debug.Log(angle);
        characterRenderer.rotation = Quaternion.Lerp(characterRenderer.rotation, newRot,0.2f);

        if(angle > 90 || angle < -90)
        {
            Vector3 scale = characterRenderer.localScale;
            scale.y = -characterYsize;
            characterRenderer.localScale = scale;
        }
        else
        {
            Vector3 scale = characterRenderer.localScale;
            scale.y = characterYsize;
            characterRenderer.localScale = scale;
        }
    }

    private void HandelMovement()
    {
        if (movementInput == Vector2.zero) { return; }

        rb2.velocity += movementInput * Time.fixedDeltaTime * speedMod / rb2.mass;

    }
    private void VelocityClamping()
    {
        Vector2 v = rb2.velocity;
        if (v.magnitude > maxVelocity)
        {
           v = maxVelocity * v.normalized;
        }
        rb2.velocity = v;
    }

    IEnumerator PickUpInteractListenWindow()
    {
        yield return new WaitForSeconds(0.4f);
        PickUpPressed = false;
    }
}
