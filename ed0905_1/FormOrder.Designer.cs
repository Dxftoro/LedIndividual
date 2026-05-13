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
            this.clientsBox = new System.Windows.Forms.ComboBox();
            this.dateTimeDelivr = new System.Windows.Forms.DateTimePicker();
            this.buttonOk = new System.Windows.Forms.Button();
            this.deliveredCheckBox = new System.Windows.Forms.CheckBox();
            this.orderInfoGrid = new System.Windows.Forms.DataGridView();
            this.insertButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.orderInfoPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.orderInfoGrid)).BeginInit();
            this.orderInfoPanel.SuspendLayout();
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
            this.dateTimeOrder.Location = new System.Drawing.Point(12, 63);
            this.dateTimeOrder.Name = "dateTimeOrder";
            this.dateTimeOrder.Size = new System.Drawing.Size(198, 20);
            this.dateTimeOrder.TabIndex = 1;
            this.dateTimeOrder.ValueChanged += new System.EventHandler(this.dateTimeOrder_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Дата заказа";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(261, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Дата доставки";
            // 
            // clientsBox
            // 
            this.clientsBox.FormattingEnabled = true;
            this.clientsBox.Location = new System.Drawing.Point(61, 12);
            this.clientsBox.Name = "clientsBox";
            this.clientsBox.Size = new System.Drawing.Size(401, 21);
            this.clientsBox.TabIndex = 5;
            this.clientsBox.SelectedIndexChanged += new System.EventHandler(this.clientsBox_SelectedIndexChanged);
            this.clientsBox.SelectionChangeCommitted += new System.EventHandler(this.clientsBox_SelectionChangeCommitted);
            // 
            // dateTimeDelivr
            // 
            this.dateTimeDelivr.Enabled = false;
            this.dateTimeDelivr.Location = new System.Drawing.Point(264, 63);
            this.dateTimeDelivr.Name = "dateTimeDelivr";
            this.dateTimeDelivr.Size = new System.Drawing.Size(198, 20);
            this.dateTimeDelivr.TabIndex = 6;
            this.dateTimeDelivr.ValueChanged += new System.EventHandler(this.dateTimeDelivr_ValueChanged);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(387, 98);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 10;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // deliveredCheckBox
            // 
            this.deliveredCheckBox.AutoSize = true;
            this.deliveredCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.deliveredCheckBox.Location = new System.Drawing.Point(374, 47);
            this.deliveredCheckBox.Name = "deliveredCheckBox";
            this.deliveredCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.deliveredCheckBox.Size = new System.Drawing.Size(88, 17);
            this.deliveredCheckBox.TabIndex = 12;
            this.deliveredCheckBox.Text = "Доставлено";
            this.deliveredCheckBox.UseVisualStyleBackColor = false;
            this.deliveredCheckBox.CheckedChanged += new System.EventHandler(this.deliveredCheckBox_CheckedChanged);
            // 
            // orderInfoGrid
            // 
            this.orderInfoGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.orderInfoGrid.Location = new System.Drawing.Point(3, 32);
            this.orderInfoGrid.Name = "orderInfoGrid";
            this.orderInfoGrid.Size = new System.Drawing.Size(440, 366);
            this.orderInfoGrid.TabIndex = 13;
            this.orderInfoGrid.SelectionChanged += new System.EventHandler(this.orderInfoGrid_SelectionChanged);
            // 
            // insertButton
            // 
            this.insertButton.Location = new System.Drawing.Point(3, 3);
            this.insertButton.Name = "insertButton";
            this.insertButton.Size = new System.Drawing.Size(116, 23);
            this.insertButton.TabIndex = 16;
            this.insertButton.Text = "Добавить";
            this.insertButton.UseVisualStyleBackColor = true;
            this.insertButton.Click += new System.EventHandler(this.insertButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Enabled = false;
            this.updateButton.Location = new System.Drawing.Point(125, 3);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(116, 23);
            this.updateButton.TabIndex = 15;
            this.updateButton.Text = "Изменить";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Enabled = false;
            this.deleteButton.ForeColor = System.Drawing.Color.Red;
            this.deleteButton.Location = new System.Drawing.Point(247, 3);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(116, 23);
            this.deleteButton.TabIndex = 14;
            this.deleteButton.Text = "Удалить";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // orderInfoPanel
            // 
            this.orderInfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.orderInfoPanel.Controls.Add(this.insertButton);
            this.orderInfoPanel.Controls.Add(this.orderInfoGrid);
            this.orderInfoPanel.Controls.Add(this.updateButton);
            this.orderInfoPanel.Controls.Add(this.deleteButton);
            this.orderInfoPanel.Location = new System.Drawing.Point(12, 127);
            this.orderInfoPanel.Name = "orderInfoPanel";
            this.orderInfoPanel.Size = new System.Drawing.Size(450, 405);
            this.orderInfoPanel.TabIndex = 17;
            this.orderInfoPanel.Visible = false;
            // 
            // FormOrder
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 544);
            this.Controls.Add(this.orderInfoPanel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.dateTimeDelivr);
            this.Controls.Add(this.clientsBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimeOrder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.deliveredCheckBox);
            this.Name = "FormOrder";
            this.Text = "Заказ";
            this.Load += new System.EventHandler(this.FormOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.orderInfoGrid)).EndInit();
            this.orderInfoPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimeOrder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox clientsBox;
        private System.Windows.Forms.DateTimePicker dateTimeDelivr;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.CheckBox deliveredCheckBox;
        private System.Windows.Forms.DataGridView orderInfoGrid;
        private System.Windows.Forms.Button insertButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Panel orderInfoPanel;
    }
}