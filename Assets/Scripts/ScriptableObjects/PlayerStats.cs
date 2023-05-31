using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjectss/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public int DineroPlayer;
    public int AtaquePlayer;
    public int DefensaPlayer;
    public int AtaqueExtra;
    public int DefensaExtra;
    public float VidaPlayer;
    public bool enTienda;
    public bool mundo1;

    public float GetDefensaExtra()
    {
        return DefensaExtra;
    }
}
