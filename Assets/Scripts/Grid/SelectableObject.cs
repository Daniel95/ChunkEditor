using UnityEngine;
using UnityEngine.UI;

public class SelectableObject : MonoBehaviour {

    private int objectValue;

    private Vector2 objectSize;

    public void SetObjectNumber(int _objNumber) {
        objectValue = _objNumber;

        //save all the values in this selectable object, as listed in the ObjectInNodeInfo
        GetComponent<Image>().sprite = ObjectsInNodeInfo.NodeSprites[_objNumber];
        objectSize = ObjectsInNodeInfo.NodeSizes[_objNumber];
    }

    public void ChooseObject() {
        GetComponentInParent<ObjectSelection>().SelectObject(objectValue, objectSize);
    }

    public int ObjectValue
    {
        set { objectValue = value; }
    }

    public Vector2 ObjectSize
    {
        set { objectSize = value; }
    }
}
