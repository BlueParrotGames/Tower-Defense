using UnityEngine;

public static class ParticleSystemExtension
{
    public static void EnableEmission(this ParticleSystem particleSystem, bool enabled)
    {
        ParticleSystem.EmissionModule emission = particleSystem.emission;
        emission.enabled = enabled;
    }

    public static float GetEmissionTimeRate(this ParticleSystem particleSystem)
    {
        return particleSystem.emission.rateOverTime.constantMax;
    }

    public static void SetEmissionTimeRate(this ParticleSystem particleSystem, float emissionRate)
    {
        ParticleSystem.EmissionModule emission = particleSystem.emission;
        ParticleSystem.MinMaxCurve rate = emission.rateOverTime;
        rate.constantMax = emissionRate;
        emission.rateOverTime = rate;
    }

    public static float GetEmissionDistRate(this ParticleSystem particleSystem)
    {
        return particleSystem.emission.rateOverDistance.constantMax;
    }

    public static void SetEmissionDistRate(this ParticleSystem particleSystem, float emissionRate)
    {
        ParticleSystem.EmissionModule emission = particleSystem.emission;
        ParticleSystem.MinMaxCurve rate = emission.rateOverDistance;
        rate.constantMax = emissionRate;
        emission.rateOverDistance = rate;
    }
}