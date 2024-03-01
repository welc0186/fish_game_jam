using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Alf.UI;
using System.Linq;
using Alf.Utils;
using Alf.Utils.SceneUtils;
using System;
using Alf.Game.MainGame;
using Alf.Game.GameMenus;

namespace Alf.Game.Management
{

public class GameMenuManager : Singleton<GameMenuManager>
{
	
	const string PANEL_PREFAB = "MainMenuPanel";
	const string BUTTON_PREFAB = "TMPButton";
	const string TITLE_PREFAB = "MenuTitleLabel";
	const string CANVAS_NAME = "Canvas";
	const string MENU_SCENE = "MainMenu";
	const string MAIN_SCENE = "MainGame";

	bool _paused;
	GameObject _menuParent;
	GameObject _panelPrefab;
	GameObject _buttonPrefab;
	GameObject _titlePrefab;
	GameObject _menuPanel;

	GameMenu _titleMenu;
	GameMenu _settingsMenu;
	GameMenu _pauseMenu;
	GameMenu _gameOverMenu;

	Action _closeMenuPanelDelegate;
	Action _gameOverMenuDelegate;
	Action<bool> _pauseMenuDelegate;

    protected override void Awake()
	{

		base.Awake();
		if(_doomed) return;
		
		_panelPrefab  = Resources.Load<GameObject>(PANEL_PREFAB);
		_buttonPrefab = Resources.Load<GameObject>(BUTTON_PREFAB);
		_titlePrefab  = Resources.Load<GameObject>(TITLE_PREFAB);

		// CreateMenuPanel();

        _titleMenu = new GameMenu(new IMenuItemFactory[] {
        	new PrefabTMPLabelFactory(_titlePrefab, "GYRO HERO"),
			new PrefabTMPButtonFactory(_buttonPrefab, () => GameEvents.onNewGame.Invoke(), "Play"),
			// new PrefabTMPButtonFactory(_buttonPrefab, () => CreateMenu(_settingsMenu),     "Settings"),
			new PrefabTMPButtonFactory(_buttonPrefab, () => Debug.Log("Settings button clicked"),     "Settings"),
			new PrefabTMPButtonFactory(_buttonPrefab, () => Application.Quit(),            "Quit")
		});

		// TO-DO: Add Settings
		// ** Settings Menu **
		_settingsMenu = new GameMenu(new IMenuItemFactory[] {
			new PrefabTMPLabelFactory(_titlePrefab, "Settings"),
			// new SettingsCheckboxFactory("Sound", new GameSetting<bool>(SettingCategory.SFXON, true)),
			// new SettingsSliderFactory(new GameSetting<int>(SettingCategory.SFXVOLUMEDB, 8)),
			// new PrefabTMPLabelFactory(titlePrefab, "Music"),
			// new SettingsSliderFactory(new GameSetting<int>(SettingCategory.MUSICVOLUMEDB, 8)),
			new PrefabTMPButtonFactory(_buttonPrefab, () => CreateMenu(_titleMenu), "Back"),
		});

		// ** Pause Menu **
		_pauseMenu = new GameMenu(new IMenuItemFactory[] {
			new PrefabTMPLabelFactory(_titlePrefab, "Game Paused"),
			new PrefabTMPButtonFactory(_buttonPrefab, () => GameEvents.onNewGame?.Invoke(),           "New Game"),
			new PrefabTMPButtonFactory(_buttonPrefab, () => CloseMenuPanel(),                         "Resume Game"),
			new PrefabTMPButtonFactory(_buttonPrefab, () => GameMenuEvents.onMainMenuEvent?.Invoke(), "Main Menu"),
			new PrefabTMPButtonFactory(_buttonPrefab, () => Application.Quit(),                       "Quit")
		});

		// ** Game Over Menu **
		_gameOverMenu = new GameMenu(new IMenuItemFactory[] {
			new PrefabTMPLabelFactory(_titlePrefab, "Game Over"),
			new PrefabTMPButtonFactory(_buttonPrefab, () => GameEvents.onNewGame?.Invoke(),           "New Game"),
			new PrefabTMPButtonFactory(_buttonPrefab, () => GameMenuEvents.onMainMenuEvent?.Invoke(), "Main Menu"),
			new PrefabTMPButtonFactory(_buttonPrefab, () => Application.Quit(),                       "Quit")
		});

		_closeMenuPanelDelegate = delegate(){CloseMenuPanel();};
		_gameOverMenuDelegate = delegate(){CreateMenu(_gameOverMenu);};
		_pauseMenuDelegate = delegate(bool b){CreateMenu(_pauseMenu, b);};

		GameEvents.onNewGame.Subscribe(_closeMenuPanelDelegate);
		GameEvents.onGameOver.Subscribe(_gameOverMenuDelegate);
        GameEvents.onPauseGame.Subscribe(_pauseMenuDelegate);

	}

	void Start()
	{
		if(SceneUtil.SceneLoaded(MENU_SCENE))
			CreateMenu(_titleMenu);
	}

	void OnDisable()
	{
		GameEvents.onNewGame.Unsubscribe(_closeMenuPanelDelegate);
		GameEvents.onGameOver.Unsubscribe(_gameOverMenuDelegate);
        GameEvents.onPauseGame.Unsubscribe(_pauseMenuDelegate);
	}

	private void CloseMenuPanel(bool unpause = true)
	{
		if(unpause)
			Unpause();
		
		if(_menuPanel == null)
			return;
		_menuPanel.GetComponent<GameMenuPanel>().Close();
	}

	private void CreateMenuPanel()
	{
		if(_menuPanel != null)
			return;
		
		var canvasObject = GameObject.Find(CANVAS_NAME);
		if(canvasObject == null)
		{
			Debug.LogWarning("Could not find canvas with name: " + CANVAS_NAME);
			return;
		}
		_menuPanel = GameMenuPanel.Spawn(_panelPrefab, canvasObject.transform);
		_menuPanel.transform.localPosition = Vector3.zero;
	}

    private void Unpause()
    {
        _paused = false;
		Time.timeScale = 1f;
    }

	private void Pause()
	{
		_paused = true;
		Time.timeScale = 0;
	}

    private void Pause(bool b)
    {
        if(b)
        {
            Pause();
            return;
        }
        Unpause();
    }

    private void CreateMenu(GameMenu gameMenu, bool pause = true)
    {
		if(pause) Pause();
        
		if(_menuPanel == null)
			CreateMenuPanel();

        _menuPanel.GetComponent<GameMenuPanel>().LoadMenu(gameMenu);
		// bool firstFocus = true;
		// foreach(IMenuItemFactory factory in menuItems)
		// {
		// 	var menuItem = (GameObject) factory.MakeMenuItem();
		// 	menuItem.transform.SetParent(_menuParent.transform);
		// 	if(firstFocus && menuItem.GetComponentInChildren<Selectable>() != null) 
		// 	{
		// 		firstFocus = false;
		// 		menuItem.GetComponentInChildren<Selectable>().Select();
		// 	}
		// }
    }

    void Update()
	{	
		if (Input.GetButtonUp("Cancel") && SceneManager.GetActiveScene().name == "MainGame")
		{
			if(!_paused)
			{
				GameEvents.onPauseGame.Invoke(true);
				return;
			}
			// Unpause();
		}
	}

}
}
