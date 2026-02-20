using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemUISimple : MonoBehaviour
{
    //-------- UNITY EDITOR ------------//
    [SerializeField] public InventoryItemDefinition ItemDefinition;

    [SerializeField] private Button useButton;
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text usesRemainingText;

    private InventoryUI _inventoryUI;

    //-------- UNITY METHODS --------//
    void Awake()
    {
        _inventoryUI = GetComponentInParent<InventoryUI>();
    }

    void Start()
    {
        Init(ItemDefinition);
        useButton.onClick.AddListener(OnUse);
    }

    void OnDestroy()
    {
        useButton.onClick.RemoveListener(OnUse);
    }

    //-------- PUBLIC METHODS --------//
    public void Init(InventoryItemDefinition definition)
    {
        ItemDefinition = Instantiate(definition);
        itemImage.sprite = definition.Image;
        itemNameText.text = definition.UniqueItemName;
        UpdateUsesText();
    }

    //-------- PRIVATE METHODS --------//
    private void OnUse()
    {
        _inventoryUI.NotifyItemUsed(ItemDefinition);
        ItemDefinition.NumUses--;

        UpdateUsesText();

        if (ItemDefinition.NumUses <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateUsesText()
    {
        usesRemainingText.text = $"Usos restantes: {ItemDefinition.NumUses}";
    }
}
