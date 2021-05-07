using UnityEngine;

class FollowingLeader : State
{
    OffsetPursue offsetPursue;
    public override void Enter()
    {
        offsetPursue = owner.GetComponent<OffsetPursue>();
        offsetPursue.enabled = true;
        offsetPursue.leader = owner.GetComponent<ShipController>().leader;
    }

    public override void Think()
    {
        string tag;
        if (owner.gameObject.tag == "Tie-Fighter")
        {
            tag = "X-wing";
        }
        else
        {
            tag = "Tie-Fighter";
        }
        GameObject[] ships = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject ship in ships)
        {
            if (Vector3.Distance(owner.transform.position, ship.transform.position) <= 50)
            {
                ship.GetComponent<ShipController>().target = owner.gameObject;
                ship.GetComponent<StateMachine>().ChangeState(new Fleeing());
                owner.GetComponent<ShipController>().target = ship;
                owner.ChangeState(new Pursuing());
            }
        }
    }

    public override void Exit()
    {
        offsetPursue.enabled = false;
    }
}


//class Patrol : State
//{
//    FollowPath followPath;
//    PathFinder pathFinder;
//    public override void Enter()
//    {
//        followPath = owner.GetComponent<FollowPath>();
//        pathFinder = owner.GetComponent<PathFinder>();
//        owner.GetComponent<ShipController>().target = GameObject.Find("Millennium Falcon");
//        pathFinder.end = owner.GetComponent<ShipController>().target.transform;
//        followPath.enabled = true;
//        pathFinder.enabled = true;
//    }

//    public override void Think()
//    {

//    }

//    public override void Exit()
//    {
//        followPath.enabled = false;
//        pathFinder.enabled = false;
//    }
//}

class FindFalcon : State
{
    Pursue pursue;
    public override void Enter()
    {
        owner.GetComponent<ShipController>().isShooing = false;
        pursue = owner.GetComponent<Pursue>();
        pursue.enabled = true;
        owner.GetComponent<ShipController>().target = GameObject.Find("Millennium Falcon");
        pursue.target = owner.GetComponent<ShipController>().target.GetComponent<Boid>();
    }

    public override void Think()
    {
        string tag;
        if (owner.gameObject.tag == "Tie-Fighter")
        {
            tag = "X-wing";
            if (Vector3.Distance(owner.transform.position, pursue.target.transform.position) < 20)
            {
                owner.ChangeState(new Pursuing());
            }
        }
        else
        {
            tag = "Tie-Fighter";
            if (Vector3.Distance(owner.transform.position, pursue.target.transform.position) < 20)
            {
                owner.GetComponent<ShipController>().leader = pursue.target;
                owner.ChangeState(new FollowingLeader());
            }
        }
        GameObject[] ships = GameObject.FindGameObjectsWithTag(tag);


        foreach (GameObject ship in ships)
        {
            if (Vector3.Distance(owner.transform.position, ship.transform.position) < 20)
            {
                ship.GetComponent<ShipController>().target = owner.gameObject;
                ship.GetComponent<StateMachine>().ChangeState(new Fleeing());
                owner.GetComponent<ShipController>().target = ship;
                owner.ChangeState(new Pursuing());
                break;
            }
        }

    }

    public override void Exit()
    {
        pursue.enabled = false;
    }
}

class Pursuing : State
{
    Pursue pursue;
    public override void Enter()
    {
        pursue = owner.GetComponent<Pursue>();
        pursue.enabled = true;
        pursue.target = owner.GetComponent<ShipController>().target.GetComponent<Boid>();
    }

