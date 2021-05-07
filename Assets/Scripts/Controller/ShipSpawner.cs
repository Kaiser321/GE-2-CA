using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    public GameObject xwingPrehab;
    public GameObject tiePrehab;
    // Start is called before the first frame update
    void Start()
    {
        SpawnXWingSquad(new Vector3(0, 0, 0), new Vector3(0, 0, 0));
    }


    public void SpawnXWingSquad(Vector3 pos, Vector3 rot)
    {
        List<int> gap = new List<int>() { 2, -2 };
        GameObject squad = new GameObject("X-wing Squad");
        GameObject leader = Instantiate(xwingPrehab);
        leader.name = "X-wing Leader";
        leader.GetComponent<ShipController>().isLeader = true;
        leader.transform.parent = squad.transform;

        foreach (int g in gap)
        {
            GameObject fighter = Instantiate(xwingPrehab, squad.transform);
            fighter.name = "X-wing";
            fighter.transform.position = new Vector3(g, 0, 0);
            fighter.transform.parent = squad.transform;
        }

        squad.transform.position = pos;
        squad.transform.eulerAngles = rot;
    }

    public void SpawnTIEFighterSquad(Vector3 pos, Vector3 rot)
    {
        List<int> gap = new List<int>() { 8, -8 };
        GameObject squad = new GameObject("TIE-Fighter Squad");
        GameObject leader = Instantiate(tiePrehab);
        leader.name = "TIE-Fighter Leader";
        leader.GetComponent<ShipController>().isLeader = true;
        leader.transform.parent = squad.transform;

        foreach (int g in gap)
        {
            GameObject fighter = Instantiate(tiePrehab, squad.transform);
            fighter.name = "TIE-Fighter";
            fighter.transform.position = new Vector3(g, 0, 0);
            fighter.transform.parent = squad.transform;
        }

        squad.transform.position = pos;
        squad.transform.eulerAngles = rot;

    }
}
