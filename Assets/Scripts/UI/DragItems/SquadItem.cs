using UnityEngine;
using UnityEngine.UI;

public class SquadItem : MonoBehaviour
{
    [SerializeField] private SquadPreview _preview;
    [SerializeField] private SquadPlan _plan;
    [SerializeField] private Image _image;

    public SquadPreview Preview => _preview;
    public SquadPlan Plan => _plan;
    public Image Image => _image;
}
