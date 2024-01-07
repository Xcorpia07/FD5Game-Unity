using System.Collections;
using System.Collections.Generic;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;

public class MaskController : MonoBehaviour
{

    private Animator animatorComponent;
    public SpriteRenderer sprite;
    private Rigidbody2D jugadorRb;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private GameObject disparo;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private bool estaSuelo = true;

    private enum MovementState { idle, running, jumping, falling }
    private MovementState state = MovementState.idle;


    // Start is called before the first frame update
    private void Start()
    {
        animatorComponent = gameObject.GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        jugadorRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        jugadorRb.velocity = new Vector2(dirX * moveSpeed, jugadorRb.velocity.y);

        if (Input.GetButtonDown("Jump") && estaSuelo)
        {

            jugadorRb.velocity = new Vector2(jugadorRb.velocity.x, jumpForce);
            estaSuelo = false;

        }

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject fuego = Instantiate(disparo, puntoDisparo.position, Quaternion.identity);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (jugadorRb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (jugadorRb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        animatorComponent.SetInteger("state", (int)state);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag =="Suelo")
        {
            estaSuelo = true;
        }
    }

}

