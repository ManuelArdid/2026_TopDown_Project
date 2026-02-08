using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] public DropDefinition DropDefinition;

    public void NotifyPickUp()
    {
        Destroy(gameObject);
    }
}
