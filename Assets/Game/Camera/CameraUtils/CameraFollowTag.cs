using System;
using UnityEngine;

namespace Alf.Utils
{

public class CameraFollowTag : MonoBehaviour
{

    [SerializeField] string tagName = "Player";
    [SerializeField] float smoothTime = 0.125f;
    
    Transform _tagTransform;
    Vector3 _smoothVelocity = Vector3.zero;
    Vector3 _offset;

    void FixedUpdate()
    {
        if(_tagTransform == null)
            FindTagTransform();

        if(_tagTransform == null)
            return;
        
        var targetPosition = _tagTransform.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _smoothVelocity, smoothTime);
    }

    private void FindTagTransform()
    {
        _tagTransform = GameObject.FindGameObjectWithTag(tagName)?.transform;
        if(_tagTransform != null)
            _offset = transform.position - _tagTransform.position;
    }
    }
}
