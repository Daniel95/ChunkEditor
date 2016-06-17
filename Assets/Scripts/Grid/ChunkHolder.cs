using UnityEngine;
using System.Collections.Generic;

public class ChunkHolder : MonoBehaviour {

    public const int xLength = 13;

    private static int currentYLength = 6;

    private List<int[,]> allChunks = new List<int[,]>();
    
    //add the chunk to allChunks and creates a 2 dimenional grid from it.
    public void AddChunk(string _chunkData) {

        allChunks.Add(SeedConverter.UncompressChunk(_chunkData));
    }

    //returns a random chunk from allChunks
    public int[,] GetRandomChunk() {
        return allChunks[Random.Range(0, allChunks.Count)];
    }

    //adds a chunk to allChunks
    public void AddChunk(int[,] _chunk) {
        allChunks.Add(_chunk);
    }

    public static int CurrentYLength
    {
        set { currentYLength = value; }
        get { return currentYLength; }
    }
}
