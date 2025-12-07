namespace Main
{
    partial class FormChart
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
            button_close = new Button();
            pictureBox1 = new PictureBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1275, 868);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // button_close
            // 
            button_close.Location = new Point(27, 811);
            button_close.Name = "button_close";
            button_close.Size = new Size(75, 58);
            button_close.TabIndex = 1;
            button_close.Text = "Закрыть";
            button_close.UseVisualStyleBackColor = true;
            button_close.Click += button_close_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(15, 22);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1254, 771);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // FormChart
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1299, 881);
            Controls.Add(button_close);
            Controls.Add(groupBox1);
            Name = "FormChart";
            Text = "FormChart";
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button button_close;
        private PictureBox pictureBox1;
    }
}