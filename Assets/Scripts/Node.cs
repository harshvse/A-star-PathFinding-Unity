using UnityEngine;
//A node is any point in the map where the agent can check to see if he can move to it
public class Node
{
    public bool walkable;
    public Vector3 worldPostion;
    public int gCost;
    public int hCost;
    public int gridX, gridY;
    public Node parent;

    public Node(bool _walkable, Vector3 _worldPostion, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPostion = _worldPostion;
        gridX = _gridX;
        gridY = _gridY;
    }
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
