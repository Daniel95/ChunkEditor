public class PlayerPickups : ScoreBase {

    private int pickups;

    public int Pickups {
        get { return pickups; }
    }

    public override void ChangeNumber(int _value)
    {
        pickups++;
        base.ChangeNumber(_value);
    }

    protected override void UpdateTextField()
    {
        base.UpdateTextField();
        textField.text = standardText + pickups;
    }

    public override void ResetValue()
    {
        pickups = 0;
        base.ResetValue();
    }
}
