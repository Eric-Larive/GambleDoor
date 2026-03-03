
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public int currentFloor;
    
    public static GameManager Instance;
    public TextMeshProUGUI FloorText;
    
    private void Awake() => Instance = this;
    private void Start() => ShuffleDoors();

    private void ShuffleDoors()
    {
        Door[] doors = FindObjectsByType<Door>(FindObjectsSortMode.None);
        foreach (var door in doors)
        {
            door.outcome = OddsCalculator.Instance.GetWeightedOutcome();
        }
    }

    private void AdvanceFloor()
    {
        currentFloor++;
        ShuffleDoors();
        FloorText.text = $"Floor {currentFloor}";
    }

    public void RevealDoor(Door door)
    {
        switch (door.outcome)
        {
            case DoorOutcome.Boon:
                BoonType boon = OddsCalculator.Instance.GetRandomBoon();
                Debug.Log($"Boon: {boon}");
                break;
            case DoorOutcome.Malus:
                MalusType malus = OddsCalculator.Instance.GetRandomMalus();
                Debug.Log($"Malus: {malus}");
                break;
            case DoorOutcome.Nothing:
                Debug.Log("Nothing...");
                break;
        }
        AdvanceFloor();
    }
}