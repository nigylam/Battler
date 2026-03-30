using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private SquadCreator _creator;
    [SerializeField] private Army _army;
}
