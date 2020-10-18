using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneBase : BaseMonoBehaviour
{
    [SerializeField] protected float radius = 3f;
    [SerializeField] protected LayerMask layerMask;
    protected List<TankInfo> prev_TankInfos = new List<TankInfo>();
    protected List<TankInfo> curr_TankInfos = new List<TankInfo>();
    protected List<ushort> idEffects = new List<ushort>();

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    // Update is called once per frame
}
