using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{

    private Vector3 puntoDerecha = new Vector3(1, 0, 0);
    private Vector3 puntoIzquierda = new Vector3(-1, 0, 0);
    [SerializeField]
    private LayerMask layerRayCast;
    [SerializeField]
    private int m_VelocidadMovimiento;
    private Rigidbody2D m_RigidBody;
    private enum Direccion { derecha, izquierda}
    private Direccion m_Direccion;
    private SpriteRenderer m_SpriteRenderer;

    [SerializeField]
    private GameObject coinSpawn;

    private void Awake()
    {
        m_RigidBody= GetComponent<Rigidbody2D>();   
    }
    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();  
        int numero = Random.Range(0, 2);
        if(numero == 1)
            m_Direccion= Direccion.derecha;
        else
          m_Direccion= Direccion.izquierda;
    }
     
    // Update is called once per frame
    void Update()
    {
        if(m_Direccion == Direccion.derecha)
        {
            m_SpriteRenderer.flipX = true;
            m_RigidBody.velocity = new Vector3(m_VelocidadMovimiento, m_RigidBody.velocity.y, 0);
        }
        else
        {
            m_SpriteRenderer.flipX = false;
            m_RigidBody.velocity = new Vector3(-m_VelocidadMovimiento, m_RigidBody.velocity.y, 0);
        }
       
        //RayCastDerecha
        RaycastHit2D HitDerecha = Physics2D.Raycast(transform.position + puntoDerecha, -transform.up, 3, ~layerRayCast);
        if (HitDerecha.collider == null)
            m_Direccion= Direccion.izquierda;

        //RayCastIzquierda
        RaycastHit2D HitIzquierda = Physics2D.Raycast(transform.position + puntoIzquierda, -transform.up, 3, ~layerRayCast);
        if (HitIzquierda.collider == null)
            m_Direccion = Direccion.derecha;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Guantazo"))
        {
            GameObject moneda = Instantiate(coinSpawn);
            moneda.transform.position = transform.position;
            Destroy(this.gameObject);
        }
    }
}
