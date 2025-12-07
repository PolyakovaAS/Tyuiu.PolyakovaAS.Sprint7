namespace Main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_table_Click(object sender, EventArgs e)
        {
            toolStripMain formTable = new toolStripMain();
            formTable.Show();
        }

        private void button_au_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormReports formReports = new FormReports();
            formReports.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormChart chartForm = new FormChart();
            chartForm.Show();
        }
    }
}