using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alf.Modules.ChunkSystem
{

public class Chunk2D : MonoBehaviour
{

    public static GameObject Spawn(float width, float height, Vector2 position, GameObject parent = null)
    {
        var ret = new GameObject("Chunk2D", typeof(Chunk2D), typeof(BoxCollider2D));
        if(parent != null)
            ret.transform.SetParent(parent.transform);
        ret.transform.position = position;
        var collider = ret.GetComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = new Vector2(width, height);
        return ret;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        ChunkEvents.onColliderEnterChunk.Invoke(new ChunkEventData(){
            Chunk = gameObject,
            Collider = collider
        });
    }
    
    void OnTriggerExit2D(Collider2D collider)
    {
        ChunkEvents.onColliderExitChunk.Invoke(new ChunkEventData(){
            Chunk = gameObject,
            Collider = collider
        });
    }
    
    public Bounds GetBounds()
    {
        var colliderArea = GetComponent<BoxCollider2D>().size;
        return new Bounds(gameObject.transform.position, colliderArea);
    }

}
}