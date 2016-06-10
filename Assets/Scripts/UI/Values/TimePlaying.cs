using UnityEngine;
using UnityEngine.UI;

public class TimePlaying : ScoreBase {

    //all variables needed for the time
    private float timeCounter, minutes, seconds, fraction;

    protected override void Count()
    {
        base.Count();
        timeCounter += Time.deltaTime;

        minutes = timeCounter / 60;
        seconds = timeCounter % 60;
        fraction = (timeCounter * 100) % 100;

        UpdateTextField();
    }

    public int TimeInt() {
        //format the time in integers, like: (example) 001298
        return Mathf.RoundToInt(minutes) * 10000 + Mathf.RoundToInt(seconds) * 100 + Mathf.RoundToInt(fraction);
    }

    protected override void UpdateTextField()
    {
        base.UpdateTextField();
        //format the time like: (example) 00:12:98
        textField.text = standardText + string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
    }

    public override void ResetValue()
    {
        timeCounter = 0;
        base.ResetValue();
    }
}
