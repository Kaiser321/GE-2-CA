using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Scene1 : State
{
    /*
     * In Scene 1 Falcon will fly towards the camera
     */
    Camera camera;
    GameObject falcon;
    GameObject sceneTarget;
    public override void Enter()
    {
        camera = owner.GetComponent<SceneController>().camera;
        falcon = owner.GetComponent<SceneController>().falcon;
        falcon.transform.position = new Vector3(0, 0, 0);
        camera.transform.position = new Vector3(0, 0, 90);
        camera.transform.eulerAngles = new Vector3(-5, 180, 0);
        sceneTarget = new GameObject("Target");
        sceneTarget.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + 2, camera.transform.position.z + 10);
        falcon.GetComponent<Seek>().target = sceneTarget.transform.position;
        falcon.GetComponent<Seek>().enabled = true;

    }

    public override void Think()
    {
        if (Vector3.Distance(sceneTarget.transform.position, falcon.transform.position) <= 5)
        {
            owner.ChangeState(new Scene2());
        }

    }

    public override void Exit()
    {
        owner.GetComponent<SceneController>().DestoryObject(sceneTarget);
        falcon.GetComponent<Seek>().enabled = false;
    }
}

class Scene2 : State
{
    Camera camera;
    GameObject falcon;
    GameObject sceneTarget;
    public override void Enter()
    {
        camera = owner.GetComponent<SceneController>().camera;
        falcon = owner.GetComponent<SceneController>().falcon;

        camera.transform.position = new Vector3(0, falcon.transform.position.y + 10, falcon.transform.position.z - 10);
        camera.transform.eulerAngles = new Vector3(20, 0, 0);
        sceneTarget = new GameObject("Target");
        sceneTarget.transform.position = new Vector3(falcon.transform.position.x, falcon.transform.position.y, falcon.transform.position.z + 75);
        falcon.GetComponent<Seek>().target = sceneTarget.transform.position;
        falcon.GetComponent<Seek>().enabled = true;

    }

    public override void Think()
    {
        if (Vector3.Distance(sceneTarget.transform.position, falcon.transform.position) <= 5)
        {
            owner.ChangeState(new Scene3());
        }

    }

    public override void Exit()
    {
        owner.GetComponent<SceneController>().DestoryObject(sceneTarget);
        falcon.GetComponent<Boid>().maxSpeed = 10;
        falcon.GetComponent<Seek>().enabled = false;
    }
}


class Scene3 : State
{
    Camera camera;
    GameObject falcon;
    Spawner spawner;
    GameObject tieLeader;
    public override void Enter()
    {
        camera = owner.GetComponent<SceneController>().camera;
        falcon = owner.GetComponent<SceneController>().falcon;
        spawner = owner.GetComponent<Spawner>();

        GameObject s = spawner.SpawnTIEFighterSquad(falcon.transform.position + new Vector3(-100, 0, 0), falcon.transform);
        tieLeader = s.transform.Find("TIE-Fighter Leader").gameObject;

        camera.transform.position = s.transform.position + s.transform.forward * 10;
        camera.GetComponent<FollowCamera>().target = tieLeader;
        camera.GetComponent<FollowCamera>().enabled = true;


    }

    public override void Think()
    {
        if (Vector3.Distance(tieLeader.transform.position, falcon.transform.position) <= 40)
        {
            owner.ChangeState(new Scene4());
        }

    }

    public override void Exit()
    {
        falcon.GetComponent<ShipController>().target = tieLeader;
        camera.GetComponent<FollowCamera>().enabled = false;
    }
}

class Scene4 : State
{
    Camera camera;
    GameObject falcon;
    Spawner spawner;
    GameObject tieLeader;

    public override void Enter()
    {
        camera = owner.GetComponent<SceneController>().camera;
        falcon = owner.GetComponent<SceneController>().falcon;
        falcon.GetComponent<StateMachine>().ChangeState(new Fleeing());

        camera.transform.position = falcon.transform.position + falcon.transform.right * 15;
        camera.GetComponent<FollowCamera>().target = falcon;
        camera.GetComponent<FollowCamera>().enabled = true;
    }

    public override void Think()
    {
        if (Vector3.Distance(camera.transform.position, falcon.transform.position) >= 70)
        {
            owner.ChangeState(new Scene5());
        }
    }

    public override void Exit()
    {
        camera.GetComponent<FollowCamera>().enabled = false;
    }
}

class Scene5 : State
{
    Camera camera;
    GameObject falcon;
    Spawner spawner;
    GameObject tieLeader;

