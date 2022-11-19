using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UserAttackerUIItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text upgradeLevelText;

    [SerializeField]
    private TMP_Text costText;

    [SerializeField]
    private Button buyButton;

    public void Bind(
        Attacker attacker,
        UpgradeCostFormula upgradeCostFormula,
        Action action)
    {
        UpdateLevel(attacker.UpgradeLevel);
        attacker.onUpgradeLevelChanged += UpdateLevel;
        costText.text
            = upgradeCostFormula
                .GetValue(attacker.UpgradeLevel)
                .ToString();
        buyButton.onClick.AddListener(() => action.Invoke());
    }

    private void UpdateLevel(int level)
    {
        upgradeLevelText.text = (level + 1).ToString();
    }

    private void OnDestroy()
    {
        buyButton.onClick.RemoveAllListeners();
    }
}
