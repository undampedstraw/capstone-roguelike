using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }

        //PlayerPrefs.DeleteAll(); //reset all data

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
        //DontDestroyOnLoad(gameObject);
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
    public Camera mainCamera;
    public RectTransform hitpointBar;
    public GameObject hud;
    public GameObject menu;
    public Animator deathMenuAnimator;

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

    public void OnHitpointChange()
    {
        float ratio = (float)player.hitpoint / (float)player.maxHitPoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
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
        player.OnLevelUp();
        OnHitpointChange();
    }
    public void SaveState()
    {
        
        string s = "";
        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();
        PlayerPrefs.SetString("SaveState", s);
    }

    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    public void LoadState(Scene sc, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;
        //known issue: weird duplicate character issue when reentering a scene for the 2nd time. This is likely due to
        //something with singletons or instances of the player.

        if (!PlayerPrefs.HasKey("SaveState"))
            return;


        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        UnityEngine.Debug.Log("LoadState");

        pesos = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        if(GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());
        weapon.SetWeaponLevel(int.Parse(data[3]));

        ////chooses where player spawns based on game object in scene
        //player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    //death menu respawn
    public void Respawn()
    {
        //KNOWN ISSUE: OnClick() for respawn button doesn't connect to gamemanager after first respawn
        //since gamemanager instance changes. need to find a fix
        UnityEngine.Debug.Log("RESPAWN PLAYER!");
        deathMenuAnimator.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        player.Respawn();
    }
}
