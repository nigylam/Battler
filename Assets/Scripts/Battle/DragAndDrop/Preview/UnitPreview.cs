using UnityEngine;

public class UnitPreview : MonoBehaviour
{
    [SerializeField] private Material _available;
    [SerializeField] private Material _blocked;
    [SerializeField] private SkinnedMeshRenderer[] _renderers;

    public void SetAvailable()
    {
        Set(_available);
    }

    public void SetBlocked()
    {
        Set(_blocked);
    }

    private void Set(Material material)
    {
        foreach (var renderer in _renderers)
            renderer.material = material;
    }
}
