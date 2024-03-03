using UnityEngine;

namespace Alf.Game.NPC
{

public class NPCMoveToPlayer : INPCMoveBehaviour
{

    const float SPEED = 1f;    
    [SerializeField] float speed;

    GameObject _player;
    
    public Vector3 Move(GameObject gameObject)
    {
        if(_player == null)
            _player = GameObject.Find("player");

        if(_player != null)
            return Vector3.MoveTowards(gameObject.transform.position, _player.transform.position, SPEED * Time.deltaTime);

        return Vector3.zero;
    }
}
}
