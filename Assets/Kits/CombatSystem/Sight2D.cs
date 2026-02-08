using UnityEngine;

public class Sight2D : MonoBehaviour
{
    [SerializeField] float radius = 5f;
    //[SerializeField] float checkFrecuency = 5f;


    float lastCheckTime;
    Collider2D[] colliders;
    // Update is called once per frame
    void Update()
    {
        if ((Time.time - lastCheckTime) > (1f / lastCheckTime))
        {
            lastCheckTime = Time.time;
        }
        
        colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            Debug.Log($"El collider {i} se llama {colliders[i].name}.", colliders[i]);
        }
    }

    public bool IsPlayerInSight()
    {
        bool isPlayerInSight = false;
        
        colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        for (int i = 0; !isPlayerInSight && (i < colliders.Length); i++)
        {
           if(colliders[i].CompareTag("Player"))
            { isPlayerInSight = true;}
        }
        return isPlayerInSight;
    }
}