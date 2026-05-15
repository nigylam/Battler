using System.Linq;
using UnityEngine;

public class UnitPreview : MonoBehaviour
{
    [SerializeField] private Material _available;
    [SerializeField] private Material _blocked;
    [SerializeField] private SkinnedMeshRenderer[] _renderers;
    [SerializeField] private MeshRenderer[] _meshRenderers;

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

        if(_meshRenderers.Length > 0)
            foreach (var meshRenderer in _meshRenderers)
                meshRenderer.material = material;
    }
}
