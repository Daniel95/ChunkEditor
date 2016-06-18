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
        if(inputfield.text.Length % ChunkHolder.xLength == 0)
            nodeEditor.BuildSeed(inputfield.text);
    }

    public void ClearInput() {
        inputfield.text = "";
    }
}
