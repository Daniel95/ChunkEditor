using UnityEngine;

public class MoveDown : MonoBehaviour
{
    [SerializeField]
    private bool move = true;
    [SerializeField]
    private float speed = 1;
    void FixedUpdate()
    {
        if(move) transform.Translate(new Vector3(0, -GameSpeed.MoveSpeed * speed, 0),Space.World);
    }

    public bool Move {
        set { move = value; }
    }
}
