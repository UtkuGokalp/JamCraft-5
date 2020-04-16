using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPositionReferenceScript : MonoBehaviour
{
    private Transform trans => transform;
    public Transform Trans
    {
        get { return trans; }
    }
}
