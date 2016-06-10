using UnityEngine;

public class PlayerDistance : ScoreBase {

    [SerializeField]
    private float speed = 0.05f;

    private float distance;

    protected override void Count()
    {
        base.Count();
        distance += speed * (GameSpeed.SpeedMultiplier + GameSpeed.ExtraSpeed);
        UpdateTextField();
    }

    protected override void UpdateTextField()
    {
        base.UpdateTextField();
        textField.text = ""+(int)distance;
    }

    public override void ResetValue()
    {
        distance = 0;
        base.ResetValue();
    }

    public int Distance {
        get { return (int)distance; }
    }
}
