using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SeedInput : MonoBehaviour {

    [SerializeField]
    private NodeEditor nodeEditor;

    private InputField inputfield;

    void Start() {
        inputfield = GetComponent<InputField>();
    }

    public void RecieveSeedInput() {
        nodeEditor.BuildSeed(inputfield.text);
    }
}
