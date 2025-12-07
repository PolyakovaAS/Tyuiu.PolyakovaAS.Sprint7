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

            // Увеличиваем размер PictureBox для большего пространства
            pictureBox1.Size = new Size(1200, 800);

            // Создаем тестовые данные (50 записей)
            dataTable = new DataTable();
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("ФИО", typeof(string));
            dataTable.Columns.Add("Телефон", typeof(string));
            dataTable.Columns.Add("Автомобиль", typeof(string));
            dataTable.Columns.Add("Год выпуска", typeof(int));
            dataTable.Columns.Add("Статус", typeof(string));
            dataTable.Columns.Add("Стоимость", typeof(int));
            dataTable.Columns.Add("Механик", typeof(string));
            dataTable.Columns.Add("Длительность (дни)", typeof(int));
            dataTable.Columns.Add("Приоритет", typeof(string));

            GenerateTestData();

            CreateCharts();
        }

        private void GenerateTestData()
        {
            string[] names = {
                "Иванов И.И.", "Петров П.П.", "Сидоров С.С.", "Козлов Д.А.", "Николаев П.В.",
                "Федоров С.М.", "Васильев К.Д.", "Алексеев И.Н.", "Григорьев О.П.", "Дмитриев Р.С.",
                "Егоров В.Л.", "Жуков А.К.", "Зайцев М.И.", "Ильин С.В.", "Кузнецов П.А.",
                "Лебедев Д.М.", "Михайлов А.С.", "Новиков В.П.", "Орлов С.Д.", "Павлов М.К.",
                "Романов А.В.", "Семенов И.Д.", "Тарасов П.М.", "Ушаков В.С.", "Филиппов А.П.",
                "Харитонов М.В.", "Цветков С.А.", "Чернов П.И.", "Широков В.М.", "Щукин А.С."
            };

            string[] cars = {
                "Toyota Camry", "Honda Civic", "BMW X5", "Toyota Corolla", "Honda Accord",
                "BMW X3", "Toyota RAV4", "Honda CR-V", "BMW 5 Series", "Toyota Land Cruiser",
                "Mercedes E-Class", "Audi A4", "Volkswagen Golf", "Hyundai Tucson", "Kia Sportage",
                "Ford Focus", "Nissan Qashqai", "Mazda CX-5", "Skoda Octavia", "Renault Duster",
                "Lexus RX", "Volvo XC60", "Subaru Forester", "Mitsubishi Outlander", "Jeep Grand Cherokee"
            };

            string[] statuses = { "Новая заявка", "Диагностика", "Ожидание запчастей", "В ремонте", "Тестирование", "Готов", "Выдан" };
            string[] mechanics = { "Иванов А.С.", "Петров В.И.", "Сидоров М.П.", "Козлов Д.А.", "Николаев П.В.", "Мастерская 1", "Мастерская 2" };
            string[] priorities = { "Низкий", "Средний", "Высокий", "Срочный" };

            Random rnd = new Random();

            for (int i = 1; i <= 50; i++)
            {
                string car = cars[rnd.Next(cars.Length)];
                string brand = car.Split(' ')[0];
                int baseCost = brand switch
                {
                    "BMW" => rnd.Next(20000, 50000),
                    "Mercedes" => rnd.Next(25000, 60000),
                    "Audi" => rnd.Next(18000, 45000),
                    "Lexus" => rnd.Next(30000, 70000),
                    "Volvo" => rnd.Next(22000, 48000),
                    _ => rnd.Next(8000, 35000)
                };

                // Добавляем надбавку за возраст автомобиля
                int carYear = rnd.Next(2005, 2024);
                int yearMultiplier = 2024 - carYear;
                int finalCost = baseCost + (yearMultiplier * 500);

                dataTable.Rows.Add(
                    i,
                    names[rnd.Next(names.Length)],
                    $"+7{rnd.Next(900, 1000)}{rnd.Next(1000000, 9999999)}",
                    car,
                    carYear,
                    statuses[rnd.Next(statuses.Length)],
                    finalCost,
                    mechanics[rnd.Next(mechanics.Length)],
                    rnd.Next(1, 21),
                    priorities[rnd.Next(priorities.Length)]
                );
            }
        }

        private void CreateCharts()
        {
            Bitmap chartImage = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics graphics = Graphics.FromImage(chartImage))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                // Красивый фон с градиентом
                using (var bgBrush = new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(pictureBox1.Width, pictureBox1.Height),
                    Color.FromArgb(240, 245, 255),
                    Color.FromArgb(220, 230, 250)))
                {
                    graphics.FillRectangle(bgBrush, 0, 0, pictureBox1.Width, pictureBox1.Height);
                }

                // Заголовок отчета
                DrawReportHeader(graphics);

                // === ДИАГРАММА 1: СТАТУСЫ РЕМОНТА (столбчатая 3D) ===
                var statusStats = (from row in dataTable.AsEnumerable()
                                   group row by row.Field<string>("Статус") into statusGroup
                                   select new { Status = statusGroup.Key, Count = statusGroup.Count() })
                                  .OrderByDescending(x => x.Count)
                                  .ToList();

                Draw3DBarChart(graphics, statusStats, 50, 100, 400, 250, "Распределение по статусам");

                // === ДИАГРАММА 2: РАСПРЕДЕЛЕНИЕ МАРОК (круговая 3D) ===
                var brandStats = (from row in dataTable.AsEnumerable()
                                  let brand = row.Field<string>("Автомобиль").Split(' ')[0]
                                  group row by brand into brandGroup
                                  select new { Brand = brandGroup.Key, Count = brandGroup.Count() })
                                 .OrderByDescending(x => x.Count)
                                 .Take(8) // Берем топ-8 марок
                                 .ToList();

                Draw3DPieChart(graphics, brandStats, 500, 100, 300, 250, "Топ-8 марок автомобилей");

                // === ДИАГРАММА 3: СТОИМОСТЬ РЕМОНТА (линейный с областями) ===
                var monthlyStats = (from row in dataTable.AsEnumerable()
                                    let cost = row.Field<int>("Стоимость")
                                    let duration = row.Field<int>("Длительность (дни)")
                                    select new { Стоимость = cost, Длительность = duration })
                                   .OrderBy(x => x.Стоимость)
                                   .ToList();

                DrawAreaChart(graphics, monthlyStats, 50, 400, 400, 250, "Стоимость vs Длительность ремонта");

                // === ДИАГРАММА 4: РЕЙТИНГ МЕХАНИКОВ (радарная диаграмма) ===
                var mechanicStats = (from row in dataTable.AsEnumerable()
                                     group row by row.Field<string>("Механик") into mechanicGroup
                                     select new
                                     {
                                         Механик = mechanicGroup.Key,
                                         Количество = mechanicGroup.Count(),
                                         СредняяСтоимость = mechanicGroup.Average(r => r.Field<int>("Стоимость")),
                                         СредняяДлительность = mechanicGroup.Average(r => r.Field<int>("Длительность (дни)"))
                                     })
                                    .OrderByDescending(x => x.Количество)
                                    .Take(6)
                                    .ToList();

                DrawRadarChart(graphics, mechanicStats, 500, 400, 350, 250, "Эффективность механиков");

                // === СТАТИСТИКА В ЦИФРАХ ===
                DrawStatisticsPanel(graphics, 880, 100, 300, 550);

                // === ЛЕГЕНДА И ИНФОРМАЦИЯ ===
                DrawInfoPanel(graphics, 880, 670, 300, 120);
            }

            pictureBox1.Image = chartImage;
        }

        private void DrawReportHeader(Graphics graphics)
        {
            using (var titleFont = new Font("Segoe UI", 24, FontStyle.Bold))
            using (var subtitleFont = new Font("Segoe UI", 12, FontStyle.Italic))
            {
                graphics.DrawString("АНАЛИТИЧЕСКИЙ ОТЧЕТ", titleFont,
                    new SolidBrush(Color.FromArgb(30, 60, 114)), 400, 20);

                graphics.DrawString($"Авторемонтная мастерская • Данные на {DateTime.Now:dd.MM.yyyy} • {dataTable.Rows.Count} записей",
                    subtitleFont, new SolidBrush(Color.Gray), 400, 60);
            }
        }

        private void Draw3DBarChart(Graphics graphics, System.Collections.IList stats, int x, int y, int width, int height, string title)
        {
            // Фон диаграммы
            using (var bgBrush = new LinearGradientBrush(
                new Rectangle(x, y, width, height),
                Color.White,
                Color.FromArgb(245, 248, 255),
                LinearGradientMode.Vertical))
            {
                graphics.FillRectangle(bgBrush, x, y, width, height);
            }

            graphics.DrawRectangle(new Pen(Color.FromArgb(200, 200, 220), 2), x, y, width, height);

            // Заголовок
            using (var titleFont = new Font("Segoe UI", 14, FontStyle.Bold))
            {
                graphics.DrawString(title, titleFont, new SolidBrush(Color.FromArgb(40, 40, 60)), x + 10, y - 30);
            }

            int barWidth = 40;
            int spacing = 30;
            int startX = x + 40;
            int startY = y + height - 40;
            int maxHeight = height - 80;

            // Находим максимальное значение
            int maxCount = 0;
            foreach (dynamic stat in stats)
            {
                if (stat.Count > maxCount) maxCount = stat.Count;
            }
            if (maxCount == 0) maxCount = 1;

            Color[] barColors = {
                Color.FromArgb(65, 105, 225),   // Royal Blue
                Color.FromArgb(50, 205, 50),    // Lime Green
                Color.FromArgb(255, 140, 0),    // Dark Orange
                Color.FromArgb(220, 20, 60),    // Crimson
                Color.FromArgb(138, 43, 226),   // Blue Violet
                Color.FromArgb(255, 215, 0)     // Gold
            };

            for (int i = 0; i < stats.Count; i++)
            {
                dynamic stat = stats[i];
                int barHeight = (int)((double)stat.Count / maxCount * maxHeight);
                int barX = startX + i * (barWidth + spacing);
                int barY = startY - barHeight;

                // 3D эффект - боковая грань
                using (var sideBrush = new SolidBrush(Color.FromArgb(
                    Math.Max(barColors[i % barColors.Length].R - 40, 0),
                    Math.Max(barColors[i % barColors.Length].G - 40, 0),
                    Math.Max(barColors[i % barColors.Length].B - 40, 0))))
                {
                    Point[] sidePolygon = {
                        new Point(barX + barWidth, barY),
                        new Point(barX + barWidth + 8, barY - 8),
                        new Point(barX + barWidth + 8, startY - 8),
                        new Point(barX + barWidth, startY)
                    };
                    graphics.FillPolygon(sideBrush, sidePolygon);
                }

                // 3D эффект - верхняя грань
                using (var topBrush = new SolidBrush(Color.FromArgb(
                    Math.Max(barColors[i % barColors.Length].R - 20, 0),
                    Math.Max(barColors[i % barColors.Length].G - 20, 0),
                    Math.Max(barColors[i % barColors.Length].B - 20, 0))))
                {
                    Point[] topPolygon = {
                        new Point(barX, barY),
                        new Point(barX + barWidth, barY),
                        new Point(barX + barWidth + 8, barY - 8),
                        new Point(barX + 8, barY - 8)
                    };
                    graphics.FillPolygon(topBrush, topPolygon);
                }

                // Основной столбец
                using (var mainBrush = new LinearGradientBrush(
                    new Rectangle(barX, barY, barWidth, barHeight),
                    barColors[i % barColors.Length],
                    Color.FromArgb(
                        Math.Min(barColors[i % barColors.Length].R + 60, 255),
                        Math.Min(barColors[i % barColors.Length].G + 60, 255),
                        Math.Min(barColors[i % barColors.Length].B + 60, 255)),
                    LinearGradientMode.Vertical))
                {
                    graphics.FillRectangle(mainBrush, barX, barY, barWidth, barHeight);
                }

                graphics.DrawRectangle(new Pen(Color.FromArgb(80, 80, 80), 1), barX, barY, barWidth, barHeight);

                // Подписи
                using (var labelFont = new Font("Segoe UI", 9))
                using (var valueFont = new Font("Segoe UI", 10, FontStyle.Bold))
                {
                    // Значение сверху
                    graphics.DrawString(stat.Count.ToString(), valueFont,
                        new SolidBrush(Color.FromArgb(40, 40, 40)),
                        barX + barWidth / 2 - 10, barY - 25);

                    // Название статуса снизу
                    string shortStatus = stat.Status.Length > 10 ? stat.Status.Substring(0, 10) + "..." : stat.Status;
                    graphics.DrawString(shortStatus, labelFont,
                        new SolidBrush(Color.FromArgb(80, 80, 80)),
                        barX, startY + 5);
                }
            }

            // Оси
            using (var axisPen = new Pen(Color.FromArgb(120, 120, 140), 2))
            {
                graphics.DrawLine(axisPen, x + 30, y + 20, x + 30, startY);
                graphics.DrawLine(axisPen, x + 30, startY, x + width - 10, startY);
            }
        }

        private void Draw3DPieChart(Graphics graphics, System.Collections.IList stats, int x, int y, int width, int height, string title)
        {
            // Фон
            using (var bgBrush = new LinearGradientBrush(
                new Rectangle(x, y, width, height),
                Color.White,
                Color.FromArgb(255, 248, 245),
                LinearGradientMode.Vertical))
            {
                graphics.FillRectangle(bgBrush, x, y, width, height);
            }

            graphics.DrawRectangle(new Pen(Color.FromArgb(200, 200, 220), 2), x, y, width, height);

            // Заголовок
            using (var titleFont = new Font("Segoe UI", 14, FontStyle.Bold))
            {
                graphics.DrawString(title, titleFont, new SolidBrush(Color.FromArgb(40, 40, 60)), x + 10, y - 30);
            }

            // Сумма всех значений
            int total = 0;
            foreach (dynamic stat in stats)
            {
                total += stat.Count;
            }
            if (total == 0) return;

            // 3D эффект - смещение для объема
            int depth = 20;
            Rectangle pieRect = new Rectangle(x + 40 + depth / 2, y + 40 + depth / 2, width - 80, height - 80);
            Rectangle pieRect3D = new Rectangle(x + 40, y + 40, width - 80, height - 80);

            float startAngle = 0;

            Color[] pieColors = {
                Color.FromArgb(255, 99, 132),   // Красный
                Color.FromArgb(54, 162, 235),   // Синий
                Color.FromArgb(255, 205, 86),   // Желтый
                Color.FromArgb(75, 192, 192),   // Бирюзовый
                Color.FromArgb(153, 102, 255),  // Фиолетовый
                Color.FromArgb(255, 159, 64),   // Оранжевый
                Color.FromArgb(201, 203, 207),  // Серый
                Color.FromArgb(50, 168, 82)     // Зеленый
            };

            // Рисуем 3D тень
            for (int d = depth; d > 0; d--)
            {
                float shadowAngle = 0;
                for (int i = 0; i < stats.Count; i++)
                {
                    dynamic stat = stats[i];
                    float sweepAngle = 360f * stat.Count / total;

                    using (var shadowBrush = new SolidBrush(Color.FromArgb(30, 30, 30)))
                    {
                        graphics.FillPie(shadowBrush,
                            x + 40 + d, y + 40 + d, width - 80, height - 80,
                            shadowAngle, sweepAngle);
                    }
                    shadowAngle += sweepAngle;
                }
            }

            // Рисуем основные секции
            for (int i = 0; i < stats.Count; i++)
            {
                dynamic stat = stats[i];
                float sweepAngle = 360f * stat.Count / total;

                using (var brush = new LinearGradientBrush(
                    pieRect,
                    pieColors[i % pieColors.Length],
                    Color.FromArgb(
                        Math.Min(pieColors[i % pieColors.Length].R + 60, 255),
                        Math.Min(pieColors[i % pieColors.Length].G + 60, 255),
                        Math.Min(pieColors[i % pieColors.Length].B + 60, 255)),
                    LinearGradientMode.ForwardDiagonal))
                {
                    graphics.FillPie(brush, pieRect3D, startAngle, sweepAngle);
                }

                graphics.DrawPie(new Pen(Color.FromArgb(80, 80, 80), 1), pieRect3D, startAngle, sweepAngle);

                // Подписи с процентами
                double midAngle = (startAngle + sweepAngle / 2) * Math.PI / 180;
                int labelX = (int)(x + width / 2 + Math.Cos(midAngle) * (width / 3));
                int labelY = (int)(y + height / 2 + Math.Sin(midAngle) * (height / 3));

                double percentage = Math.Round((double)stat.Count / total * 100, 1);
                string label = $"{stat.Brand}\n{percentage}%";

                using (var labelFont = new Font("Segoe UI", 9, FontStyle.Bold))
                {
                    SizeF textSize = graphics.MeasureString(label, labelFont);
                    graphics.DrawString(label, labelFont,
                        new SolidBrush(Color.FromArgb(40, 40, 40)),
                        labelX - textSize.Width / 2, labelY - textSize.Height / 2);
                }

                startAngle += sweepAngle;
            }
        }

        private void DrawAreaChart(Graphics graphics, System.Collections.IList data, int x, int y, int width, int height, string title)
        {
            // Фон
            using (var bgBrush = new LinearGradientBrush(
                new Rectangle(x, y, width, height),
                Color.White,
                Color.FromArgb(245, 255, 248),
                LinearGradientMode.Vertical))
            {
                graphics.FillRectangle(bgBrush, x, y, width, height);
            }

            graphics.DrawRectangle(new Pen(Color.FromArgb(200, 200, 220), 2), x, y, width, height);

            // Заголовок
            using (var titleFont = new Font("Segoe UI", 14, FontStyle.Bold))
            {
                graphics.DrawString(title, titleFont, new SolidBrush(Color.FromArgb(40, 40, 60)), x + 10, y - 30);
            }

            int startX = x + 50;
            int startY = y + height - 50;
            int chartHeight = height - 100;
            int chartWidth = width - 100;

            if (data.Count == 0) return;

            // Находим мин и макс значения
            int maxCost = 0;
            int minCost = int.MaxValue;
            int maxDuration = 0;

            foreach (dynamic item in data)
            {
                if (item.Стоимость > maxCost) maxCost = item.Стоимость;
                if (item.Стоимость < minCost) minCost = item.Стоимость;
                if (item.Длительность > maxDuration) maxDuration = item.Длительность;
            }

            if (minCost == int.MaxValue) minCost = 0;
            if (maxCost == minCost) maxCost = minCost + 1;
            if (maxDuration == 0) maxDuration = 1;

            PointF[] costPoints = new PointF[data.Count];
            PointF[] durationPoints = new PointF[data.Count];

            for (int i = 0; i < data.Count; i++)
            {
                dynamic item = data[i];
                float pointX = startX + i * chartWidth / (data.Count - 1);

                // Нормализуем стоимость
                float costY = startY - (float)(item.Стоимость - minCost) / (maxCost - minCost) * chartHeight;
                costPoints[i] = new PointF(pointX, costY);

                // Нормализуем длительность (смещаем на пол-графика)
                float durationY = startY - (float)item.Длительность / maxDuration * chartHeight * 0.5f;
                durationPoints[i] = new PointF(pointX, durationY);
            }

            // Заполнение области стоимости
            PointF[] costArea = new PointF[costPoints.Length + 2];
            costPoints.CopyTo(costArea, 0);
            costArea[costPoints.Length] = new PointF(costPoints[costPoints.Length - 1].X, startY);
            costArea[costPoints.Length + 1] = new PointF(costPoints[0].X, startY);

            using (var areaBrush = new LinearGradientBrush(
                new RectangleF(x, y, width, height),
                Color.FromArgb(100, 54, 162, 235),
                Color.FromArgb(50, 54, 162, 235),
                LinearGradientMode.Vertical))
            {
                graphics.FillPolygon(areaBrush, costArea);
            }

            // Заполнение области длительности
            PointF[] durationArea = new PointF[durationPoints.Length + 2];
            durationPoints.CopyTo(durationArea, 0);
            durationArea[durationPoints.Length] = new PointF(durationPoints[durationPoints.Length - 1].X, startY);
            durationArea[durationPoints.Length + 1] = new PointF(durationPoints[0].X, startY);

            using (var areaBrush = new LinearGradientBrush(
                new RectangleF(x, y, width, height),
                Color.FromArgb(100, 255, 99, 132),
                Color.FromArgb(50, 255, 99, 132),
                LinearGradientMode.Vertical))
            {
                graphics.FillPolygon(areaBrush, durationArea);
            }

            // Линии графиков
            using (var costPen = new Pen(Color.FromArgb(54, 162, 235), 3))
            using (var durationPen = new Pen(Color.FromArgb(255, 99, 132), 3))
            {
                graphics.DrawLines(costPen, costPoints);
                graphics.DrawLines(durationPen, durationPoints);
            }

            // Точки на графиках
            for (int i = 0; i < costPoints.Length; i += 3) // Рисуем каждую 3-ю точку
            {
                graphics.FillEllipse(Brushes.White, costPoints[i].X - 4, costPoints[i].Y - 4, 8, 8);
                graphics.DrawEllipse(new Pen(Color.FromArgb(54, 162, 235), 2),
                    costPoints[i].X - 4, costPoints[i].Y - 4, 8, 8);

                graphics.FillEllipse(Brushes.White, durationPoints[i].X - 4, durationPoints[i].Y - 4, 8, 8);
                graphics.DrawEllipse(new Pen(Color.FromArgb(255, 99, 132), 2),
                    durationPoints[i].X - 4, durationPoints[i].Y - 4, 8, 8);
            }

            // Легенда
            using (var legendFont = new Font("Segoe UI", 10))
            {
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(54, 162, 235)), startX, y + 20, 15, 15);
                graphics.DrawString("Стоимость ремонта", legendFont, Brushes.Black, startX + 20, y + 18);

                graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 99, 132)), startX + 150, y + 20, 15, 15);
                graphics.DrawString("Длительность (дни)", legendFont, Brushes.Black, startX + 170, y + 18);
            }

            // Оси
            using (var axisPen = new Pen(Color.FromArgb(120, 120, 140), 2))
            {
                graphics.DrawLine(axisPen, x + 40, y + 20, x + 40, startY);
                graphics.DrawLine(axisPen, x + 40, startY, x + width - 10, startY);
            }
        }

        private void DrawRadarChart(Graphics graphics, System.Collections.IList stats, int x, int y, int width, int height, string title)
        {
            // Фон
            using (var bgBrush = new LinearGradientBrush(
                new Rectangle(x, y, width, height),
                Color.White,
                Color.FromArgb(255, 245, 248),
                LinearGradientMode.Vertical))
            {
                graphics.FillRectangle(bgBrush, x, y, width, height);
            }

            graphics.DrawRectangle(new Pen(Color.FromArgb(200, 200, 220), 2), x, y, width, height);

            // Заголовок
            using (var titleFont = new Font("Segoe UI", 14, FontStyle.Bold))
            {
                graphics.DrawString(title, titleFont, new SolidBrush(Color.FromArgb(40, 40, 60)), x + 10, y - 30);
            }

            if (stats.Count == 0) return;

            // Центр радара
            int centerX = x + width / 2;
            int centerY = y + height / 2;
            int radius = Math.Min(width, height) / 2 - 50;

            // Рисуем концентрические круги
            for (int i = 1; i <= 5; i++)
            {
                int currentRadius = radius * i / 5;
                graphics.DrawEllipse(new Pen(Color.FromArgb(200, 200, 220), 1),
                    centerX - currentRadius, centerY - currentRadius,
                    currentRadius * 2, currentRadius * 2);
            }

            // Рисуем оси
            int axesCount = stats.Count;
            for (int i = 0; i < axesCount; i++)
            {
                double angle = 2 * Math.PI * i / axesCount - Math.PI / 2;
                int endX = (int)(centerX + radius * Math.Cos(angle));
                int endY = (int)(centerY + radius * Math.Sin(angle));

                graphics.DrawLine(new Pen(Color.FromArgb(180, 180, 200), 1), centerX, centerY, endX, endY);

                // Подписи осей
                dynamic stat = stats[i];
                string label = stat.Механик.Split(' ')[0]; // Берем только фамилию
                graphics.DrawString(label, new Font("Segoe UI", 9),
                    new SolidBrush(Color.FromArgb(80, 80, 80)),
                    endX - 20, endY - 10);
            }

            // Нормализуем значения
            double maxCount = 0;
            double maxCost = 0;
            double maxDuration = 0;

            foreach (dynamic stat in stats)
            {
                if (stat.Количество > maxCount) maxCount = stat.Количество;
                if (stat.СредняяСтоимость > maxCost) maxCost = stat.СредняяСтоимость;
                if (stat.СредняяДлительность > maxDuration) maxDuration = stat.СредняяДлительность;
            }

            // Рисуем линии для каждого показателя
            Color[] indicatorColors = {
               Color.FromArgb(178, 255, 99, 132),    // Количество - красный с альфа 0.7 (178/255 ≈ 0.7)
    Color.FromArgb(178, 54, 162, 235),    // Стоимость - синий с альфа 0.7
    Color.FromArgb(178, 75, 192, 192)     // Длительность - бирюзовый с альфа 0.7
            };

            // Преобразуем SolidBrush в нужный формат
            SolidBrush[] indicatorBrushes = indicatorColors.Select(c => new SolidBrush(c)).ToArray();

            for (int indicator = 0; indicator < 3; indicator++)
            {
                PointF[] points = new PointF[axesCount];

                for (int i = 0; i < axesCount; i++)
                {
                    dynamic stat = stats[i];
                    double angle = 2 * Math.PI * i / axesCount - Math.PI / 2;

                    double value = indicator switch
                    {
                        0 => stat.Количество / maxCount,
                        1 => stat.СредняяСтоимость / maxCost,
                        2 => stat.СредняяДлительность / maxDuration,
                        _ => 0
                    };

                    int pointX = (int)(centerX + radius * value * Math.Cos(angle));
                    int pointY = (int)(centerY + radius * value * Math.Sin(angle));

                    points[i] = new PointF(pointX, pointY);
                }

                // Заполняем область
                PointF[] areaPoints = new PointF[points.Length + 1];
                points.CopyTo(areaPoints, 0);
                areaPoints[points.Length] = points[0];

                using (var areaBrush = new SolidBrush(Color.FromArgb(40,
                    indicatorBrushes[indicator].Color.R,
                    indicatorBrushes[indicator].Color.G,
                    indicatorBrushes[indicator].Color.B)))
                {
                    graphics.FillPolygon(areaBrush, areaPoints);
                }

                // Рисуем линию
                using (var linePen = new Pen(indicatorBrushes[indicator].Color, 2))
                {
                    for (int i = 0; i < points.Length; i++)
                    {
                        int next = (i + 1) % points.Length;
                        graphics.DrawLine(linePen, points[i], points[next]);
                    }
                }

                // Точки
                foreach (var point in points)
                {
                    graphics.FillEllipse(Brushes.White, point.X - 3, point.Y - 3, 6, 6);
                    graphics.DrawEllipse(new Pen(indicatorBrushes[indicator].Color, 1),
                        point.X - 3, point.Y - 3, 6, 6);
                }
            }

            // Легенда
            string[] legendLabels = { "Количество заказов", "Средняя стоимость", "Средняя длительность" };
            for (int i = 0; i < 3; i++)
            {
                graphics.FillRectangle(indicatorBrushes[i], centerX - 100, centerY + radius + 20 + i * 20, 15, 15);
                graphics.DrawString(legendLabels[i], new Font("Segoe UI", 9),
                    Brushes.Black, centerX - 80, centerY + radius + 18 + i * 20);
            }
        }

        private void DrawStatisticsPanel(Graphics graphics, int x, int y, int width, int height)
        {
            // Фон панели
            using (var bgBrush = new LinearGradientBrush(
                new Rectangle(x, y, width, height),
                Color.FromArgb(248, 249, 252),
                Color.FromArgb(235, 238, 248),
                LinearGradientMode.Vertical))
            {
                graphics.FillRectangle(bgBrush, x, y, width, height);
            }

            graphics.DrawRectangle(new Pen(Color.FromArgb(200, 200, 220), 2), x, y, width, height);

            // Заголовок панели
            using (var panelFont = new Font("Segoe UI", 16, FontStyle.Bold))
            {
                graphics.DrawString("СТАТИСТИКА", panelFont,
                    new SolidBrush(Color.FromArgb(40, 40, 60)), x + 20, y + 20);
            }

            // Рассчитываем статистику
            int totalOrders = dataTable.Rows.Count;
            int totalCost = dataTable.AsEnumerable().Sum(row => row.Field<int>("Стоимость"));
            double avgCost = totalOrders > 0 ? (double)totalCost / totalOrders : 0;

            var statusGroups = from row in dataTable.AsEnumerable()
                               group row by row.Field<string>("Статус") into g
                               select new { Status = g.Key, Count = g.Count() };

            var brandGroups = from row in dataTable.AsEnumerable()
                              let brand = row.Field<string>("Автомобиль").Split(' ')[0]
                              group row by brand into g
                              select new { Brand = g.Key, Count = g.Count() };

            string mostPopularBrand = brandGroups.OrderByDescending(x => x.Count).FirstOrDefault()?.Brand ?? "Нет данных";
            string mostCommonStatus = statusGroups.OrderByDescending(x => x.Count).FirstOrDefault()?.Status ?? "Нет данных";

            int urgentOrders = dataTable.AsEnumerable()
                .Count(row => row.Field<string>("Приоритет") == "Срочный");

            // Отображаем статистику
            y += 60;
            using (var statFont = new Font("Segoe UI", 11))
            using (var valueFont = new Font("Segoe UI", 12, FontStyle.Bold))
            {
                DrawStatItem(graphics, "📊 Всего заказов", totalOrders.ToString(), x + 20, y, width - 40, 40, statFont, valueFont, Color.FromArgb(65, 105, 225));
                y += 50;

                DrawStatItem(graphics, "💰 Средняя стоимость", $"{avgCost:0} руб.", x + 20, y, width - 40, 40, statFont, valueFont, Color.FromArgb(50, 205, 50));
                y += 50;

                DrawStatItem(graphics, "🚗 Популярная марка", mostPopularBrand, x + 20, y, width - 40, 40, statFont, valueFont, Color.FromArgb(255, 140, 0));
                y += 50;

                DrawStatItem(graphics, "⚡ Частый статус", mostCommonStatus, x + 20, y, width - 40, 40, statFont, valueFont, Color.FromArgb(220, 20, 60));
                y += 50;

                DrawStatItem(graphics, "⚠ Срочных заказов", urgentOrders.ToString(), x + 20, y, width - 40, 40, statFont, valueFont, Color.FromArgb(138, 43, 226));
                y += 50;

                DrawStatItem(graphics, "👨‍🔧 Всего механиков", "7", x + 20, y, width - 40, 40, statFont, valueFont, Color.FromArgb(255, 215, 0));
            }
        }

        private void DrawStatItem(Graphics graphics, string label, string value, int x, int y, int width, int height, Font labelFont, Font valueFont, Color color)
        {
            // Фон элемента
            using (var itemBrush = new SolidBrush(Color.White))
            {
                graphics.FillRectangle(itemBrush, x, y, width, height);
            }

            graphics.DrawRectangle(new Pen(Color.FromArgb(220, 220, 230), 1), x, y, width, height);

            // Цветной индикатор слева
            using (var indicatorBrush = new SolidBrush(color))
            {
                graphics.FillRectangle(indicatorBrush, x, y, 5, height);
            }

            // Метка
            graphics.DrawString(label, labelFont, new SolidBrush(Color.FromArgb(80, 80, 80)), x + 15, y + 10);

            // Значение
            graphics.DrawString(value, valueFont, new SolidBrush(Color.FromArgb(40, 40, 40)), x + width - 100, y + 10);
        }

        private void DrawInfoPanel(Graphics graphics, int x, int y, int width, int height)
        {
            // Фон панели
            using (var bgBrush = new LinearGradientBrush(
                new Rectangle(x, y, width, height),
                Color.FromArgb(240, 248, 255),
                Color.FromArgb(220, 235, 250),
                LinearGradientMode.Vertical))
            {
                graphics.FillRectangle(bgBrush, x, y, width, height);
            }

            graphics.DrawRectangle(new Pen(Color.FromArgb(180, 180, 200), 1), x, y, width, height);

            using (var infoFont = new Font("Segoe UI", 9))
            {
                graphics.DrawString("📈 4 типа визуализации данных", infoFont, Brushes.Black, x + 10, y + 10);
                graphics.DrawString("🎨 Профессиональный дизайн", infoFont, Brushes.Black, x + 10, y + 30);
                graphics.DrawString("📊 Анализ 50+ записей", infoFont, Brushes.Black, x + 10, y + 50);
                graphics.DrawString("⏱ Автоматическая генерация", infoFont, Brushes.Black, x + 10, y + 70);
                graphics.DrawString($"🔄 Обновлено: {DateTime.Now:HH:mm}", infoFont, Brushes.Black, x + 10, y + 90);
            }
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}