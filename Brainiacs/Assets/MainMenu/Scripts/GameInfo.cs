using UnityEngine;
using System.Collections;

public class GameInfo : MonoBehaviour {

    public string mapName;
    public GameModeEnum gameMode = GameModeEnum.Score;

    public int time = 9;
    public int winScore = 1;
    public int lifes = 1;

    public PlayerTypeEnum player1type = PlayerTypeEnum.Player;
    public PlayerTypeEnum player2type = PlayerTypeEnum.Player;
    public PlayerTypeEnum player3type = PlayerTypeEnum.AI;
    public PlayerTypeEnum player4type = PlayerTypeEnum.None;

    public CharacterEnum player1char = CharacterEnum.DaVinci;
    public CharacterEnum player2char = CharacterEnum.Tesla;
    public CharacterEnum player3char = CharacterEnum.Nobel;
    public CharacterEnum player4char = CharacterEnum.Curie;

    public int player1score = 0;
    public int player2score = 0;
    public int player3score = 0;
    public int player4score = 0;

    public int player1lifes = 0;
    public int player2lifes = 0;
    public int player3lifes = 0;
    public int player4lifes = 0;


    public override string ToString()
    {
        string s = "";
        s += "gameMode = " + gameMode+"\n";

        s += "time = " + time + "\n";
        s += "score = " + winScore + "\n";
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
