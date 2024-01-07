using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoController : MonoBehaviour

{
    [SerializeField] private float velocidad = 4.5f;
    [SerializeField] private float tiempoDisparo = 1.5f;
    private float tiempo = 0;
    private int dir = 1;
    private MaskController maskController;


    void Start()
    {
        maskController = FindObjectOfType<MaskController>();
        if (maskController != null && maskController.sprite != null && maskController.sprite.flipX)
        {
            dir = -1;
        }
    }


    void Update()
    {
        transform.position = new Vector3(transform.position.x + (velocidad * Time.deltaTime * dir), transform.position.y, transform.position.z);
        tiempo += Time.deltaTime;
        if (tiempo >= tiempoDisparo)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Suelo" || other.tag == "Enemigo")
        {
            Destroy(gameObject);
        }


    }
}
