using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected Button button;
    [SerializeField] protected TextMeshProUGUI textMeshProUGUI;

    public void SetVisual(string text, UnityAction OnPlantClick)
    {
        textMeshProUGUI.text = text;
        button.onClick.AddListener(OnPlantClick);
    }

    private void OnValidate()
    {
        if (!gameObject.activeInHierarchy) return;

        if (button == null) Logging.LogWarning(gameObject.name + " Missing button");
        if (textMeshProUGUI == null) Logging.LogWarning(gameObject.name + " Missing text");
    }
}
