using UnityEngine;
using System.Collections;

public class GameInfo : MonoBehaviour {

    public string mapName;
    public GameModeEnum gameMode = GameModeEnum.Time;

    public int time = 9;
    public int score = 9;
    public int lifes = 9;

    public PlayerTypeEnum player1type = PlayerTypeEnum.Player;
    public PlayerTypeEnum player2type = PlayerTypeEnum.Player;
    public PlayerTypeEnum player3type = PlayerTypeEnum.AI;
    public PlayerTypeEnum player4type = PlayerTypeEnum.None;

    public CharacterEnum player1char = CharacterEnum.DaVinci;
    public CharacterEnum player2char = CharacterEnum.Tesla;
    public CharacterEnum player3char = CharacterEnum.Nobel;
    public CharacterEnum player4char = CharacterEnum.Einstein;

    public override string ToString()
    {
        string s = "";
        s += "gameMode = " + gameMode+"\n";

        s += "time = " + time + "\n";
        s += "score = " + score + "\n";
        s += "lifes = " + lifes + "\n";

        s += "player1type = " + player1type + "\n";
        s += "player2type = " + player2type + "\n";
        s += "player3type = " + player3type + "\n";
        s += "player4type = " + player4type + "\n";

        s += "player1char = " + player1char + "\n";
        s += "player2char = " + player2char + "\n";
        s += "player3char = " + player3char + "\n";
        s += "player4char = " + player4char + "\n";
        
        return s;
    }

}
