
namespace CryptoGetterForNet45
{
    partial class CryptoGetterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CryptoGetterForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ServerListComboBox = new System.Windows.Forms.ComboBox();
            this.SerialTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.DesignerTextBox = new System.Windows.Forms.TextBox();
            this.GetDataButton = new System.Windows.Forms.Button();
            this.DesignerCopyButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.GTINTextBox = new System.Windows.Forms.TextBox();
            this.SGTINTextBox = new System.Windows.Forms.TextBox();
            this.DMXPictureBox = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.KeyTextBox = new System.Windows.Forms.TextBox();
            this.CodeTextBox = new System.Windows.Forms.TextBox();
            this.SaveImageButton = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.WTSTextBox = new System.Windows.Forms.TextBox();
            this.WtsCopyButton = new System.Windows.Forms.Button();
            this.SUZTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SUZCopyButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DMXPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Сервер БД";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(19, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(10);
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
            this.label3.Location = new System.Drawing.Point(19, 119);
            this.label3.Margin = new System.Windows.Forms.Padding(10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "GTIN";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(19, 153);
            this.label4.Margin = new System.Windows.Forms.Padding(10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 45);
            this.label4.TabIndex = 3;
            this.label4.Text = "Серийный номер";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ServerListComboBox
            // 
            this.ServerListComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ServerListComboBox.FormattingEnabled = true;
            this.ServerListComboBox.Location = new System.Drawing.Point(131, 19);
            this.ServerListComboBox.Margin = new System.Windows.Forms.Padding(10);
            this.ServerListComboBox.Name = "ServerListComboBox";
            this.ServerListComboBox.Size = new System.Drawing.Size(254, 28);
            this.ServerListComboBox.TabIndex = 1;
            // 
            // SerialTextBox
            // 
            this.SerialTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SerialTextBox.Location = new System.Drawing.Point(131, 162);
            this.SerialTextBox.Margin = new System.Windows.Forms.Padding(10);
            this.SerialTextBox.Name = "SerialTextBox";
            this.SerialTextBox.Size = new System.Drawing.Size(130, 26);
            this.SerialTextBox.TabIndex = 4;
            this.SerialTextBox.TextChanged += new System.EventHandler(this.SerialTextBox_TextChanged);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(19, 300);
            this.label6.Margin = new System.Windows.Forms.Padding(10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 67);
            this.label6.TabIndex = 9;
            this.label6.Text = "Layout Designer";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DesignerTextBox
            // 
            this.DesignerTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DesignerTextBox.Location = new System.Drawing.Point(131, 300);
            this.DesignerTextBox.Margin = new System.Windows.Forms.Padding(10);
            this.DesignerTextBox.Multiline = true;
            this.DesignerTextBox.Name = "DesignerTextBox";
            this.DesignerTextBox.ReadOnly = true;
            this.DesignerTextBox.Size = new System.Drawing.Size(423, 67);
            this.DesignerTextBox.TabIndex = 8;
            // 
            // GetDataButton
            // 
            this.GetDataButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GetDataButton.Location = new System.Drawing.Point(405, 16);
            this.GetDataButton.Margin = new System.Windows.Forms.Padding(10);
            this.GetDataButton.Name = "GetDataButton";
            this.GetDataButton.Size = new System.Drawing.Size(110, 172);
            this.GetDataButton.TabIndex = 5;
            this.GetDataButton.Text = "Получить криптокод";
            this.GetDataButton.UseVisualStyleBackColor = true;
            this.GetDataButton.Click += new System.EventHandler(this.GetDataButton_Click);
            // 
            // DesignerCopyButton
            // 
            this.DesignerCopyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DesignerCopyButton.Location = new System.Drawing.Point(574, 300);
            this.DesignerCopyButton.Margin = new System.Windows.Forms.Padding(10);
            this.DesignerCopyButton.Name = "DesignerCopyButton";
            this.DesignerCopyButton.Size = new System.Drawing.Size(113, 67);
            this.DesignerCopyButton.TabIndex = 9;
            this.DesignerCopyButton.Text = "Копировать";
            this.DesignerCopyButton.UseVisualStyleBackColor = true;
            this.DesignerCopyButton.Click += new System.EventHandler(this.DesignerCopyButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClearButton.Location = new System.Drawing.Point(594, 19);
            this.ClearButton.Margin = new System.Windows.Forms.Padding(10);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(93, 75);
            this.ClearButton.TabIndex = 13;
            this.ClearButton.Text = "Очистить форму";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // GTINTextBox
            // 
            this.GTINTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GTINTextBox.Location = new System.Drawing.Point(131, 116);
            this.GTINTextBox.Margin = new System.Windows.Forms.Padding(10);
            this.GTINTextBox.Name = "GTINTextBox";
            this.GTINTextBox.Size = new System.Drawing.Size(135, 26);
            this.GTINTextBox.TabIndex = 3;
            this.GTINTextBox.TextChanged += new System.EventHandler(this.GTINTextBox_TextChanged);
            // 
            // SGTINTextBox
            // 
            this.SGTINTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SGTINTextBox.Location = new System.Drawing.Point(131, 67);
            this.SGTINTextBox.Margin = new System.Windows.Forms.Padding(10);
            this.SGTINTextBox.Name = "SGTINTextBox";
            this.SGTINTextBox.Size = new System.Drawing.Size(254, 26);
            this.SGTINTextBox.TabIndex = 2;
            this.SGTINTextBox.Text = "04605310023518oAjiB1Ol4b2uD";
            this.SGTINTextBox.TextChanged += new System.EventHandler(this.SGTINTextBox_TextChanged);
            // 
            // DMXPictureBox
            // 
            this.DMXPictureBox.BackColor = System.Drawing.SystemColors.Info;
            this.DMXPictureBox.Location = new System.Drawing.Point(131, 559);
            this.DMXPictureBox.Margin = new System.Windows.Forms.Padding(10);
            this.DMXPictureBox.Name = "DMXPictureBox";
            this.DMXPictureBox.Size = new System.Drawing.Size(188, 188);
            this.DMXPictureBox.TabIndex = 11;
            this.DMXPictureBox.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(19, 211);
            this.label7.Margin = new System.Windows.Forms.Padding(10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "Ключ";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(19, 254);
            this.label8.Margin = new System.Windows.Forms.Padding(10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 26);
            this.label8.TabIndex = 13;
            this.label8.Text = "Криптокод";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // KeyTextBox
            // 
            this.KeyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.KeyTextBox.Location = new System.Drawing.Point(131, 208);
            this.KeyTextBox.Margin = new System.Windows.Forms.Padding(10);
            this.KeyTextBox.Name = "KeyTextBox";
            this.KeyTextBox.ReadOnly = true;
            this.KeyTextBox.Size = new System.Drawing.Size(100, 26);
            this.KeyTextBox.TabIndex = 10;
            // 
            // CodeTextBox
            // 
            this.CodeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CodeTextBox.Location = new System.Drawing.Point(131, 254);
            this.CodeTextBox.Margin = new System.Windows.Forms.Padding(10);
            this.CodeTextBox.Name = "CodeTextBox";
            this.CodeTextBox.ReadOnly = true;
            this.CodeTextBox.Size = new System.Drawing.Size(455, 26);
            this.CodeTextBox.TabIndex = 11;
            // 
            // SaveImageButton
            // 
            this.SaveImageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveImageButton.Location = new System.Drawing.Point(339, 608);
            this.SaveImageButton.Margin = new System.Windows.Forms.Padding(10);
            this.SaveImageButton.Name = "SaveImageButton";
            this.SaveImageButton.Size = new System.Drawing.Size(99, 73);
            this.SaveImageButton.TabIndex = 12;
            this.SaveImageButton.Text = "Сохранить";
            this.SaveImageButton.UseVisualStyleBackColor = true;
            this.SaveImageButton.Click += new System.EventHandler(this.SaveImeageButton_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(19, 410);
            this.label10.Margin = new System.Windows.Forms.Padding(10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 20);
            this.label10.TabIndex = 17;
            this.label10.Text = "WTS";
            // 
            // WTSTextBox
            // 
            this.WTSTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.WTSTextBox.Location = new System.Drawing.Point(131, 387);
            this.WTSTextBox.Margin = new System.Windows.Forms.Padding(10);
            this.WTSTextBox.Multiline = true;
            this.WTSTextBox.Name = "WTSTextBox";
            this.WTSTextBox.ReadOnly = true;
            this.WTSTextBox.Size = new System.Drawing.Size(423, 66);
            this.WTSTextBox.TabIndex = 16;
            // 
            // WtsCopyButton
            // 
            this.WtsCopyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.WtsCopyButton.Location = new System.Drawing.Point(574, 387);
            this.WtsCopyButton.Margin = new System.Windows.Forms.Padding(10);
            this.WtsCopyButton.Name = "WtsCopyButton";
            this.WtsCopyButton.Size = new System.Drawing.Size(113, 67);
            this.WtsCopyButton.TabIndex = 19;
            this.WtsCopyButton.Text = "Копировать";
            this.WtsCopyButton.UseVisualStyleBackColor = true;
            this.WtsCopyButton.Click += new System.EventHandler(this.WtsCopyButton_Click);
            // 
            // SUZTextBox
            // 
            this.SUZTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SUZTextBox.Location = new System.Drawing.Point(131, 473);
            this.SUZTextBox.Margin = new System.Windows.Forms.Padding(10);
            this.SUZTextBox.Multiline = true;
            this.SUZTextBox.Name = "SUZTextBox";
            this.SUZTextBox.ReadOnly = true;
            this.SUZTextBox.Size = new System.Drawing.Size(423, 66);
            this.SUZTextBox.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(19, 497);
            this.label5.Margin = new System.Windows.Forms.Padding(10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 20);
            this.label5.TabIndex = 21;
            this.label5.Text = "СУЗ";
            // 
            // SUZCopyButton
            // 
            this.SUZCopyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SUZCopyButton.Location = new System.Drawing.Point(574, 474);
            this.SUZCopyButton.Margin = new System.Windows.Forms.Padding(10);
            this.SUZCopyButton.Name = "SUZCopyButton";
            this.SUZCopyButton.Size = new System.Drawing.Size(113, 67);
            this.SUZCopyButton.TabIndex = 22;
            this.SUZCopyButton.Text = "Копировать";
            this.SUZCopyButton.UseVisualStyleBackColor = true;
            this.SUZCopyButton.Click += new System.EventHandler(this.SUZCopyButton_Click);
            // 
            // CryptoGetterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(705, 771);
            this.Controls.Add(this.SUZCopyButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.SUZTextBox);
            this.Controls.Add(this.WtsCopyButton);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.WTSTextBox);
            this.Controls.Add(this.SaveImageButton);
            this.Controls.Add(this.CodeTextBox);
            this.Controls.Add(this.KeyTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.DMXPictureBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.DesignerCopyButton);
            this.Controls.Add(this.GetDataButton);
            this.Controls.Add(this.DesignerTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.SerialTextBox);
            this.Controls.Add(this.GTINTextBox);
            this.Controls.Add(this.SGTINTextBox);
            this.Controls.Add(this.ServerListComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CryptoGetterForm";
            this.Text = "CryptoGetter by S.M.S.";
            ((System.ComponentModel.ISupportInitialize)(this.DMXPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ServerListComboBox;
        private System.Windows.Forms.TextBox SerialTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox DesignerTextBox;
        private System.Windows.Forms.Button GetDataButton;
        private System.Windows.Forms.Button DesignerCopyButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.TextBox GTINTextBox;
        private System.Windows.Forms.TextBox SGTINTextBox;
        private System.Windows.Forms.PictureBox DMXPictureBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox KeyTextBox;
        private System.Windows.Forms.TextBox CodeTextBox;
        private System.Windows.Forms.Button SaveImageButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox WTSTextBox;
        private System.Windows.Forms.Button WtsCopyButton;
        private System.Windows.Forms.TextBox SUZTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button SUZCopyButton;
    }
}

