using UnityEngine;

public class PickableInventoryItem : MonoBehaviour
{
    //----------- UNITY EDITOR -----------//
    [SerializeField] InventoryItemDefinition ItemDefinition;

    //---------- UNITY METHODS ----------//
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryUI.Instance.NotifyItemPicked(ItemDefinition);

            Destroy(gameObject);
        }
    }
}
