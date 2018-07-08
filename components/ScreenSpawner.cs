using System.Collections;
using System.Collections.Generic;
using SplatterSystem.TopDown;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ScreenSpawner : MonoBehaviour
{

    public Settings settings;
    public GameObject spawner;

    private int maxEnemy;

	void Start () {
        InvokeRepeating("SpawnAll", 0, 2);
	}

    // void HandleGameCompleted()
    // {
    //     Spawn();
    // }

    public void SpawnAll()
    // {
    //     var count = FindObjectsOfType<Target>().Length;
    //     for (int i = count; i <= settings.maxEnemy; i++)
    //     {
    //         Spawn();
    //     }
    }

    public void Spawn()
    {
        var spawned = Instantiate(spawner);
        var info = RandomPositionAndDirection(false);
        Vector3 randomPosition = info[0];

        // var vertical = Random.Range(0f, 1f) >= 0.5f;
        // if (vertical)
        // {
        //     spawned.GetComponent<SpriteRenderer>().color = Color.magenta;
        //     spawned.GetComponent<Target>().hitSplatterColor = Color.magenta;
        // }
        // var killer = Random.Range(0f, 1f) >= 0.8f;
        // if (killer)
        // {
        //     spawned.GetComponent<Target>().setKiller();
        // }
       
       
        //        spawned.transform.localScale = Vector3.one;
        spawned.transform.position = RandomPosition(); //Camera.main.ViewportToWorldPoint(randomPosition);
        spawned.GetComponent<Target>().movimentDirection = RandomPosition();
            //spawned.transform.position * -1; // Camera.main.ViewportToWorldPoint(info[1]);
    }
   

    private Vector3 RandomPosition(bool viewportToWorldPoint = true, float zIndex = 1)
    {
        var vertical = Random.Range(0f, 1f) >= 0.5f;
        var range = Random.Range(0f, 1f);
        var side = Random.Range(0f, 1f) >= 0.5f ? 1 : 0;
        var offset = side > 0 ? 0.05f : -0.05f;
        var sideWithOffset = side + offset;
        Vector3 randomPosition =
            vertical ? new Vector3(sideWithOffset, range, zIndex) : new Vector3(range, sideWithOffset, zIndex);
        return viewportToWorldPoint ? Camera.main.ViewportToWorldPoint(randomPosition) : randomPosition;
    }

    private Vector3[] RandomPositionAndDirection(bool viewportToWorldPoint = true, float zIndex = 1)
    {
        var vertical = Random.Range(0f, 1f) >= 0.5f;
        var range = Random.Range(0f, 1f);
        var side = Random.Range(0f, 1f) >= 0.5f ? 1 : 0;
        var offset = side > 0 ? 0.05f : -0.05f;
        var sideWithOffset = side + offset;
        Vector3 randomPosition =
            vertical ? new Vector3(sideWithOffset, range, zIndex) : new Vector3(range, sideWithOffset, zIndex);

        Vector3 direction =
            vertical ? new Vector3(Random.Range(0f, 1f) <= 0.5f ? 1 : 0, range, zIndex) : new Vector3(range, Random.Range(0f, 1f) <= 0.5f ? 1 : 0, zIndex);

        var vec = new Vector3[2];
        vec[0] =  viewportToWorldPoint ? Camera.main.ViewportToWorldPoint(randomPosition) : randomPosition;
        vec[1] = direction;
        return vec;
    }

    private void debug()
    {
            // maxEnemy = settings.maxEnemy;
//        var spawned = Instantiate(spawner);
//        Vector3 v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
//        spawned.transform.localScale = Vector3.one;
//        spawned.transform.position = v3Pos;
//
//        var spawned2 = Instantiate(spawner);
//        Vector3 v3Pos2 = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
//        spawned2.transform.localScale = Vector3.one;
//        spawned2.transform.position = v3Pos2;
//
//        var spawned3 = Instantiate(spawner);
//        Vector3 v3Pos3 = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
//        spawned3.transform.localScale = Vector3.one;
//        spawned3.transform.position = v3Pos3;
//
//        var spawned4 = Instantiate(spawner);
//        Vector3 v3Pos4 = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
//        spawned4.transform.localScale = Vector3.one;
//        spawned4.transform.position = v3Pos4;
//
    }
}

