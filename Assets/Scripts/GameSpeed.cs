using UnityEngine;
using System.Collections;
public class GameSpeed : MonoBehaviour
{

    [SerializeField]
    private float gameSpeedIncrement = 0.00021f;

    [SerializeField]
    private float gameSpeedIncrementMultiplier = 0.99993f;

    [SerializeField]
    private float startSpeed = 0.05f;

    [SerializeField]
    private static float extraSpeed = 0f;
    private static float speed;

    private static float speedMultiplier = 1;

    void Start()
    {
        speed = startSpeed;
    }

    void FixedUpdate()
    {
        speedMultiplier += gameSpeedIncrement;
        gameSpeedIncrement *= gameSpeedIncrementMultiplier;
    }

    public void SuperMode()
    {
        StartCoroutine(SuperModeAceleration());
    }

    IEnumerator SuperModeAceleration()
    {
        while (extraSpeed < 0.15f)
        {
            extraSpeed += 0.001f;
            yield return new WaitForFixedUpdate();
        }
    }

    public void NormalMode()
    {
        StartCoroutine(NormalModeAceleration());
    }

    IEnumerator NormalModeAceleration()
    {
        while (extraSpeed > 0)
        {
            extraSpeed -= 0.001f;
            yield return new WaitForFixedUpdate();
        }
    }

    public void Reset()
    {
        speedMultiplier = 1;
        startSpeed = speed;
    }

    public static float ExtraSpeed {
        get { return extraSpeed; }
    }

    public static float MoveSpeed
    {
        //return startMoveSpeed * gameSpeed to know how fast things are moving
        get { return speed * speedMultiplier + extraSpeed; }
    }

    public static float SpeedMultiplier
    {
        //return the speed multiplier, which is incremented
        get { return speedMultiplier; }
    }
}
