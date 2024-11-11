using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExMenu : MonoBehaviour
{
    public TMPro.TextMeshProUGUI levelText, hitpointText, pesosText, upgradeCostText, xpText;

    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    //char sel
    public void OnArrowClick(bool right)
    {
        if(right)
        {
            currentCharacterSelection++;
            if(currentCharacterSelection == GameManager.instance.playerSprites.Count)
            {
                currentCharacterSelection = 0;
            }
        }
        else
        {
            currentCharacterSelection--;
            if (currentCharacterSelection < 0)
            {
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
            } 
        }

        OnSelectChanged();
    }
    private void OnSelectChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    //weapon
    public void OnUpgradeClick()
    {
        //get weapon reference
        if (GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }

    //update char info
    public void UpdateMenu()
    {

        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if(GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
            upgradeCostText.text = "MAX";
        else
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();

        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        //UnityEngine.Debug.Log(GameManager.instance.pesos.ToString());
        pesosText.text = GameManager.instance.pesos.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();

        int currLevel = GameManager.instance.GetCurrentLevel();
        if (currLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + " total experience points";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currLevel - 1);
            int currLevelXp = GameManager.instance.GetXpToLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currXpIntoLevel = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1.0f, 1.0f);
            xpText.text = currXpIntoLevel.ToString() + " / " + diff;
        }
    }

    public void PauseGame()
    {
        if (Time.timeScale == 1)
        {
            WaitForPause();
            Time.timeScale = 0;
        }
        else
            Time.timeScale = 1;
    }

    IEnumerator WaitForPause()
    {
        yield return new WaitForSeconds(0.25f);
    }
}
