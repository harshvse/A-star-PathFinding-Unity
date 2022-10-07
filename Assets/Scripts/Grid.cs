using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public Vector3 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY; //how many nodes are there in an axis

    private void Start()
    {
        nodeDiameter = 2 * nodeRadius;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();

    }

    void CreateGrid()
    {
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        grid = new Node[gridSizeX, gridSizeY];
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (i * nodeDiameter + nodeRadius) + Vector3.forward * (j * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));

                grid[i, j] = new Node(walkable, worldPoint);
            }
        }
    }

    public Node NodeFromWorldPosition(Vector3 worldPosition)
    {
        float percentageX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentageY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y; //notice how we need to use z as y here
        percentageX = Mathf.Clamp01(percentageX);
        percentageY = Mathf.Clamp01(percentageY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentageX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentageY);

        return grid[x, y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if (grid != null)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.worldPostion, Vector3.one * (nodeDiameter - 0.1f));
            }

        }
    }

}
