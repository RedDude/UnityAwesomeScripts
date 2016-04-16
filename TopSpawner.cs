using System;
using UnityEngine;
using System.Collections;
using PrefabsConstants;
using Random = System.Random;


public class TopSpawner : MonoBehaviour, ISpawnerMessageTarget
{

    Spawner spawner;
    public GameObject testSpawn;

    public PathfinderMap PathfinderMap;
    private PathfinderTile Ledge;

    void Start () {
        spawner = GetComponent<Spawner>();
    }
	
	void Update () {
	
	}

    //JOGAR NO PATHFINDER
    private void SelectLedge(int index)
    {
        Ledge = PathfinderMap.getLedges()[index];
        transform.position = new Vector3(Ledge.transform.position.x, transform.position.y, 0.0f);
    }

    private void SelectRandomLedge() {
        SelectLedge(PathfinderMap.SelectRandomLedge());
    }

    public void SpawnElementOnLedge(string objToSpawn, int? ledgeIndex = null)
    {
        var go = (GameObject) Resources.Load(objToSpawn, typeof (GameObject));
        SpawnElementOnLedge(go, ledgeIndex);
    }

    public void SpawnElementOnLedge(GameObject objToSpawn, int? ledgeIndex = null)
    {
        if (ledgeIndex == null)
            SelectRandomLedge();
        else
            SelectLedge((int)ledgeIndex);

        spawner.objToSpawn = objToSpawn;
        spawner.Spawn();
    }

    public void NewSpawn(GameObject o)
    {
        var jw = o.GetComponent<JumpTween>();
        if (jw)
            jw.JumpTo(Ledge.transform.position);
    }

    void OnGUI()
    {

        return;
        if (GUI.Button(new Rect(0, 0, 100, 30), "Random Ledge"))
        {
            SelectRandomLedge();
        }

        if (GUI.Button(new Rect(100, 0, 100, 30), "Random Item"))
        {
            //NewSpawn((GameObject)Resources.Load("Prefabs/Item", typeof(GameObject)));
            SpawnElementOnLedge((GameObject)Resources.Load(Prefabs.Mice, typeof(GameObject)));
        }
        //if (GUI.Button(new Rect(310, 30, 200, 30), "Spawn Enemy")) Application.LoadLevel(Application.loadedLevelName);
    }
}
