using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(GameManager.instance);
            return;
        }

        //PlayerPrefs.DeleteAll(); //reset all data

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    //resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //references
    public player player;
    public MeleeWeapon weapon;
    public FloatingTextManager floatingTextManager;

    //logic
    public int pesos;
    public int experience;

    //floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    public bool TryUpgradeWeapon()
    {
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;
        if(pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }

    public int GetCurrentLevel()
    {
        int current_level = 0;
        int add = 0;
        while(experience >= add)
        {
            add += xpTable[current_level];
            current_level++;

            if (current_level == xpTable.Count)
                return current_level;
        }

        return current_level;
    }

    public int GetXpToLevel(int level)
    {
        int current_level = 0;
        int xp = 0;
        while(current_level < level)
        {
            xp += xpTable[current_level];
            current_level++;
        }

        return xp;

    }

    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel())
            OnLevelUp();
    }

    public void OnLevelUp()
    {
        UnityEngine.Debug.Log("leveled up");
        player.OnLevelUp();
    }
    public void SaveState()
    {
        UnityEngine.Debug.Log("SaveState");
        
        string s = "";
        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();
        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene sc, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;


        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        UnityEngine.Debug.Log("LoadState");

        pesos = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        if(GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());
        weapon.SetWeaponLevel(int.Parse(data[3]));
    }
}
