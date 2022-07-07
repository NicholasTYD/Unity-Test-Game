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

    private void Start()
    {
        playerMain = General.Instance.Player.GetComponent<PlayerMain>();
    }

    void upgradeHealth(int pos)
    {
        Button button = upgradeButtons[pos];
        TextMeshProUGUI text = upgradeTexts[pos];
        text.text = "Increase max health by " + healthIncrease + ".";
        button.onClick.AddListener(() => playerMain.IncreaseMaxHealth(healthIncrease));
        button.onClick.RemoveAllListeners();
    }

    void upgradeAttack(int pos)
    {
        Button button = upgradeButtons[pos];
        TextMeshProUGUI text = upgradeTexts[pos];
        text.text = "Increase base damage by " + attackIncrease + ".";
        button.onClick.AddListener(() => playerMain.IncreaseAttack(attackIncrease));
        button.onClick.RemoveAllListeners();
    }

    void upgradeAttackSpeed(int pos)
    {
        Button button = upgradeButtons[pos];
        TextMeshProUGUI text = upgradeTexts[pos];
        text.text = "Increase attack speed by " + attackSpeedIncrease + ".";
        button.onClick.AddListener(() => playerMain.IncreaseAttackSpeed(attackSpeedIncrease));
        button.onClick.RemoveAllListeners();
    }

    void upgradeParryDamageBonusDuration(int pos)
    {
        Button button = upgradeButtons[pos];
        TextMeshProUGUI text = upgradeTexts[pos];
        text.text = "Post parry damage bonus duration +" + parryDamageBonusDurationIncrease + "s.";
        button.onClick.AddListener(() => playerMain.IncreaseParryDamageBonusDuration(parryDamageBonusDurationIncrease));
        button.onClick.RemoveAllListeners();
    }

    void upgradeParryDamageBonusMultiplier(int pos)
    {
        Button button = upgradeButtons[pos];
        TextMeshProUGUI text = upgradeTexts[pos];
        text.text = "Post parry damage bonus +" + attackSpeedIncrease + "%.";
        button.onClick.AddListener(() => playerMain.IncreaseAttackSpeed(parryDamageBonusMultiplierIncrease));
        button.onClick.RemoveAllListeners();
    }

    void upgradeMovementSpeed(int pos)
    {
        Button button = upgradeButtons[pos];
        TextMeshProUGUI text = upgradeTexts[pos];
        text.text = "Movement Speed +" + movementSpeedIncrease + ".";
        button.onClick.AddListener(() => playerMain.IncreaseMovementSpeed(movementSpeedIncrease));
        button.onClick.RemoveAllListeners();
    }

    void closeMenuOnClick()
    {
        this.gameObject.SetActive(false);
    }
}
