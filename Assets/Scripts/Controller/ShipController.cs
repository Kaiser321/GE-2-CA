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
    public GameObject[] cameraPositions;
    public bool foundPath = false;
    public Spawner spawner;

    public void OnEnable()
    {
        StartCoroutine(Shoot());
    }

    void Start()
    {
        if (tag != "Falcon")
        {
            if (isLeader)
            {
                leader = GetComponent<Boid>();
                //GetComponent<StateMachine>().SetGlobalState(new Alive());
                GetComponent<StateMachine>().ChangeState(new FindFalcon());
            }
            else
            {

                if (transform.parent.gameObject.name.Contains("TIE-Fighter Squad"))
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

    }

    System.Collections.IEnumerator Shoot()
    {
        while (true)
        {
            if (isShooing)
            {
                foreach (GameObject fp in fireingPoints)
                {
                    GameObject bullet = Instantiate(laserBullet, fp.transform.position, fp.transform.rotation);
                    bullet.GetComponent<BulletController>().targetTag = transform.tag == "Tie-Fighter" ? "X-wing" : "Tie-Fighter";
                }

            }
            yield return new WaitForSeconds(1.0f / rateOfFire);
        }
    }

    public void AssignRandomCameraPosition(Camera cam)
    {
        cam.transform.parent = null;

        if (cam.GetComponent<FollowCamera>().enabled)
        {
            cam.GetComponent<FollowCamera>().enabled = false;
        }

        GameObject camPos = cameraPositions[Random.Range(0, cameraPositions.Length)];
        if (camPos.tag == "Follow Cam")
        {
            cam.transform.parent = camPos.transform;
        }

        if (camPos.tag == "Tracking Cam")
        {
            cam.GetComponent<FollowCamera>().target = gameObject;
            cam.GetComponent<FollowCamera>().enabled = true;
        }


        cam.transform.position = camPos.transform.position;
        cam.transform.rotation = camPos.transform.rotation;
    }

    //public void Explode()
    //{
    //    gameObject.SetActive(false);

    //    Destroy(gameObject,3);
    //}

    private void Update()
    {
        if (health <= 0 && tag != "Falcon")
        {
            gameObject.SetActive(false);
            spawner.RemoveShip(gameObject);
            Destroy(gameObject,3);
        }
    }



    //public void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(other);
    //}

}

