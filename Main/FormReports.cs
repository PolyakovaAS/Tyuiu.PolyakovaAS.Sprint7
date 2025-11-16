using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Main
{
    public partial class FormReports : Form
    {
        private DataTable dataTable;

        public FormReports()
        {
            InitializeComponent();
            // Создаем таблицу с тестовыми данными
            dataTable = new DataTable();
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("ФИО", typeof(string));
            dataTable.Columns.Add("Телефон", typeof(string));
            dataTable.Columns.Add("Автомобиль", typeof(string));
            dataTable.Columns.Add("Статус", typeof(string));

            // Заполняем тестовыми данными
            dataTable.Rows.Add(1, "Иванов И.И.", "+79991112233", "Toyota Camry", "В ремонте");
            dataTable.Rows.Add(2, "Петров П.П.", "+79994445566", "Honda Civic", "Готов");
            dataTable.Rows.Add(3, "Сидоров С.С.", "+79997778899", "BMW X5", "Диагностика");
            dataTable.Rows.Add(4, "Козлов Д.А.", "+79993334455", "Toyota Corolla", "В ремонте");
            dataTable.Rows.Add(5, "Николаев П.В.", "+79996667788", "Honda Accord", "Готов");
            dataTable.Rows.Add(6, "Федоров С.М.", "+79995554433", "BMW X3", "В ремонте");
            dataTable.Rows.Add(7, "Васильев К.Д.", "+79992221100", "Toyota RAV4", "Диагностика");
            dataTable.Rows.Add(8, "Алексеев И.Н.", "+79998887766", "Honda CR-V", "Готов");
            dataTable.Rows.Add(9, "Григорьев О.П.", "+79991119988", "BMW 5 Series", "В ремонте");
            dataTable.Rows.Add(10, "Дмитриев Р.С.", "+79994446655", "Toyota Land Cruiser", "Диагностика");

            GenerateReports();
        }

        private void GenerateReports()
        {
            // Выводим все данные в одну таблицу
            dataGridView1.DataSource = dataTable;

            // Статистика
            int total = dataTable.Rows.Count;
            int inRepair = dataTable.AsEnumerable()
                .Count(row => row.Field<string>("Статус") == "В ремонте");
            int completed = dataTable.AsEnumerable()
                .Count(row => row.Field<string>("Статус") == "Готов");
            int diagnostics = dataTable.AsEnumerable()
                .Count(row => row.Field<string>("Статус") == "Диагностика");

            // Заполняем label
            label1.Text = $"Всего записей: {total}";
            label2.Text = $"В ремонте: {inRepair}";
            label3.Text = $"Готово: {completed}";
            label4.Text = $"На диагностике: {diagnostics}";

            // Популярные марки для label5
            var topCar = dataTable.AsEnumerable()
                .GroupBy(row => row.Field<string>("Автомобиль"))
                .Select(g => new { Car = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .FirstOrDefault();

            label5.Text = topCar != null ?
                $"Популярная марка: {topCar.Car} ({topCar.Count})" :
                "Нет данных";
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_export_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text files (*.txt)|*.txt";
                saveFileDialog.Title = "Экспорт отчета";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string report = $"ОТЧЕТ АВТОРЕМОНТНОЙ МАСТЕРСКОЙ\n\n";
                    report += $"{label1.Text}\n";
                    report += $"{label2.Text}\n";
                    report += $"{label3.Text}\n";
                    report += $"{label4.Text}\n";
                    report += $"{label5.Text}\n\n";

                    report += "ВСЕ ДАННЫЕ:\n";
                    foreach (DataRow row in dataTable.Rows)
                    {
                        report += $"{row["ID"]}. {row["ФИО"]} - {row["Автомобиль"]} ({row["Статус"]})\n";
                    }

                    report += $"\nДата формирования: {DateTime.Now}";

                    File.WriteAllText(saveFileDialog.FileName, report);
                    MessageBox.Show("Отчет экспортирован в файл!", "Успех",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка экспорта: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dada_Enter(object sender, EventArgs e)
        {

        }
    }
    }



    
