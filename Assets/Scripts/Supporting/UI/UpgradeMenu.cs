using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] List<Button> upgradeButtons;
    [SerializeField] List<TextMeshProUGUI> upgradeTexts;

    [SerializeField] float healthIncrease;
    [SerializeField] float attackIncrease;
    [SerializeField] float attackSpeedIncrease;
    [SerializeField] float parryDamageBonusDurationIncrease;
    [SerializeField] float parryDamageBonusMultiplierIncrease;
    [SerializeField] float movementSpeedIncrease;

    PlayerMain playerMain;
    int numberOfUpgradeChoices = 3;
    int totalAvailableUpgrades = 6;

    private void Start()
    {
        playerMain = General.Instance.Player.GetComponent<PlayerMain>();
    }

    public void PresentUpgrades()
    {
        gameObject.SetActive(true);
        GameManager.Instance.Pause();

        bool[] alreadyChosen = new bool[totalAvailableUpgrades];

        for (int upgradeSlot = 0; upgradeSlot < numberOfUpgradeChoices; upgradeSlot++)
        {
            int chosenUpgrade = Random.Range(0, 0);
            // int chosenUpgrade = Random.Range(0, totalAvailableUpgrades);
            while (alreadyChosen[chosenUpgrade])
            {
                chosenUpgrade = Random.Range(0, totalAvailableUpgrades);
            }
            // alreadyChosen[chosenUpgrade] = true;
            alreadyChosen[chosenUpgrade] = false;

            Button button = upgradeButtons[upgradeSlot];
            TextMeshProUGUI text = upgradeTexts[upgradeSlot];

            switch (chosenUpgrade)
            {
                case 0:
                    upgradeHealth(upgradeSlot, button, text);
                    break;
                case 1:
                    upgradeAttack(upgradeSlot, button, text);
                    break;
                case 2:
                    upgradeAttackSpeed(upgradeSlot, button, text);
                    break;
                case 3:
                    upgradeParryDamageBonusDuration(upgradeSlot, button, text);
                    break;
                case 4:
                    upgradeParryDamageBonusMultiplier(upgradeSlot, button, text);
                    break;
                case 5:
                    upgradeMovementSpeed(upgradeSlot, button, text);
                    break;
                default:
                    Debug.Log("You broke something oops");
                    break;
            }
            button.onClick.AddListener(closeAndResetMenuOnClick);
        }
    }

    void upgradeHealth(int pos, Button button, TextMeshProUGUI text)
    {
        text.text = "Increase max health by " + healthIncrease + ".";
        button.onClick.AddListener(() => playerMain.IncreaseMaxHealth(healthIncrease));
    }

    void upgradeAttack(int pos, Button button, TextMeshProUGUI text)
    {
        text.text = "Increase base damage by " + attackIncrease + ".";
        button.onClick.AddListener(() => playerMain.IncreaseAttack(attackIncrease));
    }

    void upgradeAttackSpeed(int pos, Button button, TextMeshProUGUI text)
    {
        text.text = "Increase attack speed by " + attackSpeedIncrease + ".";
        button.onClick.AddListener(() => playerMain.IncreaseAttackSpeed(attackSpeedIncrease));
    }

    void upgradeParryDamageBonusDuration(int pos, Button button, TextMeshProUGUI text)
    {
        text.text = "Post parry damage bonus duration +" + parryDamageBonusDurationIncrease + "s.";
        button.onClick.AddListener(() => playerMain.IncreaseParryDamageBonusDuration(parryDamageBonusDurationIncrease));
    }

    void upgradeParryDamageBonusMultiplier(int pos, Button button, TextMeshProUGUI text)
    {
        text.text = "Post parry damage bonus +" + parryDamageBonusMultiplierIncrease * 100 + "%.";
        button.onClick.AddListener(() => playerMain.IncreaseAttackSpeed(parryDamageBonusMultiplierIncrease));
    }

    void upgradeMovementSpeed(int pos, Button button, TextMeshProUGUI text)
    {
        text.text = "Movement Speed +" + movementSpeedIncrease + ".";
        button.onClick.AddListener(() => playerMain.IncreaseMovementSpeed(movementSpeedIncrease));
    }

    void closeAndResetMenuOnClick()
    {
        WaveSpawner.Instance.UpgradesChosen = true;
        foreach (Button button in upgradeButtons)
        {
            button.onClick.RemoveAllListeners();
        }
        this.gameObject.SetActive(false);

        GameManager.Instance.Unpause();
    }
}
