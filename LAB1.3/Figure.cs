using System.Collections.Generic;
using System.Drawing;

namespace FigurePatterns
{
    // Базовая фигура. Метод Clone реализует паттерн Prototype.
    public abstract class Figure
    {
        protected Figure(string name, List<Point> cells)
        {
            Name = name;
            Cells = new List<Point>(cells);
        }

        // Конструктор копирования.
        protected Figure(Figure figure)
        {
            Name = figure.Name;
            Cells = new List<Point>(figure.Cells);
        }

        public string Name { get; private set; }

        public List<Point> Cells { get; private set; }

        public int CellCount => Cells.Count;

        public abstract Figure Clone();
    }

    // Обычные фигуры (4 клетки).
    public class LineFigure : Figure
    {
        public LineFigure() : base("Линия", new List<Point>
        {
            new Point(0, 0),
            new Point(1, 0),
            new Point(2, 0),
            new Point(3, 0)
        })
        {
        }

        private LineFigure(LineFigure figure) : base(figure)
        {
        }

        public override Figure Clone()
        {
            return new LineFigure(this);
        }
    }

    public class SquareFigure : Figure
    {
        public SquareFigure() : base("Квадрат", new List<Point>
        {
            new Point(0, 0),
            new Point(1, 0),
            new Point(0, 1),
            new Point(1, 1)
        })
        {
        }

        private SquareFigure(SquareFigure figure) : base(figure)
        {
        }

        public override Figure Clone()
        {
            return new SquareFigure(this);
        }
    }

    public class TFigure : Figure
    {
        public TFigure() : base("Т-фигура", new List<Point>
        {
            new Point(0, 0),
            new Point(1, 0),
            new Point(2, 0),
            new Point(1, 1)
        })
        {
        }

        private TFigure(TFigure figure) : base(figure)
        {
        }

        public override Figure Clone()
        {
            return new TFigure(this);
        }
    }

    public class LFigure : Figure
    {
        public LFigure() : base("Г-фигура", new List<Point>
        {
            new Point(0, 0),
            new Point(0, 1),
            new Point(0, 2),
            new Point(1, 2)
        })
        {
        }

        private LFigure(LFigure figure) : base(figure)
        {
        }

        public override Figure Clone()
        {
            return new LFigure(this);
        }
    }

    // Суперфигуры: клеток больше, чем у обычных.
    public class SuperLineFigure : Figure
    {
        public SuperLineFigure() : base("Супер-линия", new List<Point>
        {
            new Point(0, 0),
            new Point(1, 0),
            new Point(2, 0),
            new Point(3, 0),
            new Point(4, 0)
        })
        {
        }

        private SuperLineFigure(SuperLineFigure figure) : base(figure)
        {
        }

        public override Figure Clone()
        {
            return new SuperLineFigure(this);
        }
    }

    public class SuperSquareFigure : Figure
    {
        public SuperSquareFigure() : base("Супер-квадрат", CreateCells())
        {
        }

        private static List<Point> CreateCells()
        {
            List<Point> cells = new List<Point>();

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    cells.Add(new Point(x, y));
                }
            }

            return cells;
        }

        private SuperSquareFigure(SuperSquareFigure figure) : base(figure)
        {
        }

        public override Figure Clone()
        {
            return new SuperSquareFigure(this);
        }
    }
}
