using UnityEngine;

public class PickableInventoryItem : MonoBehaviour
{
    //----------- UNITY EDITOR -----------//
    [SerializeField] PickableInventoryItem ItemDefinition;

    //---------- UNITY METHODS ----------//
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
