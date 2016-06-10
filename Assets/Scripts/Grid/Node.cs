using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour {

    private Vector2 position;

    private Vector2 size;

    private int objNumber;

    private Image image;

    private Sprite sprite;

    private ObjectSelection objectSelection;

    private Color startColor;

    [SerializeField]
    private Color selectedColor;

    void Start() {
        image.sprite = ObjectsInNodeInfo.NodeSprites[objNumber];
        startColor = image.color;
    }

    void Awake() {
        image = GetComponent<Image>();

        objectSelection = GameObject.Find("ObjectSelectionMenu").GetComponent<ObjectSelection>();
    }

    void OnDisable() {
        objectSelection.DoneSelecting -= SetNewObject;
    }

    public void Init(Vector2 _pos, int _objectValue, int _maxObjNumber) {
        position = _pos;

        objNumber = _objectValue;
    }

    public void ChangeObject() {
        //set the color to selectedColor
        image.color = selectedColor;

        objectSelection.DoneSelecting += SetNewObject;

        objectSelection.StartMenu(GetComponent<Node>());
    }

    public void SetNewObject(int _newObjNumber) {
        //reset the color of the object
        image.color = startColor;

        objNumber = _newObjNumber;
        image.sprite = ObjectsInNodeInfo.NodeSprites[objNumber];


        //unsubscribe itself to the setnewobject method, because it is done editing
        objectSelection.DoneSelecting -= SetNewObject;
    }

    public void Reset() {
        objNumber = 0;
        image.sprite = ObjectsInNodeInfo.NodeSprites[objNumber];
    }

    public Vector2 Position {
        get { return position; }
    }

    public Vector2 Size
    {
        get { return size; }
    }

    public int ObjNumber
    {
        get { return objNumber; }
    }
}
