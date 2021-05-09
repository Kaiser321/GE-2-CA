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
    public AudioSource flyingAudioSource;
    public AudioSource shootingAudioSource;
    public AudioClip[] flyingBySounds;
    public AudioClip explodingSound;
    public AudioClip[] shootingSounds;

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
                    PlaySound("Shooting");
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

    private void Update()
    {
        if (health <= 0 && tag != "Falcon")
        {
            PlaySound("Explode");
            gameObject.SetActive(false);
            spawner.RemoveShip(gameObject);
            Destroy(gameObject, 3);
        }
    }


    public void PlaySound(string type)
    {
        AudioClip audioClip;
        if (type == "Flyby")
        {
            audioClip = flyingBySounds[Random.Range(0, flyingBySounds.Length)];
            flyingAudioSource.PlayOneShot(audioClip);
        }
        else if (type == "Shooting")
        {
            audioClip = shootingSounds[Random.Range(0, shootingSounds.Length)];
            shootingAudioSource.PlayOneShot(audioClip);
        }
        else
        {
            audioClip = explodingSound;
            flyingAudioSource.PlayOneShot(audioClip);
        }

    }
}

