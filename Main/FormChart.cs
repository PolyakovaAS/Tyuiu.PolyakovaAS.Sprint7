using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Main
{
    public partial class FormChart : Form
    {
        private DataTable dataTable;

        public FormChart()
        {
            InitializeComponent();

            // Создаем тестовые данные
            dataTable = new DataTable();
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("ФИО", typeof(string));
            dataTable.Columns.Add("Телефон", typeof(string));
            dataTable.Columns.Add("Автомобиль", typeof(string));
            dataTable.Columns.Add("Статус", typeof(string));
            dataTable.Columns.Add("Стоимость", typeof(int));

            dataTable.Rows.Add(1, "Иванов И.И.", "+79991112233", "Toyota Camry", "В ремонте", 15000);
            dataTable.Rows.Add(2, "Петров П.П.", "+79994445566", "Honda Civic", "Готов", 12000);
            dataTable.Rows.Add(3, "Сидоров С.С.", "+79997778899", "BMW X5", "Диагностика", 8000);
            dataTable.Rows.Add(4, "Козлов Д.А.", "+79993334455", "Toyota Corolla", "В ремонте", 9000);
            dataTable.Rows.Add(5, "Николаев П.В.", "+79996667788", "Honda Accord", "Готов", 18000);
            dataTable.Rows.Add(6, "Федоров С.М.", "+79995554433", "BMW X3", "В ремонте", 22000);
            dataTable.Rows.Add(7, "Васильев К.Д.", "+79992221100", "Toyota RAV4", "Диагностика", 7500);
            dataTable.Rows.Add(8, "Алексеев И.Н.", "+79998887766", "Honda CR-V", "Готов", 16000);
            dataTable.Rows.Add(9, "Григорьев О.П.", "+79991119988", "BMW 5 Series", "В ремонте", 25000);
            dataTable.Rows.Add(10, "Дмитриев Р.С.", "+79994446655", "Toyota Land Cruiser", "Диагностика", 11000);

            CreateCharts();
        }

        private void CreateCharts()
        {
            Bitmap chartImage = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics graphics = Graphics.FromImage(chartImage))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.Clear(Color.Lavender);

                // === ДИАГРАММА 1: СТАТУСЫ РЕМОНТА (столбчатая) ===
                var statusStats = (from row in dataTable.AsEnumerable()
                                   group row by row.Field<string>("Статус") into statusGroup
                                   select new { Status = statusGroup.Key, Count = statusGroup.Count() }).ToList();

                DrawBarChart(graphics, statusStats, 50, 50, 400, 200, "Статистика по статусам ремонта");

                // === ДИАГРАММА 2: СТОИМОСТЬ РЕМОНТА (линейная) ===
                var costByCar = (from row in dataTable.AsEnumerable()
                                 orderby row.Field<int>("Стоимость")
                                 select new { Car = row.Field<string>("Автомобиль"), Cost = row.Field<int>("Стоимость") }).ToList();

                DrawLineChart(graphics, costByCar, 50, 300, 400, 200, "Стоимость ремонта по автомобилям");

                // === ДИАГРАММА 3: РАСПРЕДЕЛЕНИЕ МАРОК (круговая) ===
                var brandStats = (from row in dataTable.AsEnumerable()
                                  let brand = row.Field<string>("Автомобиль").Split(' ')[0]
                                  group row by brand into brandGroup
                                  select new { Brand = brandGroup.Key, Count = brandGroup.Count() }).ToList();

                DrawPieChart(graphics, brandStats, 500, 50, 200, 200, "Распределение марок");

                // === ЛЕГЕНДА ===
                DrawLegend(graphics, 500, 300, 200, 150);
            }

            pictureBox1.Image = chartImage;
        }

        private void DrawBarChart(Graphics graphics, System.Collections.IList stats, int x, int y, int width, int height, string title)
        {
            // Фон
            using (var brush = new LinearGradientBrush(
                new Rectangle(x, y, width, height),
                Color.LightBlue, Color.White, 90f))
            {
                graphics.FillRectangle(brush, x, y, width, height);
            }
            graphics.DrawRectangle(Pens.Gray, x, y, width, height);

            // Заголовок
            graphics.DrawString(title, new Font("Arial", 10, FontStyle.Bold), Brushes.DarkBlue, x, y - 20);

            int barWidth = 60;
            int spacing = 20;
            int startX = x + 30;
            int startY = y + height - 30;
            int maxHeight = height - 60;

            // Находим максимальное значение
            int maxCount = 0;
            foreach (dynamic stat in stats)
            {
                if (stat.Count > maxCount) maxCount = stat.Count;
            }
            if (maxCount == 0) maxCount = 1;

            Color[] colors = { Color.Red, Color.Blue, Color.Green, Color.Orange };

            for (int i = 0; i < stats.Count; i++)
            {
                dynamic stat = stats[i];
                int barHeight = (int)((double)stat.Count / maxCount * maxHeight);
                int barX = startX + i * (barWidth + spacing);
                int barY = startY - barHeight;

                // Градиент для столбца
                using (var brush = new LinearGradientBrush(
                    new Rectangle(barX, barY, barWidth, barHeight),
                    colors[i % colors.Length],
                    Color.White,
                    LinearGradientMode.Vertical))
                {
                    graphics.FillRectangle(brush, barX, barY, barWidth, barHeight);
                }
                graphics.DrawRectangle(Pens.Black, barX, barY, barWidth, barHeight);

                // Тень
                graphics.DrawRectangle(new Pen(Color.Gray, 2), barX + 2, barY + 2, barWidth, barHeight);

                // Подписи
                graphics.DrawString(stat.Status, new Font("Arial", 8), Brushes.Black, barX, startY + 5);
                graphics.DrawString(stat.Count.ToString(), new Font("Arial", 9, FontStyle.Bold),
                            Brushes.DarkRed, barX + barWidth / 2 - 8, barY - 20);
            }

            // Оси
            graphics.DrawLine(Pens.Black, x + 20, y + 20, x + 20, startY);
            graphics.DrawLine(Pens.Black, x + 20, startY, x + width - 10, startY);
        }

        private void DrawLineChart(Graphics graphics, System.Collections.IList data, int x, int y, int width, int height, string title)
        {
            // Фон
            using (var brush = new LinearGradientBrush(
                new Rectangle(x, y, width, height),
                Color.LightYellow, Color.White, 90f))
            {
                graphics.FillRectangle(brush, x, y, width, height);
            }
            graphics.DrawRectangle(Pens.Gray, x, y, width, height);

            graphics.DrawString(title, new Font("Arial", 10, FontStyle.Bold), Brushes.DarkBlue, x, y - 20);

            int startX = x + 30;
            int startY = y + height - 30;
            int chartHeight = height - 60;

            // Находим мин и макс значения
            int maxCost = 0;
            int minCost = int.MaxValue;
            foreach (dynamic item in data)
            {
                if (item.Cost > maxCost) maxCost = item.Cost;
                if (item.Cost < minCost) minCost = item.Cost;
            }
            if (minCost == int.MaxValue) minCost = 0;
            if (maxCost == minCost) maxCost = minCost + 1;

            Point[] points = new Point[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                dynamic item = data[i];
                int pointX = startX + i * (width - 60) / (data.Count - 1);
                int pointY = startY - (int)((double)(item.Cost - minCost) / (maxCost - minCost) * chartHeight);
                points[i] = new Point(pointX, pointY);
            }

            // Линия графика
            using (var pen = new Pen(Color.Red, 3))
            {
                graphics.DrawLines(pen, points);
            }

            // Точки
            foreach (var point in points)
            {
                graphics.FillEllipse(Brushes.Blue, point.X - 4, point.Y - 4, 8, 8);
                graphics.DrawEllipse(Pens.DarkBlue, point.X - 4, point.Y - 4, 8, 8);
            }

            // Оси
            graphics.DrawLine(Pens.Black, x + 20, y + 20, x + 20, startY);
            graphics.DrawLine(Pens.Black, x + 20, startY, x + width - 10, startY);
        }

        private void DrawPieChart(Graphics graphics, System.Collections.IList stats, int x, int y, int width, int height, string title)
        {
            graphics.DrawString(title, new Font("Arial", 10, FontStyle.Bold), Brushes.DarkBlue, x, y - 20);

            // Сумма всех значений
            int total = 0;
            foreach (dynamic stat in stats)
            {
                total += stat.Count;
            }
            if (total == 0) return;

            Rectangle pieRect = new Rectangle(x + 20, y + 20, width - 40, height - 40);
            float startAngle = 0;

            Color[] colors = { Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Purple, Color.Cyan };

            for (int i = 0; i < stats.Count; i++)
            {
                dynamic stat = stats[i];
                float sweepAngle = 360f * stat.Count / total;

                using (var brush = new SolidBrush(colors[i % colors.Length]))
                {
                    graphics.FillPie(brush, pieRect, startAngle, sweepAngle);
                }
                graphics.DrawPie(Pens.Black, pieRect, startAngle, sweepAngle);

                // Подписи долей
                double midAngle = (startAngle + sweepAngle / 2) * Math.PI / 180;
                int labelX = (int)(x + width / 2 + Math.Cos(midAngle) * (width / 3));
                int labelY = (int)(y + height / 2 + Math.Sin(midAngle) * (height / 3));

                string label = $"{stat.Brand}\n({stat.Count})";
                graphics.DrawString(label, new Font("Arial", 8), Brushes.Black, labelX, labelY);

                startAngle += sweepAngle;
            }
        }

        private void DrawLegend(Graphics graphics, int x, int y, int width, int height)
        {
            using (var brush = new SolidBrush(Color.LightGray))
            {
                graphics.FillRectangle(brush, x, y, width, height);
            }
            graphics.DrawRectangle(Pens.Black, x, y, width, height);

            graphics.DrawString("Легенда", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, x + 10, y + 10);

            string[] legendItems = {
                "■ Столбчатая диаграмма - статусы",
                "■ Линейный график - стоимость",
                "■ Круговая диаграмма - марки",
                "📊 3 типа визуализации",
                "🎨 Градиенты и тени",
                "📝 Подписи и легенда"
            };

            for (int i = 0; i < legendItems.Length; i++)
            {
                graphics.DrawString(legendItems[i], new Font("Arial", 8), Brushes.Black,
                           x + 10, y + 40 + i * 20);
            }
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}