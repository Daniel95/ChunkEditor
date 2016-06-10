using UnityEngine;
using System.Collections;

public class DynamicText : ScoreBase {

    private string addedText;

    public void AddString(string _text)
    {
        addedText = _text;
        UpdateTextField();
    }

    protected override void UpdateTextField()
    {
        base.UpdateTextField();
        textField.text = standardText + addedText;
    }
}
