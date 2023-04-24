using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryVisual : MonoBehaviour
{
    [SerializeField] private Transform seedPrefab;
    [SerializeField] private Transform productPrefab;
    [SerializeField] private Transform contain;

    private void Start()
    {
        LoadSeedItems();
    }

    public void LoadSeedItems()
    {
        ClearAllContain();

        Seeds[] seeds = Inventory.Instance.GetAllSeeds();

        foreach (var seed in seeds)
        {
            SeedItem seedItem = Instantiate(seedPrefab, contain).GetComponent<SeedItem>();

            string text = $"Seed {seed.name}: {seed.amounts}";

            seedItem.SetVisual(text, () =>
            {
                if (seed.amounts == 0) return;

                AgriculturalSO agriculturalSO = Resources.Load<AgriculturalSO>(seed.name);
                GridSystem.Instance.SetAgricultural(agriculturalSO);

                HideInventory();
            });
        }
    }

    public void LoadProductItems()
    {
        ClearAllContain();

        Products[] products = Inventory.Instance.GetAllProducts();

        foreach (var product in products)
        {
            ProductItem productItem = Instantiate(productPrefab, contain).GetComponent<ProductItem>();

            string text = $"Product {product.name}: {product.amounts}";

            productItem.SetVisual(text, () =>
            {
                if (product.amounts == 0) return;

                AgriculturalSO agriculturalSO = Resources.Load<AgriculturalSO>(product.name);
                GameData.Instance.SetCoint(agriculturalSO.price);
                Inventory.Instance.SetAmountProduct(product.name, -1);

                HideInventory();
            });
        }
    }

    private void ClearAllContain()
    {
        foreach (Transform child in contain)
        {
            Destroy(child.gameObject);
        }
    }

    private void HideInventory()
    {
        gameObject.SetActive(false);
    }
}
