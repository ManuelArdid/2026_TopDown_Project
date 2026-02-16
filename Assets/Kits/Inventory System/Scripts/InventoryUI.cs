using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    static public InventoryUI Instance;

    //-------- UNITY EDITOR ------------//

    [SerializeField] private GameObject InventoryItemPrefab;
    [SerializeField] private PlayerCharacter Owner;

    //-------- CLASS VARIABLES --------//

    GridLayoutGroup _grid;

    //-------- UNITY METHODS --------//

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _grid = GetComponentInChildren<GridLayoutGroup>();
    }

    //-------- PUBLIC METHODS --------//

    public void NotifyItemPicked(InventoryItemDefinition itemDefinition)
    {
        GameObject item = Instantiate(InventoryItemPrefab, _grid.transform);
        item.GetComponent<InventoryItemUI>().Init(itemDefinition);
    }

    public void NotifyItemUsed(InventoryItemDefinition itemDefinition)
    {
        Owner.NotifyInventoryItemUsed(itemDefinition);
    }

    public bool Contains(InventoryItemDefinition itemDefinition)
    {
        InventoryItemUI[] items = GetComponentsInChildren<InventoryItemUI>();
        return Array.Find(items, item => item.ItemDefinition.UniqueItemName == itemDefinition.UniqueItemName) != null;
    }

    public void Consume(InventoryItemDefinition itemDefinition)
    {
        InventoryItemUI[] items = GetComponentsInChildren<InventoryItemUI>();
        InventoryItemUI item = Array.Find(items, item => item.ItemDefinition.UniqueItemName == itemDefinition.UniqueItemName);
        item.ItemDefinition.NumUses--;
        if (item.ItemDefinition.NumUses <= 0)
        {
            Destroy(item.gameObject);
        }
    }
}
