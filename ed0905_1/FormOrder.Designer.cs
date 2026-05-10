namespace ed0905_1
{
    partial class FormOrder
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
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimeOrder = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.clientsBox = new System.Windows.Forms.ComboBox();
            this.dateTimeDelivr = new System.Windows.Forms.DateTimePicker();
            this.costBox = new System.Windows.Forms.NumericUpDown();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.costBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Клиент";
            // 
            // dateTimeOrder
            // 
            this.dateTimeOrder.Location = new System.Drawing.Point(12, 88);
            this.dateTimeOrder.Name = "dateTimeOrder";
            this.dateTimeOrder.Size = new System.Drawing.Size(198, 20);
            this.dateTimeOrder.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Дата заказа";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(217, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Дата доставки";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Стоимость";
            // 
            // clientsBox
            // 
            this.clientsBox.FormattingEnabled = true;
            this.clientsBox.Location = new System.Drawing.Point(101, 12);
            this.clientsBox.Name = "clientsBox";
            this.clientsBox.Size = new System.Drawing.Size(317, 21);
            this.clientsBox.TabIndex = 5;
            // 
            // dateTimeDelivr
            // 
            this.dateTimeDelivr.Location = new System.Drawing.Point(220, 88);
            this.dateTimeDelivr.Name = "dateTimeDelivr";
            this.dateTimeDelivr.Size = new System.Drawing.Size(198, 20);
            this.dateTimeDelivr.TabIndex = 6;
            // 
            // costBox
            // 
            this.costBox.Location = new System.Drawing.Point(101, 39);
            this.costBox.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.costBox.Name = "costBox";
            this.costBox.Size = new System.Drawing.Size(317, 20);
            this.costBox.TabIndex = 7;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(262, 123);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(343, 123);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 10;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // FormOrder
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(430, 156);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.costBox);
            this.Controls.Add(this.dateTimeDelivr);
            this.Controls.Add(this.clientsBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimeOrder);
            this.Controls.Add(this.label1);
            this.Name = "FormOrder";
            this.Text = "Заказ";
            ((System.ComponentModel.ISupportInitialize)(this.costBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimeOrder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox clientsBox;
        private System.Windows.Forms.DateTimePicker dateTimeDelivr;
        private System.Windows.Forms.NumericUpDown costBox;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
    }
}