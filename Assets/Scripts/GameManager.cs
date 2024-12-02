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
        if (instance != null && instance != this)
        {
            //UnityEngine.Debug.Log("iuoserghsdl");
            Destroy(gameObject);
            //Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            //return;
        }
        else
            instance = this;
        //PlayerPrefs.DeleteAll(); //reset all data

        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);

    }

    //resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //references
    [SerializeField]
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

    public void OnSceneLoaded(Scene sc, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
        ////KNOWN ISSUE: this code works but it gives a MissingReferenceException error for some reason.
        ///This is because of player.transform.position.... despite it still being accessible?
    }

    public void LoadState(Scene sc, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
            return;


        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        UnityEngine.Debug.Log("LoadState");

        pesos = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        if (GetCurrentLevel() != 1)
        {
            player.SetLevel(GetCurrentLevel());
            
        }
        weapon.SetWeaponLevel(int.Parse(data[3]));
    }

    //death menu respawn
    public void Respawn()
    {
        UnityEngine.Debug.Log("RESPAWN PLAYER!");
        deathMenuAnimator.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Presentation");
        player.Respawn();
    }
}
