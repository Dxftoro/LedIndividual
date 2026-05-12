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

		private void reportButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.ShowDialog();
			string filename = dialog.FileName;

			Excel.Application excelObj = new Excel.Application();
			excelObj.Visible = true;

			Excel.Workbook workbook = excelObj.Workbooks.Open(filename, 0, false, 5, "", "", false,
                Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

			Worksheet worksheet = workbook.Sheets[1];

			PriceListExportView view = new PriceListExportView();
			view.Setup(connection);
			System.Data.DataTable table = view.GetDataTable();

            for (int i = 0; i < table.Columns.Count; i++)
                worksheet.Cells[1, i + 1] = table.Columns[i].ColumnName;

            for (int i = 0; i < table.Rows.Count; i++)
                for (int j = 0; j < table.Columns.Count; j++)
                    worksheet.Cells[i + 2, j + 1] = table.Rows[i][j].ToString();

            workbook.Save();
			workbook.Close();
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
	}
}
