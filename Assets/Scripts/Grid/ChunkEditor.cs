using UnityEngine;

public class ChunkEditor : MonoBehaviour
{
    [SerializeField]
    private GenerateChunk generateChunk;

    [SerializeField]
    private ChunkHolder chunkLibary;

    [SerializeField]
    private SeedDisplay seedDisplay;

    [SerializeField]
    private Transform buildPosition;

    [SerializeField]
    private GameObject editableNodePrefab;

    private int lastYLength = 0;

    private int[,] editableChunk;

    private bool removingNodes;

    void Awake()
    {
        editableChunk = new int[ChunkHolder.xLength, ChunkHolder.CurrentYLength];
    }

    public void EditChunkHeight(int _newYLength)
    {
        ChunkHolder.CurrentYLength = _newYLength;

        //if last Y Length is lower then the current Y Length, add nodes to the chunk
        if (lastYLength < ChunkHolder.CurrentYLength)
        {
            AddNodesToChunk();
        } //else remove nodes from chunk
        else
        {
            RemoveNodesFromChunk();
        }

        int[,] temporaryGrid = editableChunk;

        //instantiate editableChunk again with a new y length
        editableChunk = new int[ChunkHolder.xLength, ChunkHolder.CurrentYLength];

        CopyValuesIn2dArray(temporaryGrid);

        lastYLength = ChunkHolder.CurrentYLength;

        seedDisplay.UpdateSeed(editableChunk);
    }

    private void AddNodesToChunk()
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

                //we are the parent of this node
                editableNode.transform.SetParent(transform);

                //give the node his starting position and values
                editableNode.GetComponent<Node>().Init(new Vector2(x, y), 0, generateChunk.objToSpawnNameLength());
            }
        }
    }

    private void RemoveNodesFromChunk()
    {
        //destroy every chunk that does no longer exist in the editableChunk
        foreach (Node _node in transform.GetComponentsInChildren<Node>())
        {
            //the node y starts at 0, so if the node y is bigger or the same as Ylength, destroy it
            if (_node.Position.y >= ChunkHolder.CurrentYLength)
            {
                _node.Delete();
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

    public void EditANode(Vector2 _position, int _value)
    {
        editableChunk[(int)_position.x, (int)_position.y] = _value;

        seedDisplay.UpdateSeed(editableChunk);
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

        seedDisplay.UpdateSeed(editableChunk);
    }

    public void EditNodes(Vector2 _middlePosition, Vector2 _nodeSize, Vector2 _selectionSize, int _nodeValue)
    {
        //the position we start editing
        Vector2 positionToEdit = _middlePosition - new Vector2(Mathf.Floor(_nodeSize.x / 2), Mathf.Floor(_nodeSize.y / 2));

        //Vector2 selectionCount = new Vector2(_selectionSize.x / _nodeSize.x, _selectionSize.y / _nodeSize.y);

        for (int y = 0; y < _nodeSize.y; y++)
        {
            for (int x = 0; x < _nodeSize.x; x++)
            {
                if (x - Mathf.Floor(_nodeSize.x / 2) % _nodeSize.x == 0 && y - Mathf.Floor(_nodeSize.y / 2) % _nodeSize.y == 0)
                {
                    EditANode(positionToEdit + new Vector2(x, y), _nodeValue);
                    break;
                }
           }
        }
    }

    /*
    public void EditNodes(Vector2 _middlePosition, Vector2 _nodeSize, Vector2 _selectionSize, int _nodeValue) {
        //the position we start editing
        Vector2 positionToEdit = _middlePosition - new Vector2(Mathf.Floor(_nodeSize.x / 2), Mathf.Floor(_nodeSize.y / 2));

        Vector2 selectionCount = new Vector2(_selectionSize.x / _nodeSize.x, _selectionSize.y / _nodeSize.y);

        for (int y = 0; y < _nodeSize.y; y++) {
            if (y < _nodeSize.y * selectionCount.y)
            {
                for (int x = 0; x < _nodeSize.x; x++)
                {
                    if (x < _nodeSize.x * selectionCount.x)
                    {
                        EditANode(positionToEdit + new Vector2(x, y), _nodeValue);
                    }
                }
            }
        }

        chunkLibary.CompressChunk(editableChunk);
    }
    */
}