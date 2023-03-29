using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{

    public static CharacterMenu instance;

    // Text Fields
    public TMP_Text levelText, hitPointText, lirasText, upgradeCostText, xpText;

    // Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    // Character Selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;

            if(currentCharacterSelection == GameManager.instance.playerSprite.Count)
                currentCharacterSelection= 0;

            OnSelectionChanged();
        }
        else
        {
            currentCharacterSelection--;

            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprite.Count - 1;

            OnSelectionChanged();
        }
    }
    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprite[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    // Weaoon Upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }

    // Update character information
    public void UpdateMenu()
    {
        // Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprite[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
            upgradeCostText.text = "MAX";
        else
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        
        // Meta 
        levelText.text = GameManager.instance.GetLevel().ToString();
        hitPointText.text = GameManager.instance.player.hitPoint.ToString();
        lirasText.text = GameManager.instance.liras.ToString();

        // xp Bar
        int currLevel = GameManager.instance.GetLevel();
        if (currLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + "total experience";
            xpBar.localScale = Vector3.one;
        }else
        {
            // 30 xp, level 1
            int prevLevelXp = GameManager.instance.GetXpToLevel(currLevel - 1);
            int currentLevelXp = GameManager.instance.GetXpToLevel(currLevel);

            int neededLevelXp = currentLevelXp - prevLevelXp;
            int currentXpToLevel = GameManager.instance.experience - prevLevelXp;

            float xpBarCompletion = (float)currentXpToLevel / (float)neededLevelXp;

            xpText.text = currentXpToLevel.ToString() + " / " + neededLevelXp.ToString();
            xpBar.localScale = new Vector3(xpBarCompletion, 1, 1);
        }
    }

}
