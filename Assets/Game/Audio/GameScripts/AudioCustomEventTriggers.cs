using Alf.Game.MainGame;
using Alf.Utils;
using Alf.UI;
using Alf.Game.NPC;
using Alf.Game.Player;

public class NewGameCustomEvent     : ICustomEvent {public CustomEvent Event => GameEvents.onNewGame;}
public class GameOverCustomEvent    : ICustomEvent {public CustomEvent Event => GameEvents.onGameOver;}
public class ButtonClickEvent       : ICustomEvent {public CustomEvent Event => UICustomEvents.onButtonClick;}
public class NPCDeathEvent          : ICustomEvent {public CustomEvent Event => NPCEvents.onNPCDeath;}
public class PlayerChargeBeginEvent : ICustomEvent {public CustomEvent Event => PlayerEvents.onPlayerChargeBegin;}
public class PlayerChargeEndEvent   : ICustomEvent {public CustomEvent Event => PlayerEvents.onPlayerChargeEnd;}