using System.Collections;
using System.Collections.Generic;
//using System.Media; reinstall vs to fix
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    //resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //references
    public player player;

    //logic
    public int pesos;
    public int experience;

    public void SaveState()
    {
        //UnityEngine.Debug.Log("SaveState");
        string s = "";
        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += "0";
        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState()
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        //UnityEngine.Debug.Log("LoadState");

        pesos = int.Parse(data[1]);
        experience = int.Parse(data[2]);
    }
}
