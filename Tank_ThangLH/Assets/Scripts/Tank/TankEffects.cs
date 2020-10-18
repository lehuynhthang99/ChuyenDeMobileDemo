using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEffects : BaseMonoBehaviour
{
    public List<EffectBase> effects = new List<EffectBase>();
    [SerializeField] protected TankInfo tankInfo;
    
    protected void OnEnable()
    {
        effects.Clear();
    }

    // Update is called once per frame
    public override void UpdateNormal()
    {
        int i = 0;
        while (i < effects.Count)
        {
            effects[i].DoUpdate(tankInfo, Time.deltaTime);
            if (effects[i].duration <= 0f)
            {
                effects[i].DoOnDisable(tankInfo);
                effects.RemoveAt(i);
            }
            else i++;
        }
    }

    public void AddEffect(EffectBase _inEffect)
    {
        effects.Add(_inEffect);
    }

    public void RemoveEffect(EffectBase _inEffect)
    {
        effects.Remove(_inEffect);
    }
}
