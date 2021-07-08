
namespace CryptoGetterForNet45
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CityComboBox = new System.Windows.Forms.ComboBox();
            this.SerialTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BarCodeTextBox = new System.Windows.Forms.TextBox();
            this.DesignerTextBox = new System.Windows.Forms.TextBox();
            this.GetDataButton = new System.Windows.Forms.Button();
            this.BarCodeCopyButton = new System.Windows.Forms.Button();
            this.DesignerCopyButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.GTINTextBox = new System.Windows.Forms.TextBox();
            this.SGTINTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(16, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "City";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(17, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "SGTIN";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(16, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "GTIN";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(17, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Serial";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CityComboBox
            // 
            this.CityComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CityComboBox.FormattingEnabled = true;
            this.CityComboBox.Location = new System.Drawing.Point(105, 12);
            this.CityComboBox.Name = "CityComboBox";
            this.CityComboBox.Size = new System.Drawing.Size(176, 28);
            this.CityComboBox.TabIndex = 1;
            // 
            // SerialTextBox
            // 
            this.SerialTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SerialTextBox.Location = new System.Drawing.Point(105, 110);
            this.SerialTextBox.Name = "SerialTextBox";
            this.SerialTextBox.Size = new System.Drawing.Size(135, 26);
            this.SerialTextBox.TabIndex = 4;
            this.SerialTextBox.TextChanged += new System.EventHandler(this.SerialTextBox_TextChanged);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(13, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 67);
            this.label5.TabIndex = 8;
            this.label5.Text = "BarCode  online generator";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(13, 227);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 67);
            this.label6.TabIndex = 9;
            this.label6.Text = "Layout Designer";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BarCodeTextBox
            // 
            this.BarCodeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BarCodeTextBox.Location = new System.Drawing.Point(101, 149);
            this.BarCodeTextBox.Multiline = true;
            this.BarCodeTextBox.Name = "BarCodeTextBox";
            this.BarCodeTextBox.Size = new System.Drawing.Size(437, 67);
            this.BarCodeTextBox.TabIndex = 6;
            // 
            // DesignerTextBox
            // 
            this.DesignerTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DesignerTextBox.Location = new System.Drawing.Point(101, 227);
            this.DesignerTextBox.Multiline = true;
            this.DesignerTextBox.Name = "DesignerTextBox";
            this.DesignerTextBox.Size = new System.Drawing.Size(437, 67);
            this.DesignerTextBox.TabIndex = 8;
            // 
            // GetDataButton
            // 
            this.GetDataButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GetDataButton.Location = new System.Drawing.Point(382, 12);
            this.GetDataButton.Name = "GetDataButton";
            this.GetDataButton.Size = new System.Drawing.Size(78, 124);
            this.GetDataButton.TabIndex = 5;
            this.GetDataButton.Text = "Get Data";
            this.GetDataButton.UseVisualStyleBackColor = true;
            this.GetDataButton.Click += new System.EventHandler(this.GetDataButton_Click);
            // 
            // BarCodeCopyButton
            // 
            this.BarCodeCopyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BarCodeCopyButton.Location = new System.Drawing.Point(546, 149);
            this.BarCodeCopyButton.Name = "BarCodeCopyButton";
            this.BarCodeCopyButton.Size = new System.Drawing.Size(75, 67);
            this.BarCodeCopyButton.TabIndex = 7;
            this.BarCodeCopyButton.Text = "Copy";
            this.BarCodeCopyButton.UseVisualStyleBackColor = true;
            this.BarCodeCopyButton.Click += new System.EventHandler(this.BarCodeCopyButton_Click);
            // 
            // DesignerCopyButton
            // 
            this.DesignerCopyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DesignerCopyButton.Location = new System.Drawing.Point(546, 228);
            this.DesignerCopyButton.Name = "DesignerCopyButton";
            this.DesignerCopyButton.Size = new System.Drawing.Size(75, 67);
            this.DesignerCopyButton.TabIndex = 9;
            this.DesignerCopyButton.Text = "Copy";
            this.DesignerCopyButton.UseVisualStyleBackColor = true;
            this.DesignerCopyButton.Click += new System.EventHandler(this.DesignerCopyButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClearButton.Location = new System.Drawing.Point(546, 12);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 67);
            this.ClearButton.TabIndex = 10;
            this.ClearButton.Text = "Clear Form";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // GTINTextBox
            // 
            this.GTINTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GTINTextBox.Location = new System.Drawing.Point(105, 78);
            this.GTINTextBox.Name = "GTINTextBox";
            this.GTINTextBox.Size = new System.Drawing.Size(135, 26);
            this.GTINTextBox.TabIndex = 3;
            this.GTINTextBox.TextChanged += new System.EventHandler(this.GTINTextBox_TextChanged);
            // 
            // SGTINTextBox
            // 
            this.SGTINTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SGTINTextBox.Location = new System.Drawing.Point(105, 46);
            this.SGTINTextBox.Name = "SGTINTextBox";
            this.SGTINTextBox.Size = new System.Drawing.Size(254, 26);
            this.SGTINTextBox.TabIndex = 2;
            this.SGTINTextBox.TextChanged += new System.EventHandler(this.SGTINTextBox_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(633, 310);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.DesignerCopyButton);
            this.Controls.Add(this.BarCodeCopyButton);
            this.Controls.Add(this.GetDataButton);
            this.Controls.Add(this.DesignerTextBox);
            this.Controls.Add(this.BarCodeTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.SerialTextBox);
            this.Controls.Add(this.GTINTextBox);
            this.Controls.Add(this.SGTINTextBox);
            this.Controls.Add(this.CityComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "CryptoGetter by S.M.S.";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CityComboBox;
        private System.Windows.Forms.TextBox SerialTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox BarCodeTextBox;
        private System.Windows.Forms.TextBox DesignerTextBox;
        private System.Windows.Forms.Button GetDataButton;
        private System.Windows.Forms.Button BarCodeCopyButton;
        private System.Windows.Forms.Button DesignerCopyButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.TextBox GTINTextBox;
        private System.Windows.Forms.TextBox SGTINTextBox;
    }
}

