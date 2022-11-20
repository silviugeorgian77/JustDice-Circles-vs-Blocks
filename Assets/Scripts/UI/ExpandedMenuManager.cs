using System.Threading.Tasks;
using UnityEngine;

public class ExpandedMenuManager : MonoBehaviour
{
    [SerializeField]
    private Transform contentTransform;

    [SerializeField]
    private GameObject simpleButtonUIItemPrefab;

    [SerializeField]
    private GameObject attackerUIItemPrefab;

    [SerializeField]
    private GameObject userAttackerUIItemPrefab;

    [SerializeField]
    private GameObject notEnoughCurrencyNotif;

    private GameConfig gameConfig;
    private UserData userData;

    private void Awake()
    {
        notEnoughCurrencyNotif.SetActive(false);
    }

    public void Init(GameConfig gameConfig, UserData userData)
    {
        this.gameConfig = gameConfig;
        this.userData = userData;

        InitScrollViewItems();
    }

    private void InitScrollViewItems()
    {
        InitUserAttackerItem();
        InitAttackerItems();
        InitClearDataButton();
    }

    private void InitUserAttackerItem()
    {
        var userAttackerUIItemObject = Instantiate(
            userAttackerUIItemPrefab,
            contentTransform
        );
        var userAttackerUIItem
            = userAttackerUIItemObject.GetComponent<UserAttackerUIItem>();
        userAttackerUIItem.Bind(
            userData.UserAttacker,
            gameConfig.tapUpgradeCostFormula,
            () =>
            {
                var transactionSuccessful
                    = userData.AddCurrency(
                        -gameConfig
                            .tapUpgradeCostFormula
                            .GetValue(userData.UserAttacker.UpgradeLevel)
                    );
                if (transactionSuccessful)
                {
                    userData.UserAttacker.UpgradeLevel++;
                }
                else
                {
                    ShowNotEnoughCurrency();
                }
            }
        );
    }

    private void InitAttackerItems()
    {
        GameObject attackerUIItemObject;
        AttackerUIItem attackerUIItem;
        foreach (var attacker in userData.Attackers)
        {
            attackerUIItemObject = Instantiate(
                attackerUIItemPrefab,
                contentTransform
            );
            attackerUIItem
                = attackerUIItemObject.GetComponent<AttackerUIItem>();
            attackerUIItem.Bind(
                attacker,
                gameConfig.attackerUpgradeCostFormula,
                () =>
                {
                    var transactionSuccessful
                        = userData.AddCurrency(
                            -gameConfig
                                .attackerUpgradeCostFormula
                                .GetValue(attacker.UpgradeLevel)
                        );
                    if (transactionSuccessful)
                    {
                        attacker.UpgradeLevel++;
                    }
                    else
                    {
                        ShowNotEnoughCurrency();
                    }
                }
            );
        }
    }

    private void InitClearDataButton()
    {
        var clearDataItemObject = Instantiate(
            simpleButtonUIItemPrefab,
            contentTransform
        );
        var clearDataItem
            = clearDataItemObject.GetComponent<SimpleButtonUIItem>();
        clearDataItem.Bind("Clear Data", () =>
        {
            userData.Reset(gameConfig.maxAttackersCount);
        });
    }

    private async void ShowNotEnoughCurrency()
    {
        notEnoughCurrencyNotif.SetActive(true);
        await Task.Delay(2000);
        if (notEnoughCurrencyNotif == null)
        {
            return;
        }
        notEnoughCurrencyNotif.SetActive(false);
    }
}
