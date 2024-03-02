using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alf.Utils;

namespace Alf.Game.NPC
{

public interface INPCMoveBehaviour
{
    Vector3 Move(GameObject gameObject);
}
}
