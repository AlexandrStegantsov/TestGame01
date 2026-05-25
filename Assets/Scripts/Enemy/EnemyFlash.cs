using UnityEngine;

public class EnemyFlash : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer;

    [SerializeField] private Color hitColor =
        Color.red;

    private Color originalColor;

    private Material material;

    private void Awake()
    {
        material =
            targetRenderer.material;

        originalColor =
            material.color;

        Health health =
            GetComponent<Health>();

        health.OnHealthChanged +=
            OnHealthChanged;
    }

    private void OnDestroy()
    {
        Health health =
            GetComponent<Health>();

        if (health != null)
        {
            health.OnHealthChanged -=
                OnHealthChanged;
        }
    }

    private void OnHealthChanged(
        float current,
        float max)
    {
        StopAllCoroutines();

        StartCoroutine(FlashRoutine());
    }

    private System.Collections.IEnumerator
        FlashRoutine()
    {
        material.color = hitColor;

        yield return new WaitForSeconds(0.1f);

        material.color = originalColor;
    }
}