using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alf.Game.Player
{
public class PlayerInput : MonoBehaviour
{
    
    const float REFRACTORY_SECONDS = 0.25f;
    const float BEGIN_CHARGE_SECONDS = 0.1f;

    float _chargeTimer;
    
    // Update is called once per frame
    void Update()
    {   
        if(!Input.GetKey("space"))
        {
            if(_chargeTimer > BEGIN_CHARGE_SECONDS)
            {
                PlayerEvents.onPlayerChargeEnd.Invoke();
            }
            _chargeTimer = 0;
            return;
        }
        
        _chargeTimer += Time.deltaTime;
        if(_chargeTimer >= BEGIN_CHARGE_SECONDS)
            PlayerEvents.onPlayerChargeBegin.Invoke();
        
    }
}
}
