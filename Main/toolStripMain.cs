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
        }

        private void CreateEmptyTable()
        {
            dataTable_PII = new DataTable();

            dataTable_PII.Columns.Add("ID", typeof(int));
            dataTable_PII.Columns.Add("ФИО", typeof(string));
            dataTable_PII.Columns.Add("Телефон", typeof(string));
            dataTable_PII.Columns.Add("Автомобиль", typeof(string));
            dataTable_PII.Columns.Add("Статус", typeof(string));
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
                            dataTable_PII.Rows.Add(
                                int.Parse(fields[0]),
                                fields[1],
                                fields[2],
                                fields[3],
                                fields[4]
                            );
                        }
                    }
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
                    writer.WriteLine("ID;ФИО;Телефон;Автомобиль;Статус");

                    foreach (DataRow row in dataTable_PII.Rows)
                    {
                        writer.WriteLine(
                            $"{row["ID"]};{row["ФИО"]};{row["Телефон"]};{row["Автомобиль"]};{row["Статус"]}"
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
            dataTable_PII.Rows.Add(newRow);
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null &&
                dataGridView1.CurrentRow.Index >= 0)
            {
                dataTable_PII.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
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
    }
}