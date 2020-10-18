using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : BaseMonoBehaviour
{
    [SerializeField] protected TankInfo tankInfo;
    protected bool canActive = true;
    protected bool isActive = false;
    protected bool isButton;

    [SerializeField] protected float delayNextSkill;
    protected float countDownDelayNextSkill;

    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
    }

    protected virtual void OnEnable()
    {
        canActive = true;
        isActive = false;
    }

    protected virtual void OnDisable()
    {

    }

    public override void UpdateNormal()
    {
        if (isButton && canActive)
            ActivateSkill();
        if (isActive)
            DoSkill();
        else if (!canActive)
            DelayNextSkill();
    }

    public void GetInput(bool _isButton)
    {
        isButton = _isButton;
    }

    protected virtual void ActivateSkill()
    {
        canActive = false;
        isActive = true;
    }

    protected virtual void DelayNextSkill()
    {
        if (countDownDelayNextSkill >= 0f)
            countDownDelayNextSkill -= Time.deltaTime;
        else canActive = true;
    }

    protected virtual void DoSkill()
    {

    }
}
