using UnityEngine.UI;

public class PlayerDeaths : ScoreBase
{
    private Text deathsTextField;

    //standard deaths text field text
    private string deathsText;

    private int deaths;

    public int Deaths
    {
        get { return deaths; }
    }

    public void Died()
    {
        deaths++;
        UpdateTextField();
    }

    protected override void UpdateTextField()
    {
        base.UpdateTextField();
        textField.text = standardText + deaths;
    }

    public override void ResetValue()
    {
        deaths = 0;
        base.ResetValue();
    }
}
