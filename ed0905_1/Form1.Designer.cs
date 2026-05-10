namespace ed0905_1
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
            this.clientGridButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.productGridButton = new System.Windows.Forms.Button();
            this.orderGridButton = new System.Windows.Forms.Button();
            this.orderDataGridButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.insertButton = new System.Windows.Forms.Button();
            this.reportButton = new System.Windows.Forms.Button();
            this.buttonPriceListGrid = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // clientGridButton
            // 
            this.clientGridButton.Location = new System.Drawing.Point(12, 12);
            this.clientGridButton.Name = "clientGridButton";
            this.clientGridButton.Size = new System.Drawing.Size(116, 31);
            this.clientGridButton.TabIndex = 0;
            this.clientGridButton.Text = "Клиенты";
            this.clientGridButton.UseVisualStyleBackColor = true;
            this.clientGridButton.Click += new System.EventHandler(this.clientGridButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(134, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(654, 426);
            this.dataGridView1.TabIndex = 1;
            // 
            // productGridButton
            // 
            this.productGridButton.Location = new System.Drawing.Point(12, 49);
            this.productGridButton.Name = "productGridButton";
            this.productGridButton.Size = new System.Drawing.Size(116, 31);
            this.productGridButton.TabIndex = 2;
            this.productGridButton.Text = "Товары";
            this.productGridButton.UseVisualStyleBackColor = true;
            this.productGridButton.Click += new System.EventHandler(this.productGridButton_Click);
            // 
            // orderGridButton
            // 
            this.orderGridButton.Location = new System.Drawing.Point(12, 86);
            this.orderGridButton.Name = "orderGridButton";
            this.orderGridButton.Size = new System.Drawing.Size(116, 31);
            this.orderGridButton.TabIndex = 3;
            this.orderGridButton.Text = "Заказы";
            this.orderGridButton.UseVisualStyleBackColor = true;
            this.orderGridButton.Click += new System.EventHandler(this.orderGridButton_Click);
            // 
            // orderDataGridButton
            // 
            this.orderDataGridButton.Location = new System.Drawing.Point(12, 123);
            this.orderDataGridButton.Name = "orderDataGridButton";
            this.orderDataGridButton.Size = new System.Drawing.Size(116, 31);
            this.orderDataGridButton.TabIndex = 4;
            this.orderDataGridButton.Text = "Данные заказов";
            this.orderDataGridButton.UseVisualStyleBackColor = true;
            this.orderDataGridButton.Click += new System.EventHandler(this.orderDataGridButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.ForeColor = System.Drawing.Color.Red;
            this.deleteButton.Location = new System.Drawing.Point(12, 415);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(116, 23);
            this.deleteButton.TabIndex = 5;
            this.deleteButton.Text = "Удалить";
            this.deleteButton.UseVisualStyleBackColor = true;
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(12, 386);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(116, 23);
            this.updateButton.TabIndex = 6;
            this.updateButton.Text = "Изменить";
            this.updateButton.UseVisualStyleBackColor = true;
            // 
            // insertButton
            // 
            this.insertButton.Location = new System.Drawing.Point(12, 357);
            this.insertButton.Name = "insertButton";
            this.insertButton.Size = new System.Drawing.Size(116, 23);
            this.insertButton.TabIndex = 7;
            this.insertButton.Text = "Добавить";
            this.insertButton.UseVisualStyleBackColor = true;
            // 
            // reportButton
            // 
            this.reportButton.Location = new System.Drawing.Point(12, 197);
            this.reportButton.Name = "reportButton";
            this.reportButton.Size = new System.Drawing.Size(116, 31);
            this.reportButton.TabIndex = 8;
            this.reportButton.Text = "Отчёт";
            this.reportButton.UseVisualStyleBackColor = true;
            this.reportButton.Click += new System.EventHandler(this.reportButton_Click);
            // 
            // buttonPriceListGrid
            // 
            this.buttonPriceListGrid.Location = new System.Drawing.Point(12, 160);
            this.buttonPriceListGrid.Name = "buttonPriceListGrid";
            this.buttonPriceListGrid.Size = new System.Drawing.Size(116, 31);
            this.buttonPriceListGrid.TabIndex = 9;
            this.buttonPriceListGrid.Text = "Прайс-лист";
            this.buttonPriceListGrid.UseVisualStyleBackColor = true;
            this.buttonPriceListGrid.Click += new System.EventHandler(this.buttonPriceListGrid_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonPriceListGrid);
            this.Controls.Add(this.reportButton);
            this.Controls.Add(this.insertButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.orderDataGridButton);
            this.Controls.Add(this.orderGridButton);
            this.Controls.Add(this.productGridButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.clientGridButton);
            this.Name = "Form1";
            this.Text = "Склад";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button clientGridButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button productGridButton;
        private System.Windows.Forms.Button orderGridButton;
        private System.Windows.Forms.Button orderDataGridButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button insertButton;
        private System.Windows.Forms.Button reportButton;
        private System.Windows.Forms.Button buttonPriceListGrid;
    }
}

