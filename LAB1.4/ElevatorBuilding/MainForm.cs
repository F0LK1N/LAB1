namespace ElevatorBuilding;

public class MainForm : Form
{
    private readonly Building _building = Building.Instance;
    private readonly Elevator _elevator = Elevator.Instance;

    private readonly TreeView _tree = new TreeView();
    private readonly NumericUpDown _floorInput = new NumericUpDown();
    private readonly Button _callButton = new Button();
    private readonly Label _statusLabel = new Label();
    private readonly Label _singletonLabel = new Label();
    private readonly System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

    private TreeNode _elevatorNode = null!;

    public MainForm()
    {
        Text = "Лифт в здании";
        ClientSize = new Size(660, 420);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        _tree.Dock = DockStyle.Fill;
        _tree.HideSelection = false;
        _tree.Font = new Font("Segoe UI", 10F);
        BuildTree();
        layout.Controls.Add(_tree, 0, 0);

        var rightPanel = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(10)
        };
        BuildRightPanel(rightPanel);
        layout.Controls.Add(rightPanel, 1, 0);

        Controls.Add(layout);

        _timer.Interval = 500;
        _timer.Tick += Timer_Tick;
        _timer.Start();

        UpdateStatus();
    }

    private void BuildTree()
    {
        var root = _tree.Nodes.Add("Здание");
        _elevatorNode = root.Nodes.Add("Лифт");

        foreach (var floor in _building.Floors)
        {
            var floorNode = root.Nodes.Add($"Этаж {floor.Number}");

            foreach (var room in floor.Rooms)
                floorNode.Nodes.Add($"{room.Name} — {room.Area} м²");
        }

        root.ExpandAll();
    }

    private void BuildRightPanel(Panel panel)
    {
        var controlBox = new GroupBox
        {
            Text = "Управление лифтом",
            Left = 10,
            Top = 10,
            Width = 320,
            Height = 100
        };

        var floorLabel = new Label
        {
            Text = "Этаж:",
            Left = 15,
            Top = 35,
            AutoSize = true
        };

        _floorInput.Left = 65;
        _floorInput.Top = 32;
        _floorInput.Width = 60;
        _floorInput.Minimum = 1;
        _floorInput.Maximum = _building.Floors.Count;
        _floorInput.Value = 1;

        _callButton.Text = "Вызвать лифт";
        _callButton.Left = 145;
        _callButton.Top = 30;
        _callButton.Width = 155;
        _callButton.Height = 30;
        _callButton.Click += CallButton_Click;

        controlBox.Controls.Add(floorLabel);
        controlBox.Controls.Add(_floorInput);
        controlBox.Controls.Add(_callButton);

        var statusBox = new GroupBox
        {
            Text = "Состояние",
            Left = 10,
            Top = 120,
            Width = 320,
            Height = 110
        };

        _statusLabel.Left = 15;
        _statusLabel.Top = 25;
        _statusLabel.AutoSize = true;
        _statusLabel.Font = new Font("Segoe UI", 10F);
        statusBox.Controls.Add(_statusLabel);

        var singletonBox = new GroupBox
        {
            Text = "Singleton",
            Left = 10,
            Top = 240,
            Width = 320,
            Height = 120
        };

        _singletonLabel.Left = 15;
        _singletonLabel.Top = 25;
        _singletonLabel.AutoSize = true;
        singletonBox.Controls.Add(_singletonLabel);

        panel.Controls.Add(controlBox);
        panel.Controls.Add(statusBox);
        panel.Controls.Add(singletonBox);
    }

    private void CallButton_Click(object? sender, EventArgs e)
    {
        _elevator.Call((int)_floorInput.Value);
        UpdateStatus();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        if (!_elevator.IsMoving)
            return;

        _elevator.MoveOneFloor();
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        _statusLabel.Text =
            $"Текущий этаж: {_elevator.CurrentFloor}\r\n" +
            $"Цель: {_elevator.TargetFloor}\r\n" +
            $"Состояние: {(_elevator.IsMoving ? "движется" : "стоит")}";

        _elevatorNode.Text = $"Лифт (этаж {_elevator.CurrentFloor})";

        bool buildingLift = ReferenceEquals(_building.Lift, Elevator.Instance);
        bool floorLift = ReferenceEquals(_building.Floors[0].Lift, Elevator.Instance);

        _singletonLabel.Text =
            $"Лифт здания = Elevator.Instance: {buildingLift}\r\n" +
            $"Лифт этажа 1 = Elevator.Instance: {floorLift}\r\n" +
            $"Конструкторы классов закрыты (private).";
    }
}
