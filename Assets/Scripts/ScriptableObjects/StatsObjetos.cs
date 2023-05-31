using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatsObjetos", menuName = "ScriptableObjectss/StatsObjetos")]
public class StatsObjetos : ScriptableObject
{
    [Header("Stats para el player")]
    public int ArmaduraCasco;
    public int ArmaduraBotas;
    public int ArmaduraPechera;
    public int ArmaGuante;

    [Header("Precio Armas")]
    public int PrecioCasco;
    public int PrecioBotas;
    public int PrecioPechera;
    public int PrecioGuante;

    public int TotalArmadura()
    {
        return ArmaduraBotas + ArmaduraPechera + ArmaduraCasco;
    }
    public int TotalAtaque()
    {
        return ArmaGuante;
    }
}
