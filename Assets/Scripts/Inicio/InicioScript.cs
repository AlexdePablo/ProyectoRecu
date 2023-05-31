using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InicioScript : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private PlayerStatsBase statsBase;
    [SerializeField]
    private ItemsComprados itemsComprados;
    // Start is called before the first frame update
    void Start()
    {
        playerStats.AtaquePlayer = statsBase.AtaquePlayerBase;
        playerStats.DefensaPlayer = statsBase.DefensaPlayerBase;
        playerStats.DineroPlayer = statsBase.DineroPlayerBase;
        playerStats.VidaPlayer = statsBase.VidaPlayerBase;
        playerStats.AtaqueExtra = statsBase.AtaqueExtraBase;
        playerStats.DefensaExtra = statsBase.DefensaExtraBase;
        statsBase.mundo1 = false;
        statsBase.enTienda = true;
        playerStats.mundo1 = statsBase.mundo1;
        playerStats.enTienda = statsBase.enTienda;
        itemsComprados.Pechera = false;
        itemsComprados.Guantes = false;
        itemsComprados.Casco = false;
        itemsComprados.Botas = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Jugar()
    {
        SceneManager.LoadScene("SampleScene");
    } 
}
