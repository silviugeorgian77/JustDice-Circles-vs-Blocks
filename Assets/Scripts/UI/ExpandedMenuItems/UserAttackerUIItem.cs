using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEditor.Experimental.GraphView;

public class UserAttackerUIItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text upgradeLevelText;

    [SerializeField]
    private TMP_Text costText;

    [SerializeField]
    private Button buyButton;

    private UpgradeCostFormula upgradeCostFormula;

    public void Bind(
        Attacker attacker,
        UpgradeCostFormula upgradeCostFormula,
        Action action)
    {
        this.upgradeCostFormula = upgradeCostFormula;
        UpdateLevel(attacker.UpgradeLevel);
        attacker.onUpgradeLevelChanged += UpdateLevel;
        buyButton.onClick.AddListener(() => action.Invoke());
    }

    private void UpdateLevel(int level)
    {
        upgradeLevelText.text = "Level: " + level.ToString();
        costText.text
            = "Cost: "
            + upgradeCostFormula
                    .GetValue(level)
                    .ToString();
    }

    private void OnDestroy()
    {
        buyButton.onClick.RemoveAllListeners();
    }
}
