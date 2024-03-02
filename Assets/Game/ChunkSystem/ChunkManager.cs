using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alf.Modules.ChunkSystem
{

public class ChunkManager2D
{
    
    public Dictionary<Vector2Int, GameObject> Chunks { get; private set; }

    GameObject _gridParent;
    float _width;
    float _height;

    public ChunkManager2D(float chunkWidth, float chunkHeight, Vector2 origin)
    {
        _width = chunkWidth;
        _height = chunkHeight;
        _gridParent = new GameObject("Chunks");
        
        Chunks = new Dictionary<Vector2Int, GameObject>
        {
            {Vector2Int.zero, Chunk2D.Spawn(_width, _height, origin, _gridParent)}
        };
    }

    public void AddChunk(Vector2Int coord)
    {
        if(Chunks.ContainsKey(coord))
            return;
        var spawnPos = new Vector2(
            _width  * coord.x,
            _height * coord.y
        );
        Chunks.Add(coord, Chunk2D.Spawn(_width, _height, spawnPos, _gridParent));
    }

    public void AddChunks(List<Vector2Int> coords)
    {
        foreach(Vector2Int coord in coords)
        {
            AddChunk(coord);
        }
    }

    public Vector2Int? WhereChunk(GameObject chunk)
    {
        foreach(KeyValuePair<Vector2Int, GameObject> entry in Chunks)
        {
            if(Chunks[entry.Key] == chunk)
                return entry.Key;
        }
        return null;
    }

    public List<GameObject> GetChunks(HashSet<Vector2Int> coords)
    {
        var ret = new List<GameObject>();
        foreach(Vector2Int coord in coords)
        {
            if(!Chunks.ContainsKey(coord))
                continue;
            ret.Add(Chunks[coord]);
        }
        return ret;
    }

}
}
