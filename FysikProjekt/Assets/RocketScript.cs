using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketScript : MonoBehaviour
{
    public Text exhaustUi;
    public Text hullMassUi;
    public Text fuelMasstUi;
    public Text velocityUi;
    public Text alltitudeUi;

    //rocket stuff
    private float m_currentVelocity;
    public float m_startVelocity = 0f;
    public float m_exhaustVelocity = 2050;

    //public float m_fuelMass = 392400f;
    //public float m_hullMass = 20000f;
    //public float m_engineMass = 8371f;
    public float m_fuelMass = 400f;
    public float m_hullMass = 400f;
    public float m_engineMass = 0f;
    private float m_currentMass;
    private float m_startMass;

    private float m_distanceFromEarthSurface = 0f;
    private float m_earthRadius = 6356766.0f;
    //public float m_propellantBurnRate = 2616f; //kg/s (F-1 Engine stats from book)
    public float m_propellantBurnRate = 0.5f;
    private float m_worldScale= 0.01f;
    public float m_velGainScale = 100;
    // Use this for initialization
    void Start()
    {
        exhaustUi.text = "Exhaust velocity: " + m_exhaustVelocity.ToString();//"Velocity: " + velocity.ToString() + " m/s";
        hullMassUi.text = "Rocket mass: " + m_currentMass.ToString() + " kg";
        velocityUi.text = "Velocity: " + m_currentVelocity.ToString() + " m/s";
        fuelMasstUi.text = "Fuel mass: " + m_fuelMass.ToString() + " kg";
        alltitudeUi.text = "Alltitude: " + m_distanceFromEarthSurface.ToString() + " m";

        m_startMass = m_fuelMass + m_hullMass + m_engineMass;
        m_currentMass = m_startMass;

    }
    // Update is called once per frame
    void Update()
    {

        if (UpdateRocketMass())
        {
            HandleVelocity();
        }
        HandleGravity();
        UpdateRocketPosition();

        hullMassUi.text = "Rocket mass: " + m_currentMass.ToString() + " kg";
        velocityUi.text = "Velocity: " + m_currentVelocity.ToString() + " m/s";
        fuelMasstUi.text = "Fuel mass: " + m_fuelMass.ToString() + " kg";
        alltitudeUi.text = "Alltitude: " + m_distanceFromEarthSurface.ToString() + " m";
    }
    
    bool UpdateRocketMass()
    {
        // if rocketmass is larger than hullmass we still have fuel to burn
        if (m_currentMass > m_hullMass)
        {
            m_currentMass = m_currentMass - m_propellantBurnRate * Time.deltaTime * m_velGainScale;
            return true;
        }
        else
        {
            //we are no longer gaining upwardvelocity
            return false;
        }
    }

    void UpdateRocketPosition()
    {
        transform.position = new Vector3(0, transform.position.y + (m_currentVelocity * m_worldScale), 0);
        // might seem a little odd to update this variable but it makes the code easier to read in HandleGravity()
        m_distanceFromEarthSurface = transform.position.y* (1/ m_worldScale);
    }
    void HandleGravity()
    {
        // a * t = v.  a = 9.81*(re^2/(re+h)^2)
       //if (m_currentMass > m_hullMass)
       //{
        m_currentVelocity -= (9.81f * (Mathf.Pow(m_earthRadius, 2) / (Mathf.Pow(m_earthRadius + m_distanceFromEarthSurface, 2)))) * Time.deltaTime * m_velGainScale;
        //}
        //else
        //{
        //    m_currentVelocity -= (9.81f * (Mathf.Pow(m_earthRadius, 2) / (Mathf.Pow(m_earthRadius + m_distanceFromEarthSurface, 2)))) * Time.deltaTime * m_velGainScale;
        //}
    }
    void HandleVelocity()
    {
        // v(t) = v0 + ve * ln(m0/m(t))
        m_currentVelocity = m_startVelocity + m_exhaustVelocity * Mathf.Log(m_startMass / m_currentMass);
    }
}
