using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsBase", menuName = "ScriptableObjectss/PlayerStatsBase")]
public class PlayerStatsBase : ScriptableObject
{
    public int DineroPlayerBase;
    public int AtaquePlayerBase;
    public int DefensaPlayerBase;
    public int AtaqueExtraBase;
    public int DefensaExtraBase;
    public float VidaPlayerBase;
    public bool enTienda;
    public bool mundo1;
}
