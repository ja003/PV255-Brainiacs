using UnityEngine;
using System.Collections;
using System;

public class PlayerResult : IComparable
{
    public int playerNumber;
    public int score;
    public int deaths;

    public PlayerResult(int playerNumber,int score,int deaths)
    {
        this.playerNumber = playerNumber;
        this.score = score;
        this.deaths = deaths;
    }

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;

        PlayerResult otherResult = obj as PlayerResult;
        if (otherResult != null)
        {
            if (score == otherResult.score)
            {
                return deaths.CompareTo(otherResult.deaths);
            }
            return score.CompareTo(otherResult.score)*-1;
        }
        else
            throw new ArgumentException("Object is not a PlayerResult");
    }

    public override string ToString()
    {
        return playerNumber + ": " + score + "/" + deaths;
    }

}
