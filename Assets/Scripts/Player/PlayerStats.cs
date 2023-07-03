using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Tooltip("Starting HP of this character class")]
    public int baseHP;

    [Tooltip("Starting Mana of this character class")]
    public int baseMana;

    [Tooltip("Starting Strength of this character class")]
    public int baseStrength;

    [Tooltip("Starting Defense of this character class")]
    public int baseDefence;

    public float baseSpeed;

    public int level = 1;

    public int currentXP;

    public int xpToLevel = 100;


    public void GetXP(int xpGet)
    {
        currentXP += xpGet;

        if (currentXP >= xpToLevel)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        currentXP -= xpToLevel;

        level++;

        NextLevelXP();

        LevelUpController levelUpController = GetComponent<LevelUpController>();
        levelUpController.SelectSkills();
    }


    public void NextLevelXP()
    {
        xpToLevel = Mathf.RoundToInt(xpToLevel * 1.2f);
    }
}
