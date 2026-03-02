using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ShuffleDoors();
    }

    private void ShuffleDoors()
    {
        Door[] doors = FindObjectsByType<Door>(FindObjectsSortMode.None);
        foreach (var door in doors)
        {
            door.outcome = GetWeightedOutcome();
        }
    }
    
    private static DoorOutcome GetWeightedOutcome()
    {
        float boonWeight = 30f;
        float malusWeight = 20f;
        float nothingWeight = 50f;

        float total = boonWeight + malusWeight + nothingWeight;
        float roll = Random.Range(0f, total);

        if (roll < boonWeight) return DoorOutcome.Boon;
        if (roll < boonWeight + malusWeight) return DoorOutcome.Malus;
        return DoorOutcome.Nothing;
    }

    public static void RevealDoor(Door door)
    {
        switch (door.outcome)
        {
            case DoorOutcome.Boon:
                Debug.Log("You got a Boon!");
                break;
            case DoorOutcome.Malus:
                Debug.Log("You got a Malus!");
                break;
            case DoorOutcome.Nothing:
                Debug.Log("Nothing...");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}