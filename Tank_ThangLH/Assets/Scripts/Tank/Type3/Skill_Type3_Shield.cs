using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Type3_Shield : SkillBase
{
    [SerializeField] protected float duration;
    private float countDownDuration;
    [SerializeField] protected GameObject shield;

    public override void Awake()
    {
        base.Awake();
        countDownDuration = duration;
        countDownDelayNextSkill = delayNextSkill;
        shield.SetActive(false);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        countDownDuration = duration;
        shield.SetActive(false);
        countDownDelayNextSkill = delayNextSkill;
    }

    protected override void ActivateSkill()
    {
        base.ActivateSkill();
        shield.SetActive(true);
        tankInfo.tankHealth.m_Shield += 30000f;
    }
    protected override void DoSkill()
    {
        countDownDuration -= Time.deltaTime;
        if (countDownDuration <= 0f)
        {
            countDownDuration = duration;
            //change when have u want to have a condition to active skill
            shield.SetActive(false);
            tankInfo.tankHealth.m_Shield -= 30000f;
            isActive = false;
            countDownDelayNextSkill = delayNextSkill;
        }
    }
}
