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
    // Use this for initialization
    void Start()
    {
        exhaustUi.text = "Exhaust velocity: " + fuelPower.ToString();//"Velocity: " + velocity.ToString() + " m/s";
        hullMassUi.text = "Rocket mass: " + hullMass.ToString() + " kg";
        velocityUi.text = "Velocity: " + velocity.ToString() + " m/s";
        fuelMasstUi.text = "Fuel mass: " + fuelMass.ToString() + " kg";
        alltitudeUi.text = "Alltitude: " + alltitude.ToString() + " m";
        //print(textUi.text);
        //textUi.text = "Exhaust velocity: " + fuelPower.ToString() + "\n" + "Rocket mass: " + hullWeight.ToString() + " kg" + "\n" + "Velocity: " + velocity.ToString() + " m/s";
    }

    // Update is called once per frame
    void Update()
    {

        alltitudeUi.text = "Alltitude: " + gameObject.transform.position.y + " m";
    }
}
