using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEditor.Experimental.GraphView;

public class AttackerUIItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text titleText;

    [SerializeField]
    private TMP_Text upgradeLevelText;

    [SerializeField]
    private TMP_Text costText;

    [SerializeField]
    private Button buyButton;

    [SerializeField]
    private TMP_Text buyButtonText;

    public void Bind(
       Attacker attacker,
       UpgradeCostFormula upgradeCostFormula,
       Action action)
    {
        titleText.text = "Circle" + attacker.Id;
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
        if (level == 0)
        {
            buyButtonText.text = "BUY";
        }
        else
        {
            buyButtonText.text = "UPGRADE";
        }
    }

    private void OnDestroy()
    {
        buyButton.onClick.RemoveAllListeners();
    }
}
