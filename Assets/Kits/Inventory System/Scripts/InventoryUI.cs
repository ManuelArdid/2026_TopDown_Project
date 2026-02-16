using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    static public InventoryUI Instance;

    //-------- UNITY EDITOR ------------//

    [SerializeField] private GameObject InventoryItemPrefab;

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
}
