using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    private Vector2 position;

    private Vector2 size;

    //our true value is the value that we will use in the final string
    private int trueValue;

    //the value we show we contain, if there is another node overlapping us
    private int confirmedValue = 0;

    private bool confirmed;

    //the node that is overlapping us
    private Vector2 parentNodePosition;

    private int oldSpriteValue;

    private Image image;

    private Sprite sprite;

    private NodeSelection nodeSelection;

    private Color startColor;

    [SerializeField]
    private Color selectedColor;

    void Start() {
        image = GetComponent<Image>();

        nodeSelection = GetComponentInParent<NodeSelection>();
        nodeSelection.AddNode(position, this);

        image.sprite = ObjectsInNodeInfo.NodeSprites[trueValue];
        startColor = image.color;
    }

    public void Init(Vector2 _pos, int _objectValue, int _maxObjNumber) {
        position = _pos;

        trueValue = _objectValue;
    }

    public void Delete() {
        nodeSelection.RemoveNode(this);
        Destroy(this.gameObject);
    }

    public void Select(int _spriteValue) {
        image.sprite = ObjectsInNodeInfo.NodeSprites[_spriteValue];
    }

    public void Deselect()
    {
        if (!confirmed)
        {
            image.sprite = ObjectsInNodeInfo.NodeSprites[trueValue];
        }
        else
        {
            image.sprite = ObjectsInNodeInfo.NodeSprites[confirmedValue];
        }
    }

    public void Reset() {
        trueValue = 0;
        image.sprite = ObjectsInNodeInfo.NodeSprites[trueValue];
    }

    public void MouseOver() {
        nodeSelection.SelectedNode(this);
    }

    public void MouseExit() {
        
    }

    public void MouseDown()
    {
        nodeSelection.ConfirmNode(this);
    }

    public Vector2 Position {
        get { return position; }
    }

    public Vector2 Size
    {
        set { size = value; }
        get { return size; }
    }

    public int TrueValue
    {
        set { trueValue = value; }
        get { return trueValue; }
    }

    public bool Confirmed
    {
        get { return confirmed; }
        set { confirmed = value; }
    }

    public int ConfirmedValue
    {
        set { confirmedValue = value; }
    }

    public Vector2 ParentNodePosition
    {
        get { return parentNodePosition; }
        set { parentNodePosition = value; }
    }
}
