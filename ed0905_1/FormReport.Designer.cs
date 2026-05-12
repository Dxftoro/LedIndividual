namespace ed0905_1
{
    partial class FormReport
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.clientsBox = new System.Windows.Forms.ComboBox();
            this.dateTimeBorder = new System.Windows.Forms.DateTimePicker();
            this.buttonFetch = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.reportGrid = new System.Windows.Forms.DataGridView();
            this.reportChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reportGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportChart)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Клиент";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(375, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Дата";
            // 
            // clientsBox
            // 
            this.clientsBox.FormattingEnabled = true;
            this.clientsBox.Location = new System.Drawing.Point(52, 6);
            this.clientsBox.Name = "clientsBox";
            this.clientsBox.Size = new System.Drawing.Size(302, 21);
            this.clientsBox.TabIndex = 2;
            // 
            // dateTimeBorder
            // 
            this.dateTimeBorder.Location = new System.Drawing.Point(414, 7);
            this.dateTimeBorder.Name = "dateTimeBorder";
            this.dateTimeBorder.Size = new System.Drawing.Size(165, 20);
            this.dateTimeBorder.TabIndex = 3;
            // 
            // buttonFetch
            // 
            this.buttonFetch.Location = new System.Drawing.Point(752, 4);
            this.buttonFetch.Name = "buttonFetch";
            this.buttonFetch.Size = new System.Drawing.Size(197, 23);
            this.buttonFetch.TabIndex = 4;
            this.buttonFetch.Text = "Сформировать отчёт";
            this.buttonFetch.UseVisualStyleBackColor = true;
            this.buttonFetch.Click += new System.EventHandler(this.buttonFetch_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonFetch);
            this.panel1.Controls.Add(this.clientsBox);
            this.panel1.Controls.Add(this.dateTimeBorder);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(952, 33);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.reportChart);
            this.panel2.Controls.Add(this.reportGrid);
            this.panel2.Location = new System.Drawing.Point(12, 51);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(952, 435);
            this.panel2.TabIndex = 6;
            // 
            // reportGrid
            // 
            this.reportGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.reportGrid.Location = new System.Drawing.Point(6, 3);
            this.reportGrid.Name = "reportGrid";
            this.reportGrid.Size = new System.Drawing.Size(402, 429);
            this.reportGrid.TabIndex = 0;
            // 
            // reportChart
            // 
            chartArea3.Name = "ChartArea1";
            this.reportChart.ChartAreas.Add(chartArea3);
            this.reportChart.Location = new System.Drawing.Point(414, 3);
            this.reportChart.Name = "reportChart";
            series3.ChartArea = "ChartArea1";
            series3.IsValueShownAsLabel = true;
            series3.Name = "Суммы недоставленных заказов";
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.reportChart.Series.Add(series3);
            this.reportChart.Size = new System.Drawing.Size(535, 429);
            this.reportChart.TabIndex = 1;
            this.reportChart.Text = "Суммы недоставленных заказов";
            title3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            title3.Name = "Title1";
            title3.Text = "Суммы недоставленных заказов";
            this.reportChart.Titles.Add(title3);
            // 
            // FormReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 498);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormReport";
            this.Text = "FormReport";
            this.Load += new System.EventHandler(this.FormReport_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.reportGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox clientsBox;
        private System.Windows.Forms.DateTimePicker dateTimeBorder;
        private System.Windows.Forms.Button buttonFetch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView reportGrid;
        private System.Windows.Forms.DataVisualization.Charting.Chart reportChart;
    }
}