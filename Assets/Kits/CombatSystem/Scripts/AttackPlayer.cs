using UnityEngine;
using System.Collections;

public class AttackPlayer : MonoBehaviour
{
    public Transform Player;
    public float offsetDistance = 1f;

    public float attackAngle = 45f;
    public float attackDuration = 0.1f;

    private Vector3 lastDirection = Vector3.down;
    private bool isAttacking = false;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false; // Empieza invisible
    }

    void Update()
    {
        // Dirección de movimiento
        Vector3 movement = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"),
            0f
        );

        if (movement != Vector3.zero)
            lastDirection = movement.normalized;

        float baseAngle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg + 90f;

        if (!isAttacking)
            transform.rotation = Quaternion.Euler(0f, 0f, baseAngle);

        transform.position = Player.position + lastDirection * offsetDistance;

        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(AttackRotation(baseAngle));
        }
    }

    IEnumerator AttackRotation(float baseAngle)
    {
        isAttacking = true;
        spriteRenderer.enabled = true; // Se hace visible al atacar

        float elapsed = 0f;
        float targetAngle = baseAngle - attackAngle;

        // Giro hacia la derecha
        while (elapsed < attackDuration)
        {
            elapsed += Time.deltaTime;
            float angle = Mathf.Lerp(baseAngle, targetAngle, elapsed / attackDuration);
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            yield return null;
        }

        elapsed = 0f;

        // Volver
        while (elapsed < attackDuration)
        {
            elapsed += Time.deltaTime;
            float angle = Mathf.Lerp(targetAngle, baseAngle, elapsed / attackDuration);
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            yield return null;
        }

        spriteRenderer.enabled = false; // Vuelve a ocultarse
        isAttacking = false;
    }
}
