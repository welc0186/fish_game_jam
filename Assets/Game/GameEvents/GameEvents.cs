using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alf.Utils;
using System;

namespace Alf.Game.MainGame
{

public static class GameEvents
{

    // public static Action onNewGame;
    public static CustomEvent onNewGame = new CustomEvent();
    public static CustomEvent onGameOver = new CustomEvent();
    public static CustomEvent<bool> onPauseGame = new CustomEvent<bool>();

}
}
