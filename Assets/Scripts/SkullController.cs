using System.Collections;
using System.Collections.Generic;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;

public class SkullController : MonoBehaviour
{

    private Animator animatorComponent;
    private Rigidbody2D enemigoRb;


    private enum MovementState { idle, hit }
    private MovementState stateEnemigo = MovementState.idle;

    private bool deteccionColision = false;
    private int colisionesCount = 0;
    private float deteccionColisionTime = 1.0f;
    private int maxColisiones = 3;


    // Start is called before the first frame update
    private void Start()
    {
        animatorComponent = gameObject.GetComponent<Animator>();
        enemigoRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Disparo" && !deteccionColision)
        {
            stateEnemigo = MovementState.hit;
            animatorComponent.SetInteger("stateEnemigo", (int)stateEnemigo);
            colisionesCount++;

            if (colisionesCount >= maxColisiones)
            {
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(temporizadorColision());
            }
        }
    }

    private IEnumerator temporizadorColision()
    {
        deteccionColision = true;
        yield return new WaitForSeconds(deteccionColisionTime);
        deteccionColision = false;

        // Volver a idle después del cooldown
        stateEnemigo = MovementState.idle;
        animatorComponent.SetInteger("stateEnemigo", (int)stateEnemigo);
    }

}
