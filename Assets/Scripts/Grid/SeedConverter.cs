using UnityEngine;
using System;
using System.Collections.Generic;

public class SeedConverter : MonoBehaviour {



    public static int[,] UncompressChunk(string _chunkData)
    {
        int yLength = _chunkData.Length / ChunkHolder.xLength;

        //make a new 2 dimensional array, this is the chunk we are later going to push into allChunks
        int[,] chunk = new int[ChunkHolder.xLength, yLength];

        int counter = 0;

        //assign every int in the 2 dimensional array
        for (int y = 0; y < yLength; y++)
        {
            for (int x = 0; x < ChunkHolder.xLength; x++)
            {
                //convert char from _chunkData to an int in chunk
                chunk[x, y] = (int)Char.GetNumericValue(_chunkData[counter]);
                counter++;
            }
        }

        return chunk;
    }

    //compresses the chunk to a string so it can be stored.
    public static string CompressChunk(int[,] _chunk)
    {
        List<string> chunkData = new List<string>();

        //add every value in the char as a string
        for (int y = 0; y < _chunk.Length / ChunkHolder.xLength; y++)
        {
            for (int x = 0; x < ChunkHolder.xLength; x++)
            {
                chunkData.Add(_chunk[x, y].ToString());
            }
        }

        //merge the whole string into one.
        return string.Join("", chunkData.ToArray());
    }
}
