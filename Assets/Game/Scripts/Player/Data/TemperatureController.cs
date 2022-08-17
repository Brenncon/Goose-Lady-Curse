using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureController : MonoBehaviour
{
    public Stat coldProtection;
    public SurvivalStat temperatureStat;
    public float changeRate = 1;
    [Header("Forest")]
    public float forestDayTemperature;
    private float forestNightTemperature;
    public float forestRainyDayTemperature;
    private float forestRainyNightTemperature;
    [Header("Beach")]
    public float beachDayTemperature;
    private float beachNightTemperature;
    
    private float mountainDayTemperature;
    private float mountainNightTemperature;
    [Header("Mountain")]
    public float mountainSnowyDayTemperature;
    private float mountainSnowyNightTemperature;

    private float targetTemperture;
    private float envirenmentTemperature;

    public void Initialize()
    {
        ApplyForestDay();//need to vary depending on the respawn locations
    }

    private void Update()
    {
        targetTemperture = envirenmentTemperature + coldProtection.finalValue;
        if (temperatureStat.currentValue != targetTemperture)
        {
            temperatureStat.currentValue = Mathf.Lerp(temperatureStat.currentValue,targetTemperture,changeRate*Time.deltaTime);
        }
    }

    public void ApplyForestDay()
    {
        SetEnvironmentTemperature(forestDayTemperature);
    }

    private void ApplyForestNight()
    {
        SetEnvironmentTemperature(forestNightTemperature);
    }

    public void ApplyForestRainyDay()
    {
        SetEnvironmentTemperature(forestDayTemperature);
    }

    private void ApplyForestRainyNight()
    {
        SetEnvironmentTemperature(forestRainyNightTemperature);
    }

    public void ApplyBeachDay()
    {
        SetEnvironmentTemperature(beachDayTemperature);
    }

    private void ApplyBeachNight()
    {
        SetEnvironmentTemperature(beachNightTemperature);
    }

    private void ApplyMountainDay()
    {
        SetEnvironmentTemperature(mountainDayTemperature);
    }

    private void ApplyMountainNight()
    {
        SetEnvironmentTemperature(mountainNightTemperature);
    }

    public void ApplyMountainSnowyDay()
    {
        SetEnvironmentTemperature(mountainSnowyDayTemperature);
    }

    private void ApplyMountainSnowyNight()
    {
        SetEnvironmentTemperature(mountainSnowyNightTemperature);
    }

    private void SetEnvironmentTemperature(float temperature)
    {
        envirenmentTemperature = temperature;
    }
}
