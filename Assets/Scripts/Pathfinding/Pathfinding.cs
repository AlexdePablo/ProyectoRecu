/*
 *
 * Based on the project https://github.com/goet/Unity-Tilemap-Pathfinding
 * 
*/
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// An implementation of A* for the new TileMap system released by Unity
/// </summary>
public class Pathfinding : MonoBehaviour
{
    [Tooltip("The tilemap that contains the walkable terrain.")]
    [SerializeField]
    private Tilemap tilemap;

    [Tooltip("The different tiles that are considered walkable from your palette.")]
    [SerializeField]
    private Tile[] m_WalkableTiles;

    [Tooltip("Whether the path can do diagonal steps or not.")]
    public bool m_IncludeDiagonals = true;

    /// <summary>
    /// Simple check to see if the target cell is a walkable one.
    /// </summary>
    /// <param name="target">The cell space</param>
    /// <returns></returns>
    public bool IsWalkableTile(Vector3Int target)
    {
        Tile tile = tilemap.GetTile<Tile>(target);
        return m_WalkableTiles.Contains<Tile>(tile);
    }

    /// <summary>
    /// Looks for a path given cell space.
    /// </summary>
    /// <param name="start">The starting position (cell space)</param>
    /// <param name="target">The destination the path should lead to (cell space)</param>
    /// <param name="path">The path created (cell space)</param>
    public void FindPath(Vector3Int start, Vector3Int target, out List<Vector3Int> path)
    {
        Debug.Log("looking for path");
        path = new List<Vector3Int>();
        Node startNode = new Node(start);
        Node targetNode = new Node(target);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        if (!IsWalkableTile(target))
            return;

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode.Equals(targetNode))
            {
                path = RetracePath(startNode, currentNode);
                return;
            }

            foreach (Node neighbour in GetNeighbours(currentNode))
            {
                if (closedSet.Contains(neighbour))
                    continue;

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.Parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                        openSet = openSet.OrderBy(a => a.fCost).ToList();
                    }
                }
            }

            openSet = openSet.OrderBy(a => a.fCost).ToList();
        }
    }

    private List<Vector3Int> RetracePath(Node startNode, Node endNote)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        Node currentNode = endNote;

        while (!currentNode.Equals(startNode))
        {
            path.Add(currentNode.Position);
            currentNode = currentNode.Parent;
        }
        path.Add(startNode.Position);
        Debug.Log("path found, steps: " + path.Count);

        path.Reverse();
        return path;
    }

    private int GetDistance(Node a, Node b)
    {
        int dstX = Mathf.Abs(a.Position.x - b.Position.x);
        int dstY = Mathf.Abs(a.Position.y - b.Position.y);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);

        return 14 * dstX + 10 * (dstY - dstX);
    }

    private List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                //skip diagonal nodes
                if (!m_IncludeDiagonals && Mathf.Abs(x) == 1 && Mathf.Abs(y) == 1)
                    continue;

                Vector3Int newNodeCoordinates = new Vector3Int(node.Position.x + x, node.Position.y + y, 0);

                if (IsWalkableTile(newNodeCoordinates))
                    neighbours.Add(new Node(newNodeCoordinates));
            }
        }

        return neighbours;
    }

    /// <summary>
    /// Converts an existing path in cell space to world space according
    /// to the associated tilemap cell positions.
    /// </summary>
    /// <param name="cellPath">The existing path in cell space</param>
    /// <param name="worldPath">The output path in world space</param>
    public void FromCellPathToWorldPath(List<Vector3Int> cellPath, out List<Vector3> worldPath)
    {
        worldPath = new List<Vector3>();
        foreach (Vector3Int cell in cellPath)
            worldPath.Add(tilemap.GetCellCenterWorld(cell));
    }

    /// <summary>
    /// Finds a path from start to target where all coordinates are given in world space.
    /// </summary>
    /// <param name="start">The starting point (world space)</param>
    /// <param name="target">The destination point (world space)</param>
    /// <param name="path">The path in steps for each tile (world space)</param>
    public void FindPathWorldSpace(Vector3 start, Vector3 target, out List<Vector3> path)
    {
        Vector3Int startCell = tilemap.WorldToCell(start);
        startCell.z = 0;
        Vector3Int targetCell = tilemap.WorldToCell(target);
        targetCell.z = 0;

        List<Vector3Int> cellPath;
        FindPath(startCell, targetCell, out cellPath);

        FromCellPathToWorldPath(cellPath, out path);
    }
}
