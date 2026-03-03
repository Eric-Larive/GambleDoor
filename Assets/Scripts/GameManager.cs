
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private int _currentFloor = 0;
    
    public static GameManager Instance;
    public TextMeshProUGUI FloorText;
    
    [Header("Base Weights (Floor 0)")]
    public float baseBoonWeight = 30f;
    public float baseMalusWeight = 20f;
    public float baseNothingWeight = 50f;
    [Header("Boon Types")]
    public WeightedBoon[] boonTypes;
    [Header("Malus Types")]
    public WeightedMalus[] malusTypes;

    private void Awake() => Instance = this;
    private void Start() => ShuffleDoors();

    private void ShuffleDoors()
    {
        Door[] doors = FindObjectsByType<Door>(FindObjectsSortMode.None);
        foreach (var door in doors)
        {
            door.outcome = GetWeightedOutcome();
        }
    }
    
    private DoorOutcome GetWeightedOutcome()
    {
        // As floor increases: boons get rarer, malus gets more common
        float boon = Mathf.Max(5f,  baseBoonWeight - _currentFloor * 2f);
        float malus = baseMalusWeight + _currentFloor * 3f;
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

    private void AdvanceFloor()
    {
        _currentFloor++;
        ShuffleDoors();
        FloorText.text = $"Floor {_currentFloor}";
    }

    public void RevealDoor(Door door)
    {
        switch (door.outcome)
        {
            case DoorOutcome.Boon:
                BoonType boon = Instance.GetRandomBoon();
                Debug.Log($"Boon: {boon}");
                break;
            case DoorOutcome.Malus:
                MalusType malus = Instance.GetRandomMalus();
                Debug.Log($"Malus: {malus}");
                break;
            case DoorOutcome.Nothing:
                Debug.Log("Nothing...");
                break;
        }
        AdvanceFloor();
    }
}