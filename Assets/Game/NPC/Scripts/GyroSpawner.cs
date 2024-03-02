using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alf.Modules.ChunkSystem;
using Alf.Utils;

namespace Alf.Game.NPC
{

public static class GyroSpawner
{

    const float GYRO_MIN_SPACING = 2f;
    const float MAXOFFSET = 0.5f;
    
    public static List<GameObject> SpawnInChunk(GameObject chunkGO, float densityFactor)
    {
        var ret = new List<GameObject>();
        var chunk = chunkGO.GetComponent<Chunk2D>();
        if (chunk == null)
        {
            Debug.LogWarning("Could not find Chunk2D component");
            return ret;
        }

        var spawnLocations = new List<Vector2>();
        var chunkBounds = chunk.GetBounds();
        var startX = chunkBounds.min.x;
        var startY = chunkBounds.min.y;
        
        for(float x = startX; x < chunkBounds.max.x; x += GYRO_MIN_SPACING)
        {
            for(float y = startY; y < chunkBounds.max.y; y += GYRO_MIN_SPACING)
            {
                spawnLocations.Add(new Vector2(x, y));
            }
        }

        foreach(Vector2 spawnLocation in spawnLocations)
        {
            ret.Add(Gyro.Spawn(spawnLocation.RandOffset(0, MAXOFFSET), Quaternion.identity));
        }
        
        return ret;
    }

    public static List<GameObject> SpawnInChunks(List<GameObject> chunkGOs, float densityFactor)
    {
        var ret = new List<GameObject>();
        foreach(GameObject chunkGO in chunkGOs)
        {
            ret.AddRange(SpawnInChunk(chunkGO, densityFactor));
        }
        return ret;
    }

}
}
