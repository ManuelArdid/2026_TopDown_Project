using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Inventory/Item Definition")]
public class InventoryItemDefinition : ScriptableObject
{
    public Sprite Image;

    [FormerlySerializedAs("ItemName")]
    public string UniqueItemName;

    public float HealthRecovery;
    
    public int Bullets;

    public int NumUses = 1;
}
