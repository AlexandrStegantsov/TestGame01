using UnityEngine;

public class VFXAutoReturn : MonoBehaviour
{
    private ParticleSystem particleSystemComponent;

    private VFXPool ownerPool;

    private void Awake()
    {
        particleSystemComponent =
            GetComponent<ParticleSystem>();
    }

    public void Initialize(VFXPool pool)
    {
        ownerPool = pool;

        particleSystemComponent.Play();
    }

    private void Update()
    {
        if (ownerPool == null)
            return;

        if (particleSystemComponent.isPlaying)
            return;

        ownerPool.Return(gameObject);

        ownerPool = null;
    }
}