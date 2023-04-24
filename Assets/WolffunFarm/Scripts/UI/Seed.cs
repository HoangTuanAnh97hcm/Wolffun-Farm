using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Seed : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private AgriculturalSO agriculturalSO;
    [SerializeField] private GameObject inventory;

    public void OnPointerClick(PointerEventData eventData)
    {
        GridSystem.Instance.SetAgricultural(agriculturalSO);
        inventory.SetActive(false);
    }

    private void OnValidate()
    {
        if (!gameObject.activeInHierarchy) return;

        if (agriculturalSO == null)
            Logging.LogWarning($"{gameObject.name}: AgriculturaSO is null. Please set reference to it");
        if (inventory == null)
            Logging.LogWarning($"{gameObject.name}: Inventory GameObject is null. Please set reference to it");
    }
}
