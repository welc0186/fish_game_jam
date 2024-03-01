using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alf.Utils.StateMachines;
using Alf.Utils;
using Alf.Utils.SceneUtils;
using Alf.Game.GameMenus;
using Alf.Game.MainGame;
using System;

public class GameSceneManager : PersistentSingleton<GameSceneManager>
{

    const float INIT_SECONDS = 0.2f;
    const string MAIN_MENU = "MainMenu";
    const string MAIN_GAME = "MainGame";
    
    StateMachine _sceneStateMachine = new StateMachine();

    IState _initSceneState;
    IState _mainMenuSceneState;
    IState _mainGameSceneState;

    void Start()
    {
        if(_doomed) return;
        InitStates();
        InitLinks();
        _sceneStateMachine.Run(_initSceneState);
    }

    Action LoadSceneAction(string sceneName)
    {
        return () => {SceneLoader.Instance.LoadSceneByPath(sceneName);};
    }

    private void InitStates()
    {
        _initSceneState = new DelayState(INIT_SECONDS);
        _mainMenuSceneState = new State(LoadSceneAction(MAIN_MENU), "Main Menu Scene");
        _mainGameSceneState = new State(LoadSceneAction(MAIN_GAME), "Main Game Scene");
    }

    private void InitLinks()
    {
        var newGameActionWrapper = new ActionWrapper
        {
            Subscribe = handler => GameEvents.onNewGame.Subscribe(handler),
            Unsubscribe = handler => GameEvents.onNewGame.Unsubscribe(handler)
        };

        var mainMenuActionWrapper = new ActionWrapper
        {
            Subscribe = handler => GameMenuEvents.onMainMenuEvent += handler,
            Unsubscribe = handler => GameMenuEvents.onMainMenuEvent -= handler,
        };
        
        // Go to main menu from init scene
        _initSceneState.AddLink(new Link(_mainMenuSceneState));

        // Go to new game from menu
        _mainMenuSceneState.AddLink(new EventLink(newGameActionWrapper,  _mainGameSceneState));

        // Go to new game from main game
        _mainGameSceneState.AddLink(new EventLink(newGameActionWrapper,  _mainGameSceneState));

        // Go to main menu from main game
        _mainGameSceneState.AddLink(new EventLink(mainMenuActionWrapper, _mainMenuSceneState));
    }
}
