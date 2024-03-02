using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

namespace Alf.PlayerScore
{

public class PlayerScoreDisplay : MonoBehaviour
{
    
    int _score;
    TMP_Text _text;
    
    void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _score = 0;
        UpdateText();
    }

    void OnEnable()
    {
        PlayerScoreEvents.onPlayerScoreAdd.Subscribe(AddScore);
        PlayerScoreEvents.onPlayerScoreRequest.Subscribe(HandleScoreRequest);
    }

    void OnDisable()
    {
        PlayerScoreEvents.onPlayerScoreAdd.Unsubscribe(AddScore);
        PlayerScoreEvents.onPlayerScoreRequest.Unsubscribe(HandleScoreRequest);
    }

    private void AddScore(int score)
    {
        _score += score;
        UpdateText();
    }

    private void UpdateText()
    {
        _text.text = _score.ToString();
    }
    
    private void HandleScoreRequest(PlayerScoreRequest request)
    {
         request.Score = _score;
         request.RequestMet = true;  
    }
}
}
