using Microsoft.Office.Interop.Excel;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ed0905_1
{
	public partial class Form1 : Form
	{
		private NpgsqlConnection connection;
		private TableDataAdapter adapter;

		public Form1()
		{
			InitializeComponent();
			this.adapter = new ProductDataAdapter();
			Connect();
			UpdateData();
		}

		private void UpdateData()
		{
			adapter.Setup(connection, dataGridView1);
			this.Text = $"Склад (представление: {adapter.GetTableName()})";
		}

		private Task Connect()
		{
			connection = new NpgsqlConnection("server=26.60.242.39;port=5432;userid=ftorozol;password=1488;database=ed0905_1");
			
			try
			{
				connection.Open();
				Debug.WriteLine("Connected successfuly!");
			}
			catch (Exception exc)
			{
				MessageBox.Show("Can't connect to database! " + exc.Message);
				Close();
			}

			return Task.CompletedTask;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
		}

		private void clientGridButton_Click(object sender, EventArgs e)
		{
			adapter = new ClientDataAdapter();
			UpdateData();
		}
		
		private void productGridButton_Click(object sender, EventArgs e)
		{
			adapter = new ProductDataAdapter();
			UpdateData();
		}

		private void orderGridButton_Click(object sender, EventArgs e)
		{
			adapter = new OrderDataAdapter();
			UpdateData();
		}

		private void orderDataGridButton_Click(object sender, EventArgs e)
		{
			adapter = new OrderInfoDataAdapter();
			UpdateData();
		}

		private void buttonPriceListGrid_Click(object sender, EventArgs e)
		{
			adapter = new PriceListDataAdapter();
			UpdateData();
		}

		private void insertButton_Click(object sender, EventArgs e)
		{
			Form additionForm = adapter.CreateInstanceForm(connection, null);
			DialogResult result = additionForm.ShowDialog();
			if (result == DialogResult.OK) UpdateData();
		}

		private void updateButton_Click(object sender, EventArgs e)
		{
			Form additionForm = adapter.CreateInstanceForm(connection,
				dataGridView1.SelectedRows.Count > 0 ? dataGridView1.SelectedRows[0] : null);
			DialogResult result = additionForm.ShowDialog();
			if (result == DialogResult.OK) UpdateData();
		}

		private void deleteButton_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (DataGridViewRow row in dataGridView1.SelectedRows)
				{
					DataRowView rowView = row.DataBoundItem as DataRowView;
					adapter.DeleteByDataRow(connection, rowView.Row);
				}
				UpdateData();
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		private void dataGridView1_SelectionChanged(object sender, EventArgs e)
		{
			updateButton.Enabled = dataGridView1.SelectedRows.Count == 1;
			deleteButton.Enabled = dataGridView1.SelectedRows.Count > 0;
		}

        private void reportButton_Click(object sender, EventArgs e)
        {

        }

        private void buttonPriceListExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() != DialogResult.OK) return;
            string filename = dialog.FileName;

            Excel.Application excelObj = new Excel.Application();
            excelObj.Visible = false;

            Excel.Workbook workbook = null;

            try
            {
                workbook = excelObj.Workbooks.Open(filename, 0, false, 5, "", "", false,
                    Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

                excelObj.Visible = true;

                Excel.Worksheet worksheet = workbook.Sheets[1];
                Range range = worksheet.Range["a1:c1"];
                range.Merge();
                range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                range.Interior.Color = Color.LightGray;

                PriceListExportView view = new PriceListExportView();
                view.Setup(connection);

                range.Value = view.GetTableName();
                System.Data.DataTable table = view.GetDataTable();

                for (int i = 0; i < table.Columns.Count; i++)
                    worksheet.Cells[2, i + 1] = table.Columns[i].ColumnName;

                for (int i = 0; i < table.Rows.Count; i++)
                    for (int j = 0; j < table.Columns.Count; j++)
                        worksheet.Cells[i + 3, j + 1] = table.Rows[i][j].ToString();

                worksheet.Columns.AutoFit();
                workbook.Save();
                workbook.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                if (workbook != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                if (excelObj != null)
                {
                    excelObj.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelObj);
                }
            }
        }
    }
}
