using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreVisual : MonoBehaviour
{
    [SerializeField] private StoreItem storePrefab;
    [SerializeField] private Transform contain;

    private void Start()
    {
        LoadStoreItems();
    }

    public void LoadStoreItems()
    {
        StoreSO[] storeItems = Resources.LoadAll<StoreSO>("");
        foreach (var item in storeItems)
        {
            Item seedItem = Instantiate(storePrefab, contain).GetComponent<Item>();

            string text = $"Seed {item.name}: {item.amount}";

            seedItem.SetVisual(text, () =>
            {
                if (GameData.Instance.GetCoint() < item.price) return;

                GameData.Instance.SetCoint(-item.price);
                Inventory.Instance.SetAmountSeed(item.name, item.amount);

                HideInventory();
            });
        }
    }

    private void HideInventory()
    {
        gameObject.SetActive(false);
    }
}
