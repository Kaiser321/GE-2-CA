using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XWingController : MonoBehaviour
{
    public int health = 10;
    public GameObject target;
    public Boid leader;
    public bool isLeader = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isLeader)
        {
            leader = GetComponent<Boid>();
            GetComponent<StateMachine>().ChangeState(new PursuingTarget());
        }
        else
        {
            leader = transform.parent.Find("X-wing Leader").gameObject.GetComponent<Boid>();
            GetComponent<StateMachine>().ChangeState(new FollowingLeader());
        }

    }

}

class FollowingLeader : State
{
    OffsetPursue OffsetPursue;
    public override void Enter()
    {
        OffsetPursue = owner.GetComponent<OffsetPursue>();
        OffsetPursue.enabled = true;
        OffsetPursue.leader = owner.GetComponent<XWingController>().leader;
    }

    public override void Think()
    {

    }

    public override void Exit()
    {
        owner.GetComponent<OffsetPursue>().enabled = false;
    }
}


class PursuingTarget : State
{
    Pursue Pursue;
    public override void Enter()
    {
        Pursue = owner.GetComponent<Pursue>();
        Pursue.enabled = true;
    }

    public override void Think()
    {

    }

    public override void Exit()
    {
        owner.GetComponent<Pursue>().enabled = false;
    }
}

class Attacking : State
{

}

class Fleeing : State
{

}
