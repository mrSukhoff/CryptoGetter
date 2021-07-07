
namespace WinFormsCryptoGetter
{
    partial class CryptoGetterForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbGTIN = new System.Windows.Forms.TextBox();
            this.tbSGTIN = new System.Windows.Forms.TextBox();
            this.tbSerial = new System.Windows.Forms.TextBox();
            this.cbCity = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.GET_button = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbBarCode = new System.Windows.Forms.TextBox();
            this.tbLayoutDesigner = new System.Windows.Forms.TextBox();
            this.CopyToBarCodeButton = new System.Windows.Forms.Button();
            this.CopyToLayoutDesignerButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(11, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "SGTIN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(11, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "GTIN";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(11, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "Serial";
            // 
            // tbGTIN
            // 
            this.tbGTIN.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbGTIN.Location = new System.Drawing.Point(91, 82);
            this.tbGTIN.Name = "tbGTIN";
            this.tbGTIN.Size = new System.Drawing.Size(136, 29);
            this.tbGTIN.TabIndex = 3;
            this.tbGTIN.TextChanged += new System.EventHandler(this.tbGTIN_TextChanged);
            // 
            // tbSGTIN
            // 
            this.tbSGTIN.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbSGTIN.Location = new System.Drawing.Point(91, 47);
            this.tbSGTIN.Name = "tbSGTIN";
            this.tbSGTIN.Size = new System.Drawing.Size(254, 29);
            this.tbSGTIN.TabIndex = 2;
            this.tbSGTIN.TextChanged += new System.EventHandler(this.tbSGTIN_TextChanged);
            // 
            // tbSerial
            // 
            this.tbSerial.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbSerial.Location = new System.Drawing.Point(91, 117);
            this.tbSerial.Name = "tbSerial";
            this.tbSerial.Size = new System.Drawing.Size(136, 29);
            this.tbSerial.TabIndex = 4;
            this.tbSerial.TextChanged += new System.EventHandler(this.tbSerial_TextChanged);
            // 
            // cbCity
            // 
            this.cbCity.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbCity.FormattingEnabled = true;
            this.cbCity.Location = new System.Drawing.Point(91, 12);
            this.cbCity.Name = "cbCity";
            this.cbCity.Size = new System.Drawing.Size(136, 29);
            this.cbCity.TabIndex = 1;
            this.cbCity.SelectedIndexChanged += new System.EventHandler(this.cbCity_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(12, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 21);
            this.label4.TabIndex = 7;
            this.label4.Text = "City";
            // 
            // GET_button
            // 
            this.GET_button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GET_button.Location = new System.Drawing.Point(361, 12);
            this.GET_button.Name = "GET_button";
            this.GET_button.Size = new System.Drawing.Size(74, 134);
            this.GET_button.TabIndex = 5;
            this.GET_button.Text = "GET Crypto";
            this.GET_button.UseVisualStyleBackColor = true;
            this.GET_button.Click += new System.EventHandler(this.GET_button_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(12, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 69);
            this.label5.TabIndex = 9;
            this.label5.Text = "BarCode  online generator";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(11, 251);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 71);
            this.label6.TabIndex = 10;
            this.label6.Text = "Layout Designer";
            // 
            // tbBarCode
            // 
            this.tbBarCode.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbBarCode.Location = new System.Drawing.Point(91, 164);
            this.tbBarCode.Multiline = true;
            this.tbBarCode.Name = "tbBarCode";
            this.tbBarCode.Size = new System.Drawing.Size(490, 69);
            this.tbBarCode.TabIndex = 6;
            // 
            // tbLayoutDesigner
            // 
            this.tbLayoutDesigner.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbLayoutDesigner.Location = new System.Drawing.Point(91, 251);
            this.tbLayoutDesigner.Multiline = true;
            this.tbLayoutDesigner.Name = "tbLayoutDesigner";
            this.tbLayoutDesigner.Size = new System.Drawing.Size(490, 69);
            this.tbLayoutDesigner.TabIndex = 8;
            // 
            // CopyToBarCodeButton
            // 
            this.CopyToBarCodeButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CopyToBarCodeButton.Location = new System.Drawing.Point(587, 164);
            this.CopyToBarCodeButton.Name = "CopyToBarCodeButton";
            this.CopyToBarCodeButton.Size = new System.Drawing.Size(75, 69);
            this.CopyToBarCodeButton.TabIndex = 7;
            this.CopyToBarCodeButton.Text = "Copy it!";
            this.CopyToBarCodeButton.UseVisualStyleBackColor = true;
            this.CopyToBarCodeButton.Click += new System.EventHandler(this.CopyToBarCodeButton_Click);
            // 
            // CopyToLayoutDesignerButton
            // 
            this.CopyToLayoutDesignerButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CopyToLayoutDesignerButton.Location = new System.Drawing.Point(587, 251);
            this.CopyToLayoutDesignerButton.Name = "CopyToLayoutDesignerButton";
            this.CopyToLayoutDesignerButton.Size = new System.Drawing.Size(75, 69);
            this.CopyToLayoutDesignerButton.TabIndex = 9;
            this.CopyToLayoutDesignerButton.Text = "Copy it!";
            this.CopyToLayoutDesignerButton.UseVisualStyleBackColor = true;
            this.CopyToLayoutDesignerButton.Click += new System.EventHandler(this.CopyToLayoutDesignerButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ClearButton.Location = new System.Drawing.Point(587, 12);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 69);
            this.ClearButton.TabIndex = 10;
            this.ClearButton.Text = "Clear Form";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // CryptoGetterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(690, 337);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.CopyToLayoutDesignerButton);
            this.Controls.Add(this.CopyToBarCodeButton);
            this.Controls.Add(this.tbLayoutDesigner);
            this.Controls.Add(this.tbBarCode);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.GET_button);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbCity);
            this.Controls.Add(this.tbSerial);
            this.Controls.Add(this.tbSGTIN);
            this.Controls.Add(this.tbGTIN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(678, 349);
            this.Name = "CryptoGetterForm";
            this.Text = "Crypto Getter by SMS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbGTIN;
        private System.Windows.Forms.TextBox tbSGTIN;
        private System.Windows.Forms.TextBox tbSerial;
        private System.Windows.Forms.ComboBox cbCity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button GET_button;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbBarCode;
        private System.Windows.Forms.TextBox tbLayoutDesigner;
        private System.Windows.Forms.Button CopyToBarCodeButton;
        private System.Windows.Forms.Button CopyToLayoutDesignerButton;
        private System.Windows.Forms.Button ClearButton;
    }
}

