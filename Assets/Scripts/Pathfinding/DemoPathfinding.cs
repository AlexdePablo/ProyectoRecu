using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Pathfinding))]
public class DemoPathfinding : MonoBehaviour
{
    private static DemoPathfinding m_Instance;
    public static DemoPathfinding Instance => m_Instance;

    Pathfinding m_Pathfinding;

    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else
            Destroy(gameObject);


        m_Pathfinding = GetComponent<Pathfinding>();
    }

    public void FindPathWorldSpace(Vector3 start, Vector3 target, out List<Vector3> path)
    {
        m_Pathfinding.FindPathWorldSpace(start, target, out path);
    }

    public void FindPath(Vector3Int start, Vector3Int target, out List<Vector3Int> path)
    {
        m_Pathfinding.FindPath(start, target, out path);
    }
}
