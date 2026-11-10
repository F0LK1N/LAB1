namespace ElevatorBuilding;

public sealed class Building
{
    private static readonly Building _instance = new Building();

    private Building()
    {
        Floors = new List<Floor>
        {
            new Floor(1, Elevator.Instance,
                new Room("Холл", 50),
                new Room("Охрана", 15),
                new Room("Гардероб", 20)),

            new Floor(2, Elevator.Instance,
                new Room("Офис №1", 40),
                new Room("Офис №2", 35),
                new Room("Кухня", 15)),

            new Floor(3, Elevator.Instance,
                new Room("Бухгалтерия", 45),
                new Room("Кабинет директора", 25)),

            new Floor(4, Elevator.Instance,
                new Room("Серверная", 30),
                new Room("Архив", 20)),

            new Floor(5, Elevator.Instance,
                new Room("Переговорная", 50),
                new Room("Комната отдыха", 30))
        };

        Lift = Elevator.Instance;
    }

    public static Building Instance => _instance;

    public List<Floor> Floors { get; }

    public Elevator Lift { get; }
}
