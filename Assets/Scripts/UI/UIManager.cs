using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingObject;

    public void ShowLoading()
    {
        loadingObject.SetActive(true);
    }

    public void HideLoading()
    {
        loadingObject.SetActive(false);
    }
}
