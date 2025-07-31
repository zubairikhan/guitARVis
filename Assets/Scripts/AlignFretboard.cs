using UnityEngine;

public class AlignFretboard : MonoBehaviour
{

    ApplicationManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<ApplicationManager>();
    }

    public void MoveFretBoardStationUp()
    {
        manager.MoveFretBoardStation(MoveDirection.Up);
    }

    public void MoveFretBoardStationDown()
    {
        manager.MoveFretBoardStation(MoveDirection.Down);
    }

    public void MoveFretBoardStationLeft()
    {
        manager.MoveFretBoardStation(MoveDirection.Left);
    }

    public void MoveFretBoardStationRight()
    {
        manager.MoveFretBoardStation(MoveDirection.Right);
    }

    public void MoveFretBoardStationForwards()
    {
        manager.MoveFretBoardStation(MoveDirection.Forwards);
    }

    public void MoveFretBoardStationBackwards()
    {
        manager.MoveFretBoardStation(MoveDirection.Backwards);
    }
}
