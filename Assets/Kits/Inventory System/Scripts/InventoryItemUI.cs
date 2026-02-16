using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemUI : MonoBehaviour
{
    //-------- UNITY EDITOR ------------//
    [SerializeField] public InventoryItemDefinition ItemDefinition;

    //-------- CLASS VARIABLES --------//
    private Button[] _buttons;
    private Image _itemImage;
    private TMP_Text _itemNameText;
    private InventoryUI _inventoryUI;

    //-------- ENUMS --------//
    enum ButtonAction
    {
        Discard,
        Use,
        Give,
        Sell
    }

    //-------- UNITY METHODS --------//
    void Awake()
    {
        _buttons = GetComponentsInChildren<Button>();
        _itemImage = GetComponentInChildren<Image>();
        _itemNameText = GetComponentInChildren<TMP_Text>();
        _inventoryUI = GetComponentInParent<InventoryUI>();
   }

    void Start()
    {
        Init(ItemDefinition);
    }

    void OnEnable()
    {
        _buttons[(int)ButtonAction.Discard].onClick.AddListener(OnDiscard);
        _buttons[(int)ButtonAction.Use].onClick.AddListener(OnUse);
        _buttons[(int)ButtonAction.Give].onClick.AddListener(OnGive);
        _buttons[(int)ButtonAction.Sell].onClick.AddListener(OnSell);
    }

    void OnDisable()
    {
        _buttons[(int)ButtonAction.Discard].onClick.RemoveListener(OnDiscard);
        _buttons[(int)ButtonAction.Use].onClick.RemoveListener(OnUse);
        _buttons[(int)ButtonAction.Give].onClick.RemoveListener(OnGive);
        _buttons[(int)ButtonAction.Sell].onClick.RemoveListener(OnSell);
    }

    //-------- PUBLIC METHODS --------//
    public void Init(InventoryItemDefinition definition)
    {
        ItemDefinition = Instantiate(definition);
        _itemImage.sprite = definition.Image;
        _itemNameText.text = definition.UniqueItemName;
    }

    //-------- PRIVATE METHODS --------//
    private void OnDiscard()
    {
        Debug.Log("OnDiscard", gameObject);
    }

    private void OnUse()
    {
        _inventoryUI.NotifyItemUsed(ItemDefinition);
        ItemDefinition.NumUses--;

        if (ItemDefinition.NumUses <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnGive()
    {
        Debug.Log("OnGive", gameObject);
    }

    private void OnSell()
    {
        Debug.Log("OnSell", gameObject);
    }
}
