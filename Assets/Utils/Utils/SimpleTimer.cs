using UnityEngine;
using System;
using System.Collections;

namespace Alf.Utils
{

public class TimerMB : MonoBehaviour
{

    public Action Timeout;
    private float _remaining;

	public static TimerMB Create(float seconds = 1, GameObject parent = null, bool pausable = false)
	{
		var timer = new GameObject("TimerMB", typeof(TimerMB));
		timer.GetComponent<TimerMB>()._remaining = seconds;
		if(parent != null)
			timer.transform.SetParent(parent.transform);
		return timer.GetComponent<TimerMB>();
	}

    void Update()
    {
        _remaining = _remaining - Time.deltaTime;
        if(_remaining <= 0 && gameObject != null)
        {
            Timeout?.Invoke();
            if(gameObject != null)
                Destroy(gameObject);
        }
    }

}
}
