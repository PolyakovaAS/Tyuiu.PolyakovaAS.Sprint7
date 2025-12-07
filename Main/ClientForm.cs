using System;
using System.Windows.Forms;

namespace Main
{
    public partial class ClientForm : Form
    {
        // Публичные свойства для получения данных из формы
        public string FullName { get; private set; }
        public string Phone { get; private set; }
        public string CarModel { get; private set; }
        public string Status { get; private set; }
        public decimal Cost { get; private set; }

        // Конструктор для добавления нового клиента
        public ClientForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        // Конструктор для редактирования существующего клиента
        public ClientForm(string fullName, string phone, string carModel,
                         string status, decimal cost) : this()
        {
            // Заполняем форму существующими данными
            textBox1.Text = fullName;
            textBox2.Text = phone;
            comboBox2.Text = carModel;
            comboBox1.Text = status;
            numericUpDown1.Value = cost;
        }

        private void InitializeForm()
        {
            // Заполняем выпадающие списки
            comboBox1.Items.AddRange(new string[]
            {
                "Заявка принята",
                "На диагностике",
                "В ремонте",
                "Готов к выдаче",
                "Выдан клиенту"
            });

            comboBox2.Items.AddRange(new string[]
            {
                "Toyota Camry",
                "Honda Civic",
                "BMW X5",
                "Toyota Corolla",
                "Honda Accord",
                "BMW X3",
                "Toyota RAV4",
                "Honda CR-V"
            });

            // Устанавливаем значения по умолчанию
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            // Устанавливаем текст для кнопок
            btnCancel.Text = "Отмена";
            button1.Text = "Очистить";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Проверяем, что все поля заполнены
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Введите ФИО клиента", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Введите номер телефона", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(comboBox2.Text))
            {
                MessageBox.Show("Выберите марку автомобиля", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Сохраняем данные в свойства
            FullName = textBox1.Text.Trim();
            Phone = textBox2.Text.Trim();
            CarModel = comboBox2.Text;
            Status = comboBox1.Text;
            Cost = numericUpDown1.Value;

            // Закрываем форму с результатом OK
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Закрываем форму с результатом Cancel
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Очищаем все поля
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            numericUpDown1.Value = 0;

            // Фокус на первое поле
            textBox1.Focus();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            // Можно добавить дополнительную инициализацию при загрузке формы
        }

        // Метод для быстрого заполнения тестовыми данными (опционально)
        public void FillTestData()
        {
            textBox1.Text = "Иванов Иван Иванович";
            textBox2.Text = "+79991112233";
            comboBox2.Text = "Toyota Camry";
            comboBox1.Text = "В ремонте";
            numericUpDown1.Value = 15000;
        }
    }
}