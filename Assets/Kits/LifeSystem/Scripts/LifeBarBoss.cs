using UnityEngine;
using UnityEngine.UI;

public class LifeBarBoss : MonoBehaviour
{
    //--------- UNITY EDITOR ---------//
    [SerializeField] Image imageFill;
    [SerializeField] Life life;
    [SerializeField] Transform player;       // Referencia al jugador
    [SerializeField] float visibleDistance = 5f; // Distancia a la que se muestra la barra

    //--------- UNITY METHODS ---------//
    private void OnEnable()
    {
        life.OnLifeChanged.AddListener(LifeChangeHandler);
        life.OnDeath.AddListener(OnDeathHandler);
    }

    private void OnDisable()
    {
        life.OnLifeChanged.RemoveListener(LifeChangeHandler);
        life.OnDeath.RemoveListener(OnDeathHandler);
    }

    private void Update()
    {
        if (player == null) return;

        // Calcula la distancia entre jugador y jefe
        float distance = Vector3.Distance(player.position, transform.position);

        // Activa o desactiva la barra según la distancia
        gameObject.SetActive(distance <= visibleDistance);
    }

    //--------- PUBLIC METHODS ---------//
    public void LifeChangeHandler(float currentLife)
    {
        imageFill.fillAmount = currentLife;
    }

    public void OnDeathHandler()
    {
        Destroy(gameObject);
    }
}