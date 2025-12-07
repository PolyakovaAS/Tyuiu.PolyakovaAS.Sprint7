namespace Main
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            button2 = new Button();
            button1 = new Button();
            button_table = new Button();
            button_au = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(button_table);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(415, 240);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // button2
            // 
            button2.Location = new Point(165, 103);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 3;
            button2.Text = "График";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(165, 74);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "Статистика";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button_table
            // 
            button_table.Location = new Point(165, 45);
            button_table.Name = "button_table";
            button_table.Size = new Size(75, 23);
            button_table.TabIndex = 0;
            button_table.Text = "Таблица";
            button_table.UseVisualStyleBackColor = true;
            button_table.Click += button_table_Click;
            // 
            // button_au
            // 
            button_au.Location = new Point(433, 243);
            button_au.Name = "button_au";
            button_au.Size = new Size(10, 23);
            button_au.TabIndex = 1;
            button_au.Text = "Таблица 2";
            button_au.UseVisualStyleBackColor = true;
            button_au.Click += button_au_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(439, 264);
            Controls.Add(groupBox1);
            Controls.Add(button_au);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button button_table;
        private Button button_au;
        private Button button1;
        private Button button2;
    }
}
