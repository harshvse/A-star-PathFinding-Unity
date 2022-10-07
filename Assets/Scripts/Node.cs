using UnityEngine;
//A node is any point in the map where the agent can check to see if he can move to it
public class Node
{
    public bool walkable;
    public Vector3 worldPostion;

    public Node(bool _walkable, Vector3 _worldPostion)
    {
        walkable = _walkable;
        worldPostion = _worldPostion;
    }
}
