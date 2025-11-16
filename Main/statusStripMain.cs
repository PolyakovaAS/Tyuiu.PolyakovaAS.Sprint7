using System;
using System.Data;
using System.Windows.Forms;

namespace Main
{
    public partial class statusStripMain : Form
    {
        private DataTable dataTable2;

        public statusStripMain()
        {
            InitializeComponent();
            CreateSecondTable();
            dataGridView2.DataSource = dataTable2;
        }

        private void CreateSecondTable()
        {
            dataTable2 = new DataTable();

            dataTable2.Columns.Add("Механик", typeof(string));
            dataTable2.Columns.Add("Специализация", typeof(string));
            dataTable2.Columns.Add("Загрузка", typeof(string));
            dataTable2.Columns.Add("Рейтинг", typeof(double));
            dataTable2.Columns.Add("Опыт", typeof(string));
            dataTable2.Columns.Add("Зарплата", typeof(int));

            // Заполняем тестовыми данными 20 механиков
            dataTable2.Rows.Add("Иванов А.С.", "Двигатель", "85%", 4.8, "5 лет", 65000);
            dataTable2.Rows.Add("Петров В.И.", "Кузовной ремонт", "60%", 4.9, "7 лет", 72000);
            dataTable2.Rows.Add("Сидоров М.П.", "Электрика", "75%", 4.7, "4 года", 58000);
            dataTable2.Rows.Add("Козлов Д.А.", "Трансмиссия", "45%", 4.6, "3 года", 55000);
            dataTable2.Rows.Add("Николаев П.В.", "Диагностика", "90%", 4.9, "8 лет", 75000);
            dataTable2.Rows.Add("Федоров С.М.", "Двигатель", "70%", 4.5, "6 лет", 68000);
            dataTable2.Rows.Add("Васильев К.Д.", "Кузовной ремонт", "55%", 4.7, "5 лет", 62000);
            dataTable2.Rows.Add("Алексеев И.Н.", "Электрика", "80%", 4.8, "9 лет", 78000);
            dataTable2.Rows.Add("Григорьев О.П.", "Трансмиссия", "65%", 4.6, "4 года", 59000);
            dataTable2.Rows.Add("Дмитриев Р.С.", "Диагностика", "50%", 4.9, "10 лет", 80000);
            dataTable2.Rows.Add("Егоров В.Л.", "Двигатель", "75%", 4.7, "5 лет", 66000);
            dataTable2.Rows.Add("Жуков А.К.", "Кузовной ремонт", "40%", 4.8, "6 лет", 70000);
            dataTable2.Rows.Add("Зайцев М.И.", "Электрика", "85%", 4.5, "3 года", 56000);
            dataTable2.Rows.Add("Ильин С.В.", "Трансмиссия", "70%", 4.9, "7 лет", 71000);
            dataTable2.Rows.Add("Кузнецов П.А.", "Диагностика", "95%", 4.8, "8 лет", 74000);
            dataTable2.Rows.Add("Лебедев Д.М.", "Двигатель", "60%", 4.6, "4 года", 60000);
            dataTable2.Rows.Add("Михайлов А.С.", "Кузовной ремонт", "55%", 4.7, "5 лет", 63000);
            dataTable2.Rows.Add("Новиков В.П.", "Электрика", "80%", 4.9, "6 лет", 69000);
            dataTable2.Rows.Add("Орлов С.Д.", "Трансмиссия", "45%", 4.5, "2 года", 52000);
            dataTable2.Rows.Add("Павлов М.К.", "Диагностика", "75%", 4.8, "7 лет", 73000);
        }

        private void button_clo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}