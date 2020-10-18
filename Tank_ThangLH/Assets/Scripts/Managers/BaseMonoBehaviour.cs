using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonoBehaviour : MonoBehaviour
{
    public virtual void Awake()
    {
        UpdateManager.AddBehaviour(this);
    }

    public virtual void OnDestroy()
    {
        UpdateManager.RemoveBehaviour(this);
    }

    public virtual void UpdateNormal() { }

    public virtual void UpdateFixed() { }

    public virtual void UpdateLate() { }
}
