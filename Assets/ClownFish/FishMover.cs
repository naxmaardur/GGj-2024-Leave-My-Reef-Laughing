using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FishMover : MonoBehaviour
{
    Rigidbody2D rb2;
    private float maxVelocity;
    [SerializeField]
    private float actionMaxVelocity;

    [SerializeField]
    private Transform characterRenderer;
    private float characterYsize;

    [SerializeField]
    private float speedMod;

    [SerializeField]
    ParticleSystem ParticleTrail;
    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        characterYsize = characterRenderer.localScale.y;
    }

  

    // Update is called once per frame
    void Update()
    {
        if (maxVelocity > actionMaxVelocity && maxVelocity > 0)
        {
            maxVelocity -= Time.deltaTime * 3;
        }
        if (PlayerInputHandler.Instance.movementInput == Vector2.zero && maxVelocity > 0)
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
        if(rb2.velocity.magnitude > 0.4f)
        {
            if (!ParticleTrail.isPlaying)
            {
                ParticleTrail.Play();
            }
        }
        else
        {
            if (ParticleTrail.isPlaying)
            {
                ParticleTrail.Stop();
            }
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
        if(PlayerInputHandler.Instance.movementInput == Vector2.zero) { return; }

        //characterRenderer.forward = Vector2.Lerp(characterRenderer.forward, movementInput, 0.3f);
        float angle = Mathf.Atan2(PlayerInputHandler.Instance.movementInput.y, -PlayerInputHandler.Instance.movementInput.x) * Mathf.Rad2Deg;
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
        if (PlayerInputHandler.Instance.movementInput == Vector2.zero) { return; }

        rb2.velocity += PlayerInputHandler.Instance.movementInput * Time.fixedDeltaTime * speedMod / rb2.mass;

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

    
}
