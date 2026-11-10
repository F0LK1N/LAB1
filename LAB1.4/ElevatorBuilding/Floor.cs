namespace ElevatorBuilding;

public class Floor
{
    public Floor(int number, Elevator lift, params Room[] rooms)
    {
        Number = number;
        Lift = lift;
        Rooms = rooms.ToList();
    }

    public int Number { get; }

    public List<Room> Rooms { get; }

    // На каждом этаже хранится ссылка на один и тот же лифт.
    public Elevator Lift { get; }
}
