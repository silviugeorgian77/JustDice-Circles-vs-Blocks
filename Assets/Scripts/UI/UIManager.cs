using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingObject;

    [SerializeField]
    private CurrencyWidget currencyWidget;

    [SerializeField]
    private Button expandMenuButton;

    [SerializeField]
    private Button collapseMenuButton;

    [SerializeField]
    private ExpandedMenuManager expandedMenuManager;

    private UserData userData;

    public Action onExpandMenu;
    public Action onCollapseMenu;

    public void Init(GameConfig gameConfig, UserData userData)
    {
        this.userData = userData;

        currencyWidget.Bind(userData.CurrencyCount);
        userData.onCurrencyCountChanged += OnCurrencyCountChanged;

        expandMenuButton.gameObject.SetActive(true);
        collapseMenuButton.gameObject.SetActive(false);
        expandedMenuManager.gameObject.SetActive(false);

        expandedMenuManager.Init(gameConfig, userData);

        expandMenuButton.onClick.AddListener(OnExpandMenu);
        collapseMenuButton.onClick.AddListener(OnCollapseMenu);
    }

    public void ShowLoading()
    {
        loadingObject.SetActive(true);
    }

    public void HideLoading()
    {
        loadingObject.SetActive(false);
    }

    private void OnCurrencyCountChanged(float currencyCount)
    {
        currencyWidget.Bind(currencyCount);
    }

    private void OnExpandMenu()
    {
        expandMenuButton.gameObject.SetActive(false);
        collapseMenuButton.gameObject.SetActive(true);
        expandedMenuManager.gameObject.SetActive(true);
        onExpandMenu?.Invoke();
    }

    private void OnCollapseMenu()
    {
        expandMenuButton.gameObject.SetActive(true);
        collapseMenuButton.gameObject.SetActive(false);
        expandedMenuManager.gameObject.SetActive(false);
        onCollapseMenu?.Invoke();
    }

    private void OnDestroy()
    {
        userData.onCurrencyCountChanged -= OnCurrencyCountChanged;
        expandMenuButton.onClick.RemoveListener(OnExpandMenu);
        collapseMenuButton.onClick.RemoveListener(OnCollapseMenu);
    }
}
