using Alf.Game.MainGame;
using Alf.Utils;
using UnityEngine;

namespace Alf.Game.Player
{

public class GamePlayer : MonoBehaviour
{
    
    const string PREFAB = "player";

    [SerializeField] private GameObject deathParticles;
    
    public static GameObject Spawn(Vector3 position)
    {
        var ret = GameObject.Instantiate(Resources.Load<GameObject>(PREFAB), position, Quaternion.identity);
        return ret;
    }

    void OnEnable()
    {
        PlayerEvents.onPlayerDamage.Subscribe(TakeDamage);
    }
    
    void OnDisable()
    {
        PlayerEvents.onPlayerDamage.Unsubscribe(TakeDamage);
    }

    private void TakeDamage()
    {
        SpawnParticles();
        GameEvents.onGameOver.Invoke();
        Destroy(gameObject);
    }

    void SpawnParticles()
    {
        var spawned = Instantiate(deathParticles, transform.position, Quaternion.identity);
        spawned.AddComponent<ParticleSelfDestruct>();
    }

}
}
