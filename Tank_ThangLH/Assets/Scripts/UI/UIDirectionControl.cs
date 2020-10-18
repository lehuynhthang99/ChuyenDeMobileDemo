using UnityEngine;

public class UIDirectionControl : MonoBehaviour
{
    public bool m_UseRelativeRotation = true;

    public Vector3 rotation = new Vector3(0,60,90);
    private Quaternion m_RelativeRotation;     


    private void Start()
    {
        m_RelativeRotation = Quaternion.Euler(rotation);
        //Debug.Log(m_RelativeRotation.eulerAngles);
    }


    private void Update()
    {
        if (m_UseRelativeRotation)
            transform.rotation = m_RelativeRotation;
    }
}
