using UnityEngine;
using System.Collections;

public class GridYValue : DynamicText {

    private int gridY;

    public override void ChangeNumber(int _change)
    {
        gridY = _change;
        base.ChangeNumber(_change);
    }

    protected override void UpdateTextField()
    {
        base.UpdateTextField();
        textField.text = standardText + gridY;
    }
}
