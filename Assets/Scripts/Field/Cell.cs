using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private int _indexX;
    [SerializeField] private int _indexY;
    [SerializeField] private Field _field;

    private bool _isAvailable = true;

    public Field Field => _field;
    public bool IsAvailable => _isAvailable;
    public int X => _indexX;
    public int Y => _indexY;

    public void Take()
    {
        _isAvailable = false;
    }

    public void Free()
    {
        _isAvailable = true;
    }
}
