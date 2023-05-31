using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEditor.VersionControl.Asset;

public class ControladorGameplayCharacter : MonoBehaviour
{
    private CharacterControls m_CharacterControls;
    private Rigidbody2D m_RigidBody;
    [SerializeField]
    private int m_MoveSpeed;
    [SerializeField]
    private int m_JumpForce;
    private SpriteRenderer m_SpriteRenderer;
    private enum estados { idle, caminar, golpear, salto }
    private estados m_EstadosPlayer;
    private Animator m_Animator;

    [SerializeField]
    private int FuerzaEmpuje;
    [SerializeField]
    private LayerMask layerRayCastSalto;

    private float m_StateDeltaTime;

    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private StatsObjetos statsObjetos;
    Vector2 movement;
    private void Awake()
    {
        playerStats.VidaPlayer = 20;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_CharacterControls = new CharacterControls();
        m_CharacterControls.Enable();
        m_CharacterControls.Movimiento.Salto.started += Saltar;

        m_Animator = GetComponent<Animator>();

    }

    private void Saltar(InputAction.CallbackContext obj)
    {
        RaycastHit2D HitSuelo = Physics2D.Raycast(transform.position, -transform.up, 1.6f, layerRayCastSalto);
        if (HitSuelo.collider != null)
        {
            m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, 0);
            m_RigidBody.AddForce(Vector2.up * m_JumpForce, ForceMode2D.Impulse);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        m_EstadosPlayer = estados.idle;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState(m_EstadosPlayer);
        m_RigidBody.velocity = new Vector2(0, m_RigidBody.velocity.y);
        movement = m_CharacterControls.Movimiento.Movimiento.ReadValue<Vector2>();
        if (movement.x > 0)
        {
            m_RigidBody.velocity = new Vector2(m_MoveSpeed, m_RigidBody.velocity.y);
            Vector3 newScale = transform.localScale;
            newScale.x = -4;
            transform.localScale = newScale;
        }
        else if (movement.x < 0)
        {
            m_RigidBody.velocity = new Vector2(-m_MoveSpeed, m_RigidBody.velocity.y);
            Vector3 newScale = transform.localScale;
            newScale.x = 4;
            transform.localScale = newScale;
        }

        if (m_RigidBody.velocity.y < 0)
        {
            m_RigidBody.gravityScale = 3;
        }
        else
        {
            m_RigidBody.gravityScale = 1;
        }


    }
    private void ChangeState(estados newState)
    {
        if (newState == m_EstadosPlayer)
            return;

        ExitState(m_EstadosPlayer);
        InitState(newState);
    }

    private void InitState(estados initState)
    {
        m_EstadosPlayer = initState;
        m_StateDeltaTime = 0;

        switch (m_EstadosPlayer)
        {
            case estados.idle:
                m_Animator.Play("Idle");
                break;
            case estados.caminar:
                m_Animator.Play("walk");
                //m_ComboTimeCoroutine = StartCoroutine(CorutineCombo(0.65f, 1.1f));
                break;
            case estados.golpear:
                m_Animator.Play("attack");
                break;
            case estados.salto:
                m_Animator.Play("jump");
                break;
            default:
                break;
        }
    }


    private void UpdateState(estados updateState)
    {
        m_StateDeltaTime += Time.deltaTime;

        switch (updateState)
        {
            case estados.idle:
                if (m_CharacterControls.Movimiento.Salto.IsPressed())
                    ChangeState(estados.salto);
                if(movement.x != 0)
                    ChangeState(estados.caminar);
                if (m_CharacterControls.Movimiento.Golpe.IsPressed())
                    ChangeState(estados.golpear);
                break;
            case estados.salto:
                RaycastHit2D HitSuelo = Physics2D.Raycast(transform.position, -transform.up, 1.6f, layerRayCastSalto);
                if (HitSuelo.collider != null)
                {
                    ChangeState(estados.idle);
                }
                break;
            case estados.caminar:
                if (movement.x == 0)
                    ChangeState(estados.idle);
                if (m_CharacterControls.Movimiento.Salto.IsPressed())
                    ChangeState(estados.salto);
                if (m_CharacterControls.Movimiento.Golpe.IsPressed())
                    ChangeState(estados.golpear);
                break;
            case estados.golpear:
                if (m_StateDeltaTime > 0.2)
                    ChangeState(estados.idle);
                break;
            /*case estados.PUNCH:
                if (Input.GetKeyDown(KeyCode.Space) && combo)
                    ChangeState(States.SHOOT);

                else if (m_StateDeltaTime >= 1.2f)
                    ChangeState(States.IDLE);

                break;
            case estados.SHOOT:
                if (m_StateDeltaTime >= 0.4f)
                    ChangeState(States.IDLE);

                break;*/
            default:
                break;
        }
    }

    private void ExitState(estados exitState)
    {
        switch (exitState)
        {
            case estados.idle:
                break;
            case estados.caminar:
                //StopCoroutine(m_ComboTimeCoroutine);
                break;
            case estados.golpear:
                break;
            case estados.salto:
                break;
            default:
                break;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemigo"))
        {
            m_RigidBody.AddForce(new Vector2(-10, 1) * FuerzaEmpuje, ForceMode2D.Impulse);
            if(playerStats.GetDefensaExtra() > 0)
                playerStats.VidaPlayer = playerStats.VidaPlayer - (10 - playerStats.GetDefensaExtra() / 10);
            else
                playerStats.VidaPlayer = playerStats.VidaPlayer - 10;

            GUIScripts.Instance.CambiarVida();
            if (playerStats.VidaPlayer <= 0)
                SceneManager.LoadScene("SampleScene");
        }
    }
    private void OnDestroy()
    {
        m_CharacterControls.Movimiento.Salto.started -= Saltar;
        m_CharacterControls.Disable();
    }
}
