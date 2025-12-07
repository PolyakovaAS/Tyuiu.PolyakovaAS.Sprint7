using System.Data;
using System.Text;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Main
{
    public partial class toolStripMain : Form
    {
        private DataTable dataTable_PII;
        private string currentFilePath_PII = "";

        public toolStripMain()
        {
            InitializeComponent();
            CreateEmptyTable();
            dataGridView1.DataSource = dataTable_PII;
            // Убираем вызов CustomizeDataGridView() из конструктора
            // Будем вызывать его после загрузки формы
        }

        private void CreateEmptyTable()
        {
            dataTable_PII = new DataTable();

            dataTable_PII.Columns.Add("ID", typeof(int));
            dataTable_PII.Columns.Add("ФИО", typeof(string));
            dataTable_PII.Columns.Add("Телефон", typeof(string));
            dataTable_PII.Columns.Add("Автомобиль", typeof(string));
            dataTable_PII.Columns.Add("Статус", typeof(string));
            dataTable_PII.Columns.Add("Стоимость", typeof(decimal));
        }

        // Вызываем настройку DataGridView после загрузки формы
        private void toolStripMain_Load(object sender, EventArgs e)
        {
            CustomizeDataGridView();
        }

        private void CustomizeDataGridView()
        {
            try
            {
                // Настройка внешнего вида DataGridView
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;

                // Проверяем, что колонки существуют перед настройкой
                if (dataGridView1.Columns.Count > 0)
                {
                    // Настройка заголовков (используем проверку на null)
                    if (dataGridView1.Columns.Contains("ID"))
                    {
                        dataGridView1.Columns["ID"].HeaderText = "№";
                        dataGridView1.Columns["ID"].Width = 50;
                    }

                    if (dataGridView1.Columns.Contains("ФИО"))
                        dataGridView1.Columns["ФИО"].HeaderText = "ФИО клиента";

                    if (dataGridView1.Columns.Contains("Телефон"))
                        dataGridView1.Columns["Телефон"].HeaderText = "Номер телефона";

                    if (dataGridView1.Columns.Contains("Автомобиль"))
                        dataGridView1.Columns["Автомобиль"].HeaderText = "Марка автомобиля";

                    if (dataGridView1.Columns.Contains("Статус"))
                        dataGridView1.Columns["Статус"].HeaderText = "Статус ремонта";

                    if (dataGridView1.Columns.Contains("Стоимость"))
                    {
                        dataGridView1.Columns["Стоимость"].HeaderText = "Стоимость";
                        dataGridView1.Columns["Стоимость"].DefaultCellStyle.Format = "C"; // Формат валюты
                    }
                }
            }
            catch (Exception ex)
            {
                // Логируем ошибку, но не прерываем выполнение
                Console.WriteLine($"Ошибка в CustomizeDataGridView: {ex.Message}");
            }
        }

        private void button_open_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog_PII = new OpenFileDialog())
                {
                    openFileDialog_PII.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                    openFileDialog_PII.Title = "Выберите CSV файл";

                    if (openFileDialog_PII.ShowDialog() == DialogResult.OK)
                    {
                        currentFilePath_PII = openFileDialog_PII.FileName;
                        LoadDataFromCSV(currentFilePath_PII);
                        MessageBox.Show($"Файл загружен: {Path.GetFileName(currentFilePath_PII)}", "Успех",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки файла: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataFromCSV(string filePath)
        {
            try
            {
                dataTable_PII.Clear();
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

                if (lines.Length > 0)
                {
                    // Пропускаем заголовок если есть
                    int startIndex = 0;
                    if (lines[0].Contains("ID") || lines[0].Contains("ФИО"))
                    {
                        startIndex = 1;
                    }

                    for (int i = startIndex; i < lines.Length; i++)
                    {
                        string[] fields = lines[i].Split(';');
                        if (fields.Length >= 5)
                        {
                            DataRow newRow = dataTable_PII.NewRow();
                            newRow["ID"] = int.Parse(fields[0]);
                            newRow["ФИО"] = fields[1];
                            newRow["Телефон"] = fields[2];
                            newRow["Автомобиль"] = fields[3];
                            newRow["Статус"] = fields[4];

                            // Если в CSV есть стоимость (6-е поле), используем её
                            if (fields.Length >= 6 && decimal.TryParse(fields[5], out decimal cost))
                            {
                                newRow["Стоимость"] = cost;
                            }
                            else
                            {
                                newRow["Стоимость"] = 0;
                            }

                            dataTable_PII.Rows.Add(newRow);
                        }
                    }

                    // Обновляем настройки DataGridView после загрузки данных
                    CustomizeDataGridView();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка чтения CSV: {ex.Message}");
            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(currentFilePath_PII))
                {
                    using (SaveFileDialog saveFileDialog_PII = new SaveFileDialog())
                    {
                        saveFileDialog_PII.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                        saveFileDialog_PII.Title = "Сохранить CSV файл";

                        if (saveFileDialog_PII.ShowDialog() == DialogResult.OK)
                        {
                            currentFilePath_PII = saveFileDialog_PII.FileName;
                        }
                        else
                        {
                            return;
                        }
                    }
                }

                SaveDataToCSV(currentFilePath_PII);
                MessageBox.Show($"Данные сохранены в файл:\n{currentFilePath_PII}", "Успех",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveDataToCSV(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    // Обновляем заголовок, чтобы включить стоимость
                    writer.WriteLine("ID;ФИО;Телефон;Автомобиль;Статус;Стоимость");

                    foreach (DataRow row in dataTable_PII.Rows)
                    {
                        writer.WriteLine(
                            $"{row["ID"]};{row["ФИО"]};{row["Телефон"]};{row["Автомобиль"]};{row["Статус"]};{row["Стоимость"]}"
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Не удалось сохранить файл: {ex.Message}");
            }
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            DataRow newRow = dataTable_PII.NewRow();
            newRow["ID"] = dataTable_PII.Rows.Count + 1;
            newRow["ФИО"] = "";
            newRow["Телефон"] = "";
            newRow["Автомобиль"] = "";
            newRow["Статус"] = "";
            newRow["Стоимость"] = 0;
            dataTable_PII.Rows.Add(newRow);
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null &&
                dataGridView1.CurrentRow.Index >= 0)
            {
                if (MessageBox.Show("Удалить выбранную запись?", "Подтверждение удаления",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dataTable_PII.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                }
            }
        }

        private void button_save_as_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog_PII = new OpenFileDialog())
                {
                    openFileDialog_PII.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                    openFileDialog_PII.Title = "Выберите CSV файл";

                    if (openFileDialog_PII.ShowDialog() == DialogResult.OK)
                    {
                        currentFilePath_PII = openFileDialog_PII.FileName;
                        LoadDataFromCSV(currentFilePath_PII);
                        MessageBox.Show($"Файл загружен: {Path.GetFileName(currentFilePath_PII)}", "Успех",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки файла: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Открываем форму для добавления нового клиента
            using (ClientForm clientForm = new ClientForm())
            {
                if (clientForm.ShowDialog() == DialogResult.OK)
                {
                    // Получаем данные из формы
                    string fullName = clientForm.FullName;
                    string phone = clientForm.Phone;
                    string carModel = clientForm.CarModel;
                    string status = clientForm.Status;
                    decimal cost = clientForm.Cost;

                    // Добавляем нового клиента в таблицу
                    AddNewClient(fullName, phone, carModel, status, cost);

                    // Показываем сообщение об успехе
                    MessageBox.Show("Клиент успешно добавлен!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void AddNewClient(string fullName, string phone, string carModel, string status, decimal cost)
        {
            try
            {
                // Создаем новую строку
                DataRow newRow = dataTable_PII.NewRow();

                // Генерируем новый ID (максимальный ID + 1)
                int newId = 1;
                foreach (DataRow row in dataTable_PII.Rows)
                {
                    if (row["ID"] != DBNull.Value)
                    {
                        int currentId = Convert.ToInt32(row["ID"]);
                        if (currentId >= newId)
                        {
                            newId = currentId + 1;
                        }
                    }
                }

                // Заполняем строку данными
                newRow["ID"] = newId;
                newRow["ФИО"] = fullName;
                newRow["Телефон"] = phone;
                newRow["Автомобиль"] = carModel;
                newRow["Статус"] = status;
                newRow["Стоимость"] = cost;

                // Добавляем строку в таблицу
                dataTable_PII.Rows.Add(newRow);

                // Обновляем DataGridView
                dataGridView1.Refresh();

                // Прокручиваем к новой строке
                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
                }

                // Автосохранение, если файл открыт
                if (!string.IsNullOrEmpty(currentFilePath_PII))
                {
                    SaveDataToCSV(currentFilePath_PII);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении клиента: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Пустое - не нужно
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Создаем форму с механиками
            statusStripMain mechanicsForm = new statusStripMain();

            // Показываем форму с механиками
            mechanicsForm.Show();

            // Можно закрыть текущую форму, если нужно
            // this.Close();
        }

        // Добавьте этот метод в дизайнере для обработки загрузки формы
        // В файле toolStripMain.Designer.cs в методе InitializeComponent() добавьте:
        // this.Load += new System.EventHandler(this.toolStripMain_Load);
    }
}