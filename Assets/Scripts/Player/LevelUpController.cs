using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.UI;

public class LevelUpController : MonoBehaviour
{
    [SerializeField] private Ability[] allSkills;
    [SerializeField] private List<Ability> availableSkills;
    [SerializeField] private Ability[] selectedSkills;

    public Image[] skillImage;
    public Button[] skillButtons;

    [SerializeField] private GameObject gameManagerObject;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = gameManagerObject.gameObject.GetComponent<GameManager>();
        AvailableAbilities();
    }

    private void AvailableAbilities()
    {
        for (int i = 0; i < allSkills.Length; i++)
        {
            if (allSkills[i].abilityLevel == 1)
            {
                foreach (Ability ability in selectedSkills)
                {
                    if (allSkills[i].abilityName != ability.abilityName)
                    {
                        availableSkills.Add(allSkills[i]);
                    }
                }
            }

            if (allSkills[i].abilityLevel != 1)
            {
                foreach (Ability ability in selectedSkills)
                {
                    if (allSkills[i].abilityName != ability.abilityName)
                    {
                        if (allSkills[i].connectedAbility == ability)
                        {
                            availableSkills.Add(allSkills[i]);
                        }
                    }
                }
            }
        }

    }

    public void SelectSkills()
    {
        int numSkillsToChoose = 3;

        Ability[] chosenSkills = new Ability[numSkillsToChoose];
        int[] chosenIndices = new int[numSkillsToChoose];

        for (int i = 0; i < numSkillsToChoose; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, availableSkills.Count);
            }
            while (ArrayContains(chosenIndices, randomIndex));

            chosenIndices[i] = randomIndex;
            chosenSkills[i] = availableSkills[randomIndex];
            skillImage[i].sprite = chosenSkills[i].abilityIcon;
            skillImage[i].gameObject.SetActive(true);

            string buttonName = availableSkills[randomIndex].abilityName;
            int buttonIndex = i;
            skillButtons[i].onClick.AddListener(() => ChosenAbilities(buttonName));
        }

        gameManager.currentState = GameState.AbilityChoosing;
    }


    private bool ArrayContains(int[] array, int value)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == value)
            {
                return true;
            }
        }
        return false;
    }

    private void ChosenAbilities(string buttonName)
    {
        int buttonIndex = -1;
        for (int i = 0; i < availableSkills.Count; i++)
        {
            if (availableSkills[i].abilityName == buttonName)
            {
                buttonIndex = i;
                break;
            }
        }
        if (buttonIndex >= 0 && buttonIndex < availableSkills.Count)
        {
            Ability selectedAbility = availableSkills[buttonIndex];

            System.Array.Resize(ref selectedSkills, selectedSkills.Length + 1);
            selectedSkills[selectedSkills.Length - 1] = selectedAbility;
            availableSkills.Remove(selectedAbility);
        }
        gameManager.currentState = GameState.Playing;
        skillImage[0].gameObject.SetActive(false);
        skillImage[1].gameObject.SetActive(false);
        skillImage[2].gameObject.SetActive(false);
    }

}
