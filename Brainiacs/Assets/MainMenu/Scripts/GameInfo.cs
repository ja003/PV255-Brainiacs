using UnityEngine;
using System.Collections;

public class GameInfo : MonoBehaviour {

    public string mapName = "BackgroundWonderland";
    public GameModeEnum gameMode = GameModeEnum.Deathmatch;

    public int time = 70;
    public int winScore = 10;
    public int lifes = 3;

    public PlayerTypeEnum player1type = PlayerTypeEnum.Player;
    public PlayerTypeEnum player2type = PlayerTypeEnum.Player;
    public PlayerTypeEnum player3type = PlayerTypeEnum.None;
    public PlayerTypeEnum player4type = PlayerTypeEnum.AI;

    public CharacterEnum player1char = CharacterEnum.Tesla;
    public CharacterEnum player2char = CharacterEnum.DaVinci;
    public CharacterEnum player3char = CharacterEnum.Curie;
    public CharacterEnum player4char = CharacterEnum.Curie;

    public int player1score = 0;
    public int player2score = 0;
    public int player3score = 0;
    public int player4score = 0;

    public int player1lifes = 0;
    public int player2lifes = 0;
    public int player3lifes = 0;
    public int player4lifes = 0;

    public int player1deaths = 0;
    public int player2deaths = 0;
    public int player3deaths = 0;
    public int player4deaths = 0;

    public void RefreshGameInfo()
    {
        player1type = PlayerTypeEnum.None;
        player2type = PlayerTypeEnum.None;
        player3type = PlayerTypeEnum.None;
        player4type = PlayerTypeEnum.None;

        player1char = CharacterEnum.None;
        player2char = CharacterEnum.None;
        player3char = CharacterEnum.None;
        player4char = CharacterEnum.None;

        player1score = -1;
        player2score = -1;
        player3score = -1;
        player4score = -1;

        player1lifes = -1;
        player2lifes = -1;
        player3lifes = -1;
        player4lifes = -1;

        player1deaths = -1;
        player2deaths = -1;
        player3deaths = -1;
        player4deaths = -1;
}


    public void RefreshInactiveStats()
    {
        if (player1type == PlayerTypeEnum.None)
        {
            player1score = -1;
            player1deaths = -1;
            player1lifes = -1;
        }
        if (player2type == PlayerTypeEnum.None)
        {
            player2score = -1;
            player2deaths = -1;
            player2lifes = -1;
        }
        if (player3type == PlayerTypeEnum.None)
        {
            player3score = -1;
            player3deaths = -1;
            player3lifes = -1;
        }
        if (player4type == PlayerTypeEnum.None)
        {
            player4score = -1;
            player4deaths = -1;
            player4lifes = -1;
        }
    }

    public void RefreshStats()
    {
        if (player1type == PlayerTypeEnum.None)
        {
            player1score = -1;
            player1deaths = -1;
            player1lifes = -1;
        }
        else
        {
            player1score = 0;
            player1deaths = 0;
            player1lifes = lifes;
        }
        if (player2type == PlayerTypeEnum.None)
        {
            player2score = -1;
            player2deaths = -1;
            player2lifes = -1;
        }
        else
        {
            player2score = 0;
            player2deaths = 0;
            player2lifes = lifes;
        }
        if (player3type == PlayerTypeEnum.None)
        {
            player3score = -1;
            player3deaths = -1;
            player3lifes = -1;
        }
        else
        {
            player3score = 0;
            player3deaths = 0;
            player3lifes = lifes;
        }
        if (player4type == PlayerTypeEnum.None)
        {
            player4score = -1;
            player4deaths = -1;
            player4lifes = -1;
        }
        else
        {
            player4score = 0;
            player4deaths = 0;
            player4lifes = lifes;
        }
    }

    public override string ToString()
    {
        string s = "";
        s += "gameMode = " + gameMode+"\n";

        s += "player1score = " + player1score + "\n";
        s += "player2score = " + player2score + "\n";
        s += "player3score = " + player3score + "\n";
        s += "player4score = " + player4score + "\n";

        s += "player1deaths = " + player1deaths + "\n";
        s += "player2deaths = " + player2deaths + "\n";
        s += "player3deaths = " + player3deaths + "\n";
        s += "player4deaths = " + player4deaths + "\n";

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
