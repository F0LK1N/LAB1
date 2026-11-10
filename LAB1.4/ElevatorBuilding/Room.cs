namespace ElevatorBuilding;

public class Room
{
    public Room(string name, int area)
    {
        Name = name;
        Area = area;
    }

    public string Name { get; }

    public int Area { get; }
}
