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
            UpdateData();
        }

		private void UpdateData()
		{
            adapter.Setup(connection, dataGridView1);
            this.Text = $"Склад (представление: {adapter.GetTableName()})";
        }

        private Task Connect()
		{
            connection = new NpgsqlConnection("server=26.84.220.160;port=5432;userid=postgres;password=postpass;database=ed0905_1");
			
			try
			{
				connection.Open();
				Debug.WriteLine("Connected successfuly!");
			}
			catch
			{
				MessageBox.Show("Can't connect to database!");
                Close();
			}

			return Task.CompletedTask;
		}

        private void Form1_Load(object sender, EventArgs e)
        {
			Connect();
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

        }
    }
}
