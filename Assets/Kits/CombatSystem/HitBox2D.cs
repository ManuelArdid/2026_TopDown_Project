using UnityEngine;
using UnityEngine.Assertions.Must;

public class HitBox2D : MonoBehaviour
{
    [SerializeField] string affectedTag = "Enemy";
    [SerializeField] string affectedTag_1 = "EnemyRange";
    //[SerializeField] string affectedTag_2 = "EnemyRunner";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(affectedTag) || collision.CompareTag(affectedTag_1)) //|| collision.CompareTag(affectedTag_2))
        {
            MovementController movementController = collision.GetComponent<MovementController>();
            movementController.NotifyHit(this);
        }
    }
}
