using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alf.Utils;

namespace Alf.Modules.ChunkSystem
{

public class ChunkEventData
{
    public GameObject Chunk;
    public Collider2D Collider;
}

public static class ChunkEvents
{

    public static CustomEvent<ChunkEventData> onColliderEnterChunk = new CustomEvent<ChunkEventData>();
    public static CustomEvent<ChunkEventData> onColliderExitChunk  = new CustomEvent<ChunkEventData>();

}


}
