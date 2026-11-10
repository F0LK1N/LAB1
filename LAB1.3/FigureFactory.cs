namespace FigurePatterns
{
    // Creator из паттерна Factory Method.
    public abstract class FigureFactory
    {
        public abstract Figure CreateFigure();
    }

    public class LineFigureFactory : FigureFactory
    {
        public override Figure CreateFigure()
        {
            return new LineFigure();
        }
    }

    public class SquareFigureFactory : FigureFactory
    {
        public override Figure CreateFigure()
        {
            return new SquareFigure();
        }
    }

    public class TFigureFactory : FigureFactory
    {
        public override Figure CreateFigure()
        {
            return new TFigure();
        }
    }

    public class LFigureFactory : FigureFactory
    {
        public override Figure CreateFigure()
        {
            return new LFigure();
        }
    }

    public class SuperLineFigureFactory : FigureFactory
    {
        public override Figure CreateFigure()
        {
            return new SuperLineFigure();
        }
    }

    public class SuperSquareFigureFactory : FigureFactory
    {
        public override Figure CreateFigure()
        {
            return new SuperSquareFigure();
        }
    }
}
