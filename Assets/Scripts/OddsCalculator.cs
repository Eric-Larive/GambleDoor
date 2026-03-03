using UnityEngine;

public class OddsCalculator : MonoBehaviour
{
    public static OddsCalculator Instance;
    
    [Header("Base Weights (Floor 0)")]
    public float baseBoonWeight = 30f;
    public float baseMalusWeight = 20f;
    public float baseNothingWeight = 50f;
    
    [Header("Boon Types")]
    public WeightedBoon[] boonTypes;
    [Header("Malus Types")]
    public WeightedMalus[] malusTypes;
    
    private void Awake() => Instance = this;
    
    public DoorOutcome GetWeightedOutcome()
    {
        // As floor increases: boons get rarer, malus gets more common
        float boon = Mathf.Max(5f,  baseBoonWeight -  GameManager.Instance.currentFloor * 2f);
        float malus = baseMalusWeight + GameManager.Instance.currentFloor * 3f;
        float nothing = baseNothingWeight;

        float total = boon + malus + nothing;
        float roll = Random.Range(0f, total);

        if (roll < boon) return DoorOutcome.Boon;
        if (roll < boon + malus) return DoorOutcome.Malus;
        return DoorOutcome.Nothing;
    }
    
    // --- Sub-types ---
    public BoonType GetRandomBoon()
    {
        float total = 0f;
        foreach (var b in boonTypes) total += b.weight;

        float roll = Random.Range(0f, total);
        float cumulative = 0f;
        foreach (var b in boonTypes)
        {
            cumulative += b.weight;
            if (roll < cumulative) return b.boon;
        }
        return boonTypes[0].boon;
    }

    public MalusType GetRandomMalus()
    {
        float total = 0f;
        foreach (var m in malusTypes) total += m.weight;

        float roll = Random.Range(0f, total);
        float cumulative = 0f;
        foreach (var m in malusTypes)
        {
            cumulative += m.weight;
            if (roll < cumulative) return m.malus;
        }
        return malusTypes[0].malus;
    }
}
