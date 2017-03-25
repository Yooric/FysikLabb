using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketScript : MonoBehaviour
{
    public float fuelPower = 1.0f;
    public float hullMass = 1.0f;
    public float fuelMass = 1.0f;
    float velocity = 94.0f;
    float alltitude = 0.0f;
    public Text exhaustUi;
    public Text hullMassUi;
    public Text fuelMasstUi;
    public Text velocityUi;
    public Text alltitudeUi;

    //rocket stuff
    private float m_currentVelocity;
    public float m_startVelocity = 0f;
    public float m_exhaustVelocity = 2050;

    public float m_fuelMass = 392400f;
    public float m_hullMass = 20000f;
    public float m_engineMass = 8371f;
    private float m_currentMass;
    private float m_startMass;

    private float m_distanceFromEarthSurface;
    private float m_earthRadius = 6356766.0f;
    public float m_propellantBurnRate = 2616f; //kg/s (F-1 Engine stats from book)
    // Use this for initialization
    void Start()
    {
        exhaustUi.text = "Exhaust velocity: " + fuelPower.ToString();//"Velocity: " + velocity.ToString() + " m/s";
        hullMassUi.text = "Rocket mass: " + hullMass.ToString() + " kg";
        velocityUi.text = "Velocity: " + velocity.ToString() + " m/s";
        fuelMasstUi.text = "Fuel mass: " + fuelMass.ToString() + " kg";
        alltitudeUi.text = "Alltitude: " + alltitude.ToString() + " m";

        m_startMass = m_fuelMass + m_hullMass + m_engineMass;

    }
    // Update is called once per frame
    void Update()
    {

        if (UpdateRocketMass())
        {
            HandleThrust();
        }
        HandleGravity();
        UpdateRocketPosition();
        alltitudeUi.text = "Alltitude: " + gameObject.transform.position.y + " m";
    }
    void SetMasses(float p_fuelMass, float p_hullMass, float p_engineMass)
    {
        m_fuelMass = p_fuelMass;
        m_hullMass = p_hullMass;
        m_engineMass = p_engineMass;
        m_startMass = m_fuelMass + m_hullMass + m_engineMass;
    }
    
    bool UpdateRocketMass()
    {
        // if rocketmass is larger than hullmass we still have fuel to burn
        if (m_currentMass > m_hullMass)
        {
            m_currentMass = m_currentMass - m_propellantBurnRate * Time.deltaTime;
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
        transform.position = new Vector3(0, transform.position.y + m_currentVelocity, 0);
        // might seem a little odd to update this variable but it makes the code easier to read in HandleGravity()
        m_distanceFromEarthSurface = transform.position.y;
    }
    void HandleGravity()
    {
        // a * t = v.  a = 9.81*(re^2/(re+h)^2)
        m_currentVelocity -= (9.81f * (Mathf.Pow(m_earthRadius, 2) / (Mathf.Pow(m_earthRadius + m_distanceFromEarthSurface, 2))))* Time.deltaTime;
    }
    void HandleThrust()
    {
        // v(t) = v0 + ve * ln(m0/m(t))
        m_currentVelocity += m_startVelocity + m_exhaustVelocity * Mathf.Log(m_startMass / m_currentMass);
    }
}