    public override void Think()
    {
        if (pursue.target == null)
        {
            owner.ChangeState(new FindFalcon());
        }
        if (Vector3.Distance(owner.transform.position, pursue.target.transform.position) < owner.GetComponent<ShipController>().range)
        {
            owner.GetComponent<ShipController>().isShooing = true;
        }
        else
        {
            owner.GetComponent<ShipController>().isShooing = false;
        }

        if (Vector3.Distance(owner.transform.position, pursue.target.transform.position) < 2)
        {
            owner.ChangeState(new FindFalcon());
        }

        if (pursue.target.gameObject == owner.gameObject)
        {
            owner.GetComponent<ShipController>().target = pursue.target.gameObject;
            owner.GetComponent<StateMachine>().ChangeState(new Fleeing());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<ShipController>().isShooing = false;
        pursue.enabled = false;
    }
}


class Fleeing : State
{
    Flee flee;
    GameObject falcon;
    public override void Enter()
    {
        falcon = GameObject.Find("Millennium Falcon");
        flee = owner.GetComponent<Flee>();
        flee.enabled = true;
        flee.targetGameObject = owner.GetComponent<ShipController>().target;
    }

    public override void Think()
    {
        if (owner.tag == "Falcon")
        {

        }
        else
        {
            if (flee.target == null)
            {
                owner.ChangeState(new FindFalcon());
            }

            if (Vector3.Distance(owner.transform.position, falcon.transform.position) > 50)
            {
                owner.GetComponent<ShipController>().isShooing = true;
                owner.ChangeState(new FindFalcon());
            }
        }

    }

