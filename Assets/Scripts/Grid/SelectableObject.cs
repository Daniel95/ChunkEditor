using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectableObject : MonoBehaviour {

    private int objectNumber;

    private Vector2 objectSize;

    public void SetObjectNumber(int _objNumber) {
        objectNumber = _objNumber;

        //save all the values in this selectable object, as listed in the ObjectInNodeInfo
        GetComponent<Image>().sprite = ObjectsInNodeInfo.NodeSprites[_objNumber];
        objectSize = ObjectsInNodeInfo.NodeSizes[_objNumber];
    }

    public void ChooseObject() {
        GetComponentInParent<ObjectSelection>().ChangeObject(objectNumber, objectSize);
    }

    public int ObjectNumber {
        set { objectNumber = value; }
    }

    public Vector2 ObjectSize
    {
        set { objectSize = value; }
    }
}
