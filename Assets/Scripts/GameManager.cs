using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    { 
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);

    }


    // Ressources
    public List<Sprite> playerSprite;
    public List<Sprite> weaponSprite;
    public List<int> weaponPrices;
    public List<int> xpTable;
    public RectTransform dashBar;
    public TMP_Text dashText;

    // References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;

    // Logic
    public int liras;
    public int experience;

    

    //Floating Text
    public void showText(string msg, int fontSize, Color color, Vector3 pos, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, pos, motion, duration);
    }

    // Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        // i the weapon mac level
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if(liras >= weaponPrices[weapon.weaponLevel])
        {
            liras -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    // Experience
    public int GetLevel()
    {
        int r = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count)
                return r;
        }

        return r;
    }

    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while(r < level)
        {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }

    // Dash
    public void DashCoolDown(float coolDown)
    {
        string text = "1 / 1";
        if (coolDown > 0)
            text = "0 / 1";

        dashText.text = text;
        dashBar.localScale = new Vector3(1 - coolDown, 1, 1);
    }

    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += liras.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString() + "|";
        s += "0";

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        liras = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        weapon.SetWeaponLevel(int.Parse(data[3]));

    }


}
