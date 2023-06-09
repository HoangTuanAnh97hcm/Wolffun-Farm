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

            string text = $"{item.name}: {item.amount}Seed/{item.price}Coint";

            seedItem.SetVisual(text, () =>
            {
                if (GameData.Instance.GetCoint() < item.price)
                {
                    Logging.LogError("You didn't have enought coint");
                    return;
                }

                GameData.Instance.SetCoint(-item.price);
                Inventory.Instance.SetAmountSeed(item.ID, item.amount);
            });
        }
    }
}
