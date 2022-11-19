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
    private GameObject expandedMenuObject;

    private UserData userData;

    public void Init(UserData userData)
    {
        this.userData = userData;

        currencyWidget.Bind(userData.CurrencyCount);
        userData.onCurrencyCountChanged += OnCurrencyCountChanged;

        expandMenuButton.gameObject.SetActive(true);
        collapseMenuButton.gameObject.SetActive(false);
        expandedMenuObject.SetActive(false);

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
        expandedMenuObject.SetActive(true);
    }

    private void OnCollapseMenu()
    {
        expandMenuButton.gameObject.SetActive(true);
        collapseMenuButton.gameObject.SetActive(false);
        expandedMenuObject.SetActive(false);
    }

    private void OnDestroy()
    {
        userData.onCurrencyCountChanged -= OnCurrencyCountChanged;
        expandMenuButton.onClick.RemoveListener(OnExpandMenu);
        collapseMenuButton.onClick.RemoveListener(OnCollapseMenu);
    }
}
