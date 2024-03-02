using System;
using System.Collections.Generic;
using Alf.Game.NPC;
using Alf.Modules.ChunkSystem;
using Alf.Utils;
using UnityEngine;

public class MainGameManager : Singleton<MainGameManager>
{

    const float CHUNK_W = 20;
    const float CHUNK_H = 20;
    const float STARTING_GYRO_DENSITY = 0.25f;
    const string BACKGROUND_PREFAB = "BackgroundChunk";

    ChunkManager2D _chunkManager;
    HashSet<Vector2Int> _chunksSpawnedIn;
    GameObject _npcParent;
    GameObject _bgParent;
    
    protected override void Awake()
    {
        base.Awake();
        if(_doomed) return;

        ChunkEvents.onColliderEnterChunk.Subscribe(HandleChunkEnter);
        
        InitMainGame();
    }

    void OnDestroy()
    {
        ChunkEvents.onColliderEnterChunk.Unsubscribe(HandleChunkEnter);
    }

    private void HandleChunkEnter(ChunkEventData data)
    {
        if(data.Collider.gameObject.tag != "Player")
            return;
        var chunkCoords = _chunkManager.WhereChunk(data.Chunk).Value.AdjacentCoords(true);
        SpawnGyros(chunkCoords);
    }

    private void SpawnGyros(List<Vector2Int> chunkCoords)
    {
        if(_npcParent == null)
            _npcParent = new GameObject("Gyros");
        
        _chunkManager.AddChunks(chunkCoords);
        var spawnChunkCoords = new HashSet<Vector2Int>();
        foreach(Vector2Int chunkCoord in chunkCoords)
        {
            if(!_chunksSpawnedIn.Contains(chunkCoord))
            {
                spawnChunkCoords.Add(chunkCoord);
                _chunksSpawnedIn.Add(chunkCoord);
            }
        }

        SpawnBackground(spawnChunkCoords);
        GyroSpawner.SpawnInChunks(_chunkManager.GetChunks(spawnChunkCoords), STARTING_GYRO_DENSITY)
            .SetParent(_npcParent.transform);
    }

    private void SpawnBackground(HashSet<Vector2Int> spawnChunkCoords)
    {
        if(_bgParent == null)
            _bgParent = new GameObject("Backgrounds");
        
        foreach(Vector2Int coord in spawnChunkCoords)
        {
            var spawnPos = new Vector3(
                coord.x * CHUNK_W,
                coord.y * CHUNK_H,
                0
            );
            GameObject.Instantiate(Resources.Load<GameObject>(BACKGROUND_PREFAB), spawnPos, Quaternion.identity, _bgParent.transform);
        }
    }

    private void InitMainGame()
    {
        InitChunks();
        InitNPCs();
    }

    private void InitChunks()
    {
        if(_chunkManager != null)
            return;

        
        _chunkManager = new ChunkManager2D(CHUNK_W, CHUNK_H, Vector2.zero);
        _chunkManager.AddChunk(Vector2Int.zero);
        _chunkManager.AddChunks(Vector2Int.zero.AdjacentCoords(true));
    }

    private void InitNPCs()
    {
        _chunksSpawnedIn = new HashSet<Vector2Int>();
        var spawnCoords = Vector2Int.zero.AdjacentCoords(true);
        spawnCoords.Add(Vector2Int.zero);
        SpawnGyros(spawnCoords);
    }

}
