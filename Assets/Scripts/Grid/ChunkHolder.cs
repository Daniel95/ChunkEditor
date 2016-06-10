using UnityEngine;
using System;
using System.Collections.Generic;

public class ChunkHolder : MonoBehaviour {

    [SerializeField]
    private SeedValue seedDisplay;

    public const int xLength = 13;

    private static int currentYLength = 6;

    private List<int[,]> allChunks = new List<int[,]>();
    
    //load the chunk in and creates a 2 dimenional grid from it.
    public void LoadChunk(string _chunkData) {

        allChunks.Add(UncompressChunk(_chunkData));
    }

    public int[,] UncompressChunk(string _chunkData) {
        int yLength = _chunkData.Length / xLength;

        //make a new 2 dimensional array, this is the chunk we are later going to push into allChunks
        int[,] chunk = new int[xLength, yLength];

        int counter = 0;

        //assign every int in the 2 dimensional array
        for (int y = 0; y < yLength; y++)
        {
            for (int x = 0; x < xLength; x++)
            {
                //convert char from _chunkData to an int in chunk
                chunk[x, y] = (int)Char.GetNumericValue(_chunkData[counter]);
                counter++;
            }
        }
        
        return chunk;
    }

    //compresses the chunk to a string so it can be stored.
    public void CompressChunk(int[,] _chunk)
    {
        List<string> chunkData = new List<string>();

        //add every value in the char as a string
        for (int y = 0; y < _chunk.Length / xLength; y++)
        {
            for (int x = 0; x < xLength; x++)
            {
                chunkData.Add(_chunk[x, y].ToString());
            }
        }

        //merge the whole string into one.
        string mergedString  = string.Join("", chunkData.ToArray());

        seedDisplay.ChangeValue(mergedString);
    }

    //returns a random chunk from allChunks
    public int[,] GetRandomChunk() {
        return allChunks[UnityEngine.Random.Range(0, allChunks.Count)];
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
