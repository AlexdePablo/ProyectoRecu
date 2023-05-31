using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MovimientoPersonaje : MonoBehaviour
{
    private CharacterControls m_CharacterControls;
    private Rigidbody2D m_RigidBody;
    private enum Estados { quieto, caminando};
    private Estados miEstado;
    [SerializeField]
    private Transform posicion1;
    [SerializeField]
    private Transform posicion2;
    [SerializeField]
    private PlayerStats m_PlayerStats;
    private Animator animator;
    [SerializeField]
    private TextMeshProUGUI texto;
    private void Awake()
    {
        m_CharacterControls = new CharacterControls();
        m_CharacterControls.Enable();
        m_CharacterControls.Movimiento.Entrar.started += PaDentroChavales;
    }

    private void PaDentroChavales(InputAction.CallbackContext obj)
    {
        if (m_PlayerStats.mundo1)
        {
            SceneManager.LoadScene("Mundo1");
        }
        if (m_PlayerStats.enTienda)
        {
            SceneManager.LoadScene("Tiendita");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        if(m_PlayerStats.enTienda && !m_PlayerStats.mundo1)
        {
            transform.position = posicion2.position;
            texto.text = "Tienda";
        }
        if(!m_PlayerStats.enTienda && !m_PlayerStats.mundo1)
        {
            transform.position = posicion2.position;
            texto.text = "Tienda";
        }
        if (!m_PlayerStats.enTienda && m_PlayerStats.mundo1)
        {
            texto.text = "Mundo 1";
            transform.position = posicion1.position;
        }
        miEstado = Estados.quieto;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movimiento = m_CharacterControls.Movimiento.Movimiento.ReadValue<Vector2>();
        if (movimiento.x > 0)
        {
            if (miEstado == Estados.quieto)
            {
                //tienda
                miEstado = Estados.caminando;
                StartCoroutine(moverse(1));
                m_PlayerStats.enTienda = false;
                m_PlayerStats.mundo1 = false;
                animator.SetBool("isRunning", true);
            }
        }
        else if (movimiento.x < 0)
        {
            if (miEstado == Estados.quieto)
            {
                //mundo
                miEstado = Estados.caminando;
                StartCoroutine(moverse(2));
                m_PlayerStats.enTienda = false;
                m_PlayerStats.mundo1 = false;
                animator.SetBool("isRunning", true);
            }
        }
    }
    private IEnumerator moverse(int num)
    {
        if(num == 1)
        {
            while (transform.position != posicion1.position)
            {
                m_RigidBody.velocity = (posicion1.position - transform.position).normalized * 4;
                if ((posicion1.position - transform.position).magnitude < .5f)
                {
                    transform.position = posicion1.position;
                    m_RigidBody.velocity = Vector2.zero;
                    m_PlayerStats.mundo1 = true;
                    texto.text = "Mundo 1";
                    animator.SetBool("isRunning", false);
                    break;
                }                
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            while (transform.position != posicion2.position)
            {
                m_RigidBody.velocity = (posicion2.position - transform.position).normalized * 4;
                if ((posicion2.position - transform.position).magnitude < .5f)
                {
                    transform.position = posicion2.position;
                    m_RigidBody.velocity = Vector2.zero;
                    m_PlayerStats.enTienda = true;
                    texto.text = "Tienda";
                    animator.SetBool("isRunning", false);
                    break;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        miEstado = Estados.quieto;
    }
    private void OnDestroy()
    {
        m_CharacterControls.Movimiento.Entrar.started -= PaDentroChavales;
        m_CharacterControls.Disable();
    }
}
