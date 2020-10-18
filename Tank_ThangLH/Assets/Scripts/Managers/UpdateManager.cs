using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    private int behaviourCount = 0;
    public int maxBehaviors = 300;

    private BaseMonoBehaviour[] behaviours;

    private static UpdateManager instance;

    public UpdateManager()
    {
        if (instance == null)
        {
            instance = this;
            this.behaviours = new BaseMonoBehaviour[maxBehaviors];
        }
    }

    #region Add & Remove

    public static void AddBehaviour(BaseMonoBehaviour behaviour)
    {
        if (instance.behaviours == null)
        {
            instance.behaviours = new BaseMonoBehaviour[instance.maxBehaviors];
        }
        if (instance.behaviourCount >= instance.maxBehaviors)
        {
            instance.maxBehaviors += 10;
            System.Array.Resize(ref instance.behaviours, instance.maxBehaviors);
        }
        instance.behaviours[instance.behaviourCount] = behaviour;
        instance.behaviourCount += 1;
    }


    
    public static void RemoveBehaviour(BaseMonoBehaviour behaviour)
    {
        int addAtIndex = 0;
        BaseMonoBehaviour[] tempArray = new BaseMonoBehaviour[instance.maxBehaviors];
        for (int i = 0; i < instance.behaviours.Length; i++)
        {
            if (instance.behaviours[i] == null)
            {
                continue;
            }
            else if (instance.behaviours[i] == behaviour)
            {
                instance.behaviourCount -= 1;
                continue;
            }
            tempArray[addAtIndex] = instance.behaviours[i];
            addAtIndex++;
        }

        instance.behaviours = new BaseMonoBehaviour[instance.maxBehaviors];

        for (int i = 0; i < tempArray.Length; i++)
        {
            instance.behaviours[i] = tempArray[i];
        }
    }

    #endregion

    #region Update

    private void Update()
    {
        if (behaviourCount > 0)
        {
            for (int i = 0; i < behaviourCount; i++)
            {
                if (behaviours[i] == null || !behaviours[i].isActiveAndEnabled)
                    continue;
                behaviours[i].UpdateNormal();
            }
        }
    }

    private void FixedUpdate()
    {
        if (behaviourCount > 0)
        {
            for (int i = 0; i < behaviourCount; i++)
            {
                if (behaviours[i] == null || !behaviours[i].isActiveAndEnabled)
                    continue;
                behaviours[i].UpdateFixed();
            }
        }
    }

    private void LateUpdate()
    {
        if (behaviourCount > 0)
        {
            for (int i = 0; i < behaviourCount; i++)
            {
                if (behaviours[i] == null || !behaviours[i].isActiveAndEnabled)
                    continue;
                behaviours[i].UpdateLate();
            }
        }
    }

    #endregion
}