    public override void Enter()
    {
        camera = owner.GetComponent<SceneController>().camera;
        falcon = owner.GetComponent<SceneController>().falcon;
        tieLeader = falcon.GetComponent<ShipController>().target;

        camera.transform.position = tieLeader.transform.position + tieLeader.transform.forward * 10;
        camera.transform.LookAt(tieLeader.transform);
        camera.transform.eulerAngles = new Vector3(-10, camera.transform.eulerAngles.y, camera.transform.eulerAngles.z);
    }

    public override void Think()
    {
        if (Vector3.Distance(camera.transform.position, tieLeader.transform.position) >= 15)
        {
            owner.ChangeState(new Scene6());
        }
    }

    public override void Exit()
    {
        falcon.GetComponent<Boid>().maxSpeed = 10;
        //camera.GetComponent<FollowCamera>().enabled = false;
    }
}

class Scene6 : State
{
    Camera camera;
    GameObject falcon;
    Spawner spawner;
    GameObject tieLeader;

    public override void Enter()
    {

        camera = owner.GetComponent<SceneController>().camera;
        falcon = owner.GetComponent<SceneController>().falcon;
        tieLeader = falcon.GetComponent<ShipController>().target;

        falcon.GetComponent<ShipController>().AssignRandomCameraPosition(camera);
        owner.GetComponent<StateMachine>().updatesPerSecond = 0.2f;
        falcon.GetComponent<NoiseWander>().enabled = true;

    }

    public override void Think()
    {
        if (falcon.GetComponent<ShipController>().health <= 80)
        {
            owner.ChangeState(new Scene7());
        }

        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            falcon.GetComponent<ShipController>().AssignRandomCameraPosition(camera);
        }
        else
        {
            tieLeader.GetComponent<ShipController>().AssignRandomCameraPosition(camera);
        }


        rand = Random.Range(0, 2);
        if (rand == 0)
        {
            falcon.GetComponent<Boid>().maxSpeed = 10;
        }
        else
        {
            falcon.GetComponent<Boid>().maxSpeed = 12;
        }
    }

    public override void Exit()
    {
        owner.GetComponent<StateMachine>().updatesPerSecond = 5f;
        falcon.GetComponent<NoiseWander>().enabled = false;
        falcon.GetComponent<Flee>().enabled = false;
    }
}

class Scene7 : State
{
    Camera camera;
    GameObject falcon;
    Spawner spawner;
    GameObject tieLeader;
    GameObject sceneTarget;
    GameObject startTarget;
    GameObject cluster;

    public override void Enter()
    {

        camera = owner.GetComponent<SceneController>().camera;
        falcon = owner.GetComponent<SceneController>().falcon;
        tieLeader = falcon.GetComponent<ShipController>().target;
        spawner = owner.GetComponent<Spawner>();

        cluster = spawner.SpawnAsteroidCluster(falcon.transform.position + falcon.transform.forward * 100, new Vector3(100, 50, 500), 150);

        sceneTarget = new GameObject("Target");
        sceneTarget.transform.position = cluster.transform.position + cluster.transform.forward * 550;
        startTarget = new GameObject("Start Target");
        startTarget.transform.position = cluster.transform.position + cluster.transform.forward * (-20);

        falcon.GetComponent<ShipController>().target = sceneTarget;
        falcon.GetComponent<PathFinder>().start = startTarget.transform;
        falcon.GetComponent<StateMachine>().ChangeState(new TraverseAsteroidCluster());

        tieLeader.GetComponent<ShipController>().target = sceneTarget;
        tieLeader.GetComponent<PathFinder>().start = startTarget.transform;
        tieLeader.GetComponent<StateMachine>().ChangeState(new TraverseAsteroidCluster());
    }

    public override void Think()
    {
        if (falcon.GetComponent<ShipController>().health <= 0)
        {
            owner.ChangeState(new Scene7());
        }

        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            falcon.GetComponent<ShipController>().AssignRandomCameraPosition(camera);
        }
        else
        {
            tieLeader.GetComponent<ShipController>().AssignRandomCameraPosition(camera);
        }


        rand = Random.Range(0, 2);
        if (rand == 0)
        {
            falcon.GetComponent<Boid>().maxSpeed = 10;
        }
        else
        {
            falcon.GetComponent<Boid>().maxSpeed = 12;
        }
    }

    public override void Exit()
    {
        falcon.GetComponent<NoiseWander>().enabled = false;
    }
}