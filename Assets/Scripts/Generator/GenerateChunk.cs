using UnityEngine;
using System.Collections;

public class GenerateChunk : MonoBehaviour {

    [SerializeField]
    private float divideGridHeightWaitTime = 2f;

    [SerializeField]
    private ChunkHolder chunkLibary;

    [SerializeField]
    private string[] objectToSpawnNames;

    [SerializeField]
    private GameObject spawnPos;

    private bool shouldSpawn = true;
    private bool alreadySpawning;

    private int[,] chunkToSpawnNext;

    private bool forceSpawn;

    public void StartSpawning() {
        if (!alreadySpawning)
        {
            MakeRandomChunk();
            alreadySpawning = true;
        }
    }

    public void MakeRandomChunk() {
        //make a random chunks
        MakeChunk(chunkLibary.GetRandomChunk());
    }

    public void MakeChosenChunk(int[,] _chunkToSpawn) {
        forceSpawn = true;
        chunkToSpawnNext = _chunkToSpawn;
    }

    private void MakeChunk(int[,] _chunkToSpawn)
    {
        //calculate the y Length of this chunk, take its total length and divide it by its width.
        int yLength = _chunkToSpawn.Length / ChunkHolder.xLength;

        for (int y = 0; y < yLength; y++)
        {
            for (int x = 0; x < ChunkHolder.xLength; x++)
            {
                if (objectToSpawnNames[_chunkToSpawn[x, y]] != "")
                {
                    //put the number of the grid into the objectToSpawnName array to check the right name for that object, then send it to object pool
                    GameObject spawnedObject = ObjectPool.instance.GetObjectForType(objectToSpawnNames[_chunkToSpawn[x, y]], true);

                    if (spawnedObject == null)
                        print(objectToSpawnNames[_chunkToSpawn[x, y]] + " does not exist in objectpool");
                    else
                        spawnedObject.transform.position = new Vector3(x, -y + yLength, 2f) + spawnPos.transform.position;
                }
            }
        }

            if (shouldSpawn)
                //start counting down again before spawning a new one
                StartCoroutine(WaitBeforeNextSpawn(yLength));
    }

    IEnumerator WaitBeforeNextSpawn(int _ylenth)
    {
        //this is the time we are waiting before we spawn another chunk
        //take the ylength
        //then divide it by divideGridHeightWaitTime to make the wait time adjustable
        //then divide it by the gamet time speed in this game + 1, so the time becomes shorter the longer the game plays on
        float timeToWait = _ylenth / divideGridHeightWaitTime / GameSpeed.SpeedMultiplier;

        yield return new WaitForSeconds(timeToWait);

        //Since the IENumerator and function call on each other objects will spawn in intervals.
        if (shouldSpawn)
        {
            //checks if we should make a random chunk, or make a chunk that needs to be force spawned
            if (forceSpawn)
            {
                MakeChunk(chunkToSpawnNext);
                forceSpawn = false;
                chunkToSpawnNext = null;
            } else
                MakeRandomChunk();
        }
    }

    public void PauzeSpawning(float _pauzeTime) {
        StartCoroutine(Pauze(_pauzeTime));  
    }

    IEnumerator Pauze(float _pauzeTime)
    {
        //we are unable to spawn chunks
        shouldSpawn = false;

        //the time it takes to resume spawning
        yield return new WaitForSeconds(_pauzeTime);

        //we are now able to spawn chunks
        shouldSpawn = true;
        //start the spawning again
        MakeRandomChunk();
    }

    public int objToSpawnNameLength()
    {
        return objectToSpawnNames.Length;
    }

    public bool ShouldSpawn {
        set { shouldSpawn = value; }
    }
}
