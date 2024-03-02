using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alf.Utils;

namespace Alf.Game.Player
{
public static class PlayerEvents
{
    
    public static CustomEvent onPlayerChargeBegin = new CustomEvent();
    public static CustomEvent onPlayerChargeEnd   = new CustomEvent();
    public static CustomEvent onPlayerMoveBegin   = new CustomEvent();
    public static CustomEvent onPlayerMoveEnd     = new CustomEvent();
    public static CustomEvent onPlayerDamage      = new CustomEvent();
    public static CustomEvent onPlayerDeath       = new CustomEvent();

}
}