    public override void Exit()
    {
        flee.enabled = false;
    }
}

public class Alive : State
{
    public override void Think()
    {
        if (owner.GetComponent<ShipController>().health <= 0)
        {

            owner.GetComponent<ShipController>().Explode();
        }
    }
}


//class PatrolState : State
//{
//    public override void Enter()
//    {
//        owner.GetComponent<FollowPath>().enabled = true;
//    }

//    public override void Think()
//    {
//        if (Vector3.Distance(
//            owner.GetComponent<Fighter>().enemy.transform.position,
//            owner.transform.position) < 10)
//        {
//            owner.ChangeState(new DefendState());
//        }
//    }

//    public override void Exit()
//    {
//        owner.GetComponent<FollowPath>().enabled = false;
//    }
//}

//public class DefendState : State
//{
//    public override void Enter()
//    {
//        owner.GetComponent<Pursue>().target = owner.GetComponent<Fighter>().enemy.GetComponent<Boid>();
//        owner.GetComponent<Pursue>().enabled = true;
//    }

//    public override void Think()
//    {
//        Vector3 toEnemy = owner.GetComponent<Fighter>().enemy.transform.position - owner.transform.position; 
//        if (Vector3.Angle(owner.transform.forward, toEnemy) < 45 && toEnemy.magnitude < 20)
//        {
//            GameObject bullet = GameObject.Instantiate(owner.GetComponent<Fighter>().bullet, owner.transform.position + owner.transform.forward * 2, owner.transform.rotation);
//            owner.GetComponent<Fighter>().ammo --;        
//        }
//        if (Vector3.Distance(
//            owner.GetComponent<Fighter>().enemy.transform.position,
//            owner.transform.position) > 30)
//        {
//            owner.ChangeState(new PatrolState());
//        }
//    }

//    public override void Exit()
//    {
//        owner.GetComponent<Pursue>().enabled = false;
//    }

//}


//public class AttackState : State
//{
//    public override void Enter()
//    {
//        owner.GetComponent<Pursue>().target = owner.GetComponent<Fighter>().enemy.GetComponent<Boid>();
//        owner.GetComponent<Pursue>().enabled = true;
//    }

//    public override void Think()
//    {
//        Vector3 toEnemy = owner.GetComponent<Fighter>().enemy.transform.position - owner.transform.position; 
//        if (Vector3.Angle(owner.transform.forward, toEnemy) < 45 && toEnemy.magnitude < 30)
//        {
//            GameObject bullet = GameObject.Instantiate(owner.GetComponent<Fighter>().bullet, owner.transform.position + owner.transform.forward * 2, owner.transform.rotation);
//            owner.GetComponent<Fighter>().ammo --;
//        }        
//        if (Vector3.Distance(
//            owner.GetComponent<Fighter>().enemy.transform.position,
//            owner.transform.position) < 10)
//        {
//            owner.ChangeState(new FleeState());
//        }

//    }

//    public override void Exit()
//    {
//        owner.GetComponent<Pursue>().enabled = false;
//    }
//}

//public class FleeState : State
//{
//    public override void Enter()
//    {
//        owner.GetComponent<Flee>().targetGameObject = owner.GetComponent<Fighter>().enemy;
//        owner.GetComponent<Flee>().enabled = true;
//    }

//    public override void Think()
//    {
//        if (Vector3.Distance(
//            owner.GetComponent<Fighter>().enemy.transform.position,
//            owner.transform.position) > 30)
//        {
//            owner.ChangeState(new AttackState());
//        }
//    }
//    public override void Exit()
//    {
//        owner.GetComponent<Flee>().enabled = false;
//    }
//}

//public class Alive:State
//{
//    public override void Think()
//    {

//        if (owner.GetComponent<Fighter>().health <= 0)
//        {
//            Dead dead = new Dead();
//            owner.ChangeState(dead);
//            owner.SetGlobalState(dead);
//            return;
//        }

//        if (owner.GetComponent<Fighter>().health <= 2)
//        {
//            owner.ChangeState(new FindHealth());
//            return;
//        }

//        if (owner.GetComponent<Fighter>().ammo <= 0)
//        {
//            owner.ChangeState(new FindAmmo());
//            return;
//        }
//    }
//}

//public class Dead:State
//{
//    public override void Enter()
//    {
//        SteeringBehaviour[] sbs = owner.GetComponent<Boid>().GetComponents<SteeringBehaviour>();
//        foreach(SteeringBehaviour sb in sbs)
//        {
//            sb.enabled = false;
//        }
//        owner.GetComponent<StateMachine>().enabled = false;        
//    }         
//}

//public class FindAmmo:State
//{
//    Transform ammo;
//    public override void Enter()
//    {
//        GameObject[] ammos = GameObject.FindGameObjectsWithTag("Ammo");
//        // Find the closest ammo;
//        Transform closest = ammos[0].transform;
//        foreach(GameObject go in ammos)
//        {
//            if (Vector3.Distance(go.transform.position, owner.transform.position) <
//                Vector3.Distance(closest.position, owner.transform.position))
//                {
//                    closest = go.transform;
//                }
//        }
//        ammo = closest;
//        owner.GetComponent<Seek>().targetGameObject = ammo.gameObject;
//        owner.GetComponent<Seek>().enabled = true;
//    }

//    public override void Think()
//    {
//        // If the other guy already took tghe ammo
//        if (ammo == null)
//        {
//            owner.ChangeState(new FindAmmo());
//            return;
//        }
//        if (Vector3.Distance(owner.transform.position, ammo.position) < 1)
//        {
//            owner.GetComponent<Fighter>().ammo += 10;
//            owner.RevertToPreviousState();
//            GameObject.Destroy (ammo.gameObject);
//        }
//    }

//    public override void Exit()
//    {
//        owner.GetComponent<Seek>().enabled = false;
//    }
//}

//public class FindHealth:State
//{
//    Transform health;
//    public override void Enter()
//    {
//        GameObject[] healths = GameObject.FindGameObjectsWithTag("Health");
//        // Find the closest ammo;
//        Transform closest = healths[0].transform;
//        foreach(GameObject go in healths)
//        {
//            if (Vector3.Distance(go.transform.position, owner.transform.position) <
//                Vector3.Distance(closest.position, owner.transform.position))
//                {
//                    closest = go.transform;
//                }
//        }
//        health = closest;
//        owner.GetComponent<Seek>().targetGameObject = health.gameObject;
//        owner.GetComponent<Seek>().enabled = true;
//    }

//    public override void Think()
//    {
//        // If the other guy already took the health
//        if (health == null)
//        {
//            owner.ChangeState(new FindHealth());
//            return;
//        }
//        if (Vector3.Distance(owner.transform.position, health.transform.position) < 2)
//        {
//            owner.GetComponent<Fighter>().health += 10;
//            owner.RevertToPreviousState();
//            GameObject.Destroy (health.gameObject);
//        }
//    }

//    public override void Exit()
//    {
//        owner.GetComponent<Seek>().enabled = false;
//    }
//}