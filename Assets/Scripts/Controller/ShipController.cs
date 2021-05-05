using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public int health = 10;
    public GameObject target;
    public Boid leader;
    public bool isLeader = false;
    public bool isShooing = false;
    public int rateOfFire = 1;
    public GameObject laserBullet;

    public void OnEnable()
    {
        StartCoroutine(Shoot());
    }

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

    System.Collections.IEnumerator Shoot()
    {
        while (true)
        {
            if (isShooing)
            {
                Instantiate(laserBullet, transform.position + transform.forward * 2, transform.rotation);
            }
            yield return new WaitForSeconds(1.0f / rateOfFire);
        }
    }

}

