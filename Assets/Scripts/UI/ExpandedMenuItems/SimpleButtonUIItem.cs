using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SimpleButtonUIItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text titleText;

    [SerializeField]
    private Button button;

    public void Bind(string titleString, Action buttonAction)
    {
        titleText.text = titleString;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => buttonAction.Invoke());
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
