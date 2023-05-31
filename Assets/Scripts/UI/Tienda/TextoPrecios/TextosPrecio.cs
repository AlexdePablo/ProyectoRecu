using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextosPrecio : MonoBehaviour
{
    [SerializeField]
    private StatsObjetos objetosStats;
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private TextMeshProUGUI precioCasco;
    [SerializeField]
    private TextMeshProUGUI precioPechera;
    [SerializeField]
    private TextMeshProUGUI precioBotas;
    [SerializeField]
    private TextMeshProUGUI precioGuantes;
    [SerializeField]
    private TextMeshProUGUI dineroJugador;

    // Start is called before the first frame update
    void Start()
    {
        SetearTextosTienda();
        TiendaScript.onCompra += actualizarDinero;
    }

    private void actualizarDinero()
    {
        dineroJugador.text = "x" + playerStats.DineroPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetearTextosTienda()
    {
        precioBotas.text = "x" + objetosStats.PrecioBotas;
        precioCasco.text = "x" + objetosStats.PrecioCasco;
        precioGuantes.text = "x" + objetosStats.PrecioGuante;
        precioPechera.text = "x" + objetosStats.PrecioPechera;
        dineroJugador.text = "x" + playerStats.DineroPlayer;
    }
}
