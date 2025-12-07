namespace Main
{
    partial class toolStripMain
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
            button1 = new Button();
            button_save_as = new Button();
            button_delete = new Button();
            button_add = new Button();
            button_save = new Button();
            dataGridView1 = new DataGridView();
            button2 = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(button_save_as);
            groupBox1.Controls.Add(button_delete);
            groupBox1.Controls.Add(button_add);
            groupBox1.Controls.Add(button_save);
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1168, 597);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(949, 22);
            button1.Name = "button1";
            button1.Size = new Size(75, 43);
            button1.TabIndex = 5;
            button1.Text = "+";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button_save_as
            // 
            button_save_as.Location = new Point(290, 545);
            button_save_as.Name = "button_save_as";
            button_save_as.Size = new Size(75, 40);
            button_save_as.TabIndex = 4;
            button_save_as.Text = "88";
            button_save_as.UseVisualStyleBackColor = true;
            button_save_as.Click += button_save_as_Click;
            // 
            // button_delete
            // 
            button_delete.Location = new Point(190, 545);
            button_delete.Name = "button_delete";
            button_delete.Size = new Size(75, 41);
            button_delete.TabIndex = 3;
            button_delete.Text = "Удалить";
            button_delete.UseVisualStyleBackColor = true;
            button_delete.Click += button_delete_Click;
            // 
            // button_add
            // 
            button_add.Location = new Point(98, 545);
            button_add.Name = "button_add";
            button_add.Size = new Size(75, 41);
            button_add.TabIndex = 2;
            button_add.Text = "Добавить";
            button_add.UseVisualStyleBackColor = true;
            button_add.Click += button_add_Click;
            // 
            // button_save
            // 
            button_save.Location = new Point(6, 545);
            button_save.Name = "button_save";
            button_save.Size = new Size(75, 41);
            button_save.TabIndex = 1;
            button_save.Text = "Сохранить";
            button_save.UseVisualStyleBackColor = true;
            button_save.Click += button_save_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(6, 22);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(937, 512);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // button2
            // 
            button2.Location = new Point(1087, 542);
            button2.Name = "button2";
            button2.Size = new Size(75, 43);
            button2.TabIndex = 6;
            button2.Text = "2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // toolStripMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1192, 621);
            Controls.Add(groupBox1);
            Name = "toolStripMain";
            Text = "toolStripMain";
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private DataGridView dataGridView1;
        private Button button_delete;
        private Button button_add;
        private Button button_save;
        private Button button_save_as;
        private Button button1;
        private Button button2;
    }
}