using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIScripts : MonoBehaviour
{
    private static GUIScripts m_Instance;
    public static GUIScripts Instance
    {
        get { return m_Instance; }
    }


    [Header("ImagenesVida")]
    [SerializeField]
    private TextMeshProUGUI textCoins;
    [SerializeField]
    private Image BarraVida;
    [Header("FotosVida")]
    [SerializeField]
    private Sprite vidaFull;
    [SerializeField]
    private Sprite vidaTresCuartos;
    [SerializeField]
    private Sprite vidaMitad;
    [SerializeField]
    private Sprite vidaCuarto;
    [SerializeField]
    private Sprite vidaZero;

    [Header("ScriptableObjects")]
    [SerializeField]
    private PlayerStats playerStats;

    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        ScriptCoin.OnContact += SubirMoneda;
        textCoins.text = "x" + playerStats.DineroPlayer;
    }

    private void SubirMoneda()
    {
        playerStats.DineroPlayer += 1;
        textCoins.text = "x"+ playerStats.DineroPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CambiarVida()
    {
        if (playerStats.VidaPlayer == 20)
            BarraVida.sprite = vidaFull;
        if (playerStats.VidaPlayer >= 15 && playerStats.VidaPlayer < 20)
            BarraVida.sprite = vidaTresCuartos;
        if (playerStats.VidaPlayer >= 5 && playerStats.VidaPlayer < 15)
            BarraVida.sprite = vidaMitad;
        if (playerStats.VidaPlayer >= 0.1f && playerStats.VidaPlayer < 5)
            BarraVida.sprite = vidaCuarto;
        if (playerStats.VidaPlayer < 0.1f)
            BarraVida.sprite = vidaZero;
    }
    private void OnDestroy()
    {
        ScriptCoin.OnContact -= SubirMoneda;
    }
}
