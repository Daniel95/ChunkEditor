using UnityEngine;

public class CopySeed : MonoBehaviour {

    [SerializeField]
    private SeedValue seedValue;

    public void CopyToClipBoard()
    {
        TextEditor te = new TextEditor();
        te.text = seedValue.Seed;
        te.SelectAll();
        te.Copy();
    }
}
