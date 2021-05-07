using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject xwingPrefab;
    public GameObject tiePrefab;
    public GameObject asteriodPrefab;
    public List<GameObject> xwings = new List<GameObject>();
    public List<GameObject> ties = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //SpawnAsteroidCluster(new Vector3(0, 0, 0), new Vector3(100, 50, 500), 150);
    }

    public GameObject SpawnXWingSquad(Vector3 pos, Transform tagret)
    {
        List<int> gap = new List<int>() { 2, -2 };
        GameObject squad = new GameObject("X-wing Squad");
        GameObject leader = Instantiate(xwingPrefab);
        leader.name = "X-wing Leader";
        leader.GetComponent<ShipController>().isLeader = true;
        leader.GetComponent<ShipController>().spawner = this;
        leader.transform.parent = squad.transform;
        xwings.Add(leader);
        foreach (int g in gap)
        {
            GameObject fighter = Instantiate(xwingPrefab, squad.transform);
            fighter.name = "X-wing";
            fighter.GetComponent<ShipController>().spawner = this;
            fighter.transform.position = new Vector3(g, 0, 0);
            fighter.transform.parent = squad.transform;
            xwings.Add(fighter);
        }

        squad.transform.position = pos;
        squad.transform.LookAt(tagret);
        return squad;
    }

    public GameObject SpawnTIEFighterSquad(Vector3 pos, Transform tagret)
    {
        List<int> gap = new List<int>() { 8, -8 };
        GameObject squad = new GameObject("TIE-Fighter Squad");
        GameObject leader = Instantiate(tiePrefab);
        leader.name = "TIE-Fighter Leader";
        leader.GetComponent<ShipController>().isLeader = true;
        leader.GetComponent<ShipController>().spawner = this;
        leader.transform.parent = squad.transform;
        ties.Add(leader);

        foreach (int g in gap)
        {
            GameObject fighter = Instantiate(tiePrefab, squad.transform);
            fighter.name = "TIE-Fighter";
            fighter.GetComponent<ShipController>().spawner = this;
            fighter.transform.position = new Vector3(g, 0, 0);
            fighter.transform.parent = squad.transform;
            ties.Add(fighter);
        }

        squad.transform.position = pos;
        squad.transform.LookAt(tagret);
        return squad;
    }

    public GameObject SpawnAsteroidCluster(Vector3 centerPos, Vector3 size, int num)
    {
        GameObject cluster = new GameObject("AsteroidCluster");
        cluster.transform.position = centerPos;

        for (int i = 0; i < num; i++)
        {
            GameObject asteroid = Instantiate(asteriodPrefab);
            //asteroid.transform.position = centerPos + Random.insideUnitSphere * radius;
            asteroid.transform.position = RandomInsideBox(centerPos, size);
            float asize = Random.Range(0.01f, 0.05f);
            //asteroid.transform.Find("Body").GetComponent<SphereCollider>().radius = size * 
            asteroid.transform.localScale = new Vector3(asize, asize, asize);
            asteroid.transform.eulerAngles = new Vector3(Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359));
            asteroid.transform.parent = cluster.transform;
        }
        return cluster;
    }

    public Vector3 RandomInsideBox(Vector3 centerPos, Vector3 size)
    {
        return centerPos + new Vector3(
       (Random.value - 0.5f) * size.x,
       (Random.value - 0.5f) * size.y,
       (Random.value) * size.z);
    }

    public void RemoveShip(GameObject ship)
    {
        if (ship.tag == "X-wing")
        {
            xwings.Remove(ship);
        }
        else
        {
            ties.Remove(ship);
        }
    }
}
