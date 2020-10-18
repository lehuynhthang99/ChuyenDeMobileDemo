using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellMovement_Type3 : ShellMovementBase
{
    [SerializeField] protected float turnTime;

    public float rotation;
    protected float countDownTurnTime;
    [SerializeField] protected Rigidbody rigidbody;
    // Start is called before the first frame update
    protected override void Start()
    {
        countDownTurnTime = turnTime;
    }

    // Update is called once per frame
    public override void UpdateNormal()
    {
        countDownTurnTime -= Time.deltaTime;
        if (countDownTurnTime <= 0f)
        {
            countDownTurnTime = turnTime;
            transform.rotation *= Quaternion.Euler(Vector3.up * rotation);
            rigidbody.velocity = Quaternion.AngleAxis(rotation, Vector3.up) * rigidbody.velocity;
            rotation = -rotation;
        }
    }
}
