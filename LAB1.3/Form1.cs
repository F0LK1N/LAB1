using System;
using System.Drawing;
using System.Windows.Forms;

namespace FigurePatterns
{
    public class Form1 : Form
    {
        private readonly Random random = new Random();
        private readonly FigureFactory[] normalFactories;
        private readonly FigureFactory[] superFactories;

        private Figure currentFigure;

        private readonly PictureBox pictureBoxOriginal = new PictureBox();
        private readonly PictureBox pictureBoxCopy = new PictureBox();
        private readonly Label labelOriginal = new Label();
        private readonly Label labelCopy = new Label();
        private readonly Button buttonCopy = new Button();

        public Form1()
        {
            normalFactories = new FigureFactory[]
            {
                new LineFigureFactory(),
                new SquareFigureFactory(),
                new TFigureFactory(),
                new LFigureFactory()
            };

            superFactories = new FigureFactory[]
            {
                new SuperLineFigureFactory(),
                new SuperSquareFigureFactory()
            };

            Text = "Factory Method и Prototype";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(690, 440);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Font = new Font("Segoe UI", 9F);

            CreateControls();
        }

        private void CreateControls()
        {
            Button buttonGenerate = new Button();
            buttonGenerate.Text = "Сгенерировать фигуру";
            buttonGenerate.Location = new Point(20, 20);
            buttonGenerate.Size = new Size(180, 35);
            buttonGenerate.Click += ButtonGenerate_Click;

            buttonCopy.Text = "Создать копию";
            buttonCopy.Location = new Point(210, 20);
            buttonCopy.Size = new Size(150, 35);
            buttonCopy.Enabled = false;
            buttonCopy.Click += ButtonCopy_Click;

            GroupBox groupOriginal = new GroupBox();
            groupOriginal.Text = "Оригинал";
            groupOriginal.Location = new Point(20, 70);
            groupOriginal.Size = new Size(320, 330);

            pictureBoxOriginal.Location = new Point(10, 25);
            pictureBoxOriginal.Size = new Size(300, 260);
            pictureBoxOriginal.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxOriginal.BackColor = Color.White;
            pictureBoxOriginal.SizeMode = PictureBoxSizeMode.CenterImage;

            labelOriginal.Location = new Point(10, 295);
            labelOriginal.Size = new Size(300, 25);
            labelOriginal.Text = "Фигура не сгенерирована";

            groupOriginal.Controls.Add(pictureBoxOriginal);
            groupOriginal.Controls.Add(labelOriginal);

            GroupBox groupCopy = new GroupBox();
            groupCopy.Text = "Копия";
            groupCopy.Location = new Point(350, 70);
            groupCopy.Size = new Size(320, 330);

            pictureBoxCopy.Location = new Point(10, 25);
            pictureBoxCopy.Size = new Size(300, 260);
            pictureBoxCopy.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxCopy.BackColor = Color.White;
            pictureBoxCopy.SizeMode = PictureBoxSizeMode.CenterImage;

            labelCopy.Location = new Point(10, 295);
            labelCopy.Size = new Size(300, 25);
            labelCopy.Text = "Копия не создана";

            groupCopy.Controls.Add(pictureBoxCopy);
            groupCopy.Controls.Add(labelCopy);

            Label labelHint = new Label();
            labelHint.Location = new Point(20, 410);
            labelHint.Size = new Size(650, 20);
            labelHint.Text = "Обычные фигуры состоят из 4 клеток, суперфигуры — из 5 и более.";

            Controls.Add(buttonGenerate);
            Controls.Add(buttonCopy);
            Controls.Add(groupOriginal);
            Controls.Add(groupCopy);
            Controls.Add(labelHint);
        }

        private void ButtonGenerate_Click(object sender, EventArgs e)
        {
            currentFigure = CreateRandomFigure();

            DrawFigure(pictureBoxOriginal, currentFigure);
            labelOriginal.Text = currentFigure.Name + ", клеток: " + currentFigure.CellCount;

            if (pictureBoxCopy.Image != null)
            {
                pictureBoxCopy.Image.Dispose();
                pictureBoxCopy.Image = null;
            }

            labelCopy.Text = "Копия не создана";
            buttonCopy.Enabled = true;
        }

        private Figure CreateRandomFigure()
        {
            // Суперфигура появляется с вероятностью 20%.
            if (random.Next(100) < 20)
            {
                int superIndex = random.Next(superFactories.Length);
                return superFactories[superIndex].CreateFigure();
            }

            int index = random.Next(normalFactories.Length);
            return normalFactories[index].CreateFigure();
        }

        private void ButtonCopy_Click(object sender, EventArgs e)
        {
            if (currentFigure == null)
            {
                return;
            }

            Figure copy = currentFigure.Clone();

            DrawFigure(pictureBoxCopy, copy);
            labelCopy.Text = copy.Name + ", клеток: " + copy.CellCount + " (копия)";
        }

        private void DrawFigure(PictureBox pictureBox, Figure figure)
        {
            int maxX = 0;
            int maxY = 0;

            foreach (Point cell in figure.Cells)
            {
                if (cell.X > maxX)
                {
                    maxX = cell.X;
                }

                if (cell.Y > maxY)
                {
                    maxY = cell.Y;
                }
            }

            int width = maxX + 1;
            int height = maxY + 1;
            int cellSize = 30;
            int margin = 10;

            Bitmap bitmap = new Bitmap(width * cellSize + margin * 2, height * cellSize + margin * 2);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);

                Brush brush = figure.CellCount > 4 ? Brushes.Orange : Brushes.SteelBlue;

                foreach (Point cell in figure.Cells)
                {
                    Rectangle rectangle = new Rectangle(
                        margin + cell.X * cellSize,
                        margin + cell.Y * cellSize,
                        cellSize - 2,
                        cellSize - 2);

                    graphics.FillRectangle(brush, rectangle);
                    graphics.DrawRectangle(Pens.Black, rectangle);
                }
            }

            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
            }

            pictureBox.Image = bitmap;
        }
    }
}
