using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Type2_Invisible : SkillBase
{
    [SerializeField] protected float duration;
    private float countDownDuration;

    [SerializeField] protected GameObject objectMesh;
    [SerializeField] protected GameObject canvasTank;
    public override void Awake()
    {
        base.Awake();
        countDownDuration = duration;
        countDownDelayNextSkill = delayNextSkill;
        objectMesh.SetActive(true);
        canvasTank.SetActive(true);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        countDownDuration = duration;
        countDownDelayNextSkill = delayNextSkill;
        objectMesh.SetActive(true);
        canvasTank.SetActive(true);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        objectMesh.SetActive(true);
        canvasTank.SetActive(true);
    }

    protected override void ActivateSkill()
    {
        base.ActivateSkill();
        objectMesh.SetActive(false);
        canvasTank.SetActive(false);
    }

    protected override void DoSkill()
    {
        countDownDuration -= Time.deltaTime;
        if (countDownDuration <= 0f)
        {
            countDownDuration = duration;
            //change when have u want to have a condition to active skill
            objectMesh.SetActive(true);
            canvasTank.SetActive(true);
            isActive = false;
            countDownDelayNextSkill = delayNextSkill;
        }
    }
}
