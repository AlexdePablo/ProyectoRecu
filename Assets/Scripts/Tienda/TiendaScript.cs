using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TiendaScript : MonoBehaviour
{
    [Header("Scriptables")]
    [SerializeField]
    private ItemsComprados itemsComprados;
    [SerializeField]
    private PlayerStats estadisticasJugador;
    [SerializeField]
    private StatsObjetos ObjetosStats;

    [Header("Paneles Tienda")]
    [SerializeField]
    private GameObject panelYaHasComprado;
    [SerializeField]
    private GameObject panelNoTienesDinero;
    [SerializeField]
    private GameObject panelObjetoComprado;

    public delegate void Compra();
    public static event Compra onCompra;

    // Start is called before the first frame update
    void Start()
    {
        panelNoTienesDinero.SetActive(false);
        panelObjetoComprado.SetActive(false);
        panelYaHasComprado.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ComprarGuantes()
    {
        if (!itemsComprados.Guantes)
        {
            if(estadisticasJugador.DineroPlayer >= ObjetosStats.PrecioGuante)
            {
                itemsComprados.Guantes = true;
                estadisticasJugador.DineroPlayer = estadisticasJugador.DineroPlayer - ObjetosStats.PrecioGuante;
                estadisticasJugador.AtaqueExtra += ObjetosStats.ArmaGuante;
                onCompra?.Invoke();
                panelObjetoComprado.SetActive(true);
            }
            else
            {
                panelNoTienesDinero.SetActive(true);
            }
        }
        else
        {
            panelYaHasComprado.SetActive(true);
        }
    }
    public void ComprarPechera()
    {
        if (!itemsComprados.Pechera)
        {
            if (estadisticasJugador.DineroPlayer >= ObjetosStats.PrecioPechera)
            {
                itemsComprados.Pechera = true;
                estadisticasJugador.DineroPlayer = estadisticasJugador.DineroPlayer - ObjetosStats.PrecioPechera;
                estadisticasJugador.DefensaExtra += ObjetosStats.ArmaduraPechera;
                onCompra?.Invoke();
                panelObjetoComprado.SetActive(true);
            }
            else
            {
                panelNoTienesDinero.SetActive(true);
            }
        }
        else
        {
            panelYaHasComprado.SetActive(true);
        }
    }
    public void ComprarCasco()
    {
        if (!itemsComprados.Casco)
        {
            if (estadisticasJugador.DineroPlayer >= ObjetosStats.PrecioCasco)
            {
                itemsComprados.Casco = true;
                estadisticasJugador.DineroPlayer = estadisticasJugador.DineroPlayer - ObjetosStats.PrecioCasco;
                estadisticasJugador.DefensaExtra += ObjetosStats.ArmaduraCasco;
                onCompra?.Invoke();
                panelObjetoComprado.SetActive(true);
            }
            else
            {
                panelNoTienesDinero.SetActive(true);
            }
        }
        else
        {
            panelYaHasComprado.SetActive(true);
        }
    }
    public void ComprarBotas()
    {
        if (!itemsComprados.Botas)
        {
            if (estadisticasJugador.DineroPlayer >= ObjetosStats.PrecioBotas)
            {
                itemsComprados.Botas = true;
                estadisticasJugador.DineroPlayer = estadisticasJugador.DineroPlayer - ObjetosStats.PrecioBotas;
                estadisticasJugador.DefensaExtra += ObjetosStats.ArmaduraBotas;
                onCompra?.Invoke();
                panelObjetoComprado.SetActive(true);
            }
            else
            {
                panelNoTienesDinero.SetActive(true);
            }
        }
        else
        {
            panelYaHasComprado.SetActive(true);
        }
    }
    public void BotonAceptar()
    {
        panelObjetoComprado.SetActive(false);
        panelNoTienesDinero.SetActive(false);
        panelYaHasComprado.SetActive(false);
    }
    public void Volver()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
