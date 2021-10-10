using System.Linq;
using CodeDk;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject toSpawn;
    public Transform parentOfSpawn;
    public IntVariable cubeCount;

    public void Spawn(object sender, SpawnEventArgs args)
    {
        for (int i = 0; i < args.SpawnCount; i++)
        {
            int randomX = Random.Range(-30, 31) / 2;

            Vector3 randomPos = new Vector3(randomX, 0, 0);

            int nextFilteredKey = parentOfSpawn.GetComponentsInChildren<MoveResponse>().Select(r => r.moverId).Max() + 1;

            GameObject newSpawn = Instantiate(toSpawn, randomPos, Quaternion.identity, parentOfSpawn);
            newSpawn.name = "Listener_" + nextFilteredKey;

            MoveResponse responder = newSpawn.GetComponentInChildren<MoveResponse>();
            responder.moverId = nextFilteredKey;
        }

        cubeCount.Value += args.SpawnCount;
    }
}
