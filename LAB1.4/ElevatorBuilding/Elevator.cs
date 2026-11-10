namespace ElevatorBuilding;

public sealed class Elevator
{
    private static readonly Elevator _instance = new Elevator();

    private Elevator()
    {
        CurrentFloor = 1;
        TargetFloor = 1;
    }

    public static Elevator Instance => _instance;

    public int CurrentFloor { get; private set; }

    public int TargetFloor { get; private set; }

    public bool IsMoving => CurrentFloor != TargetFloor;

    public void Call(int floor)
    {
        if (floor >= 1)
            TargetFloor = floor;
    }

    public void MoveOneFloor()
    {
        if (CurrentFloor < TargetFloor)
            CurrentFloor++;
        else if (CurrentFloor > TargetFloor)
            CurrentFloor--;
    }
}
