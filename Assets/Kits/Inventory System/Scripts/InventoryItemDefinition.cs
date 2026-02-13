using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Definition")]
public class InventoryItemDefinition : ScriptableObject
{
    public Sprite Image;
    public string ItemName;
    public float HealthRecovery;
    public int Bullets;
}
