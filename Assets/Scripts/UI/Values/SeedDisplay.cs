public class SeedDisplay : DynamicText
{
    private string seed;

    public void UpdateSeed(int[,] _chunk)
    {
        ChangeString(SeedConverter.CompressChunk(_chunk));
    }

    public override void ChangeString(string _change)
    {
        seed = _change;
        base.ChangeString(_change);
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
