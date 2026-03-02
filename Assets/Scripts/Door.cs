using UnityEngine;

public enum DoorOutcome { Boon, Malus, Nothing }

public class Door : MonoBehaviour
{
    public DoorOutcome outcome;
    private bool _revealed;

    public void OnClick()
    {
        if (_revealed) return;
        _revealed = true;
        GameManager.RevealDoor(this);
    }
}
