using System.Collections;
using System.Collections.Generic;
using Alf.Utils;
using UnityEngine;

namespace Alf.PlayerScore
{

public class PlayerScoreRequest
{
    public int Score;
    public bool RequestMet;
}

public static class  PlayerScoreEvents
{
    
    public static CustomEvent<int> onPlayerScoreAdd = new CustomEvent<int>();
    public static CustomEvent<PlayerScoreRequest> onPlayerScoreRequest = new CustomEvent<PlayerScoreRequest>();

}
}
