using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Target;

    [Tooltip("Time it takes to reach the target object")]
    [SerializeField]
    private float m_FollowDelay = 1f;
    private Vector3 m_VectorSpeed = Vector3.zero;

    [SerializeField]
    private Vector3 m_Offset;

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(m_Target.transform.position.x, m_Target.transform.position.y, -1) + m_Offset, ref m_VectorSpeed, m_FollowDelay);
    }

}
