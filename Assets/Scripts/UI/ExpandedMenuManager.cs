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
        InitClearDataButton();
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
            userData.Reset();
        });
    }
}
