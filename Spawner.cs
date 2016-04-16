using UnityEngine;
using System.Collections;
using TinyMessenger;
using UnityEngine.EventSystems;

public class Spawner : MonoBehaviour
{

    public GameObject objToSpawn; 
    public float spawnTime = 3f;
    public float startDelay = 0f;

    public bool continuesSpawn = true;

    void Start()
    {
        if(continuesSpawn)
            StartCoroutine(StartSpawmDelay(startDelay));

        MessageHub.Publish(new NewSpawner(this));
    }

    public void Spawn()
    {
        var spawnPosition = transform.position;
        spawnPosition.z += 1;
        var obj = Instantiate(objToSpawn, spawnPosition, Quaternion.identity) as GameObject;
        MessageHub.Publish(new NewSpawnEvent(this, obj));
        ExecuteEvents.Execute<ISpawnerMessageTarget>(transform.gameObject, null, (x, y) => x.NewSpawn(obj));
    }

    IEnumerator StartSpawmDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StartSpawn();
    }

    public void StartSpawn()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    public void StopSpawn()
    {
        Debug.Log("stop plz");
        CancelInvoke("Spawn");
    }

}

public class NewSpawner : TinyMessageBase
{
    public NewSpawner(Spawner sender) : base(sender) { }
}

public class NewSpawnEvent : TinyMessageBase
{
    public GameObject Spawn;

    public NewSpawnEvent(Spawner sender, GameObject spawn) : base(sender)
    {
        Spawn = spawn;
    }
}

public interface ISpawnerMessageTarget : IEventSystemHandler
{
    void NewSpawn(GameObject gameObject);
}

