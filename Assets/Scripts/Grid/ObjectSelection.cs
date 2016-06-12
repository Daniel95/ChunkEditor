using UnityEngine;
using System.Collections.Generic;

public class ObjectSelection : MonoBehaviour
{
    //the images we display that the player can choose from
    [SerializeField]
    private List<GameObject> selectionOptions;

    private int selectedObjectValue = 0;

    private Vector2 selectedObjectSize = new Vector2(1, 1);

    void Start() {
        for (int i = 0; i < selectionOptions.Count; i++)
        {
            //the object number is its number in the list
            selectionOptions[i].GetComponent<SelectableObject>().SetObjectNumber(i);
        }
    }

    //select a new object
    public void SelectObject(int _objectValue, Vector2 _objectSize)
    {
        selectedObjectValue = _objectValue;
        selectedObjectSize = _objectSize;
    }

    public int SelectedObjectValue
    {
        get { return selectedObjectValue; }
    }

    public Vector2 SelectedObjectSize
    {
        get { return selectedObjectSize; }
    }
}
