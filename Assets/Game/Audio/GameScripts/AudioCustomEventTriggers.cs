using Alf.Game.MainGame;
using Alf.Utils;
using Alf.UI;

public class NewGameCustomEvent : ICustomEvent
{
    public CustomEvent Event => GameEvents.onNewGame;
}

public class GameOverCustomEvent : ICustomEvent
{
    public CustomEvent Event => GameEvents.onGameOver;
}

public class ButtonClickEvent : ICustomEvent
{
    public CustomEvent Event => UICustomEvents.onButtonClick;
}