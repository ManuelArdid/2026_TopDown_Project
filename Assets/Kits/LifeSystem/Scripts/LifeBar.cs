using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    //--------- UNITY EDITOR ---------//
    [SerializeField] Image imageFill;
    [SerializeField] Life life;

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