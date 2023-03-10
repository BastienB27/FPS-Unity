using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobber : MonoBehaviour
{
    [SerializeField]
    private StarterAssets.StarterAssetsInputs m_Inputs;

    [SerializeField]
    private CinemachineCameraOffset m_CameraOffset;

    [SerializeField]
    private float m_DefaultBobSpeed;
    [SerializeField]
    private float m_SprintBobSpeed;

    [SerializeField]
    private float m_DefaultBobAmplitude;
    [SerializeField]
    private float m_SprintBobAmplitude;

    [SerializeField]
    private Vector2 m_DirectionnalAmpltitude = new Vector2(0.2f, 1f);
    [SerializeField]
    private Vector2 m_DirectionnalSpeedMult = new Vector2(0.3f, 1f);

    [SerializeField, Range(0f, 1f)]
    private float m_NoBobbingSmoothing = 0.9f;
    
    private float _timer = 0;

    // Update is called once per frame
    void Update()
    {
        if (m_Inputs.move.magnitude >= 0.1f)
        {
            _timer += Time.deltaTime * (m_Inputs.sprint ? m_SprintBobSpeed : m_DefaultBobSpeed);

            float amplitude = m_Inputs.sprint ? m_SprintBobAmplitude : m_DefaultBobAmplitude;
            m_CameraOffset.m_Offset = new Vector3(Mathf.Sin(_timer * m_DirectionnalSpeedMult.x) * amplitude * m_DirectionnalAmpltitude.x,
                                                  Mathf.Sin(_timer * m_DirectionnalSpeedMult.y) * amplitude * m_DirectionnalAmpltitude.y,
                                                  0f);
        } else
        {
            _timer = 0;
            m_CameraOffset.m_Offset = Vector3.Lerp(m_CameraOffset.m_Offset, Vector3.zero, m_NoBobbingSmoothing);
        }
    }
}
