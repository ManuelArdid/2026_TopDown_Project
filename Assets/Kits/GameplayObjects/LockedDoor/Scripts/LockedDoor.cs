using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] private InventoryItemDefinition KeyDefinition;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(InventoryUI.Instance.Contains(KeyDefinition))
            {
                InventoryUI.Instance.Consume(KeyDefinition);
                Destroy(gameObject);
            }
        }        
    }
}
