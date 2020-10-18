using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonZone : ZoneBase
{
    [SerializeField] protected float duration;
    [SerializeField] protected float delayPoison;
    [SerializeField] protected float damagePerDelayPoison;

    protected float countDownDuration;

    // Start is called before the first frame update
    protected override void OnEnable()
    {
        countDownDuration = duration;
    }

    public override void UpdateNormal()
    {
        countDownDuration -= Time.deltaTime;
        if (countDownDuration <= 0f)
            gameObject.SetActive(false);
    }

    public override void UpdateFixed()
    {
        curr_TankInfos.Clear();

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);

        int i = 0;
        for (i = 0; i < colliders.Length; i++)
        {
            // ... and find their rigidbody.
            TankInfo target = colliders[i].GetComponent<TankInfo>();

            // If they don't have a rigidbody, go on to the next collider.
            if (!target)
                continue;
            curr_TankInfos.Add(target);
        }

        i = 0;
        while (i<prev_TankInfos.Count)
        {
            if (curr_TankInfos.IndexOf(prev_TankInfos[i]) == -1)
            {
                prev_TankInfos[i].tankEffects.RemoveEffect(new Virtual_Effect(idEffects[i]));
                prev_TankInfos.RemoveAt(i);
                idEffects.RemoveAt(i);
            }
            else i++;
        }


        i = 0;
        while (i < curr_TankInfos.Count)
        {
            if (prev_TankInfos.IndexOf(curr_TankInfos[i]) == -1)
            {
                prev_TankInfos.Add(curr_TankInfos[i]);
                Effect_PoisonZone effect_PoisonZone = new Effect_PoisonZone(delayPoison, damagePerDelayPoison);
                idEffects.Add(effect_PoisonZone.id);
                curr_TankInfos[i].tankEffects.AddEffect(effect_PoisonZone);
            }
            else i++;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        while (0 < prev_TankInfos.Count)
        {
            prev_TankInfos[0].tankEffects.RemoveEffect(new Virtual_Effect(idEffects[0]));
            prev_TankInfos.RemoveAt(0);
            idEffects.RemoveAt(0);
        }
    }
}
