using UnityEngine;
using System.Collections;

public class GameInfo : MonoBehaviour {

    public string mapName = "BackgroundWonderland";
    public GameModeEnum gameMode = GameModeEnum.Score;

    public int time = 2;
    public int winScore = 10;
    public int lifes = 1;

    public PlayerTypeEnum player1type = PlayerTypeEnum.Player;
    public PlayerTypeEnum player2type = PlayerTypeEnum.Player;
    public PlayerTypeEnum player3type = PlayerTypeEnum.Player;
    public PlayerTypeEnum player4type = PlayerTypeEnum.Player;

    public CharacterEnum player1char = CharacterEnum.Einstein;
    public CharacterEnum player2char = CharacterEnum.Curie;
    public CharacterEnum player3char = CharacterEnum.Nobel;
    public CharacterEnum player4char = CharacterEnum.Tesla;

    public int player1score = 1;
    public int player2score = 5;
    public int player3score = 4;
    public int player4score = 1;

    public int player1lifes = 0;
    public int player2lifes = 0;
    public int player3lifes = 0;
    public int player4lifes = 0;

    public int player1deaths = 0;
    public int player2deaths = 0;
    public int player3deaths = 0;
    public int player4deaths = 1;



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
