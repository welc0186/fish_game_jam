using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alf.Utils;

namespace Alf.Game.Player
{

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    const float MOVE_THRESH_DIST = 0.2f;
    const float MOVE_THRESH_SEC = 0.1f;
    public bool Moving { get; private set; }
    
    [SerializeField] float beginRotateVelocity;
    [SerializeField] float endRotateVelocity;
    [SerializeField] float minLinearVelocity;
    [SerializeField] float maxLinearVelocity;
    [SerializeField] float secondsToChargeMax;
    
    bool _charging;
    bool _sampling;
    Vector3 _lastPosition;
    float _chargeTimer;
    Rigidbody2D _rigidBody2D;

    void OnEnable()
    {
        PlayerEvents.onPlayerChargeBegin.Subscribe(() => _charging = true);
        PlayerEvents.onPlayerChargeEnd.Subscribe(ReleaseCharge);
        _rigidBody2D = GetComponent<Rigidbody2D>();
        Moving = false;
        _sampling = false;
        _lastPosition = transform.position;
    }

    void OnDisable()
    {
        PlayerEvents.onPlayerChargeEnd.Unsubscribe(ReleaseCharge);
    }
     
    void Update()
    {
        if(_charging)
            ChargeUp();

        if(!_sampling)
            StartCoroutine(SamplePosition());
    }

    private void ChargeUp()
    {
        _chargeTimer += Time.deltaTime;
        var rotateVelocity = MathUtils.LMap(_chargeTimer, 0, secondsToChargeMax, beginRotateVelocity, endRotateVelocity);
        _rigidBody2D.angularVelocity = rotateVelocity;
    }

    private void ReleaseCharge()
    {
        var velocity = MathUtils.LMap(_chargeTimer, 0, secondsToChargeMax, minLinearVelocity, maxLinearVelocity);
        _rigidBody2D.AddForce(transform.up * velocity, ForceMode2D.Impulse);
        Moving = true;
        _rigidBody2D.angularVelocity = 0;
        _chargeTimer = 0;
        _charging = false;
    }

    IEnumerator SamplePosition()
    {
        _sampling = true;
        yield return new WaitForSeconds(MOVE_THRESH_SEC);
        if(Vector3.Magnitude(transform.position - _lastPosition) < MOVE_THRESH_DIST)
            Moving = false;
        _sampling = false;
        _lastPosition = transform.position;
    }
}
}
