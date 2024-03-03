using UnityEngine;
using Random = UnityEngine.Random;
using Alf.Game.Player;
using Alf.Utils;
using System;
using Alf.PlayerScore;

namespace Alf.Game.NPC
{

public class Gyro : MonoBehaviour
{

    const string PATH = "gyroNPC";
    const int SCORE = 1;

    [SerializeField] Sprite rottenSprite;
    [SerializeField] float minRottenSeconds = 10f;
    [SerializeField] float maxRottenSeconds = 60f;
    [SerializeField] float rottenSelfDestructSeconds = 10f;
    [SerializeField] GameObject freshParticles;
    [SerializeField] GameObject rottenParticles;

    CoroutineTimer _coroutineTimer;
    SpriteRenderer _spriteRenderer;
    INPCMoveBehaviour _currentBehaviour;
    bool _rotten;

    public static GameObject Spawn(Vector3 position, Quaternion rotation)
    {
        return GameObject.Instantiate(Resources.Load<GameObject>(PATH), position, rotation);
    }

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _currentBehaviour = new NPCMoveWander2D();
        var rottenSeconds = Random.Range(minRottenSeconds, maxRottenSeconds);
        _coroutineTimer = CoroutineTimer.Init(rottenSeconds);
        _coroutineTimer.Timeout += BecomeRotten;
    }

    void OnDisable()
    {
        _coroutineTimer.Timeout -= BecomeRotten;
        _coroutineTimer.Timeout -= ExpireTimer;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var playerMovement = collider.GetComponent<PlayerMovement>();
        if(playerMovement == null)
            return;

        if(_rotten && !playerMovement.Moving)
        {
            PlayerEvents.onPlayerDamage.Invoke();
            return;
        }
        TakeDamage();
    }

    private void TakeDamage()
    {
        PlayerScoreEvents.onPlayerScoreAdd.Invoke(SCORE);
        SelfDestruct();
    }

    void Update()
    {
        transform.position = _currentBehaviour.Move(gameObject);
    }

    void BecomeRotten()
    {
        _coroutineTimer.Timeout -= BecomeRotten;
        _rotten = true;
        _spriteRenderer.sprite = rottenSprite;
        _currentBehaviour = new NPCMoveToPlayer();
        _coroutineTimer = CoroutineTimer.Init(rottenSelfDestructSeconds);
        _coroutineTimer.Timeout += ExpireTimer;
    }

    void ExpireTimer()
    {
        SelfDestruct(true);
    }

    void SelfDestruct(bool timeout = false)
    {
        if(!timeout)
            NPCEvents.onNPCDeath.Invoke();
        SpawnParticles();
        Destroy(gameObject);
    }

    void SpawnParticles()
    {
        var particleSystem = _rotten? rottenParticles : freshParticles;
        var spawned = Instantiate(particleSystem, transform.position, Quaternion.identity);
        spawned.AddComponent<ParticleSelfDestruct>();
    }

}
}
