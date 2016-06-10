using UnityEngine;

public class ChunkEditor : MonoBehaviour
{

    [SerializeField]
    private GenerateChunk generateChunk;

    [SerializeField]
    private ChunkHolder chunkLibary;

    [SerializeField]
    private Transform buildPosition;

    [SerializeField]
    private GameObject editableNodePrefab;

    private int lastYLength = 0;

    private int[,] editableChunk;

    void Awake()
    {
        editableChunk = new int[ChunkHolder.xLength, ChunkHolder.CurrentYLength];
    }

    public void EditChunkHeight()
    {
        int[,] temporaryGrid = editableChunk;

        //instantiate editableChunk again with a new y length
        editableChunk = new int[ChunkHolder.xLength, ChunkHolder.CurrentYLength];

        CopyValuesIn2dArray(temporaryGrid);

        //if last Y Length is lower then the current Y Length, add nodes to the chunk
        if (lastYLength < ChunkHolder.CurrentYLength)
        {
            AddNodesToChunk();
        } //else remove nodes from chunk
        else
        {
            RemoveNodesFromChunk();
        }

        lastYLength = ChunkHolder.CurrentYLength;

        chunkLibary.CompressChunk(editableChunk);
    }

    public void AddNodesToChunk()
    {
        //the y length is the total length of the all the chunks, divided by its X length
        int currentYLength = transform.childCount / ChunkHolder.xLength;

        //loop through every node in the chunk
        for (int y = currentYLength; y < ChunkHolder.CurrentYLength; y++)
        {
            for (int x = 0; x < ChunkHolder.xLength; x++)
            {
                //spawn the editableChunk, positive x values are to the right, and negative y values are down. so this creates a grid from top left, to down right, just like the [,]. 
                GameObject editableNode = (GameObject)Instantiate(editableNodePrefab, new Vector3(x * 33, -y * 33, 0) + buildPosition.position, transform.rotation) as GameObject;

                //give the node his starting position and values
                editableNode.GetComponent<Node>().Init(new Vector2(x, y), 0, generateChunk.objToSpawnNameLength());

                //we are the parent of this node
                editableNode.transform.SetParent(transform);
            }
        }
    }

    public void RemoveNodesFromChunk()
    {
        //destroy every chunk that does no longer exist in the editableChunk
        foreach (Node _node in transform.GetComponentsInChildren<Node>())
        {
            //the node y starts at 0, so if the node y is bigger or the same as Ylength, destroy it
            if (_node.Position.y >= ChunkHolder.CurrentYLength)
            {
                Destroy(_node.gameObject);
            }
        }
    }

    //copies the values of the grid onto
    public void CopyValuesIn2dArray(int[,] _temporaryGrid)
    {
        //loop through every node in the chunk
        for (int y = 0; y < _temporaryGrid.Length / ChunkHolder.xLength; y++)
        {
            for (int x = 0; x < ChunkHolder.xLength; x++)
            {
                if (editableChunk.Length / ChunkHolder.xLength > y)
                {
                    editableChunk[x, y] = _temporaryGrid[x, y];
                }
            }
        }
    }

    private void EditANode(Vector2 _position, int _value)
    {
        print(_position);
        editableChunk[(int)_position.x, (int)_position.y] = _value;
    }

    public void SaveChunk()
    {
        chunkLibary.AddChunk(editableChunk);

        ResetChunk();
    }


    public void ResetChunk()
    {
        editableChunk = new int[ChunkHolder.xLength, ChunkHolder.CurrentYLength];

        foreach (Node _node in transform.GetComponentsInChildren<Node>())
        {
            _node.Reset();
        }
    }

    public void EditNodes(Vector2 _middlePosition, Vector2 _size, int _nodeValue) {
        //the position we start editing
        Vector2 positionToEdit = _middlePosition - new Vector2(Mathf.Floor(_size.x / 2), Mathf.Floor(_size.y / 2));

        for (int y = 0; y < _size.y; y++) {
            for (int x = 0; x < _size.x; x++) {
                EditANode(positionToEdit + new Vector2(x,y), _nodeValue);
            }
        }

        chunkLibary.CompressChunk(editableChunk);
    }

    /*
    public void FillNode(Vector2 _startingPosition, int _nodeObjectNumber, int _nodeSize)
    {
        //our current, 2d direction
        int xDirection = 1;

        int yDirection = 0;

        int rowLengthToCheck = 1;

        //index that we use to count
        int index = 0;

        //so we can check what the direction was when we last changed it
        bool xWasPos = false;

        bool yWasPos = true;

        bool increaseRowLength = false;

        while (CheckNodeOccupied((int)startingPosition.x, (int)startingPosition.y))
        {
            //go to the next node in the row
            if (index < rowLengthToCheck)
            {
                startingPosition.x += xDirection;
                startingPosition.y += yDirection;

                index++;
            }
            else
            {
                //reset the index
                index = 0;

                //go to the next direction for x and y
                BackAndForth(ref xDirection, ref xWasPos);
                BackAndForth(ref yDirection, ref yWasPos);

                //increase row length alternately
                if (increaseRowLength)
                {
                    rowLengthToCheck++;
                    increaseRowLength = false;
                }
                else
                    increaseRowLength = true;
            }
        }

        //check how many nodes are occupied
        int occupiedNodeIndex = 0;

        foreach (Node node in nodes)
        {
            if (node.Occupied)
            {
                occupiedNodeIndex++;
            }
        }

        //calc the new node radius, so the camera can use that number to zoom in or out
        occupatedNodesRowsRadius = OccupiedNodesRadius(occupiedNodeIndex);

        if (xPosToChange < maxXLength && yPosToChange < maxYLength)
        {
            nodes[xPosToChange, yPosToChange].Occupied = true;
            nodes[xPosToChange, yPosToChange].NodeNumber = _nodeNumber;

            if (ChosenNode != null)
                ChosenNode(nodes[xPosToChange, yPosToChange], nodeSize);
        }
    }

    bool CheckNodeOccupied(int _x, int _y)
    {
        //check if the node is occupied, and check if the node we are checking exists
        if (_x < maxXLength && _y < maxYLength)
            return nodes[_x, _y].Occupied;
        else
            return false;
    }

    //goes back and forth between -1 and 1
    private void BackAndForth(ref int _dir, ref bool _wasPos)
    {
        //if it is zero, what the direction was when we last changed it
        if (_dir == 0)
        {
            if (_wasPos)
                _dir = -1;
            else
                _dir = 1;
        }
        else //if it is positive or negative, set the bool positive or negative, and set the dir to zero
        {
            if (_dir > 0)
                _wasPos = true;
            else
                _wasPos = false;
            _dir = 0;
        }
    }
    */
}