using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 100;
    public int time = 5;
    public string targetTag;
    public float damageRadius = 2.5f;
    public ParticleSystem impactEffect;

    void Start()
    {
        Destroy(this.gameObject, time);
    }

    void Update()
    {
        transform.Translate(0, 0, Random.Range(speed - 20, speed + 20) * Time.deltaTime);

        CheckDamage(GameObject.FindGameObjectWithTag("Falcon"));

        GameObject[] ships = GameObject.FindGameObjectsWithTag(targetTag);

        foreach (GameObject ship in ships)
        {

            CheckDamage(ship);

        }

    }

    void CheckDamage(GameObject target)
    {
        if (Vector3.Distance(transform.position, target.transform.position) < damageRadius)
        {
            ParticleSystem p = Instantiate(impactEffect, transform.position, transform.rotation);
            p.GetComponent<ParticleSystemRenderer>().material = gameObject.GetComponent<TrailRenderer>().material;
            target.GetComponent<ShipController>().health -= 1;
            Destroy(gameObject);
        }
    }

}
