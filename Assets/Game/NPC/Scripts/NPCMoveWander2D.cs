using UnityEngine;
using Alf.Utils;

namespace Alf.Game.NPC
{

public class NPCMoveWander2D : INPCMoveBehaviour
{

    const float MOVE_DISTANCE = 0.5f;
    const float MOVE_TIME = 2f;

    [SerializeField] float moveDistance;
    [SerializeField] float moveTime;

    Vector2? _target = null;
    GameObject _gameObject;

    // Should be called during Update method
    public Vector3 Move(GameObject gameObject)
    {
        if(_target == null)
        {
            CalculateTarget(gameObject);
            CoroutineTimer.Init(MOVE_TIME).Timeout += () => _target = null;
        }

        var speed = MOVE_DISTANCE / MOVE_TIME;
        return Vector3.MoveTowards(gameObject.transform.position, _target.Value, speed * Time.deltaTime);
    }

    void CalculateTarget(GameObject gameObject)
    {
        var randomX = Random.Range(-1f,1f);
        var randomY = Random.Range(-1f,1f);
        var normalizedDir = new Vector2(randomX, randomY);
        normalizedDir.Normalize();
        _target = gameObject.transform.position.ToVector2() + normalizedDir * MOVE_DISTANCE;
    }
}
}
