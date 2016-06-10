public class SeedValue : ScoreBase {

    private string seed;

    public override void ChangeValue(string _change)
    {
        seed = _change;
        base.ChangeValue(_change);
    }

    protected override void UpdateTextField()
    {
        base.UpdateTextField();

        textField.text = standardText + seed;
    }

    public string Seed {
        get { return seed; }
    }
}
