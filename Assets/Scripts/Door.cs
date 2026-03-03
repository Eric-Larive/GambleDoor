using UnityEngine;

public class Door : MonoBehaviour
{
    public DoorOutcome outcome;

    public void OnClick()
    {
        GameManager.Instance.RevealDoor(this);
    }
}
