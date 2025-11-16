namespace Main
{
    partial class statusStripMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            button_clo = new Button();
            dataGridView2 = new DataGridView();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button_clo);
            groupBox1.Controls.Add(dataGridView2);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(655, 486);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // button_clo
            // 
            button_clo.Location = new Point(575, 431);
            button_clo.Name = "button_clo";
            button_clo.Size = new Size(75, 47);
            button_clo.TabIndex = 1;
            button_clo.Text = "Закрыть";
            button_clo.UseVisualStyleBackColor = true;
            button_clo.Click += button_clo_Click;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(6, 22);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(644, 390);
            dataGridView2.TabIndex = 0;
            // 
            // statusStripMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(674, 511);
            Controls.Add(groupBox1);
            Name = "statusStripMain";
            Text = "statusStripMain";
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button button_clo;
        private DataGridView dataGridView2;
    }
}