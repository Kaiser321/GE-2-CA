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
    public float rateOfFire = 1;
    public float range = 30;
    public GameObject laserBullet;
    public GameObject[] fireingPoints;

    public void OnEnable()
    {
        StartCoroutine(Shoot());
    }

    void Start()
    {
        if (isLeader)
        {
            leader = GetComponent<Boid>();
            GetComponent<StateMachine>().ChangeState(new FindFalcon());
            GetComponent<StateMachine>().SetGlobalState(new Alive());
        }
        else
        {
 
            if(transform.parent.gameObject.name == "TIE-Fighter Squad")
            {
                leader = transform.parent.Find("TIE-Fighter Leader").gameObject.GetComponent<Boid>();
            }
            else
            {
                leader = transform.parent.Find("X-wing Leader").gameObject.GetComponent<Boid>();
            }

            GetComponent<StateMachine>().ChangeState(new FollowingLeader());
        }
    }

    System.Collections.IEnumerator Shoot()
    {
        while (true)
        {
            if (isShooing)
            {
                foreach(GameObject fp in fireingPoints)
                {
                    Instantiate(laserBullet, fp.transform.position, fp.transform.rotation);
                }

            }
            yield return new WaitForSeconds(1.0f / rateOfFire);
        }
    }

    public void Explode()
    {
        Destroy(gameObject);
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(other);
    //}

}

