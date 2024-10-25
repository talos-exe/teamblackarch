using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public Vector3 playerLocation;
    public int playerLvl;
    public int sceneNumber;
    
    //This houses the values the game will start with when there is no data to load
    public GameData()
    {
        this.playerLocation = new Vector3 (0f,0f,0f);
        this.playerLvl = 1;
        this.sceneNumber = 3;
    }

}
