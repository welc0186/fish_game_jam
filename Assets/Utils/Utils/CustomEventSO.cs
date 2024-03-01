using UnityEngine;
using System;

namespace Alf.Utils
{

public abstract class CustomEventSO : ScriptableObject
{
    public virtual CustomEvent Event { get; set; }
}
}
