using UnityEngine;

public class CopySeed : MonoBehaviour {

    [SerializeField]
    private SeedDisplay seedDisplay;

    public void CopyToClipBoard()
    {
        TextEditor te = new TextEditor();
        te.text = seedDisplay.Seed;
        te.SelectAll();
        te.Copy();
    }
}
