using UnityEngine;
using TMPro;

public class CurrencyWidget : MonoBehaviour
{
    [SerializeField]
    private TMP_Text countText;

    public void Bind(float count)
    {
        countText.text = count.ToString();
    }
}
