using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

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

    [SerializeField]
    private Image bagImage;

    [SerializeField]
    private Color color0;

    [SerializeField]
    private Color color1;

    private UpgradeCostFormula upgradeCostFormula;

    public void Bind(
       Attacker attacker,
       UpgradeCostFormula upgradeCostFormula,
       Action action)
    {
        this.upgradeCostFormula = upgradeCostFormula;
        bagImage.color = attacker.Id % 2 == 0 ? color0 : color1;
        titleText.text = "Circle " + (attacker.Id + 1);
        UpdateLevel(attacker.UpgradeLevel);
        attacker.onUpgradeLevelChanged += UpdateLevel;
        buyButton.onClick.AddListener(() => action.Invoke());
    }

    private void UpdateLevel(int level)
    {
        if (level != 0)
        {
            upgradeLevelText.text = "Level: " + level.ToString();
        }
        else
        {
            upgradeLevelText.text = "Not purchased yet";
        }
        
        if (level == 0)
        {
            buyButtonText.text = "BUY";
        }
        else
        {
            buyButtonText.text = "UPGRADE";
        }
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
