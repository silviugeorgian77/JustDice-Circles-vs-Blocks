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

    private GameConfig gameConfig;
    private UserData userData;

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
                userData.UserAttacker.UpgradeLevel++;
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
                    attacker.UpgradeLevel++;
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
}
