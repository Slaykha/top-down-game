using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
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
    }

    // Weaoon Upgrade
    public void OnUpgradeClick()
    {
        // 
    }

    // Update character information
    public void UpdateMenu()
    {
        // Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprite[0];
        upgradeCostText.text = "NOT IMPLEMENTED";
        // Meta 
        levelText.text = "NOT IMPLEMENTED";
        hitPointText.text = GameManager.instance.player.hitPoint.ToString();
        lirasText.text = GameManager.instance.liras.ToString();

        // xp Bar
        xpText.text = "NOT IMPLEMENTED";
        xpBar.localScale = new Vector3(0.5f, 0, 0);

    }

}
