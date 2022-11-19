using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingObject;

    [SerializeField]
    private CurrencyWidget currencyWidget;

    private UserData userData;

    public void Init(UserData userData)
    {
        this.userData = userData;

        currencyWidget.Bind(userData.CurrencyCount);
        userData.onCurrencyCountChanged += OnCurrencyCountChanged;
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

    private void OnDestroy()
    {
        userData.onCurrencyCountChanged -= OnCurrencyCountChanged;
    }
}
